using ReferenceWorld.Common;
using ReferenceWorld.Model;
using ReferenceWorld.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReferenceWorld.Controllers
{
    public class PersonInfoController : Controller
    {
        // GET: PersonInfo
        public ActionResult Index(string id = "")
        {
            var userService = new UserService();
            var user = userService.GetLoginUserAvatar(id);
            string path = string.Empty;
            Uri path3 = System.Web.HttpContext.Current.Request.UrlReferrer;
            if (path3 != null)
            {
                path = path3.AbsolutePath;
                path = Url.Content(string.Format("~/{0}", path));
                if(path!=null)
                {
                    Session["returnUrl"] = path;
                }
            }
            return View(user);
        }

        [HttpPost]
        public JsonResult UploadAvatar(string userId)
        {
            ResultModel result = new ResultModel() { errorCode = 500, errorMes = "" };
            if (HttpContext.Request.Files.Count > 0)
            {
                try
                {
                    HttpPostedFileBase file = HttpContext.Request.Files[0];
                    string fileName = Path.GetFileName(file.FileName);
                    string fileExtension = Path.GetExtension(file.FileName);
                    string saveName = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), fileExtension);

                    string savaFile = System.Web.HttpContext.Current.Server.MapPath("~/Upload/Avatar");
                    if (!Directory.Exists(savaFile))
                    {
                        Directory.CreateDirectory(savaFile);
                    }
                    var filePath = Path.Combine(savaFile, saveName);
                    file.SaveAs(filePath);
                    var userService = new UserService();
                    int i = userService.AddUserAvatar(userId, saveName);
                    if (i > 0)
                    {
                        var returnUrl = Session["returnUrl"].ToString("");
                        result.errorCode = 200;
                        result.errorMes = returnUrl;
                    }
                    else
                    {
                        result.errorCode = 300;
                        result.errorMes = "fail";
                    }

                }
                catch (Exception e)
                {
                    result.errorCode = 400;
                    result.errorMes = e.Message;
                }
            }
            else
            {
                result.errorCode = 100;
                result.errorMes = "no picture";
            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult UploadImage(string img, string code)
        {
            ResultModel result = new ResultModel() { errorCode = 500, errorMes = "" };
            try
            {
                string savaFile = System.Web.HttpContext.Current.Server.MapPath("~/Upload/Picture");
                if (!Directory.Exists(savaFile))
                {
                    Directory.CreateDirectory(savaFile);
                }
                string[] arrStr = img.Split(',');
                byte[] imageBytes = Convert.FromBase64String(arrStr[1]);
                MemoryStream memoryStream = new MemoryStream(imageBytes, 0, imageBytes.Length);
                memoryStream.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(memoryStream);
                Random r = new Random();
                string saveName = string.Format("{0}{1}{2}", DateTime.Now.ToString("yyyyMMddHHmmss"), r.Next(10000), ".jpg");
                var filePath = Path.Combine(savaFile, saveName);
                image.Save(filePath);
                Photos photo = new Photos();
                photo.Image = saveName;
                photo.UserGuid = code;
                photo.CreateTime = DateTime.Now;
                int i = InsertPicture(photo);
                if (i > 0)
                {
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

        public static int InsertPicture(Photos photo)
        {
            var userService = new UserService();
            return userService.InsertPicture(photo);
        }

    }
}