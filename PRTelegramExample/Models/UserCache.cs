using PRTelegramBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTelegramExample.Models
{
    /// <summary>
    /// Кэш пользователя
    /// </summary>
    public class UserCache : TelegramCache
    {
        /// <summary>
        /// Данные
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Очистка данных
        /// </summary>
        public override void ClearData()
        {
            Data = "";
            base.ClearData();
        }
    }
}
