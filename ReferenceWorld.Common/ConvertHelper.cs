using System;
using System.Text;

namespace ReferenceWorld.Common
{
    public static class ConvertHelper
    {
        #region Convert
        /// <summary>
        /// To Int
        /// </summary>
        /// <param name="data"></param>
        /// <param name="RtnData"></param>
        /// <returns></returns>
        public static int ToInt(this object data, int RtnData)
        {
            int rtnData = RtnData;
            try
            {
                rtnData = Convert.ToInt32(data);
            }
            catch
            {
                rtnData = RtnData;
            }
            return rtnData;
        }

        /// <summary>
        /// To Long
        /// </summary>
        /// <param name="data"></param>
        /// <param name="RtnData"></param>
        /// <returns></returns>
        public static long ToLong(this object data, long RtnData)
        {
            long rtnData = RtnData;
            try
            {
                if (data != null && data.ToString() != "")
                    rtnData = Convert.ToInt64(data);
            }
            catch
            {
                rtnData = RtnData;
            }
            return rtnData;
        }

        /// <summary>
        /// To Float
        /// </summary>
        /// <param name="data"></param>
        /// <param name="RtnData"></param>
        /// <returns></returns>
        public static float ToFloat(this object data, float RtnData)
        {
            float rtnData = RtnData;
            try
            {
                if (data != null)
                    rtnData = Convert.ToSingle(data);
            }
            catch
            {
                rtnData = RtnData;
            }
            return rtnData;
        }

        /// <summary>
        /// To Money
        /// </summary>
        /// <param name="Money"></param>
        /// <returns></returns>
        public static string ToMoney(this object Money)
        {
            double floatMoney = Money.ToDouble(0);
            if (floatMoney <= 0)
            {
                return "0.00";
            }
            else
            {
                floatMoney = Math.Round(floatMoney, 1, MidpointRounding.AwayFromZero);//
                string rtnStr = floatMoney.ToString("0.0") + "0";
                return rtnStr;
            }
        }
        /// <summary>
        /// To Double
        /// </summary>
        /// <param name="data"></param>
        /// <param name="RtnData"></param>
        /// <returns></returns>
        public static double ToDouble(this object data, double RtnData)
        {
            double rtnData = RtnData;
            try
            {
                if (data != null)
                    rtnData = Convert.ToDouble(data);
            }
            catch
            {
                rtnData = RtnData;
            }
            return rtnData;
        }
        /// <summary>
        /// To Decimal
        /// </summary>
        /// <param name="data"></param>
        /// <param name="RtnData"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object data, decimal RtnData)
        {
            decimal rtnData = RtnData;
            try
            {
                if (data != null)
                    rtnData = Math.Round(Convert.ToDecimal(data), 2, MidpointRounding.AwayFromZero);
            }
            catch
            {
                rtnData = RtnData;
            }
            return rtnData;
        }

        /// <summary>
        /// To String
        /// </summary>
        /// <param name="data"></param>
        /// <param name="RtnData"></param>
        /// <returns></returns>
        public static string ToString(this object data, string RtnData)
        {
            string rtnData = RtnData;
            try
            {
                if (data != null && data.ToString().Length > 0)
                    rtnData = Convert.ToString(data);
            }
            catch
            {
                rtnData = RtnData;
            }
            return rtnData;
        }

        /// <summary>
        /// To Char
        /// </summary>
        /// <param name="data"></param>
        /// <param name="RtnData"></param>
        /// <returns></returns>
        public static char ToChar(this object data, char RtnData)
        {
            char rtnData = RtnData;
            try
            {
                if (data != null && data.ToString().Length > 0)
                    rtnData = Convert.ToChar(data);
            }
            catch
            {
                rtnData = RtnData;
            }
            return rtnData;
        }

        /// <summary>
        /// To DateTime String
        /// </summary>
        /// <param name="format">format</param>
        /// <returns></returns>
        public static string ToDateTime(this object data, string RtnData, string format)
        {
            string rtnData = RtnData;
            try
            {
                if (data != null && data.ToString().Length > 0 && Convert.ToDateTime(data).ToString("yyyy") != "1900")
                    rtnData = Convert.ToDateTime(data).ToString(format);
            }
            catch
            {
            }
            return rtnData;
        }

        /// <summary>
        /// To DateTime
        /// </summary>
        /// <param name="data"></param>
        /// <param name="RtnData"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object data, DateTime RtnData)
        {
            DateTime rtnData = RtnData;
            try
            {
                if (data != null && data.ToString().Length > 0 && Convert.ToDateTime(data).ToString("yyyy") != "1900")
                    rtnData = Convert.ToDateTime(data);
            }
            catch
            {
            }
            return rtnData;
        }

        public static byte[] StreamToBytes(System.IO.Stream stream)
        {
            if (stream == null)
                return null;
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            return bytes;
        }
        public static System.IO.Stream BytesToStream(byte[] bytes)
        {
            if (bytes == null)
                return null;
            System.IO.Stream stream = new System.IO.MemoryStream(bytes);
            return stream;
        }

        #endregion
        
        #region MD5 Encrypt
        /// <summary>
        /// MD5
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns>result</returns>
        public static string MD5Encrypt(string input)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            string encoded = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(input))).Replace("-", "");
            return encoded;
        }
        #endregion

        #region Date of return based on DD mm yyyy mm DD YY
        /// <summary>
        /// CaculateWeekDay(2004,12,9)
        /// </summary>
        /// <param name="y">Year</param>
        /// <param name="m">Month</param>
        /// <param name="d">Day</param>
        /// <returns></returns>
        public static string CaculateWeekDay(int y, int m, int d)
        {
            if (m == 1) m = 13;
            if (m == 2) m = 14;
            int week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7 + 1;
            string weekstr = "";
            switch (week)
            {
                case 1: weekstr = "Monday"; break;
                case 2: weekstr = "Tuesday"; break;
                case 3: weekstr = "Wednesday"; break;
                case 4: weekstr = "Thursday"; break;
                case 5: weekstr = "Friday"; break;
                case 6: weekstr = "Saturday"; break;
                case 7: weekstr = "Sunday"; break;
            }
            return weekstr;
        }
        #endregion

    }
}