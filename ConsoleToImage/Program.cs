using PDFLibNet;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;

namespace ConsoleToImage
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPPT = @"e:/test.pdf";
            string savaFile = @"E:/imgage/";
            CommonPPT.Test();

            PDFWrapper pdfWrapper = new PDFWrapper();

            CommonPPT.ConvertPDF2Image(inputPPT, savaFile, "testimgage", 1, 4, ImageFormat.Jpeg, CommonPPT.Definition.One);

            Console.ReadKey();
        }
    }
}
