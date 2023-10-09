using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Notifications.Models
{
    public class FcmNotificationSetting
    {
        public string SenderId { get; set; }
        public string ServerKey { get; set; }
    }
}
