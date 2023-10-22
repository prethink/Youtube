using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramExample.TelegramCommands
{
    public class CacheCommand
    {
        [ReplyMenuHandler("setcache")]
        public static async Task SetCache(ITelegramBotClient botClient,Update update)
        {
            update.GetCacheData<UserCache>().Id = update.GetChatId();
            string msg = $"Запомнил данные:{update.GetChatId()}";
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }

        [ReplyMenuHandler("getcache")]
        public static async Task GetCache(ITelegramBotClient botClient, Update update)
        {
            var cache = update.GetCacheData<UserCache>();
            string msg = "";
            if(cache.Id != null)
            {
                msg = $"Данные из кэша: {cache.Id}";
            }
            else
            {
                msg = "Данных нет";
            }
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }

        [ReplyMenuHandler("clearcache")]
        public static async Task ClearCache(ITelegramBotClient botClient, Update update)
        {
            update.GetCacheData<UserCache>().ClearData();
            string msg = "ClearData";
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }
    }
}
