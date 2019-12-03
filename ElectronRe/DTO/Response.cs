using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Dto
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<DataDto> Data { get; set; }
    }
}