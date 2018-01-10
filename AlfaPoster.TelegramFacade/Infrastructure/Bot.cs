using Telegram.Bot;

namespace AlfaPoster.TelegramFacade.Infrastructure
{
    public static class Bot
    {
        public static TelegramBotClient Api { get; private set; }
        public static string PostCommand { get; private set; }
        public static string InfoMessage { get; private set; }
        public static string SuccessMessage { get; private set; }
        
        public static void Init(string token, string botName)
        {
            Api = new TelegramBotClient(token);
            PostCommand =  "/post@" + botName;
            InfoMessage = $"Постит реплай, либо сообщение после команды \"{PostCommand}\" в https://twitter.com/AlfaDevs. Подписывайтесь, ставьте лайки!";
            SuccessMessage = "Запощено!";
        }  
            
    }
}
