using System.Collections.Generic;

namespace AlfaPoster.TelegramFacade.Infrastructure.Config
{
    public class Config
    {
        public TwitterConfig TwitterConfig { get; set; }
        public TelegramConfig TelegramConfig { get; set; }
        public string HostUrl { get; set; }        
        public bool Ngrok { get; set; }
    }
}
