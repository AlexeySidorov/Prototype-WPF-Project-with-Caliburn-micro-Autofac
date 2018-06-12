using Newtonsoft.Json;

namespace Task2.Infrastructure.Models
{
    public class TranslatableValue
    {
        public string Default { get; set; }

        [JsonProperty("RUS")]
        public string Russian { get; set; }

        [JsonProperty("ENG")]
        public string English { get; set; }
    }
}
