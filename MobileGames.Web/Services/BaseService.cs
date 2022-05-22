using System.Net.Http.Headers;
using System.Text;
using MobileGames.Web.Models;
using MobileGames.Web.Services.IServices;
using Newtonsoft.Json;

namespace MobileGames.Web.Services;

public class BaseService : IBaseService
{
    public ResponseDto ResponseModel { get; set; }
    private IHttpClientFactory _httpClient { get; set; }

    public BaseService(IHttpClientFactory httpClient)
    {
        ResponseModel = new ResponseDto();
        _httpClient = httpClient;
    }

    public async Task<T> SendAsync<T>(ApiRequest request)
    {
        try
        {
            var client = _httpClient.CreateClient("MongoApi");
            var requestMessage = new HttpRequestMessage();
            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.RequestUri = new Uri(request.Url);
            client.DefaultRequestHeaders.Clear();
            
            if (request.Data != null)
            {
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8,
                    "application/json");
            }

            if (!string.IsNullOrEmpty(request.AccessToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", request.AccessToken);
            }

            requestMessage.Method = request.ApiType switch
            {
                Config.ApiType.GET => HttpMethod.Get,
                Config.ApiType.POST => HttpMethod.Post,
                Config.ApiType.PUT => HttpMethod.Put,
                Config.ApiType.DELETE => HttpMethod.Delete,
                _ => HttpMethod.Get
            };

            var responseApi = await client.SendAsync(requestMessage);
            var apiContent = await responseApi.Content.ReadAsStringAsync();
            var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);

            return apiResponseDto;
        }
        catch (Exception e)
        {
            var dto = new ResponseDto
            {
                Message = "Error",
                ErrorMessage = new List<string>
                {
                    Convert.ToString(e.Message)
                },
                IsSuccess = false
            };

            var res = JsonConvert.SerializeObject(dto);
            var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
            return apiResponseDto;
        }
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(true);
    }
}