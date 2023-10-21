using PRTelegramBot.Attributes;
using PRTelegramBot.Helpers;
using PRTelegramBot.Helpers.TG;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramExample
{
    public class LessonTwo
    {
        #region Reply
        [ReplyMenuHandler(1, "Тест")]
        public static async Task Example(ITelegramBotClient botClient, Update update)
        {
            var message = "Hello world One";
            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        }

        [ReplyMenuHandler("Меню")]
        public static async Task Menu(ITelegramBotClient botClient, Update update)
        {
            var message = "Меню";

            var menuListString = new List<string>();

            var ran = new Random().Next(0, 1000);

            menuListString.Add("Меню");
            menuListString.Add($"Меню ({ran})");

            var menu = MenuGenerator.ReplyKeyboard(2, menuListString);

            var option = new OptionMessage();
            option.MenuReplyKeyboardMarkup = menu;

            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message, option);
        }


        #endregion

        #region Slash
        [SlashHandler("/set")]
        [ReplyMenuHandler("get")]
        public static async Task Get(ITelegramBotClient botClient, Update update)
        {
            var message = "Пример /get и /get_1";
            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        }

        [SlashHandler("/get")]
        public static async Task GetSlash(ITelegramBotClient botClient, Update update)
        {
            if (update.Message.Text.Contains("_"))
            {
                var spl = update.Message.Text.Split('_');
                if (spl.Length > 1)
                {
                    var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, $"Команда /get и параметр {spl[1]}");
                }
                else
                {
                    var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, "Команда /get");
                }
            }
            else
            {
                var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, "Команда /get");
            }


        }

        #endregion

        #region Inline
        [ReplyMenuHandler("inline")]
        public static async Task Inline(ITelegramBotClient botClient, Update update)
        {
            var message = "Пример inline";

            List<IInlineContent> menu = new List<IInlineContent>();

            var exampleOne = new InlineCallback("Пример 1", CustomTHeader.ExampleOne);
            var url = new InlineURL("Google", "https://googles.com");
            var webapp = new InlineWebApp("WebApp", "https://prethink.github.io/telegram/webapp.html");

            var exampleTwo = new InlineCallback<EntityTCommand<long>>("Название кнопки 2", CustomTHeader.ExampleTwo, new EntityTCommand<long>(5));
            var exampleThree = new InlineCallback<EntityTCommand<long>>("Название кнопки 3", CustomTHeader.ExampleThree, new EntityTCommand<long>(3));

            menu.Add(exampleOne);
            menu.Add(url);
            menu.Add(webapp);
            menu.Add(exampleTwo);
            menu.Add(exampleThree);

            var menuItems = MenuGenerator.InlineKeyboard(1, menu);

            var optins = new OptionMessage();
            optins.MenuInlineKeyboardMarkup = menuItems;

            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message, optins);
        }

        [InlineCallbackHandler<CustomTHeader>(CustomTHeader.ExampleOne)]
        public static async Task InlineExample(ITelegramBotClient botClient, Update update)
        {
            var message = "Пример ExampleOne";

            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        }


        [InlineCallbackHandler<CustomTHeader>(CustomTHeader.ExampleTwo, CustomTHeader.ExampleThree)]
        public static async Task InlineExampleTwoThree(ITelegramBotClient botClient, Update update)
        {
            var command = InlineCallback<EntityTCommand<long>>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
            if (command != null)
            {
                var message = $"Данные {command.Data.EntityId}";
                var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
            }

        }
        #endregion
    }
}
