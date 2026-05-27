using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace MainClient.Ipc
{
    public sealed class BrowserRunResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public JToken? Data { get; set; }
    }
}
