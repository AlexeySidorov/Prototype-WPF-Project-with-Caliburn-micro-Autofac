using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Task2.Infrastructure.Models;

namespace Task2.Infrastructure.Helpers
{
    public static class ErrorMessageHelper
    {
        public static void ErrorMessage(string json)
        {
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var error = JsonConvert.DeserializeObject<ErrorModel>(json, jsonSerializerSettings);
            MessageBox.Show(error.Error, "Error");
        }
    }
}
