using System.Collections.Generic;
using System.Net.Http;
using DateRangeAccounting.API.Models;
using Newtonsoft.Json;

namespace ConsoleTestApp.Services
{
    public class DateRangeService
    {
        private readonly string _appPath;
        private readonly HttpClientService _service;

        public DateRangeService(string appPath, HttpClientService service)
        {
            _appPath = appPath;
            _service = service;
        }

        public IEnumerable<DateRangeViewModel> Find(DateRangeViewModel dateRangeViewModel)
        {
            using (var client = _service.CreateClient())
            {
                var response = client.PostAsJsonAsync(_appPath + "/api/date/find" , dateRangeViewModel).Result;
                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<IEnumerable<DateRangeViewModel>>(json);
            }
        }

        public string Add(string token, DateRangeViewModel range)
        {
            using (var client = _service.CreateClient(token))
            {
                var response = client.PostAsJsonAsync(_appPath + "/api/date", range).Result;
                return response.StatusCode.ToString();
            }
        }
    }
}
