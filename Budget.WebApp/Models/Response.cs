using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.WebApp.Models
{
    public class ServerResponse
    {
        public static ServerResponse Ok()
        {
            var r = new ServerResponse();
            return r;
            //new System.Web.Mvc.JsonResult();
            //return JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(r));
        }

        public static ServerResponse Ok(object result)
        {
            var r = new ServerResponse(result);
            return r;
            //return JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(r));
        }

        public static ServerResponse Error(string errorMessage)
        {
            var r = new ServerResponse(errorMessage);
            return r;
            //return JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(r));
        }

        private ServerResponse()
        {
            Success = true;
        }

        private ServerResponse(object result)
        {
            Result = result;
            Success = true;
        }

        private ServerResponse(string message)
        {
            ErrorMessage = message;
        }


        public object Result { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
    }
}