using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoControllerWithJpa.Controllers;
using System.Collections.Generic;
using VideoControllerWithJpa.Models;
using System.Web.Http;

namespace VideoControllerWithJpa.Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestVideoAddAndListViaController()
        {
            // Using a simple list in memory for this unit test to ensure the controller is working.
            var controller = new VideoController(new Repository.InMemoryVideoRepository());

            var video = TestHelper.randomVideo();

            bool ok = controller.Post(video);
            Assert.IsTrue(ok);

            List<Video> videos = controller.Get();

            var videoFound = videos.Exists(v => v.Equals(video));
            Assert.IsTrue(videoFound);
        }

    }
}
