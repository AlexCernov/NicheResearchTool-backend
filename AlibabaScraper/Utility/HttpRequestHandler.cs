using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AlibabaScraper.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AlibabaScraper.Utility
{
    public static class HttpRequestHandler
    {

        public static (List<OfferList>,SnData) GetOffers(string url, int entries)
        {
            var resultPages = (decimal)entries / 50;
            int pages = entries < 50 ? 1 : decimal.ToInt32(Math.Ceiling(resultPages));
            List<OfferList> returnList = new List<OfferList>();
            JsonSerializer serializer = new JsonSerializer();
            var jsonFilter = JObject.Parse(GetJson(url));
            var pageFilterData = serializer.Deserialize<AlibabaPageGeneratorJson>(jsonFilter.CreateReader());
            var filterData = pageFilterData.props.snData;
            int page = 1;
            while (pages != 0)
            {
                
                string attachPageNumber = page > 1 ? "&page=" + page : "";
                string requestUrl = url + attachPageNumber;
                
            
            while (true)
            {
                var jsonParsed = JObject.Parse(GetJson(requestUrl));
                var toDeserialize = serializer.Deserialize<AlibabaPageGeneratorJson>(jsonParsed.CreateReader());
                returnList.AddRange(toDeserialize.props.offerResultData.offerList);
                if (toDeserialize.props.offerResultData.firstScreen == false || toDeserialize.props.offerResultData.asyncRequestUrl == null) break;
                requestUrl = "https://www.alibaba.com/trade/search?" + toDeserialize.props.offerResultData.asyncRequestUrl;
            }
            pages--;
            page++;
            }
            return (returnList, filterData);
        }

        public static string GetJson(string url)
        {
            
            string json = DownloadPageAsync(url);
            int startPos = json.IndexOf("window.__page__data__config =") + "window.__page__data__config =".Length + 1;
            int length = json.IndexOf("window.__page__data = window.__page__data__config.props") - startPos;
            return json.Substring(startPos, length);
        }

        public static string DownloadPageAsync(string url)
        {
            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage(new HttpMethod("GET"), url);
            request.Headers.TryAddWithoutValidation("authority", "www.alibaba.com");
            request.Headers.TryAddWithoutValidation("accept", "application/json, text/javascript, */*; q=0.01");
            request.Headers.TryAddWithoutValidation("sec-fetch-dest", "empty");
            request.Headers.TryAddWithoutValidation("x-requested-with", "XMLHttpRequest");
            request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.87 Safari/537.36");
            request.Headers.TryAddWithoutValidation("sec-fetch-site", "same-origin");
            request.Headers.TryAddWithoutValidation("sec-fetch-mode", "cors");
            request.Headers.TryAddWithoutValidation("accept-language", "en-US,en;q=0.9");
            request.Headers.TryAddWithoutValidation("cookie", "ali_apache_id=11.227.117.44.1581375319584.170559.7; XSRF-TOKEN=4635d444-2053-4c34-be2b-4bcf061b8060; cna=Wc3IFv+YtFACAbwaTksHTf/I; xman_us_f=x_l=0; acs_usuc_t=acs_rt=bbad494eb2bb4efdbc2742d30be91d9a; cookie2=u8873dbb40b321cc204a2f81d91cc800; t=e8fd580fb68dd284c08a027bcc8c35f9; _tb_token_=eeb7e387b74e8; _bl_uid=5akqb6CbhLO2Ot1hddXv749mhpkt; JSESSIONID=A3AA7081FEBD973E7014306764BC61C5; ali_apache_track=\"\"; ali_apache_tracktmp=\"\"; xman_t=0sZojcKhfQR11nwUFzKgtWLSrSB7NZYcrmX5HQ/hJiCLWE1XrBRon8bqPngDNssa4zH0WRQDuM1NynlGeCqRaF2omksG2E8tCaf1TBdZPnQ=; xman_f=asESgDUIp/1v7iAKkdJ23sjikJ3R3lBWgaCthJE7+lxnc5+6AXRQ5E9iJxgTEjukwTxGrr0WlNWVkNvBro0zlHAgpo2CO73OPEyhftmtSWFk8BxdMhS5hA==; _m_h5_tk=906b1dac5975d46580a6a410f7cc7b94_1581383966684; _m_h5_tk_enc=2830eb6d0694e9eba9f82115308b0b9d; acs_rt=188.26.78.75.1581375329489.4; l=cBrFC_WeQbh4drYwBOCwourza77OSIRAguPzaNbMi_5Cp1L1mj_OoS6eqep6VAWdt9YB46VFKQe9-etuivxkktSmfVgl.; isg=BA8PVwwAjUcy0om7zhcx-bJTnqMZNGNWD0kb-SEcq36E8C_yKQTzpg1m8jjOiDvO");

            var response =  httpClient.SendAsync(request).Result;
            var returnString =  response.Content.ReadAsStringAsync().Result;
            return returnString;

        }
    }
}
