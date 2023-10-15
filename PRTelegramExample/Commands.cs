using PRTelegramBot.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramExample
{
    public class Commands
    {
        [ReplyMenuHandler(true,"Тест","Hello","World")]
        public static async Task Example(ITelegramBotClient botClient, Update update)
        {
            var message = "Hello world";
            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient,update, message);
        }
    }
}
