using PRTelegramBot.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTelegramExample.Models
{
    /// <summary>
    /// Кастомные заголовки команд
    /// </summary>
    [InlineCommand]
    public enum CustomTHeader
    {
        ExampleOne = 100,
        ExampleTwo,
        ExampleThree,
        CustomPage,
        Favorite
    }
}
