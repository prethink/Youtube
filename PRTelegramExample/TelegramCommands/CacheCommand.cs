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
    /// <summary>
    /// Урок 4 - работа с кэшем
    /// </summary>
    public class CacheCommand
    {
        /// <summary>
        /// Установка значения в кэш пользователя
        /// Напишите в боте setcache
        /// </summary>
        [ReplyMenuHandler("setcache")]
        public static async Task SetCache(ITelegramBotClient botClient,Update update)
        {
            /* Записываем данные в кэш пользователя
             * Для быстрого получения id пользователя используйте update.GetChatId()
             */ 
            update.GetCacheData<UserCache>().Id = update.GetChatId();
            string msg = $"Запомнил данные:{update.GetChatId()}";
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Получение данных из кэша пользователя
        /// Напишите в боте getcache
        /// </summary>>
        [ReplyMenuHandler("getcache")]
        public static async Task GetCache(ITelegramBotClient botClient, Update update)
        {
            //Получаем кэш пользователя
            var cache = update.GetCacheData<UserCache>();
            string msg = "";
            //Есть данные в кэше
            if(cache.Id != null)
            {
                msg = $"Данные из кэша: {cache.Id}";
            }
            else // Нет данных
            {
                msg = "Данных нет";
            }
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Очистка кэша пользователя
        /// Напишите в боте clearcache
        /// </summary>
        [ReplyMenuHandler("clearcache")]
        public static async Task ClearCache(ITelegramBotClient botClient, Update update)
        {
            //Очистка данных кэша пользователя
            update.GetCacheData<UserCache>().ClearData();
            string msg = "ClearData";
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }
    }
}
