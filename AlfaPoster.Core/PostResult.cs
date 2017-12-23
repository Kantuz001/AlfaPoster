using AlfaPoster.Core.Abstractions;

namespace AlfaPoster.Core
{
    public class PostResult : IPostResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}