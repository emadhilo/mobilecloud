using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoControllerAndRetrofit.Models;
using VideoControllerAndRetrofit.Controllers;
using System.Net;
using System.Collections.Generic;

namespace VideoControllerAndRetrofit.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void testVideoAddAndList()
        {
            // Information about the video
            String title = "Programming Cloud Services for Android Handheld Systems";
            String url = "http://coursera.org/some/video";
            long duration = 60 * 10 * 1000; // 10min in milliseconds
            Video video = new Video(title, url, duration);

            var controller = new VideoSvcController();

            bool ok = controller.Post(video);
            Assert.IsTrue(ok);

            List<Video> videos = controller.Get();

            var videoFound = videos.Exists(v => v.Equals(video));
            Assert.IsTrue(videoFound);
        }
    }
}
