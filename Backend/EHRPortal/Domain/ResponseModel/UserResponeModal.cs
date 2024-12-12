using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResponseModel
{
    public class UserResponeModal
    {
        public int StatusCode { get; set; }
        public string message { get; set; }
        public object Data { get; set; }
        public object token   { get; set; }
        public UserResponeModal(int statusCode, string message, object data, object token)
        {
            this.StatusCode = statusCode;
            this.message = message;
            this.Data = data;
            this.token = token;
        }
    }
}
