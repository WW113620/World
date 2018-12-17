using ReferenceWorld.Common;
using ReferenceWorld.Model;
using ReferenceWorld.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReferenceWorld.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        #region login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LoginValidator(string username, string userpwd, string isChecked)
        {
            ResultModel result = new ResultModel() { errorCode = 500, errorMes = "" };
            try
            {
                var userService = new UserService();
                string encPwd = ConvertHelper.MD5Encrypt(userpwd);
                var user = userService.GetLoginUserInfo(username, encPwd);
                if (user != null && user.Id > 0)
                {
                    int check = isChecked.ToInt(0);
                    var _expireTimeUtil = CookieHelper.TimeUtil.D;
                    string _expireTimeSpan = string.Empty;
                    if (check == 1)
                    {
                        _expireTimeUtil = CookieHelper.TimeUtil.None;
                        _expireTimeSpan = string.Empty;
                    }
                    else
                    {
                        _expireTimeUtil = CookieHelper.TimeUtil.M;
                        _expireTimeSpan = "1";
                    }
                    CookieHelper.SetCookie(LoginHelper.LoginCookieGuid, user.UserGuid, _expireTimeUtil, _expireTimeSpan);
                    CookieHelper.SetCookie(LoginHelper.LoginCookieName, user.UserName, _expireTimeUtil, _expireTimeSpan);
                    CookieHelper.SetCookie(LoginHelper.LoginCookieEmail, user.Email, _expireTimeUtil, _expireTimeSpan);
                    CookieHelper.SetCookie(LoginHelper.UserNameType, user.UserType.ToString(""), _expireTimeUtil, _expireTimeSpan);
                    result.errorCode = 200;
                    result.errorMes = "ok";
                }
                else
                {
                    result.errorCode = 202;
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
        #endregion

        #region register
        public ActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">1=Username;2=Email</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult IsExistValue(string value, int type)
        {
            ResultModel result = new ResultModel() { errorCode = 500, errorMes = "" };
            try
            {
                var userService = new UserService();
                var user = userService.GeExistUserInfo(value, type);
                if (user != null && user.Id > 0 && !string.IsNullOrWhiteSpace(user.UserName))
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
        public JsonResult RegisterValidator(UserEntity user)
        {
            ResultModel result = new ResultModel() { errorCode = 500, errorMes = "" };
            try
            {
                user.UserGuid = CommonHelper.CreateGuid("user");
                user.Password = ConvertHelper.MD5Encrypt(user.Password);
                user.HeadImage = "";
                user.UserType = 0;
                user.UserState = 0;
                user.AddTime = DateTime.Now;
                var userService = new UserService();
                int i = userService.AddUser(user);
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

        public ActionResult CRegister()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ComRegisterValidator(string Company,string Email,string Password,string Address)
        {
            ResultModel result = new ResultModel() { errorCode = 500, errorMes = "" };
            try
            {
                UserEntity user = new UserEntity();
                user.UserGuid = CommonHelper.CreateGuid("user");
                user.FirstName = "";
                user.LastName = "";
                user.UserName = Company;
                user.Company = Company;
                user.Email = Email;
                user.Address = Address;
                user.Password = ConvertHelper.MD5Encrypt(Password);
                user.HeadImage = "";
                user.UserType = 1;
                user.UserState = 0;
                user.AddTime = DateTime.Now;
                var userService = new UserService();
                int i = userService.AddUser(user);
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
        #endregion

        #region LayOut
        public ActionResult LoginOut()
        {
            CookieHelper.DelCookie(LoginHelper.LoginCookieID);
            CookieHelper.DelCookie(LoginHelper.LoginCookieGuid);
            CookieHelper.DelCookie(LoginHelper.LoginCookieName);
            CookieHelper.DelCookie(LoginHelper.LoginCookieEmail);
            CookieHelper.DelCookie(LoginHelper.LoginCookieFirstName);
            CookieHelper.DelCookie(LoginHelper.LoginCookieLastName);
            CookieHelper.DelCookie(LoginHelper.UserNameType);
            if (System.Web.HttpContext.Current.Session != null)
            {
                System.Web.HttpContext.Current.Session.Clear();
            }
            return RedirectToAction("Index","Login");
        }
        #endregion
    }
}