using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoServiceWithSpringSecurity.Models;

namespace VideoServiceWithSpringSecurity.Tests
{
    public static class TestHelper
    {
        public static Video randomVideo()
        {
            // Information about the video
            String id = Guid.NewGuid().ToString();
            String title = "Video-" + id;
            String url = "http://coursera.org/some/video-" + id;

            Random random = new Random();
            int minutes = random.Next(0, 100) + 1;
            long duration = 60 * minutes * 1000; // random time up to 1hr

            return new Video(title, url, duration);
        }

    }
}
