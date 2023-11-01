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
    /// <summary>
    /// Урок 5 - постраничная навигация в сообщениях
    /// </summary>
    public class PageCommand
    {
        /// <summary>
        /// Тестовые данные для сообщений
        /// </summary>
        static List<string> pageData = new List<string>()
        {
            "Страница 1",
            "Страница 2",
            "Страница 3",
            "Страница 4",
            "Страница 5",
        };

        /// <summary>
        /// Постраничный вывод сообщений
        /// Напишите в чате pages
        /// </summary>

        [ReplyMenuHandler("pages")]
        public static async Task Pages(ITelegramBotClient botClient, Update update)
        {
            //Данные для постраничного вывода
            var data = await pageData.GetPaged(1, 1);
            //Генерация дополнительной кнопки для кастомной обработки данных
            var button = new InlineCallback("⭐", CustomTHeader.Favorite);
            //Генерация постраничного меню 
            // 1 - Текущая страница
            // 2 - количество страниц
            // 3 - Заголовок для определения откуда была выполнена команда
            // 4 - кастомная кнопка для дополнительной обработки информации
            var generateMenu = MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount, CustomTHeader.CustomPage, button: button);
            var option = new OptionMessage();
            option.MenuInlineKeyboardMarkup = generateMenu;
            await PRTelegramBot.Helpers.Message.Send(botClient,update,data.Results.FirstOrDefault(),option);
        }

        /// <summary>
        /// Обработка постраничной навигации
        /// NextPage - нажатие следующей страници
        /// PreviousPage - предыдущая страница
        /// CurrentPage - текущая страница
        /// </summary>

        [InlineCallbackHandler<THeader>(THeader.NextPage,THeader.PreviousPage, THeader.CurrentPage)]
        public static async Task PageHandler(ITelegramBotClient botClient, Update update)
        {
            //Пытаемся получить данные команды
            var command = InlineCallback<PageTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
            if(command != null) 
            {
                //Функционал практически тот же, только другая страница
                var data = await pageData.GetPaged(command.Data.Page, 1);
                var button = new InlineCallback("⭐", CustomTHeader.Favorite);
                var generateMenu = MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount, CustomTHeader.CustomPage, button: button);
                var option = new OptionMessage();
                option.MenuInlineKeyboardMarkup = generateMenu;
                await PRTelegramBot.Helpers.Message.Edit(botClient, update, data.Results.FirstOrDefault(), option);
            }
        }

        /// <summary>
        /// Обработка дополнительной кнопки
        /// </summary>
        [InlineCallbackHandler<CustomTHeader>(CustomTHeader.Favorite)]
        public static async Task PageHandlerFavorite(ITelegramBotClient botClient, Update update)
        {
            string msg = "Обработка данных";
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }
    }
}
