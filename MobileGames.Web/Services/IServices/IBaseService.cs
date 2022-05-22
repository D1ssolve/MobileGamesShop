using MobileGames.Web.Models;

namespace MobileGames.Web.Services.IServices;

public interface IBaseService : IDisposable
{
    ResponseDto ResponseModel { get; set; }
    Task<T> SendAsync<T>(ApiRequest request);
}