using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MovieAppAPI.Helpers
{
    public static class HttpHelper
    {
        public static void AddCorsWithError(this HttpResponse httpResponse, string message)
        {
            httpResponse.Headers.Add("Application-Error", message);
            httpResponse.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            httpResponse.Headers.Add("Access-Control-Allow-Origin", "*");
         }

         public static void AddPaginationHeader(this HttpResponse httpResponse, int pageNumber, int pageSize, int totalPages, int totalItems)
         {
            var pagination = new PaginationHeaders(pageNumber, pageSize, totalPages, totalItems);
             httpResponse.Headers.Add("Pagination", JsonConvert.SerializeObject(pagination, new JsonSerializerSettings{
                 ContractResolver = new CamelCasePropertyNamesContractResolver()
             }));
             httpResponse.Headers.Add("Access-Control-Expose-Headers", "Pagination");
         }
    }
}