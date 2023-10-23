using Microsoft.VisualBasic.FileIO;
using PRTelegramBot.Attributes;
using PRTelegramBot.Helpers;
using PRTelegramBot.Helpers.TG;
using PRTelegramBot.Models;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Models.TCommands;
using PRTelegramExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using THeader = PRTelegramBot.Models.Enums.THeader;

namespace PRTelegramExample.TelegramCommands
{
    public class PageCommand
    {
        static List<string> pageData = new List<string>()
        {
            "Страница 1",
            "Страница 2",
            "Страница 3",
            "Страница 4",
            "Страница 5",
        };

        [ReplyMenuHandler("pages")]
        public static async Task Pages(ITelegramBotClient botClient, Update update)
        {
            var data = await pageData.GetPaged(1, 1);
            var button = new InlineCallback("⭐", CustomTHeader.Favorite);
            var generateMenu = MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount, CustomTHeader.CustomPage, button: button);
            var option = new OptionMessage();
            option.MenuInlineKeyboardMarkup = generateMenu;
            await PRTelegramBot.Helpers.Message.Send(botClient,update,data.Results.FirstOrDefault(),option);
        }

        [InlineCallbackHandler<THeader>(THeader.NextPage,THeader.PreviousPage, THeader.CurrentPage)]
        public static async Task PageHandler(ITelegramBotClient botClient, Update update)
        {
            var command = InlineCallback<PageTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
            if(command != null)
            {
                var data = await pageData.GetPaged(command.Data.Page, 1);
                var button = new InlineCallback("⭐", CustomTHeader.Favorite);
                var generateMenu = MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount, CustomTHeader.CustomPage, button: button);
                var option = new OptionMessage();
                option.MenuInlineKeyboardMarkup = generateMenu;
                await PRTelegramBot.Helpers.Message.Edit(botClient, update, data.Results.FirstOrDefault(), option);
            }
        }

        [InlineCallbackHandler<CustomTHeader>(CustomTHeader.Favorite)]
        public static async Task PageHandlerFavorite(ITelegramBotClient botClient, Update update)
        {
            string msg = "Обработка данных";
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }
    }
}
