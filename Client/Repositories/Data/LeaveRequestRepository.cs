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
    public class LeaveRequestRepository : GeneralRepository<LeaveRequest, int>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor contextAccessor;

        public LeaveRequestRepository(Address address, string request = "LeaveRequests/") : base(address, request)
        {
            this.address = address;
            this.request = request;

            contextAccessor = new HttpContextAccessor();
            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public HttpStatusCode AddLeaveRequest(LeaveRequest entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(request + "AddLeaveRequest", content).Result;
            return result.StatusCode;
        }

        //public string AddLeaveRequest(LeaveRequest entity)
        //{
        //    StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        //    var result = httpClient.PostAsync(request + "AddLeaveRequest", content).Result.Content.ReadAsStringAsync().Result;
        //    return result;
        //}

        public async Task<List<LeaveRequestVM>> GetLR()
        {
            List<LeaveRequestVM> entities = new List<LeaveRequestVM>();

            using (var response = await httpClient.GetAsync(request + "GetLeaveRequests"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<LeaveRequestVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<List<LeaveRequestVM>> GetEmployeeLR(int id)
        {
            List<LeaveRequestVM> entities = new List<LeaveRequestVM>();

            using (var response = await httpClient.GetAsync(request + "GetEmployeeLeaveRequests/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<LeaveRequestVM>>(apiResponse);
            }
            return entities;
        }
    }
}
