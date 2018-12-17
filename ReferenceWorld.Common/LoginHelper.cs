namespace ReferenceWorld.Common
{
    public class LoginHelper
    {
        public static string LoginCookieID { get { return "Login_Cookies_UserID"; } }
        public static string LoginCookieGuid { get { return "Login_Cookies_Guid"; } }
        public static string LoginCookieName { get { return "Login_Cookies_UserName"; } }
        public static string LoginCookieNameType { get { return "Login_Cookies_UserNameType"; } }
        public static string LoginCookieEmail { get { return "Login_Cookies_Email"; } }
        public static string LoginCookieNickName { get { return "Login_Cookies_NickName"; } }
        public static string LoginCookieFirstName { get { return "Login_Cookies_FirstName"; } }
        public static string LoginCookieLastName { get { return "Login_Cookies_LastName"; } }
        /// <summary>
        /// Login UserGuid
        /// </summary>
        public static string UserGuid
        {
            get { return CookieHelper.GetCookie(LoginCookieGuid); }
        }
        /// <summary>
        /// Login UserName
        /// </summary>
        public static string UserName
        {
            get { return CookieHelper.GetCookie(LoginCookieName); }
        }
        /// <summary>
        /// Login UserNameType
        /// </summary>
        public static string UserNameType
        {
            get { return CookieHelper.GetCookie(LoginCookieNameType); }
        }
        /// <summary>
        /// Login UserNameEmail
        /// </summary>
        public static string UserNameEmail
        {
            get { return CookieHelper.GetCookie(LoginCookieEmail); }
        }
        /// <summary>
        /// Login UserNickName
        /// </summary>
        public static string UserNickName
        {
            get { return CookieHelper.GetCookie(LoginCookieNickName); }
        }

    }
}