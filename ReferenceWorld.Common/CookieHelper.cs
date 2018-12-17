using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReferenceWorld.Common
{
    public class CookieHelper
    {
        #region Time Util
        public enum TimeUtil
        {
            /// <summary>
            /// Year
            /// </summary>
            Y = 0,
            /// <summary>
            /// Month
            /// </summary>
            M = 1,
            /// <summary>
            /// Day
            /// </summary>
            D = 2,
            /// <summary>
            /// Hour
            /// </summary>
            H = 3,
            /// <summary>
            /// Minute
            /// </summary>
            mi = 4,
            /// <summary>
            /// One month
            /// </summary>
            s = 5,
            /// <summary>
            /// no
            /// </summary>
            None
        } 
        #endregion

        #region Set Cookie Value
        /// <summary>
        ///  Set Cookie Value
        /// </summary>
        /// <param name="_name">Cookie Name</param>
        /// <param name="_value">Cookie Value</param>
        public static void SetCookie(string _name, string _value)
        {
            SetCookie(_name, _value, TimeUtil.None, string.Empty, string.Empty, false, string.Empty, false);
        }
        /// <summary>
        /// Set Cookie Value
        /// </summary>
        /// <param name="_name">Cookie Name</param>
        /// <param name="_value">Cookie Value</param>
        /// <param name="_expireTimeUtil">Expire Unit</param>
        /// <param name="_expireTimeSpan">Expire Time Interval</param>
        public static void SetCookie(string _name, string _value, TimeUtil _expireTimeUtil, string _expireTimeSpan)
        {
            SetCookie(_name, _value, _expireTimeUtil, _expireTimeSpan, string.Empty, false, string.Empty, false);
        }
        /// <summary>
        /// Set Cookie Value
        /// </summary>
        /// <param name="_name">Cookie Name</param>
        /// <param name="_value">Cookie Value</param>
        /// <param name="_expireTimeUtil">Expire Unit</param>
        /// <param name="_expireTimeSpan">Expire Time Interval</param>
        /// <param name="_domain">Sets the domain to which this cookie is associated</param>
        /// <param name="_httpOnly">Specifies whether the cookie can be accessed through client script, and default false</param>
        /// <param name="_path">The virtual path</param>
        /// <param name="_secure">HTTPS， default false</param>
        public static void SetCookie(string _name, string _value, TimeUtil _expireTimeUtil, string _expireTimeSpan, string _domain, bool _httpOnly, string _path, bool _secure)
        {
            SetCookie(_name, _value, GetExpireTime(_expireTimeUtil, _expireTimeSpan), _domain, _httpOnly, _path, _secure);
        }

        /// <summary>
        /// Set Cookie Value
        /// </summary>
        /// <param name="_name">Cookie Name</param>
        /// <param name="_value">Cookie Value</param>
        /// <param name="_expireTime">Expire Time</param>
        /// <param name="_domain">Sets the domain to which this cookie is associated</param>
        /// <param name="_httpOnly">Specifies whether the cookie can be accessed through client script, and is false by default</param>
        /// <param name="_path">The virtual path</param>
        /// <param name="_secure">HTTPS，default false</param>
        public static void SetCookie(string _name, string _value, DateTime _expireTime, string _domain, bool _httpOnly, string _path, bool _secure)
        {
            _value = System.Web.HttpUtility.UrlEncode(_value);
            HttpCookie _cookie = new HttpCookie(_name);
            _cookie.Value = _value;
            if (!string.IsNullOrEmpty(_domain))
                _cookie.Domain = _domain;
            _cookie.Expires = _expireTime;
            _cookie.HttpOnly = _httpOnly;
            if (!string.IsNullOrEmpty(_path))
                _cookie.Path = _path;
            _cookie.Secure = _secure;
            HttpContext.Current.Response.Cookies.Add(_cookie);
        }
        public static DateTime GetExpireTime(TimeUtil _expireTimeUtil, string _expireTimeSpan)
        {
            DateTime _dateTime = DateTime.Now.AddMonths(1);
            if (!string.IsNullOrEmpty(_expireTimeSpan))
            {
                switch (_expireTimeUtil)
                {
                    case TimeUtil.Y:
                        _dateTime = DateTime.Now.AddYears(int.Parse(_expireTimeSpan));
                        break;
                    case TimeUtil.M:
                        _dateTime = DateTime.Now.AddMonths(int.Parse(_expireTimeSpan));
                        break;
                    case TimeUtil.D:
                        _dateTime = DateTime.Now.AddDays(int.Parse(_expireTimeSpan));
                        break;
                    case TimeUtil.H:
                        _dateTime = DateTime.Now.AddHours(int.Parse(_expireTimeSpan));
                        break;
                    case TimeUtil.mi:
                        _dateTime = DateTime.Now.AddMinutes(int.Parse(_expireTimeSpan));
                        break;
                    case TimeUtil.s:
                        _dateTime = DateTime.Now.AddSeconds(int.Parse(_expireTimeSpan));
                        break;
                    case TimeUtil.None:
                    default:
                        _dateTime = DateTime.Now.AddMonths(1);
                        break;
                }
            }
            return _dateTime;
        }
        #endregion

        #region Get Cookie Value
        public static string GetCookie(string _name)
        {
            if (HttpContext.Current.Request.Cookies[_name] != null && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies[_name].Value))
            {
                return System.Web.HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[_name].Value);
            }
            else
                return string.Empty;
        }
        #endregion

        #region Delete Cookie Value
        public static void DelCookie(string strCookieName)
        {
            DelCookie(strCookieName, string.Empty);
        }
        /// <summary>
        /// Delete Cookie
        /// </summary>
        /// <param name="strCookieName">CookieName</param>
        /// <param name="strDomain">Domain</param>
        public static void DelCookie(string strCookieName, string strDomain)
        {
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            if (!string.IsNullOrEmpty(strDomain))
            {
                objCookie.Domain = strDomain;
            }
            objCookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }
        #endregion

    }
}