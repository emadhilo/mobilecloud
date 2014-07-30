using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoServlet.Models;
using VideoServlet.Controllers;
using System.Net;
using System.Web.Http.Results;
using System.Collections.Generic;

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

            // Create a new video object to use for our test.
            var newVideo = new Video { Id = myRandomID, Name = title, Url = videoUrl, Duration = duration };

            // Create an instance of the video controller to run our tests against.
            var controller = new VideosController();

            // Start by posting the new video to the API controller.
            var response = controller.PostVideo(newVideo);

            // We're expecting a 201 Created response.  The APIController class has a CreatedAtRouteNegotiatedContentResult class to represent this.
            // Let's test first to see if the correct response type is returned.
            Assert.AreEqual(typeof(CreatedAtRouteNegotiatedContentResult<String>), response.GetType());

            // Convert the response to the result we're expecting
            var createdResult = response as CreatedAtRouteNegotiatedContentResult<String>;

            // The content type returned is a string as we're simply expecting "Video added." to be returned.
            Assert.AreEqual(VideosController.VIDEO_ADDED, createdResult.Content);

            // As a further check, we can look at the URI returned with the 201 which should indicate the location of the newly added video.
            // In this example, we've used the id of the video to use later if we want to "get" that specific video, so we can verify here that
            // that it is the random GUID we've used above for this video.
            Assert.AreEqual(myRandomID, createdResult.RouteValues["id"]);

            
            // With a new video added, we can retrieve the list of videos from the API controller.
            var videolistResponse = controller.GetVideos();

            // This time we're expecting an OK (200) response so let's test the type of response that is returned.
            Assert.AreEqual(typeof(OkNegotiatedContentResult<List<Video>>), videolistResponse.GetType());

            // And then we cast the response into the result type we're expecting for further analysis.
            var videolistResult = videolistResponse as OkNegotiatedContentResult<List<Video>>;

            // Finally we can make sure the list has the new video we posted by using a lambda expression
            // to find a video with the URL we specified.
            var videoFound = videolistResult.Content.Exists(v => v.Url.Equals(videoUrl));

            Assert.AreEqual(true, videoFound);
        }

        [TestMethod]
        public void TestMissingRequestParameter()
        {
            String myRandomID = Guid.NewGuid().ToString();
            String title = "";
            String videoUrl = "http://coursera.org/some/video-" + myRandomID;
            long duration = 60 * 10 * 1000; // 10min in milliseconds

            // Create a new video object to use for our test.
            var newVideo = new Video { Id = myRandomID, Name = title, Url = videoUrl, Duration = duration };

            // Create an instance of the video controller to run our tests against.
            var controller = new VideosController();

            // Start by posting the new video to the API controller.
            var response = controller.PostVideo(newVideo);

            // We're expecting a 400 Bad Request response.
            // Let's test to see if the correct response type is returned.
            Assert.AreEqual(typeof(BadRequestErrorMessageResult), response.GetType());

            // Convert the response to the result we're expecting
            var badRequestResult = response as BadRequestErrorMessageResult;

            Assert.AreEqual(VideosController.MISSING_DATA, badRequestResult.Message);
        }

    }
}
