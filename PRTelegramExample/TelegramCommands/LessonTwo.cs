﻿using PRTelegramBot.Attributes;
using PRTelegramBot.Helpers;
using PRTelegramBot.Helpers.TG;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Models.Interface;
using PRTelegramExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramExample.TelegramCommands
{
    /// <summary>
    /// Урок 2 - работа с командами
    /// </summary>
    public class LessonTwo
    {
        /// <summary>
        /// ВНИМАНИЕ! Это команда работает только для бота с id 1
        /// Напишите в боте Тест
        /// </summary>
        #region Reply
        [ReplyMenuHandler(1, "Тест")]
        public static async Task Example(ITelegramBotClient botClient, Update update)
        {
            var message = "Hello world One";
            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        }

        /// <summary>
        /// Генерация простого меню
        /// </summary>
        [ReplyMenuHandler("Меню")]
        public static async Task Menu(ITelegramBotClient botClient, Update update)
        {
            var message = "Меню";

            //Создаем список string 
            var menuListString = new List<string>();

            //Генерация случайного значения, чтобы показать что в меню могут быть динамические пункты со скобками
            var ran = new Random().Next(0, 1000);
            
            //Создание пункта Меню - при нажатие на который будет вызвана команда Меню
            menuListString.Add("Меню");
            //Создание пункта Меню (число) - при нажатие на который будет вызвана команда Меню
            menuListString.Add($"Меню ({ran})");

            //Генерация меню в 2 столбца
            var menu = MenuGenerator.ReplyKeyboard(2, menuListString);

            var option = new OptionMessage();
            option.MenuReplyKeyboardMarkup = menu;

            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message, option);
        }


        #endregion

        #region Slash
        /// <summary>
        /// Напишите в боте /set или get
        /// Пример комбинирования команд
        /// </summary>
        [SlashHandler("/set")]
        [ReplyMenuHandler("get")]
        public static async Task Get(ITelegramBotClient botClient, Update update)
        {
            var message = "Пример /get и /get_1";
            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        }

        /// <summary>
        /// Напишите в боте /get /get_2 /get_test
        /// Пример обработки данных из slash команды
        /// </summary>
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
        /// <summary>
        /// Пример работы с Inline командами
        /// Напишите в боте 
        /// </summary>
        [ReplyMenuHandler("inline")]
        public static async Task Inline(ITelegramBotClient botClient, Update update)
        {
            var message = "Пример inline";

            //Создаем список с IInlineContent его реализовываеют все inline кнопки
            List<IInlineContent> menu = new List<IInlineContent>();

            //Inline кнопка с обработкой действия
            var exampleOne = new InlineCallback("Пример 1", CustomTHeader.ExampleOne);
            //Inline кнопка с обработка с переадресацией на сайт
            var url = new InlineURL("Google", "https://googles.com");
            //Inline кнопка с webapp
            var webapp = new InlineWebApp("WebApp", "https://prethink.github.io/telegram/webapp.html");

            //Inline кнопки с примером передачи информации
            var exampleTwo = new InlineCallback<EntityTCommand<long>>("Название кнопки 2", CustomTHeader.ExampleTwo, new EntityTCommand<long>(5));
            var exampleThree = new InlineCallback<EntityTCommand<long>>("Название кнопки 3", CustomTHeader.ExampleThree, new EntityTCommand<long>(3));

            menu.Add(exampleOne);
            menu.Add(url);
            menu.Add(webapp);
            menu.Add(exampleTwo);
            menu.Add(exampleThree);

            //Генерация меню в 1 столбец
            var menuItems = MenuGenerator.InlineKeyboard(1, menu);

            var optins = new OptionMessage();
            optins.MenuInlineKeyboardMarkup = menuItems;

            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message, optins);
        }

        /// <summary>
        /// Обработка Inline ннопки с заголовком ExampleOne
        /// </summary>
        [InlineCallbackHandler<CustomTHeader>(CustomTHeader.ExampleOne)]
        public static async Task InlineExample(ITelegramBotClient botClient, Update update)
        {
            var message = "Пример ExampleOne";

            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        }

        /// <summary>
        /// Обработка кнопки с заголовками ExampleTwo и ExampleThree
        /// </summary>
        [InlineCallbackHandler<CustomTHeader>(CustomTHeader.ExampleTwo, CustomTHeader.ExampleThree)]
        public static async Task InlineExampleTwoThree(ITelegramBotClient botClient, Update update)
        {
            //Пытаемся получить команду
            var command = InlineCallback<EntityTCommand<long>>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
            if (command != null)
            {
                //Выводим данные которые были переданы
                var message = $"Данные {command.Data.EntityId}";
                var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
            }

        }
        #endregion
    }
}
