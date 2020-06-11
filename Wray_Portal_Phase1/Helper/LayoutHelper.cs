using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Wray_Portal_Phase1.Enums;

namespace Wray_Portal_Phase1.Helper
{
    public static class LayoutHelper
    {
        public static string GrabLayout()
        {
            var controller = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            var action = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            if ($"{controller}/{action}" == WebConfigurationManager.AppSettings[ProjectSetting.SpecialRoute.ToString()])
            {
                return WebConfigurationManager.AppSettings[ProjectSetting.SpecialLayout.ToString()];
            }

            return WebConfigurationManager.AppSettings[ProjectSetting.DefaultLayout.ToString()];

                
        }

    }
}