using FlowersHub.Interfaces;
using FlowersHub.Model;
using HtmlAgilityPack;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FlowersHub.Services
{
    public class FlowerUaUpdater : IFlowerUpdater
    {
        public async Task Update(Flower flower)
        {
            var client = new HttpClient();

            var response = await client.GetAsync(flower.Url);
            var doc = new HtmlDocument();
            doc.LoadHtml(await response.Content.ReadAsStringAsync());

            flower.Title = GetMetaContentByProperty(doc, "og:title");
            flower.Description = GetMetaContentByProperty(doc, "og:description")
                .Replace("&nbsp;", " ")
                .Replace("&#34;", "")
                .Replace("Состав:", " Состав:")
                .Replace("букета:", "букета: ");
            flower.ImageUrl = GetMetaContentByProperty(doc, "og:image");
            flower.Currency = GetMetaContentByProperty(doc, "product:price:currency");
            if(double.TryParse(GetMetaContentByProperty(doc, "product:price:amount"), out var price))
                flower.PriceDouble = price;
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
