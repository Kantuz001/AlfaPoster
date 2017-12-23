using System;
using System.Threading.Tasks;
using AlfaPoster.Core.Abstractions;
using CoreTweet;

namespace AlfaPoster.Core
{
    public class TwitterAdapter : IAdapter
    {
        private readonly Tokens _tokens;
        
        public TwitterAdapter(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            _tokens = Tokens.Create(consumerKey, consumerSecret, accessToken, accessTokenSecret);
        }

        public async Task<IPostResult> PostAsync(string message)
        {
            try
            {
                await _tokens.Statuses.UpdateAsync(message);
                return new PostResult {Success = true};
            }
            catch (Exception exception)
            {
                return new PostResult
                {
                    Success = false,
                    Message = exception.Message
                };
            }
            
        }
            
    }
}