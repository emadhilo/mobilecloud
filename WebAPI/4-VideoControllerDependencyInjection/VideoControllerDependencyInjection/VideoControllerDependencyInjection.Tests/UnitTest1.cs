using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoControllerDependencyInjection.Models;
using VideoControllerDependencyInjection.Controllers;
using System.Collections.Generic;

namespace VideoControllerDependencyInjection.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestVideoAddAndList()
        {
            var controller = new VideoController(new Repository.AllowsDuplicatesVideoRepository());

            var video = randomVideo();

            bool ok = controller.Post(video);
            Assert.IsTrue(ok);

            List<Video> videos = controller.Get();

            var videoFound = videos.Exists(v => v.Equals(video));
            Assert.IsTrue(videoFound);
        }

        private Video randomVideo()
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
