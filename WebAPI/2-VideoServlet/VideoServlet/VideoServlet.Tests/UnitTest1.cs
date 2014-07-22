using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoServlet.Models;
using VideoServlet.Controllers;
using System.Net;
using System.Web.Http.Results;

namespace VideoServlet.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAddVideo()
        {
            String myRandomID = Guid.NewGuid().ToString();
            String title = "Video - " + myRandomID;
            String videoUrl = "http://coursera.org/some/video-" + myRandomID;
            long duration = 60 * 10 * 1000; // 10min in milliseconds

            var newVideo = new Video { Id = myRandomID, Name = title, Url = videoUrl, Duration = duration };

            var controller = new VideosController();
            var response = controller.PostVideo(newVideo);
            var createdResult = response as CreatedAtRouteNegotiatedContentResult<Video>;

            Assert.AreEqual(myRandomID, createdResult.RouteValues["id"]);
        }
    }
}
