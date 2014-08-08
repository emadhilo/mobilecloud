using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VideoControllerWithJpa.Models;
using VideoControllerWithJpa.Repository;

namespace VideoControllerWithJpa.Controllers
{
    public class VideoController : ApiController
    {
        private IVideoRepository _videos;

        public VideoController(IVideoRepository videos)
        {
            _videos = videos;
        }

        public bool Post(Video video)
        {
            return _videos.addVideo(video);
        }

        //Path will be: /api/video
        public List<Video> Get()
        {
            return _videos.getVideos();
        }

        //Path will be: /api/video/find?title=....
        [HttpGet]
        public List<Video> Find(string title)
        {
            return _videos.findByName(title);
        }

    }
}
