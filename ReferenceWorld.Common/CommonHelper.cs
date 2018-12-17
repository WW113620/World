using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ReferenceWorld.Common
{
    public class CommonHelper
    {

        /// <summary>
        /// Create Guid
        /// </summary>
        public static string CreateGuid(string type)
        {
            string result = string.Empty;
            switch(type.ToLower())
            {
                default:
                case "user":
                    result = "U"+Guid.NewGuid().ToString("N");
                    break;
            }
            return result;
        }

        #region Common      
        /// <summary>
        /// is Int
        /// </summary>
        /// <returns>result：true or：false</returns>
        public static bool isIntValue(string strValue)
        {
            try
            {
                Convert.ToInt64(strValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// is Float
        /// </summary>
        /// <returns>result：true or：false</returns>
        public static bool isFloatValue(string strValue)
        {
            try
            {
                Convert.ToSingle(strValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Remove Number
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string RemoveNumber(string key)
        {
            return Regex.Replace(key, @"\d", "");
        }

        /// <summary>
        /// Remove No Number
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string RemoveNotNumber(string key)
        {
            return Regex.Replace(key, @"[^\d]*", "");
        }
        /// <summary>
        ///Full - byte half - angle digital conversion
        /// </summary>
        public static string ChangeCharToInt(string strNum)
        {
            strNum = strNum.Replace("０", "0");
            strNum = strNum.Replace("１", "1");
            strNum = strNum.Replace("２", "2");
            strNum = strNum.Replace("３", "3");
            strNum = strNum.Replace("４", "4");

            strNum = strNum.Replace("５", "5");
            strNum = strNum.Replace("６", "6");
            strNum = strNum.Replace("７", "7");
            strNum = strNum.Replace("８", "8");
            strNum = strNum.Replace("９", "9");
            return strNum;
        }

        public static bool IsEmail(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return false;
            Regex regex = new Regex(@"^[a-zA-Z0-9][a-zA-Z0-9_\-]*@[a-zA-Z0-9_\-]+(\.[a-zA-Z0-9_\-]+)+$", RegexOptions.IgnoreCase);
            return regex.IsMatch(strValue);
        }

        public static bool IsUrlFormat(string strValue)
        {
            Regex re = new Regex(@"(http://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            return re.IsMatch(strValue);
        }
        #endregion



    }
   
}