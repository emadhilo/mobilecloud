using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VideoControllerAndRetrofit.Models;

namespace VideoControllerAndRetrofit.Controllers
{
    public class TestController : ApiController
    {
        public List<Video> Get()
        {
            // Information about the video
            String title = "Programming Cloud Services for Android Handheld Systems";
            String url = "http://coursera.org/some/video/" + Guid.NewGuid().ToString();
            long duration = 60 * 10 * 1000; // 10min in milliseconds
            Video video = new Video(title, url, duration);

            var controller = new VideoSvcController();

            controller.Post(video);

            return controller.Get();
        }
    }
}
