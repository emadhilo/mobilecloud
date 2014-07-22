using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VideoServlet.Models;

namespace VideoServlet.Controllers
{
    public class VideosController : ApiController
    {
        private List<Video> videos = new List<Video>();

        public const string VIDEO_ADDED = "Video added.";

        public IEnumerable<Video> GetVideos()
        {
            return videos;
        }

        public IHttpActionResult PostVideo(Video video)
        {
            videos.Add(video);

            return CreatedAtRoute("DefaultApi", new { id = video.Id }, video);
        }

    }
}
