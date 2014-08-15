using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using VideoServiceWithSpringSecurity.Models;

namespace VideoServiceWithSpringSecurity.Tests
{
    [TestClass]
    public class IntegrationTest
    {
        // Before running these tests:
        // 1. Check properties on the VideoServiceWithSpringSecurity project and make sure that SSEnabled = true
        // 2. Set the _httpsURL variable below to the appropriate url/port that's used (the SSL URL will be shown in the properties).
        // 3. Right click the VideoServiceWithSpringSecurity project and select View in Browser to start up the service.

        private string _httpsURL = "https://localhost:44300/";

        [TestMethod]
        public async Task TestRedirectToLoginWithoutAuth()
        {
            // Add this call back so it ignores the fact our ssl certificate is unsafe
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            using (var client = new HttpClient())
            {
                var video = TestHelper.randomVideo();

                client.BaseAddress = new Uri(_httpsURL);

                HttpResponseMessage response = await client.PostAsJsonAsync("api/Video", video);
                
                //Response should be Unauthorized (401) since we're not logged in.
                Assert.IsTrue(response.StatusCode.Equals(HttpStatusCode.Unauthorized));
            }
        }

        [TestMethod]
        public async Task TestDenyVideoAddWithoutLogin()
        {
            // Add this call back so it ignores the fact our ssl certificate is unsafe
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            using (var client = new HttpClient())
            {
                var video = TestHelper.randomVideo();

                client.BaseAddress = new Uri(_httpsURL);

                HttpResponseMessage response = await client.PostAsJsonAsync("api/Video", video);

                //Response should be Unauthorized (401) since we're not logged in.
                Assert.IsTrue(response.StatusCode.Equals(HttpStatusCode.Unauthorized));

                // Now, let's login and ensure that the Video wasn't added
                response = await client.PostAsJsonAsync("api/Auth/Login", new Login() { Username = "coursera", password = "changeit" });
                Assert.IsTrue(response.IsSuccessStatusCode);

                response = await client.GetAsync("api/Video");
                Assert.IsTrue(response.IsSuccessStatusCode);
                var videos = await response.Content.ReadAsAsync<List<Video>>();

                // We should NOT get back the video that we added above!
                var videoFound = videos.Exists(v => v.Equals(video));
                Assert.IsFalse(videoFound);
            }
        }

        [TestMethod]
        public async Task TestVideoAddAndList()
        {
            // Add this call back so it ignores the fact our ssl certificate is unsafe
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            using (var client = new HttpClient())
            {
                var video = TestHelper.randomVideo();

                client.BaseAddress = new Uri(_httpsURL);

                // Login first to get authenticated
                HttpResponseMessage response = await client.PostAsJsonAsync("api/Auth/Login", new Login() { Username = "coursera", password = "changeit" });
                Assert.IsTrue(response.IsSuccessStatusCode);

                // Post the video
                response = await client.PostAsJsonAsync("api/Video", video);

                Assert.IsTrue(response.IsSuccessStatusCode);

                response = await client.GetAsync("api/Video");
                Assert.IsTrue(response.IsSuccessStatusCode);
                var videos = await response.Content.ReadAsAsync<List<Video>>();

                // We should now get back the video that we added
                var videoFound = videos.Exists(v => v.Equals(video));
                Assert.IsTrue(videoFound);
            }
        }

        [TestMethod]
        public async Task TestLogout()
        {
            // Add this call back so it ignores the fact our ssl certificate is unsafe
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            using (var client = new HttpClient())
            {
                var video = TestHelper.randomVideo();

                client.BaseAddress = new Uri(_httpsURL);

                // Login first to get authenticated
                HttpResponseMessage response = await client.PostAsJsonAsync("api/Auth/Login", new Login() { Username = "coursera", password = "changeit" });
                Assert.IsTrue(response.IsSuccessStatusCode);

                // Post the video
                response = await client.PostAsJsonAsync("api/Video", video);

                Assert.IsTrue(response.IsSuccessStatusCode);

                response = await client.GetAsync("api/Auth/Logout");
                Assert.IsTrue(response.IsSuccessStatusCode);

                // The logout service should return either "Not logged in" or "Logged out successfully" depending on if the user was logged in.
                var message = await response.Content.ReadAsAsync<String>();
                Assert.IsTrue(message.Equals("Logged out successfully"));

                // Try to get the list of videos after logging out.
                response = await client.PostAsJsonAsync("api/Video", video);

                //Response should be Unauthorized (401) since we're not logged in anymore.
                Assert.IsTrue(response.StatusCode.Equals(HttpStatusCode.Unauthorized));
            }
        }

    }
}
