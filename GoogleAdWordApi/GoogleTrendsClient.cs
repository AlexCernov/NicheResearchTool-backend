using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using GoogleTrendsApiClient.Models;
using Newtonsoft.Json;

namespace GoogleTrendsApiClient
{
    public class GoogleTrendsClient
    {
        private readonly string locale;
        private readonly int tz;
        private CookieCollection cookies;
        public KeyValuePair<string, string> interestOverTimePayload;
        public KeyValuePair<string, string> relatedTopicsPayload;
        public KeyValuePair<string, string> relatedQueriesPayload;
        private List<string> keywordList;

        public GoogleTrendsClient(List<string> kwList,string locale = "en-US", int tz = 360, string geo = "")
        {
            this.tz = tz;
            this.locale = locale;
            keywordList = kwList;
            cookies = GetCookies();
            getWidgets();
        }

        private void getWidgets()
        {
            Tuple<string, string> tempInterest; 
            var request = CreateRequest($"https://trends.google.com/trends/api/explore?hl={locale}&tz=-180&req=" + GenerateTokenRequest(keywordList,"US","today 12-m"));
            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK) throw new Exception(response.StatusDescription);
            var res = StreamToString(response.GetResponseStream()).Remove(0, 4).Replace("\n","");
            var widgetResponse = JsonConvert.DeserializeObject<dynamic>(res);
            var widgetList = widgetResponse["widgets"];
            foreach (var widget in widgetList)
            {
                if (widget.id == "RELATED_TOPICS")
                {
                    relatedTopicsPayload = new KeyValuePair<string, string>(widget.request.ToString().Trim(), widget.token.ToString());
                }
                if (widget.id == "RELATED_QUERIES")
                {
                    relatedQueriesPayload = new KeyValuePair<string, string>(widget.request.ToString().Trim(), widget.token.ToString());
                }
                if (widget.id == "TIMESERIES")
                {
                    interestOverTimePayload = new KeyValuePair<string, string>(widget.request.ToString().Trim(), widget.token.ToString());

                }
            }
        }

        public static string StreamToString(Stream stream)
        {
            using var reader = new StreamReader(stream, Encoding.UTF8);
            return reader.ReadToEnd();
        }

        public RelatedTopicsDefault GetRelatedTopics()
        {
            var request =
                CreateRequest(
                    $"https://trends.google.com/trends/api/widgetdata/relatedsearches?hl={locale}&tz={tz}&req={relatedTopicsPayload.Key}&token={relatedTopicsPayload.Value}");
            request.CookieContainer.Add(cookies);
            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK) throw new Exception(response.StatusDescription);

            var widgetResponse =
                JsonConvert.DeserializeObject<RelatedTopicsResponse>(StreamToString(response.GetResponseStream())
                    .Remove(0, 5));
            return widgetResponse.@default;
        }

        public RelatedQueriesDefault GetRelatedQueries()
        {
            var request =
                CreateRequest(
                    $"https://trends.google.com/trends/api/widgetdata/relatedsearches?hl={locale}&tz={tz}&req={relatedQueriesPayload.Key}&token={relatedQueriesPayload.Value}");
            request.CookieContainer.Add(cookies);
            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK) throw new Exception(response.StatusDescription);

            var widgetResponse =
                JsonConvert.DeserializeObject<RelatedQueriesResponse>(StreamToString(response.GetResponseStream())
                    .Remove(0, 5));
            return widgetResponse.@default;
        }

        public InterestOverTimeDefault GetInterestsOverTime()
        {
            var request =
                CreateRequest(
                    $"https://trends.google.com/trends/api/widgetdata/multiline?hl={locale}&tz={tz}&req={interestOverTimePayload.Key}&token={interestOverTimePayload.Value}");
            request.CookieContainer.Add(cookies);
            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK) throw new Exception(response.StatusDescription);

            var widgetResponse =
                JsonConvert.DeserializeObject<InterestOverTimeResponse>(StreamToString(response.GetResponseStream())
                    .Remove(0, 5));
            return widgetResponse.@default;
        }

        private CookieCollection GetCookies()
        {
            var request = (HttpWebRequest) WebRequest.Create("https://trends.google.com/trends/api/explore/pickers/geo?hl=en-US&tz=-180");
            request.Method = "GET";
            request.UserAgent =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36 Edg/80.0.361.69";
            request.KeepAlive = true;
            request.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
            request.CookieContainer = new CookieContainer();
            var response = (HttpWebResponse) request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK) throw new Exception(response.StatusDescription);
            return response.Cookies;
        }

        private HttpWebRequest CreateRequest(string url)
        {
            var request = WebRequest.CreateHttp(url);
            //    request.Proxy = new WebProxy(new Uri(proxy), false);
            request.Method = "GET";
            request.UserAgent =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36 Edg/80.0.361.69";
            request.KeepAlive = true;
            request.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
            request.Headers.Add("Origin", "https://trends.google.com/trends/");
            request.Headers.Add("accept-language", "en-US");
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(cookies);

            return request;
        }

        public string GenerateTokenRequest(List<string> kwdList,string geo, string timeframe)
        {
            var keywords = new List<ComparisonItemToken>();
            kwdList.ForEach(x =>
            {
                var keyword = new ComparisonItemToken();
                keyword.keyword = x;
                keyword.geo = geo;
                keyword.time = timeframe;
                keywords.Add(keyword);
            });
            var tokenRequest = new TokenRequest { category = 0, comparisonItem = keywords, property = "" };
            return JsonConvert.SerializeObject(tokenRequest);
        }
    }
}