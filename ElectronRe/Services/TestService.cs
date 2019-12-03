using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Dto;
using Test.Services;
using Newtonsoft.Json;

namespace ElectronRe.Services
{
    public class TestService
    {

        private static List<DataDto> _dataList = new List<DataDto>() {
            new DataDto() { Id = "1", Title = "ngForLoaded 1"},
            new DataDto() { Id = "2", Title = "ngForLoaded 2"}
        };

        public void GetTestData(string data)
        {
            var success = true;
            string message = string.Empty;

            var result = new Response()
            {
                Success = success,
                Message = message,
                Data = _dataList
            };

            Console.WriteLine($"List JSON" + JsonConvert.SerializeObject(result));
            MessagingService.Send("TestSend", JsonConvert.SerializeObject(result));
        }

    }
}
