using System.Threading.Tasks;

namespace AlfaPoster.Core.Abstractions
{
    public interface IAdapter
    {
        Task<IPostResult> PostAsync(string message);
    }
}