using Client.Base.Urls;
using Leave_Request.Models;
using Leave_Request.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class ReligionRepository : GeneralRepository<Religion, int>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;

        public ReligionRepository(Address address, string request = "Religions/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            //this.httpClient = new HttpClient
            //{
            //    BaseAddress = new Uri(address.link)
            //};
        }
    }
}
