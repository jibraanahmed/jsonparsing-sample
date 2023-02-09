using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParsing
{
    class ParseData
    {
        ReadData RData = null;
        JsonDataModel section = new JsonDataModel();
        JtokenTicketTypes obj = null;
        public void ParsingData()
        {
            RData = new ReadData();
            string data = RData.eventOfferJSON().InnerText;      
            JObject JSON = JObject.Parse(data);
            if (JSON != null)
            {
                if (((JValue)(JSON["id"])).Value != null)
                {
                    section.ID = ((JValue)(JSON["id"])).Value.ToString();
                }
                if (((JValue)(JSON["name"])).Value != null)
                {
                    section.Name = ((JValue)(JSON["name"])).Value.ToString().Replace(' ','_');
                }
                if (((JValue)(JSON["venue"]["id"])).Value != null)
                {
                    section.VenueID = ((JValue)(JSON["venue"]["id"])).Value.ToString();
                }
                if (JSON["seoEventDate"] != null)
                {
                    section.EventDate = ((JValue)JSON["seoEventDate"]).Value.ToString().Split('-')[0].Trim();
                }
                if (JSON["venue"]["name"] != null)
                {
                    section.VenueName = ((JValue)JSON["venue"]["name"]).Value.ToString();
                }
                if (JSON["id"] != null)
                {
                    section.OfferID = ((JValue)JSON["id"]).Value.ToString();
                }
                if (JSON["name"] != null)
                {
                    section.OfferName = ((JValue)JSON["name"]).Value.ToString();
                }
                try
                {
                    if (JSON["venue"]["location"]["address"]["city"] != null)
                    {
                        section.EventLocation = ((JValue)JSON["venue"]["location"]["address"]["city"]).Value.ToString(); 
                        section.EventLocation = section.EventLocation + ", " + ((JValue)JSON["venue"]["location"]["address"]["region"]["abbrev"]).Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (JSON["quantities"] != null)
                {
                    section.QuantitiesMINIMUM = int.Parse(((JValue)JSON["quantities"]["minimum"]).Value.ToString());
                    section.QuantitiesLIMIT = int.Parse(((JValue)JSON["quantities"]["limit"]).Value.ToString());
                    section.Quantitiesexact = int.Parse(((JValue)JSON["quantities"]["exact"]).Value.ToString());
                    section.Quantitiesmultiple = int.Parse(((JValue)JSON["quantities"]["multiple"]).Value.ToString());
                }
                if (JSON["areaGroups"] != null)
                {
                    IEnumerable<JToken> Filtered = JSON["areaGroups"].Children();
                    if (Filtered != null)
                    {
                        try
                        {
                            foreach (var item1 in Filtered)
                            {
                                if (((JValue)item1["label"]).Value.ToString().Equals("Section"))
                                {
                                    foreach (var item2 in item1["areas"])
                                    {
                                        Console.WriteLine("SectionName : " + ((JValue)item2["locationSecname"]).Value);
                                        Console.WriteLine("Description : " + ((JValue)item2["description"]).Value);
                                        JArray pLevels1 = (JArray)item2["priceLevelIds"];
                                        if (pLevels1 != null)
                                        {
                                            Console.WriteLine();
                                            foreach (var plevel in pLevels1)
                                            {
                                                try
                                                {
                                                    Console.Write("PriceLevelId = " + plevel + " ");
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }
                                            }
                                            Console.WriteLine();
                                        }
                                        else if (((JValue)item1["label"]).Value.ToString().Equals("Additional"))
                                        {
                                            foreach (var item3 in item1["areas"])
                                            {
                                                Console.WriteLine("SectionName : " + ((JValue)item3["locationSecname"]).Value);
                                                Console.WriteLine("Description : " + ((JValue)item3["description"]).Value);
                                                JArray pLevels2 = (JArray)item2["priceLevelIds"];
                                                if (pLevels2 != null)
                                                {
                                                    Console.WriteLine();
                                                    foreach (var plevel in pLevels2)
                                                    {
                                                        try
                                                        {
                                                            Console.Write("PriceLevelId = " + plevel + " ");
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Console.WriteLine(ex.Message);
                                                        }
                                                    }
                                                    Console.WriteLine();
                                                }
                                            }
                                        }
                                        else if (((JValue)item1["label"]).Value.ToString().Equals("Location"))
                                        {
                                            foreach (var item4 in item1["areas"])
                                            {
                                                Console.WriteLine("SectionName : " + ((JValue)item4["locationSecname"]).Value);
                                                Console.WriteLine("Description : " + ((JValue)item4["description"]).Value);
                                                JArray pLevels3 = (JArray)item2["priceLevelIds"];
                                                if (pLevels3 != null)
                                                {
                                                    Console.WriteLine();
                                                    foreach (var plevel in pLevels3)
                                                    {
                                                        try
                                                        {
                                                            Console.Write("PriceLevelId = " + plevel + " ");
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Console.WriteLine(ex.Message);
                                                        }
                                                    }
                                                    Console.WriteLine();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }                           
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }
       
        public void ParseDivs()
        {
            RData = new ReadData();
            var data = RData.GetAllDivs();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(data);
            HtmlAgilityPack.HtmlNode ismds = doc.DocumentNode.SelectSingleNode("//div[@id='ismds']");
            if (ismds != null)
            {
                JObject JSON = JObject.Parse(ismds.InnerText);
                if (JSON.Property("apiKey") != null)
                {
                    Console.WriteLine(JSON.Property("apiKey"));
                }
                if (JSON.Property("apiSecret") != null)
                {
                    Console.WriteLine(JSON.Property("apiSecret"));
                }
                if (JSON.Property("pricingUrl") != null)
                {
                    Console.WriteLine(JSON.Property("pricingUrl"));
                }
            }
            String[] ISMDSDATA = ismds.InnerText.Split(',');
            foreach (var item in ISMDSDATA)
            {
                if (item.Contains("apiKey:"))
                {
                    Console.WriteLine(item.Split(':')[1].Replace("'",String.Empty).Trim());
                }
                else if(item.Contains("apiSecret:"))
                {
                    Console.WriteLine(item.Split(':')[1].Replace("'", String.Empty).Trim());
                }
                else if(item.Contains("url:"))
                {
                    Console.WriteLine(item.Replace("url:",String.Empty).Replace("'",String.Empty).Replace("}",String.Empty).Trim());
                }
            }
        }
        private void PlayWithTicketDetails()
        {
            RData = new ReadData();
            obj = new JtokenTicketTypes();
            string data = RData.eventOfferJSON().InnerText;
            JObject JSON = JObject.Parse(data);
            IEnumerable<JToken> ticketsDetail = JSON["tickets"].Children();
            if (ticketsDetail != null)
            {
                foreach (var item in ticketsDetail)
                {
                    JToken modifieditem = item;
                    obj.price_breakdown = item.SelectToken("prices",false);
                    obj.description = item.SelectToken("description",false);
                    obj.v = item.SelectToken("id",false);
                    obj.quantity_limits = item.SelectToken("quantities",false);
                    obj.parent_ext_ticket_type = item.SelectToken("parent_ext_ticket_types",false);
                    obj.isProtected = item.SelectToken("isProtected");
                    if (obj.quantity_limits != null)
                    {
                        JToken minQty,maxQty,stepQty;
                        minQty = obj.quantity_limits.SelectToken("minimum");
                        Console.WriteLine("minQty : " + minQty);
                        maxQty = obj.quantity_limits.SelectToken("limit");
                        Console.WriteLine("maxQty : " + maxQty);
                        stepQty = obj.quantity_limits.SelectToken("multiple");
                        Console.WriteLine("stepQty : " + stepQty);
                    }
                    obj.password = item.Children().First().SelectToken("password",false);
                    if (obj.password != null)
                    {
                        JToken tokens,form_name, prompt;
                        tokens = obj.password.SelectToken("tokens",false);
                        if (tokens != null && tokens.First != null)
                        {
                            form_name = tokens.First.SelectToken("form_name", false);
                            Console.WriteLine(form_name);
                            prompt = tokens.First.SelectToken("prompt",false);
                            Console.WriteLine(prompt);
                        }
                    }
                }
                IEnumerable<JToken> priceLevels = obj.price_breakdown.Children();
                if (priceLevels != null)
                {
                    foreach (var item in priceLevels)
                    {
                        JToken price_secname = item.SelectToken("priceLevelSecname",false);
                        Console.WriteLine("price_secname " + price_secname);
                        if (price_secname != null)
                        {
                            String strPrice_secname = ((JValue)price_secname).Value.ToString();
                            Console.WriteLine("strPrice_secname " + strPrice_secname);
                            JToken plFiltered = JSON["secnames"].FirstOrDefault(pred => ((JValue)pred["secname"]).Value.Equals(strPrice_secname));
                            Console.WriteLine("plFiltered : " + priceLevels);
                            if (plFiltered != null)
                            {
                                string strV,secname,desc;
                                strV = plFiltered.SelectToken("id", false).ToString();
                                secname = plFiltered.SelectToken("secname", false).ToString();
                                desc = plFiltered.SelectToken("description", false).ToString();
                                Console.WriteLine("{0} : ,{1} : ,{2} : ",strV,secname,desc);
                            }
                        }
                        if (item != null)
                        {
                            JToken display_charges = item.SelectToken("priceDetails");
                            if (display_charges != null)
                            {
                                Console.WriteLine("display_charges : " + display_charges);
                                decimal price = 0 , total_price = 0;
                                if(item.SelectToken("displayAmount") != null)
                                {
                                    total_price = decimal.Parse(((JValue)item.SelectToken("displayAmount")).Value.ToString());
                                    Console.WriteLine(total_price);
                                }
                                if (item.SelectToken("faceValue") != null)
                                {
                                    price = decimal.Parse(((JValue)item.SelectToken("faceValue")).Value.ToString());
                                    Console.WriteLine(total_price);
                                }
                            }
                        }
                    }
                }
            }
        }
        public void PrintData()
        {
            Console.WriteLine();
            foreach (var item in typeof(JsonDataModel).GetProperties())
            {
                Console.WriteLine("{0} : {1}", item.Name, item.GetValue(section, null));
            }
            Console.WriteLine();
            PlayWithTicketDetails();
            Console.WriteLine("\t\t\t" + "TICKET TYPES");
            foreach (var item in typeof(JtokenTicketTypes).GetProperties())
            {
                Console.WriteLine("{0} : {1}",item.Name,item.GetValue(obj,null));
            }
        }
    }
}