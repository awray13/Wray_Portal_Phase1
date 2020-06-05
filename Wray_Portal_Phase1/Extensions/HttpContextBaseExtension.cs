using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Wray_Portal_Phase1.Models;

namespace Wray_Portal_Phase1.Extensions
{
    public class HttpContextBaseExtension
    {
        public static async Task RefreshAuthentication(HttpContextBase context, ApplicationUser user)
        {
            // this line performs the programmatic sign out
            context.GetOwinContext().Authentication.SignOut();

            // Now we sign back in and any new role will now be recognized
            await context.GetOwinContext().Get<ApplicationSignInManager>().SignInAsync(user, isPersistent: false, rememberBrowser: false);
        }
    }
}