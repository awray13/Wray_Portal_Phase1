using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wray_Portal_Phase1.ViewModels
{
    public class UserProfileVM
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string RoleName { get; set; }

        public string Email { get; set; }

        public string FullName
        {
            get
            {
                return $"{LastName}, {FirstName}";
            }
        }
    }
}