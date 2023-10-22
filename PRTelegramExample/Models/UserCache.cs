using PRTelegramBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTelegramExample.Models
{
    public class UserCache : TelegramCache
    {
        public string Data { get; set; }

        public override void ClearData()
        {
            Data = "";
            base.ClearData();
        }
    }
}
