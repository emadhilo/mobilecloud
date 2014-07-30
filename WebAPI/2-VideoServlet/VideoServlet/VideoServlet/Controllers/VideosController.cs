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
        public const string MISSING_DATA = "Missing ['id', 'name','duration','url'].";

        public IHttpActionResult GetVideos()
        {
            return Ok(videos);
        }

        public IHttpActionResult PostVideo(Video video)
        {
            if (video.Id == "" || video.Name == "" || video.Url == "" || video.Duration < 0)
                return BadRequest(MISSING_DATA);

            videos.Add(video);

            return CreatedAtRoute("DefaultApi", new { id = video.Id }, VIDEO_ADDED);
        }

    }
}
