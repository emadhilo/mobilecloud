using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoServlet.Models
{
    public class Video
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public long Duration { get; set; }
    }
}