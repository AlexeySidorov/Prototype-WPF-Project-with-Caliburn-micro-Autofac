using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;

namespace Task2.Infrastructure.Rest
{
    public interface IRestApi
    {
        IRestMetodsRequest Request();
    }

    public class RestApi : IRestApi
    {
        public IRestMetodsRequest Request()
        {
            var settings = new RefitSettings()
            {
                JsonSerializerSettings = new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            };

            var httpClient = new HttpClient(new HttpLoggingHandler())
            {
                BaseAddress = new Uri(Context.Current.BaseApiUrl),
                Timeout = new TimeSpan(0, 0, 10)
            };
            return RestService.For<IRestMetodsRequest>(httpClient, settings);
        }
    }
}
