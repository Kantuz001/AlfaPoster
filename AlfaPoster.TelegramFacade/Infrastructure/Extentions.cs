using System;
using System.Collections.Generic;

namespace AlfaPoster.TelegramFacade.Infrastructure
{
    public static class Extentions
    {
        public static string RemoveCommand(this string str) =>
            str.Remove(0, str.IndexOf(' '));
    }
}