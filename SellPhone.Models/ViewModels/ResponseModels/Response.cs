using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Models.ViewModels.ResponseModels
{
    public class Response<T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        #nullable enable
        public T? Data { get; set; }
    }

    public class Response
    {
        public string Status { get; set; }
        #nullable enable
        public Error? Error { get; set; }
    }
}
