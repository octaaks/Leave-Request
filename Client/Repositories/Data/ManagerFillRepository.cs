using Client.Base.Urls;
using Leave_Request.Models;
using Leave_Request.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class ManagerFillRepository : GeneralRepository<ManagerFill, int>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor contextAccessor;

        public ManagerFillRepository(Address address, string request = "ManagerFills/") : base(address, request)
        {
            this.address = address;
            this.request = request;

            contextAccessor = new HttpContextAccessor();
            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }
        public HttpStatusCode UpdateManagerFill(ManagerFill entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(request, content).Result;
            return result.StatusCode;
        }

        public async Task<List<ManagerFill>> GetManagerFills()
        {
            List<ManagerFill> entities = new List<ManagerFill>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<ManagerFill>>(apiResponse);
            }
            return entities;
        }
    }
}
