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
    public class TestController : ApiController
    {
        private IVideoRepository _repository;

        public TestController(IVideoRepository repository)
        {
            _repository = repository;
        }

        public List<Video> Get()
        {
            //Path will be: /api/test

            // For the default get we'll use DI to determine the repo used.
            var controller = new VideoController(_repository);
            var video = GenerateRandomVideo();

            controller.Post(video);
            controller.Post(video);

            // Post another unique video to make sure a collection is returned.
            controller.Post(GenerateRandomVideo());

            return controller.Get();
        }

        [HttpGet]
        public List<Video> Test(string scenario)
        {
            switch (scenario)
            {
                case "NoDuplicates": //Path will be: /api/test/test?scenario=NoDuplicates
                    return NoDuplicates();
                case "AllowDuplicates": // Path will be: /api/test/test?scenario=AllowDuplicates
                    return AllowDuplicates();
                default:
                    return Get();
            }
        }

        [HttpGet]
        public List<Video> Test(string scenario, string title)
        {
            switch (scenario)
            {
                case "Find":
                    return FindByName(title);
                default:
                    return Get();
            }
        }

        private List<Video> FindByName(string title)
        {
            var controller = new VideoController(_repository);
            var video = GenerateRandomVideo();
            video.name = title;
            controller.Post(video);
            controller.Post(video);

            // Post another unique video to make sure a collection is returned.
            controller.Post(GenerateRandomVideo());

            return controller.Find(title);
        }

        private List<Video> NoDuplicates()
        {
            //Path will be: /api/test/NoDuplicates

            // We'll use a NoDuplicatesVideoRepository implicitly here
            var controller = new VideoController(new NoDuplicatesVideoRepository());
            var video = GenerateRandomVideo();

            controller.Post(video);
            controller.Post(video);

            return controller.Get();
        }

        private List<Video> AllowDuplicates()
        {
            //Path will be: /api/test/AllowDuplicates

            // We'll use a AllowsDuplicatesVideoRepository implicitly here
            var controller = new VideoController(new AllowsDuplicatesVideoRepository());
            var video = GenerateRandomVideo();

            controller.Post(video);
            controller.Post(video);

            return controller.Get();
        }

        private Video GenerateRandomVideo()
        {
            // Information about the video
            String title = "Programming Cloud Services for Android Handheld Systems";
            String url = "http://coursera.org/some/video/" + Guid.NewGuid().ToString();
            long duration = 60 * 10 * 1000; // 10min in milliseconds
            return new Video(title, url, duration);
        }

    }
}
