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
    /// Урок 4 - пошаговое выполнение команд
    /// </summary>
    public class StepCommand
    {
        /// <summary>
        /// Регистрация следующий команды обработки
        /// Напишите в боте startstep
        /// </summary>
        [ReplyMenuHandler("startstep")]
        public static async Task StartStep(ITelegramBotClient botClient, Update update)
        {
            // Регистрация следующего шага в аргументе требуется указать метод внутри StepTelegram
            update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(StepOne));
            string msg = "Введите данные:";
            await PRTelegramBot.Helpers.Message.Send(botClient,update, msg);
        }

        /// <summary>
        /// Обработка данных шага 1
        /// </summary>
        public static async Task StepOne(ITelegramBotClient botClient, Update update)
        {
            update.GetCacheData<UserCache>().Data = update.Message.Text;
            string msg = "Данные сохранены, напишите любое сообщение чтобы их увидеть.";
            //Регистрация шага 2
            update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(StepTwo));
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Обработка данных шага 2
        /// </summary>
        public static async Task StepTwo(ITelegramBotClient botClient, Update update)
        {
            string msg = $"{update.GetCacheData<UserCache>().Data}";
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
            update.ClearStepUser();
        }

        /// <summary>
        /// Игнорирование пошагового выполнения команд
        /// Напиши в боте ignore
        /// </summary>
        [ReplyMenuHandler("ignore")]
        public static async Task IgnoreStep(ITelegramBotClient botClient, Update update)
        {
            string msg = "";
            //Есть шаг в цепочке
            if(update.HasStep())
            {
                msg = "Следующий шаг проигнорирован";
                update.ClearStepUser();
            }
            else //Нет шага в цепочке
            {
                msg = "Следующий шаг отсутсвует";
            }
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }
    }
}
