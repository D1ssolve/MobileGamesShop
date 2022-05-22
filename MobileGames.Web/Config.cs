namespace MobileGames.Web;

public static class Config
{
    public static string ProductApiBase { get; set; }
    
    public enum ApiType
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}