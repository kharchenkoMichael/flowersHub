using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FlowersHub.Infrastructure;
using FlowersHub.Interfaces;
using FlowersHub.Model;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace FlowersHub.Services
{
    public class FlowerUaSource : IFlowersSource
    {
        private readonly string _url = "https://flowers.ua/ru/getproducts";
        private readonly string _sourceType = "filter";
        private readonly List<string> _sourceIds = new List<string>();
        private readonly int _maxSourceId = 9074;
        private readonly string _sortBy = "4";
        private readonly string _sortType = "0";
        private readonly string _search = "0";
        public void Load()
        {
            var items = GetItems(); 
            var flowers = items
                .Select(item => new Flower(item, nameof(FlowerUaUpdater)))
                .ToList();
            flowers.ForEach(item => item.Update());

        }

        class Result
        {
            public string Status { get; set; }
            public string Message { get; set; }
            public Data Data { get; set; }
        }

        class Data
        {
            public string Html { get; set; }
        }

        private List<string> GetItems()
        {
            var result = new List<string>();

            if (_sourceIds.Count == 0)
                for (var i = 0; i <= _maxSourceId; i++)
                    GetItemsBySourceId(result, i.ToString());
            else
                foreach (var sourceId in _sourceIds)
                    GetItemsBySourceId(result, sourceId);

            return result;
        }

        private void GetItemsBySourceId(List<string> result, string sourceId)
        {
            var i = 0;
            while (true)
            {
                var client = new HttpClient();
                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"postedData[page]", i.ToString()},
                    {"postedData[sourceType]", _sourceType},
                    {"postedData[sourceId]", sourceId},
                    {"postedData[sortBy]", _sortBy},
                    {"postedData[sortType]", _sortType},
                    {"postedData[search]", _search}
                });
                var response = client.PostAsync(_url, content).Result;
                var deserializeObject = JsonConvert.DeserializeObject<Result>(response.Content.ReadAsStringAsync().Result);
                var doc = new HtmlDocument();
                doc.LoadHtml(deserializeObject.Data.Html);

                var items = doc.DocumentNode.GetByClass("visual");

                if (items.Count == 0)
                    break;
                
                if (items.Count != 0 && !_sourceIds.Contains(sourceId))
                    _sourceIds.Add(sourceId);

                result.AddRange(items
                    .Select(htmlNode =>
                        htmlNode.ChildNodes.FindFirst("a")?.Attributes.FirstOrDefault(item => item.Name == "href"))
                    .Where(attribute => attribute != null)
                    .Select(attribute => attribute.Value));

                i++;
            }
        }
    }
}
