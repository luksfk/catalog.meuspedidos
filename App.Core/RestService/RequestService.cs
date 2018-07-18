using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace App.Core.RestService
{
    public class RequestService<T> : IRequestService<T>
    {
        public async Task<List<T>> GetAll(string url)
        {
            var listEntities = new List<T>();

            var client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                foreach (var entity in JsonConvert.DeserializeObject<List<T>>(content))
                {
                    listEntities.Add(entity);
                }
            }

            return listEntities;
        }
    }
}