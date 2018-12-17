using ReferenceWorld.Common;
using ReferenceWorld.Model;
using ReferenceWorld.Models;
using ReferenceWorld.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReferenceWorld.Controllers
{
    public class MemberController : Controller
    {
        public ActionResult PartHeader()
        {           
            string userGuid = string.Empty;
            userGuid = LoginHelper.UserGuid;
            var userService = new UserService();
            var user = userService.GetLoginUserAvatar(userGuid);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            PartUserInfo model = new PartUserInfo();
            model.UserGuid = user.UserGuid;
            model.UserName = user.UserName;
            model.AvatarUrl = Url.Content("~/Upload/Avatar/") + user.HeadImage;
            return PartialView(model);
        }
        public ActionResult Index(string id = "")
        {
            string userGuid = string.Empty;
            if (string.IsNullOrWhiteSpace(id))
                userGuid = LoginHelper.UserGuid;
            else
                userGuid = id;
            var userService = new UserService();
            var user = userService.GetLoginUserAvatar(userGuid);
            if (user == null && string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Login");
            }
            if(user.iCount>0)
            {
                user.Star = (int)Math.Round(user.iSum / Convert.ToDouble(user.iCount), 0);
            }
            else
            {
                user.Star = 0;
            }
            Introduction duction = new Introduction();
            duction = userService.GetLoginIntroduction(userGuid);
            ViewBag.Introduction = duction;
            return View(user);
        }
        [HttpPost]
        public JsonResult SubmitComment(string userGuid, string content, string descriptionStar)
        {
            ResultModel result = new ResultModel() { errorCode = 500, errorMes = "" };
            try
            {
                Comment comment = new Comment();
                comment.UserGuid = userGuid;
                comment.Content = content;
                comment.Star = descriptionStar;
                comment.CommentName = LoginHelper.UserName;
                comment.CreateTime = DateTime.Now;
                var userService = new UserService();
                int i = userService.CreateCommonToUser(comment);
                if (i > 0)
                {
                    result.errorCode = 200;
                    result.errorMes = "ok";
                }
                else
                {
                    result.errorCode = 300;
                    result.errorMes = "no";
                }
            }
            catch (Exception e)
            {
                result.errorCode = 400;
                result.errorMes = e.Message;
            }
            return Json(result);
        }
        public ActionResult Testimony(string id)
        {
            ViewBag.UserGuid = id;
            var userService = new UserService();
            var user = userService.GetLoginUserAvatar(id);
            var com = userService.GetCommentsByUser(id);
            foreach (var item in com)
            {
                if (string.IsNullOrWhiteSpace(item.Star))
                {
                    item.Star = "";
                }
                else
                {
                    item.Star = string.Format("{0}/10", (item.Star.ToInt(0)) * 2);
                }

            }
            ViewBag.Commons = com;
            return View(user);
        }
        public ActionResult Photos(string id)
        {
            ViewBag.UserGuid = id;
            var userService = new UserService();
            var user = userService.GetLoginUserAvatar(id);
            var pics = userService.GetPhotosByUser(id);
            foreach (var item in pics)
            {
                item.Image = string.Format("{0}{1}", Url.Content("~/Upload/Picture/"), item.Image);
            }
            bool bo = false;
            if (user.UserName.Equals(LoginHelper.UserName))
            { bo = true; }
            ViewBag.IsMini = bo;
            ViewBag.Photos = pics;
            return View(user);
        }
        public ActionResult PartUploadPic()
        {
            return PartialView();
        }
        public ActionResult PartPhotoList(string code)
        {
            var userService = new UserService();
            var pics = userService.GetPhotosByUser(code);
            foreach (var item in pics)
            {
                item.Image = string.Format("{0}{1}", Url.Content("~/Upload/Picture/"), item.Image);
            }
            return PartialView(pics);
        }
        public ActionResult Team(string id)
        {
            ViewBag.UserGuid = id;
            var userService = new UserService();
            var user = userService.GetLoginUserAvatar(id);
            var memberService = new MemberService();
            var teams = memberService.GetTeamFriends(id);
            foreach (var item in teams)
            {
                item.HeadImage = string.IsNullOrEmpty(item.HeadImage) ? "" : string.Format("{0}{1}", Url.Content("~/Upload/Avatar/"), item.HeadImage);
            }
            ViewBag.Teams = teams;
            return View(user);
        }
        public ActionResult Message(string id)
        {
            bool isLogin = CommonMethod.IsLogin();
            if (!isLogin)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.UserGuid = id;
            var userService = new UserService();
            var user = userService.GetLoginUserAvatar(id);
            var memberService = new MemberService();
            var peoples = memberService.SearchResultPeople(new SearchModel());
            var messages = memberService.GetNewMessagesList(id);
            foreach (var item in messages)
            {
                item.HeadImage = string.IsNullOrEmpty(item.HeadImage) ? "" : string.Format("{0}{1}", Url.Content("~/Upload/Avatar/"), item.HeadImage);
                if (item.SendType == 1)
                {
                    var friend = memberService.GetFriendHeadImage(item.FriendGuid);
                    if (friend != null)
                    {
                        item.FriendUserName = friend.UserName;
                        item.FriendCompany = friend.Company;
                        item.FriendHeadImage = string.IsNullOrEmpty(friend.HeadImage) ? "" : string.Format("{0}{1}", Url.Content("~/Upload/Avatar/"), friend.HeadImage);
                    }
                }

            }
            ViewBag.Messages = messages;
            return View(user);
        }
        [HttpPost]
        public JsonResult GetFriendsList(string friendGuid, string myGuid)
        {
            ResultData<List<User>> result = new ResultData<List<User>>() { errorCode = 500, errorMes = "", data = new List<User>() };
            try
            {
                var memberService = new MemberService();
                var friends = memberService.GetTeamFriends(friendGuid, myGuid);
                result.data = friends.ToList();
                result.errorCode = 200;
                result.errorMes = "ok";
            }
            catch (Exception e)
            {
                result.errorCode = 400;
                result.errorMes = e.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult SendMessages(int type, string fromGuid, string toGuid, string selectGuid, string description)
        {
            ResultModel result = new ResultModel() { errorCode = 500, errorMes = "" };
            try
            {
                Message mes = new Message();
                mes.FromUserGuid = fromGuid;
                mes.ToUserGuid = toGuid;
                mes.FriendGuid = selectGuid;
                mes.Description = description;
                mes.IsRead = 0;
                mes.SendType = type;
                mes.CreateTime = DateTime.Now;
                var memberService = new MemberService();
                int i = memberService.SendMessagesToUser(mes);
                result.errorCode = 200;
                result.errorMes = "ok";
            }
            catch (Exception e)
            {
                result.errorCode = 400;
                result.errorMes = e.Message;
            }
            return Json(result);
        }

        #region Send Email
        [HttpPost]
        public JsonResult SendEmailMessage(string toGuid, string description)
        {
            ResultModel result = new ResultModel() { errorCode = 500, errorMes = "" };
            try
            {
                SendEmail(toGuid, description);
                result.errorCode = 200;
                result.errorMes = "ok";
            }
            catch (Exception e)
            {
                result.errorCode = 400;
                result.errorMes = e.Message;
            }
            return Json(result);
        }
        public static string SendEmail(string userGuid, string message)
        {
            string sendMailResult = "no";
            var userService = new UserService();
            var user = userService.GetUserInfo(userGuid);
            if (user == null)
                return sendMailResult;
            try
            {
                List<string> listCC = new List<string>() { };
                string sendEmailServer = ConfigHelper.GetConfigValue("SendEmailServer");
                string fromEmail = ConfigHelper.GetConfigValue("SendFromEmail");
                string fromPwd = ConfigHelper.GetConfigValue("SendFromPwd");
                sendMailResult = CommonMethod.SendMail(fromEmail, LoginHelper.UserName, user.Email, listCC, "new message", message, fromEmail, fromPwd, sendEmailServer);

            }
            catch (Exception e)
            {
                sendMailResult = e.Message;
            }
            return sendMailResult;
        }
        #endregion
        [HttpPost]
        public JsonResult GetMessages(string userGuid)
        {
            ResultData<int> result = new ResultData<int>() { errorCode = 500, errorMes = "", data = 0 };
            try
            {
                userGuid = HttpUtility.UrlDecode(userGuid);
                var memberService = new MemberService();
                int iCount = memberService.GetNewMessage(userGuid);
                result.data = iCount;
                result.errorCode = 200;
                result.errorMes = "ok";
            }
            catch (Exception e)
            {
                result.errorCode = 400;
                result.errorMes = e.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult DeleteMessage(string id)
        {
            ResultModel result = new ResultModel() { errorCode = 500, errorMes = "" };
            try
            {
                var memberService = new MemberService();
                int iCount = memberService.DeleteMessage(id);
                result.errorCode = 200;
                result.errorMes = "ok";
            }
            catch (Exception e)
            {
                result.errorCode = 400;
                result.errorMes = e.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult AddMyDescription(string userGuid, string description)
        {
            ResultModel result = new ResultModel() { errorCode = 500, errorMes = "" };
            try
            {
                Introduction duction = new Introduction();
                duction.UserGuid = userGuid;
                duction.Description = description;
                duction.CreateTime = DateTime.Now;
                var userService = new UserService();
                int i = userService.CreateIntroductionToUser(duction);
                if (i > 0)
                {
                    result.errorCode = 200;
                    result.errorMes = "ok";
                }
                else
                {
                    result.errorCode = 300;
                    result.errorMes = "no";
                }
            }
            catch (Exception e)
            {
                result.errorCode = 400;
                result.errorMes = e.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult SubmitDeletePic(string id)
        {
            ResultModel result = new ResultModel() { errorCode = 500, errorMes = "" };
            try
            {
                var memberService = new MemberService();
                int iCount = memberService.DeletePicture(id);
                result.errorCode = 200;
                result.errorMes = "ok";
            }
            catch (Exception e)
            {
                result.errorCode = 400;
                result.errorMes = e.Message;
            }
            return Json(result);
        }

    }
}