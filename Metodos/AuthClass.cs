namespace MicrosoftOutlook.Metodos
{
    using MicrosoftOutlook.Interface;
    using MicrosoftOutlook.Models;
    using RestSharp;
    using System.Text.Json;

    public class AuthClass : IAuthClass
    {
        private readonly IConfiguration Configuration;
        private AuthToken AuthToken { get; set; }

        public AuthClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string Login()
        {
            string response = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize? " +
                              "client_id =ffe67733-9e5f-4c8c-b5b2-39d5b880b764" +
                              "& response_type = code" +
                              "& response_mode = query" +
                              "& scope =Calendars.ReadWrite offline_access User.Read" +
                              "& state = TestEvent" +
                              "&redirect_uri =https://localhost:7071/Api/Auth/Token";
            return response;
        }

        public RestResponse Token(string code, string state)
        {
            var response = new RestResponse();

            if (!string.IsNullOrEmpty(code))
            {
                var client = new RestClient("https://login.microsoftonline.com/common/oauth2/v2.0/token");
                var request = new RestRequest();

                request.AddParameter("client_id", "ffe67733-9e5f-4c8c-b5b2-39d5b880b764");
                request.AddParameter("scope", "Calendars.ReadWrite offline_access User.Read");
                request.AddParameter("code", code);
                request.AddParameter("redirect_uri", "https://localhost:7071/Api/Auth/Token");
                request.AddParameter("grant_type", "authorization_code");
                request.AddParameter("client_secret", "xLu8Q~LvznkPE3LKHN_oWiblmxIFSJJ5iBOTJafu");

                response = client.Execute(request, Method.Post);
                AuthToken = JsonSerializer.Deserialize<AuthToken>(response.Content);
            }
            return response;
        }

        public RestResponse RefreshToken()
        {
            var client = new RestClient("https://login.microsoftonline.com/common/oauth2/v2.0/token");
            var request = new RestRequest();

            request.AddParameter("client_id", "ffe67733-9e5f-4c8c-b5b2-39d5b880b764");
            request.AddParameter("scope", "Calendars.ReadWrite offline_access User.Read");
            request.AddParameter("refresh_token", AuthToken.refresh_token);
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("client_secret", "xLu8Q~LvznkPE3LKHN_oWiblmxIFSJJ5iBOTJafu");

            var response = client.Execute(request, Method.Post);
            AuthToken = JsonSerializer.Deserialize<AuthToken>(response.Content);

            return response;
        }

        public string GetToken()
        {
            return AuthToken.access_token;
        }
    }
}
