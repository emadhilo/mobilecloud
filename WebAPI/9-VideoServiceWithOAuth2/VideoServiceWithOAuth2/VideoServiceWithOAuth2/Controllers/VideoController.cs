using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using VideoServiceWithOAuth2.Models;
using VideoServiceWithOAuth2.Repository;
using VideoServiceWithOAuth2.Filters;

namespace VideoServiceWithOAuth2.Controllers
{
    public class VideoController : ApiController
    {
        private IVideoRepository _videos;

        // Had some issues getting the repo injected when testing using self host, so
        // I've added this parameterless constructer to fudge our past it.
        public VideoController()
        {
            _videos = new LINQVideoRepository();
        }

        public VideoController(IVideoRepository videos)
        {
            _videos = videos;
        }

        [RequireHttps]
        [Authorize(Roles = "admin")]
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
