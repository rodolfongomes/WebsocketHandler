using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebsocketHandler.Demo.Api.Result
{
    public class ReturnResult
    {
        public string message { get; set; }
        public DateTime date { get; set; }
        public int expiration { get; set; }
    }
}