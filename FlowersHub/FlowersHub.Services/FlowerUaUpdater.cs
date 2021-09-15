using FlowersHub.Interfaces;
using FlowersHub.Model;
using HtmlAgilityPack;
using System.Linq;
using System.Net.Http;

namespace FlowersHub.Services
{
    public class FlowerUaUpdater : IFlowerUpdater
    {
        public void Update(Flower flower)
        {
            var client = new HttpClient();

            var response = client.GetAsync(flower.Url).Result;
            var doc = new HtmlDocument();
            doc.LoadHtml(response.Content.ReadAsStringAsync().Result);

            flower.Title = GetMetaContentByProperty(doc, "og:title");
            flower.Description = GetMetaContentByProperty(doc, "og:description");
            flower.ImageUrl = GetMetaContentByProperty(doc, "og:image");
            flower.Currency = GetMetaContentByProperty(doc, "product:price:currency");
            flower.Price = GetMetaContentByProperty(doc, "product:price:amount");
            flower.Group = GetMetaContentByProperty(doc, "product:item_group_id");
        }

        private static string GetMetaContentByProperty(HtmlDocument doc, string property)
        {
            return doc.DocumentNode.SelectNodes("//meta")
                .FirstOrDefault(item => item.Attributes.Any(attribute => attribute.Name == "property" && attribute.Value == property))?
                .Attributes
                .FirstOrDefault(item => item.Name == "content")?
                .Value;
        }
    }
}
