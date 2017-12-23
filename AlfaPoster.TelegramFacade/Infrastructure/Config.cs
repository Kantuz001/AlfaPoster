using System.Collections.Generic;

namespace AlfaPoster.TelegramFacade.Infrastructure
{
    public class Config
    {
        public string TwitterConsumerKey { get; set; }
        public string TwitterConsumerSecret { get; set; }
        public string TwitterAccessToken { get; set; }
        public string TwitterAccessTokenSecre { get; set; }
        public string HostUrl { get; set; }
        public string TelegramBotToken { get; set; }
        public bool Ngrok { get; set; }
    }
}
