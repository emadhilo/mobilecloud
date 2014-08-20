using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VideoServiceWithOAuth2.Models;

namespace VideoServiceWithOAuth2.Tests
{
    public static class TestHelper
    {
        public static Video randomVideo()
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

        public static HttpContent FormEncodedCredentials(string username, string password, string clientId)
        {
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("username", username));
            postData.Add(new KeyValuePair<string, string>("password", password));
            postData.Add(new KeyValuePair<string, string>("client_id", clientId));
            postData.Add(new KeyValuePair<string, string>("grant_type", "password"));
            postData.Add(new KeyValuePair<string, string>("client_secret", ""));

            return new FormUrlEncodedContent(postData);
        }

        public static string EncodeCredential(string userName, string password)
        {
            Encoding encoding = Encoding.UTF8;
            string credential = String.Format("{0}:{1}", userName, password);

            return Convert.ToBase64String(encoding.GetBytes(credential));
        }
    }
}
