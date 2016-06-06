using nSpotify;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpotiBotiCore
{
    public class SpotifyCore
    {
        public string SearchSpotify(string Search)
        {
            Search = Search.Replace(" ", "%20");
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.spotify.com/v1/search?q=" + Search + "&type=Track&offset=0&limit=1");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "*/*";
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var test = JsonConvert.DeserializeObject(streamReader.ReadToEnd());
                JObject joResponse = JObject.Parse(test.ToString());
                JObject ojObject = (JObject)joResponse["tracks"];
                JArray array = (JArray)ojObject["items"];
                return array[0]["external_urls"]["spotify"].ToString();
            }
        }

        public SpotifyCore()
        {
            
        }

        public static void SpotifyStart() {
            Spotify.StartSpotifyWebHelper();
            Spotify.StartSpotify();
        }
    }
}
