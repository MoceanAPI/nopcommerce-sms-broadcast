namespace Mocean
{
    public class ApiRequestConfig
    {
        public string BaseUrl { get; set; } = "https://rest.moceanapi.com";
        public string Version { get; set; } = "2";

        public ApiRequestConfig() { }

        public ApiRequestConfig(string baseUrl, string version)
        {
            this.BaseUrl = baseUrl;
            this.Version = version;
        }

        public static ApiRequestConfig make()
        {
            return new ApiRequestConfig();
        }
    }
}
