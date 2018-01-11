using System.Collections.Generic;
using System.Threading.Tasks;
using AlfaPoster.Core;
using AlfaPoster.TelegramFacade.Infrastructure;
using AlfaPoster.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;

namespace AlfaPoster.TelegramFacade.Controllers
{
    public class TelegramBotController : Controller
    {
        private readonly List<IAdapter> _adapters ;

        public TelegramBotController(IOptions<Config> settings)
        {
            _adapters = new List<IAdapter> 
            { 
                new TwitterAdapter(
                    settings.Value.TwitterConsumerKey, 
                    settings.Value.TwitterConsumerSecret, 
                    settings.Value.TwitterAccessToken, 
                    settings.Value.TwitterAccessTokenSecre)
            };
        }
        
        [HttpPost]
        [IpWhitelist("149.154.167.197", "149.154.167.233")]
        public async Task<IActionResult> Message([FromBody]Update update)
        {
            if (update.Message?.Text == null) return Ok();
            var message = update.Message;

            if (update.Message.ReplyToMessage == null && update.Message.Text.Trim() == Bot.PostCommand)
            {
                await Bot.Api.SendTextMessageAsync(message.Chat.Id, Bot.InfoMessage);
                return Ok();
            }
                
            var postText = PreparePostText(message);
            if (postText == null) return Ok();

            foreach (var adapter in _adapters)
            {
                var result = await adapter.PostAsync(postText);
                if (result.Success)
                    await Bot.Api.SendTextMessageAsync(message.Chat.Id, Bot.SuccessMessage);
                else
                    await Bot.Api.SendTextMessageAsync(message.Chat.Id, result.Message);
            }
            
            return Ok();
        }

        private string PreparePostText(Message message)
        {
            if (message.Text == null) return null;
            if (!message.Text.StartsWith(Bot.PostCommand)) return null;
           
            if (message.ReplyToMessage != null && message.Text == Bot.PostCommand)
                return message.ReplyToMessage.Text;

            var postText = message.Text.Replace(Bot.PostCommand, string.Empty);

            if (postText == string.Empty) return null;
            if (postText.Length > 240)
            {
                Bot.Api.SendTextMessageAsync(message.Chat.Id, "Не получилось запостить :( Сообщение больше 240 символов.");
                return null;
            }

            return postText;
        }
    }
}