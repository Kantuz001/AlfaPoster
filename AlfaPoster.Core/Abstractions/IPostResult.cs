namespace AlfaPoster.Core.Abstractions
{
    public interface IPostResult
    {
        bool Success { get; set; }
        string Message { get; set; }
    }
}