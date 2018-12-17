using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing.Imaging;
using ReferenceWorld.Common;
using System.IO;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;

namespace ReferenceWorld.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Convert()
        {
            string inputPPT = @"e:/test.pptx";
            string savaFile = System.Web.HttpContext.Current.Server.MapPath("~/Upload/ConvertPPT/");
            ConvertFromPowerPoint(inputPPT, savaFile);
            //PDFLibNetToPdfDemo.ConvertPDF2Image(inputPPT, savaFile,"testimgage", 1, 4, ImageFormat.Jpeg, PDFLibNetToPdfDemo.Definition.One);
            return View();
        }


        #region ppt转图片

        public static string ConvertFromPowerPoint(string sourceFilePath, string savaFile)
        {
            try
            {
                object missing = Type.Missing;

                Microsoft.Office.Interop.PowerPoint.Application application = new Microsoft.Office.Interop.PowerPoint.Application();
                Presentation presentation = null;

                try
                {
                    presentation = application.Presentations.Open(sourceFilePath, MsoTriState.msoTrue, MsoTriState.msoFalse, MsoTriState.msoFalse);
                }
                catch (System.Exception e)
                {

                }

                int PageCount = presentation.Slides.Count;

                double screenWidth = 800.0;
                double screenHeight = 600.0;

                if (!Directory.Exists(savaFile))
                {
                    Directory.CreateDirectory(savaFile);
                }
                

                for (int i = 1; i <= PageCount; i++)
                {
                    var str = savaFile + (i - 1).ToString("000") + ".jpg";
                    presentation.Slides[i].Export(str, "jpg", int.Parse(screenWidth.ToString().Split(new char[] { '.' })[0].ToString()), int.Parse(screenHeight.ToString().Split(new char[] { '.' })[0].ToString()));

                }
            }
            catch (Exception)
            {
                //KillPower();
            }
            finally
            {
                GC.Collect();
            }



            return "ok";
        }

        #endregion


    }
}