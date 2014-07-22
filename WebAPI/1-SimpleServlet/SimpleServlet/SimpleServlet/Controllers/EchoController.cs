using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleServlet.Controllers
{
    public class EchoController : ApiController
    {
        public IHttpActionResult GetEcho(string msg)
        {
            return Ok("Echo:" + msg);
        }
    }
}
