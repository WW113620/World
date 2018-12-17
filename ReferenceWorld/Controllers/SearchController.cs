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
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {;
            
            bool isLogin = ReferenceWorld.Models.CommonMethod.IsLogin();
            if (!isLogin)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.IsLogin = isLogin;
            return View();
        }

        public ActionResult Result(SearchModel model)
        {
            string id = LoginHelper.UserGuid;
            ViewBag.UserGuid = id;
            var userService = new UserService();
            var user = userService.GetLoginUserAvatar(id);
            var memberService = new MemberService();
            if (model == null)
                model = new SearchModel();
            var peoples = memberService.SearchResultPeople(model);
            foreach (var item in peoples)
            {
                item.HeadImage = string.IsNullOrEmpty(item.HeadImage) ? "" : string.Format("{0}{1}", Url.Content("~/Upload/Avatar/"), item.HeadImage);
            }
            ViewBag.Peoples = peoples;
            return View(user);
        }

        [HttpPost]
        public JsonResult FollowFriends(string userGuid, string myGuid)
        {
            ResultModel result = new ResultModel { errorCode = 500, errorMes = "" };
            try
            {
                Teams team = new Teams();
                team.FriendGuid = userGuid;
                team.MyGuid = myGuid;
                team.CreateTime = DateTime.Now;

                var memberService = new MemberService();
                bool isFollow = memberService.IsFollowFriend(team);
                if (isFollow)
                {
                    result.errorCode = 300;
                    result.errorMes = "exist";
                }
                else
                {
                    int intSend = memberService.IsSendFollowFriend(team);
                    if(intSend<=0)
                    {
                        memberService.FollowFriend(team);
                    }
                    SendRequestFollowFriend(team);
                    result.errorCode = 200;
                    result.errorMes = "ok";
                }

            }
            catch (Exception e)
            {
                result.errorCode = 400;
                result.errorMes = e.Message;
            }
            return Json(result);
        }

        public static void SendRequestFollowFriend(Teams team)
        {
            Message mes = new Message();
            mes.FromUserGuid = team.MyGuid;
            mes.ToUserGuid = team.FriendGuid;
            mes.FriendGuid = "";
            mes.Description = "";
            mes.IsRead = 0;
            mes.SendType = 2;
            mes.CreateTime = DateTime.Now;
            var memberService = new MemberService();
            int i = memberService.SendMessagesToUser(mes);
        }

        [HttpPost]
        public JsonResult SetAgreeMessage(string Id, string MyGuid,string FriendGuid,string loginName, int type)
        {
            ResultModel result = new ResultModel { errorCode = 500, errorMes = "" };
            try
            {
                var memberService = new MemberService();
                Message mes = new Message();
                mes.FromUserGuid = FriendGuid;
                mes.ToUserGuid = MyGuid;
                mes.FriendGuid = "";
                mes.IsRead = 0;
                mes.SendType = 0;
                mes.CreateTime = DateTime.Now;
                if (type==1)//Refuse
                {
                    mes.Description = string.Format("{0} refused you follow him as a friend ! ", loginName);
                }
                else
                {
                    mes.Description =string.Format("{0} is already your friend now ! ", loginName);
                    memberService.AgreeFollowFriend(FriendGuid, MyGuid);
                }               
                memberService.SendMessagesToUser(mes);
                memberService.DeleteMessage(Id);

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

        public ActionResult Test()
        {
            return View();
        }
    }
}