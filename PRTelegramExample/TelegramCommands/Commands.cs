using PRTelegramBot.Attributes;
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
    /// Урок 1 - Создание простого бота за 10 минут
    /// </summary>
    public class Commands
    {
        /// <summary>
        /// Работа простых команд
        /// Напишите в боте Тест или Hello или World
        /// Работа только для бота с id 0
        /// </summary>
        [ReplyMenuHandler(true, "Тест", "Hello", "World")]
        public static async Task Example(ITelegramBotClient botClient, Update update)
        {
            var message = "Hello world";
            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        }
    }
}
