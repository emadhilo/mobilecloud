using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using VideoServiceWithSpringSecurity.Models;
using VideoServiceWithSpringSecurity.Repository;
using VideoServiceWithSpringSecurity.Filters;

namespace VideoServiceWithSpringSecurity.Controllers
{
    public class VideoController : ApiController
    {
        private IVideoRepository _videos;

        public VideoController(IVideoRepository videos)
        {
            _videos = videos;
        }

        [RequireHttps]
        [Authorize(Roles = "user")]
        public bool Post(Video video)
        {
            return _videos.addVideo(video);
        }

        //Path will be: /api/video
        [RequireHttps]
        [Authorize(Roles = "user")]
        public List<Video> Get()
        {
            return _videos.getVideos();
        }

        //Path will be: /api/video/FindByName?title=....
        [RequireHttps]
        [HttpGet]
        [Authorize(Roles="user")]
        public List<Video> FindByName(string title)
        {
            return _videos.findByName(title);
        }

        //Path will be: /api/video/FindByDurationLessThan?duration=....
        [RequireHttps]
        [HttpGet]
        [Authorize(Roles = "user")]
        public List<Video> FindByDurationLessThan(long duration)
        {
            return _videos.findByDurationLessThan(duration);
        }
    }
}
