using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VideoControllerAndRetrofit.Models;

namespace VideoControllerAndRetrofit.Controllers
{
    public class VideoSvcController : ApiController
    {
        private List<Video> videos = new List<Video>();

        public bool Post(Video video)
        {
            videos.Add(video);

            return true;
        }

        public List<Video> Get()
        {
            return videos;
        }
    }
}