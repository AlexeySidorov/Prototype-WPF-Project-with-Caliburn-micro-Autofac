using Newtonsoft.Json;

namespace Task2.Infrastructure.Models
{
    public class ErrorModel
    {
        public string Error { get; set; }

        public Meta Meta { get; set; }

        public string Code { get; set; }
    }

    public class Meta
    {
        [JsonProperty("id")]
        public DetailsError Details { get; set; }
    }

    public class DetailsError
    {
        public string Param { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}
