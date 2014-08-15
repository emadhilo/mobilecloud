using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VideoServiceWithSpringSecurity.Models;
using VideoServiceWithSpringSecurity.Repository;

namespace VideoServiceWithSpringSecurity.Controllers
{
    public class TestController : ApiController
    {
        private IVideoRepository _repository;

        public TestController(IVideoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public void TestLogin()
        {
            var controller = new AuthController();
            var resp = controller.Login(new Login() { Username = "student", password = "changeit" });
        }

        [HttpGet]
        public List<Video> PostAndReturnCollection()
        {
            //Path will be: /api/test

            var controller = new VideoController(_repository);

            controller.Post(GenerateRandomVideo());

            return controller.Get();
        }

        private Video GenerateRandomVideo()
        {
            // Information about the video
            var newGUID = Guid.NewGuid().ToString();
            String title = "Video - " + newGUID;
            String url = "http://coursera.org/some/video/" + newGUID;
            
            Random random = new Random();
            int minutes = random.Next(0, 100) + 1;
            long duration = 60 * minutes * 1000; // random time up to 1hr

            return new Video(title, url, duration);
        }

    }
}
