using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using Task2.Infrastructure.Models;

namespace Task2.Infrastructure.Rest
{
    public interface IRestMetodsRequest
    {
        [Get("/public/catalog")]
        Task<IList<Catalog>> GetCatalogs();

        [Get("/public/catalog?filter={field}(\"{search}\",iregex)")]
        Task<IList<Catalog>> SearchCatalogs([AliasAs("field")] string field, [AliasAs("search")] string searchLine);
    }
}
