using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JsonParsing
{
    class ReadData
    {
        HtmlAgilityPack.HtmlNode node = null;
        HtmlAgilityPack.HtmlDocument doc = null;
        private StringBuilder DATAREADED()
        {
            StringBuilder SB = new StringBuilder();
            try
            {
                var fileStream = new FileStream("C:\\Users\\Syed Jibraan Ahmed\\Desktop\\JsonData.Txt",FileMode.Open,FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    SB.Append(streamReader.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return SB;
        }
        public HtmlAgilityPack.HtmlNode eventOfferJSON()
        {

            var data = DATAREADED().ToString();        
            data = data.Replace("\n","");
            doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(data);

            if (doc != null)
            {
                HtmlAgilityPack.HtmlNode node3 = doc.DocumentNode.SelectSingleNode("//div[@id='eventOfferJSON']");

                if (node3 != null)
                {
                    var offers = new List<string>();
                    try
                    {
                        offers = JsonConvert.DeserializeObject<List<string>>(node3.InnerText.Split(';')[0]);
                        node = node3;
                        return node;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                HtmlAgilityPack.HtmlNode node4 = doc.DocumentNode.SelectSingleNode("//div[@id='eventJSONData']");

                if (node4 != null)
                {
                    string subStr = node4.InnerText;
                    try
                    {
                        JObject obj = JObject.Parse(subStr);
                        node = node4;
                        return node;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return node;
        }

        public string GetAllDivs()
        {
            var data = DATAREADED().ToString();
            data = data.Replace("\n", "");
            return data;
        }
    }
}
