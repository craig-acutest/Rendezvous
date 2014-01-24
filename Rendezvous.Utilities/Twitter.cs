using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Rendezvous.Utilities
{
    public class Twitter
    {
        public string query = "Acutest";
        //    public string url = "https://api.twitter.com/1.1/users/search.json" ;
        public string url = "https://api.twitter.com/1.1/statuses/user_timeline.json";
 
        public List<Tweet> GetTwitterFeeds(int results){
            return findUserTwitter(url, query).Take(results).ToList();
        }

        private List<Tweet> findUserTwitter(string resource_url, string q)
        {
            // oauth application keys
            var oauth_token = ConfigurationManager.AppSettings["twitterOAuthToken"].ToString();
            var oauth_token_secret = ConfigurationManager.AppSettings["twitterOAuthSecret"].ToString();
            var oauth_consumer_key = ConfigurationManager.AppSettings["twitterConsumerKey"].ToString();
            var oauth_consumer_secret = ConfigurationManager.AppSettings["twitterConsumerSecret"].ToString();

            // oauth implementation details
            var oauth_version = "1.0";
            var oauth_signature_method = "HMAC-SHA1";

            // unique request details
            var oauth_nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            var timeSpan = DateTime.UtcNow
                - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();


            // create oauth signature
            var baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" +
                            "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&q={6}";

            var baseString = string.Format(baseFormat,
                                        oauth_consumer_key,
                                        oauth_nonce,
                                        oauth_signature_method,
                                        oauth_timestamp,
                                        oauth_token,
                                        oauth_version,
                                        Uri.EscapeDataString(q)
                                        );

            baseString = string.Concat("GET&", Uri.EscapeDataString(resource_url), "&", Uri.EscapeDataString(baseString));

            var compositeKey = string.Concat(Uri.EscapeDataString(oauth_consumer_secret),
                                    "&", Uri.EscapeDataString(oauth_token_secret));

            string oauth_signature;
            using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey)))
            {
                oauth_signature = Convert.ToBase64String(
                    hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(baseString)));
            }

            // create the request header
            var headerFormat = "OAuth oauth_nonce=\"{0}\", oauth_signature_method=\"{1}\", " +
                               "oauth_timestamp=\"{2}\", oauth_consumer_key=\"{3}\", " +
                               "oauth_token=\"{4}\", oauth_signature=\"{5}\", " +
                               "oauth_version=\"{6}\"";

            var authHeader = string.Format(headerFormat,
                                    Uri.EscapeDataString(oauth_nonce),
                                    Uri.EscapeDataString(oauth_signature_method),
                                    Uri.EscapeDataString(oauth_timestamp),
                                    Uri.EscapeDataString(oauth_consumer_key),
                                    Uri.EscapeDataString(oauth_token),
                                    Uri.EscapeDataString(oauth_signature),
                                    Uri.EscapeDataString(oauth_version)
                            );

            ServicePointManager.Expect100Continue = false;

            // make the request
            var postBody = "q=" + Uri.EscapeDataString(q);//
            resource_url += "?" + postBody;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resource_url);
            request.Headers.Add("Authorization", authHeader);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            var response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var objText = reader.ReadToEnd();
            List<Tweet> tweetList = new List<Tweet>();
            AcuRegex regex = new AcuRegex();
            try
            {
                JArray jsonDat = JArray.Parse(objText);
                for (int x = 0; x < jsonDat.Count(); x++)
                {
                    Tweet tweet = new Tweet();
                    tweet.ID = jsonDat[x]["id"].ToString();
                    tweet.Text = regex.convertHyperlink(jsonDat[x]["text"].ToString());
                    //tweet.Name = jsonDat[x]["name"].ToString();
                    tweet.Created = jsonDat[x]["created_at"].ToString().Substring(0, 10);

                    tweetList.Add(tweet);
                }
                return tweetList;
            }
            catch (Exception twit_error)
            {
                Tweet tweet = new Tweet();
                tweet.Text = twit_error.ToString();
                return tweetList;
            }
        }
    }
}
