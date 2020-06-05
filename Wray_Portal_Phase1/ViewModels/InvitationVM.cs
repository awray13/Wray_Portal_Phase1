using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wray_Portal_Phase1.ViewModels
{
    public class InvitationVM
    {
        public string SenderId { get; set; }

        public string ReceiverEmail { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

    }
}