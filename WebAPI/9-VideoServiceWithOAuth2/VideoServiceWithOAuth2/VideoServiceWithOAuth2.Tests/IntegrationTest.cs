using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using VideoServiceWithOAuth2.Models;
using System.Text;
using Newtonsoft.Json.Linq;

namespace VideoServiceWithOAuth2.Tests
{
    [TestClass]
    public class IntegrationTest
    {
        // Before running these tests:
        // 1. Check properties on the VideoServiceWithSpringSecurity project and make sure that SSEnabled = true
        // 2. Set the _httpsURL variable below to the appropriate url/port that's used (the SSL URL will be shown in the properties).
        // 3. Right click the VideoServiceWithSpringSecurity project and select View in Browser to start up the service.

        private string _httpsURL = "https://localhost:44301/";

        [TestMethod]
        public async Task TestVideoAddAndList()
        {
            // Add this call back so it ignores the fact our ssl certificate is unsafe
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            using (var client = new HttpClient())
            {
                var video = TestHelper.randomVideo();

                client.BaseAddress = new Uri(_httpsURL);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", TestHelper.EncodeCredential("mobile", ""));

                HttpResponseMessage response = await client.PostAsync("api/token", TestHelper.FormEncodedCredentials("admin", "pass", "mobile"));

                var tokenResponse = response.Content.ReadAsStringAsync().Result;
                var json = JObject.Parse(tokenResponse);
                var token = json["access_token"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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
        public async Task TestAccessDeniedWithIncorrectCredentials()
        {
            // Add this call back so it ignores the fact our ssl certificate is unsafe
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            using (var client = new HttpClient())
            {
                var video = TestHelper.randomVideo();

                client.BaseAddress = new Uri(_httpsURL);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", TestHelper.EncodeCredential("mobile", ""));

                HttpResponseMessage response = await client.PostAsync("api/token", TestHelper.FormEncodedCredentials("admin", "wrongpassword", "mobile"));
                
                var tokenResponse = response.Content.ReadAsStringAsync().Result;
                var json = JObject.Parse(tokenResponse);
                IDictionary<string, JToken> jsondictionary = json;
                
                Assert.IsFalse(jsondictionary.ContainsKey("access_token"));

                // Try and access the videos just to be 100% sure we can't do it without a token.
                response = await client.GetAsync("api/Video");
                Assert.IsTrue(response.StatusCode.Equals(HttpStatusCode.Unauthorized));
            }
        }

        [TestMethod]
        public async Task TestReadOnlyClientAccess()
        {
            // Add this call back so it ignores the fact our ssl certificate is unsafe
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            using (var client = new HttpClient())
            {
                var video = TestHelper.randomVideo();

                client.BaseAddress = new Uri(_httpsURL);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", TestHelper.EncodeCredential("mobile", ""));

                // We'll use a regular user so they don't have access post new videos
                HttpResponseMessage response = await client.PostAsync("api/token", TestHelper.FormEncodedCredentials("user1", "pass", "mobile"));

                var tokenResponse = response.Content.ReadAsStringAsync().Result;
                var json = JObject.Parse(tokenResponse);
                var token = json["access_token"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                response = await client.GetAsync("api/Video");
                Assert.IsTrue(response.IsSuccessStatusCode);
                var videos = await response.Content.ReadAsAsync<List<Video>>();

                // We should now get back a list of videos
                var videosFound = videos.Count > 0;
                Assert.IsTrue(videosFound);

                // Post the video
                response = await client.PostAsJsonAsync("api/Video", video);

                // We should not get an OK 200 response as we're not authorised to post, only get.
                Assert.IsFalse(response.IsSuccessStatusCode);
            }
        }

    }
}
