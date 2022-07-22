using RestSharp;

namespace MicrosoftOutlook.Interface
{
    public interface IAuthClass
    {
        string Login();
        RestResponse Token(string code, string state);
        RestResponse RefreshToken();
        string GetToken();
    }
}
