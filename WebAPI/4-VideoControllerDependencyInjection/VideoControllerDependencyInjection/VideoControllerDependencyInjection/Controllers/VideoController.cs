using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VideoControllerDependencyInjection.Models;
using VideoControllerDependencyInjection.Repository;

namespace VideoControllerDependencyInjection.Controllers
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

        public List<Video> Get()
        {
            return _videos.getVideos();
        }

        [HttpGet]
        public List<Video> Find(string title)
        {
            return _videos.findByTitle(title);
        }

    }
}
