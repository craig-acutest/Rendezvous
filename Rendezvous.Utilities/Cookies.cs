using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rendezvous.Utilities
{
    public static class Cookies
    {
        public static void CreateCookie(string name, string value, int minsExpire)
        {
            HttpCookie myCookie = new HttpCookie(name);
            DateTime now = DateTime.Now;

            myCookie.Value = value;
            myCookie.Expires = now.AddMinutes(minsExpire);

            // Add the cookie.
            HttpContext.Current.Response.Cookies.Add(myCookie);
        }

        public static string ReadCookie(string name)
        {
            HttpCookie myCookie = new HttpCookie(name);
            myCookie = HttpContext.Current.Request.Cookies[name];
            if (myCookie != null)
            {
                return myCookie.Value;
            }
            else
            {
                return null;
            }
        }
    }
}