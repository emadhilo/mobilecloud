using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using VideoControllerWithJpa.Models;

namespace VideoControllerWithJpa.Tests
{
    [TestClass]
    public class IntegrationTest
    {
        // Before running this test:
        // 1. Right click the VideoControllerWithJpa project and select View in Browser
        // 2. Set _baseURL to the appropriate url/port that's used
        private string _baseURL = "http://localhost:47251/";

        [TestMethod]
        public async Task TestVideoAddAndListViaHttpClient()
        {
            using (var client = new HttpClient())
            {
                var video = TestHelper.randomVideo();

                client.BaseAddress = new Uri(_baseURL);
                
                HttpResponseMessage response = await client.PostAsJsonAsync("api/Video", video); 
                Assert.IsTrue(response.IsSuccessStatusCode);

                bool ok = await response.Content.ReadAsAsync<bool>();
                Assert.IsTrue(ok);

                response = await client.GetAsync("api/Video");
                Assert.IsTrue(response.IsSuccessStatusCode);
                var videos = await response.Content.ReadAsAsync<List<Video>>();

                var videoFound = videos.Exists(v => v.Equals(video));
                Assert.IsTrue(videoFound);
            }
        }

    }
}
