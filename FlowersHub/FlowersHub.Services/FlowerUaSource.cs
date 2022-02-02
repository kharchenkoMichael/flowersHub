using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FlowersHub.Data;
using FlowersHub.Infrastructure;
using FlowersHub.Interfaces;
using FlowersHub.Model;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace FlowersHub.Services
{
    public class FlowerUaSource : IFlowersSource
    {
        private readonly FlowersHubContext _context;

        private readonly string _url = "https://flowers.ua/ru/getproducts";
        private readonly string _sourceType = "filter";
        private readonly List<string> _sourceIds;
        private readonly int _maxSourceId = 9074;
        private readonly string _sortBy = "4";
        private readonly string _sortType = "0";
        private readonly string _search = "0";

        private readonly ILogger _logger;

        public FlowerUaSource(FlowersHubContext context, ILogger logger)
        {
            _logger = logger;
            _context = context;
            _sourceIds = context.Sources.Select(item => item.Id).ToList();
        }
        public async Task<List<Flower>> Load()
        {
            _logger.LogInformation("Load");
            var items = await GetItems(); 
            var flowers = items
                .Distinct()
                .Select(item => new Flower(item, nameof(FlowerUaUpdater)))
                .ToList();
            await flowers.ForEachAsync(async item => await item.Update());
            return flowers;
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

        private async Task<List<string>> GetItems()
        {
            _logger.LogInformation("GetItems");
            var result = new List<string>();

            if (_sourceIds.Count == 0)
                for (var i = 0; i <= _maxSourceId; i++)
                    await GetItemsBySourceId(result, i.ToString());
            else
                foreach (var sourceId in _sourceIds)
                    await GetItemsBySourceId(result, sourceId);

            _context.Sources.RemoveRange(_context.Sources);
            _context.Sources.AddRange(_sourceIds.Select(item => new Source() {Id = item}));
            await _context.SaveChangesAsync();
            return result;
        }

        private async Task GetItemsBySourceId(List<string> result, string sourceId)
        {
            _logger.LogInformation($"GetItemsBySourceId {sourceId}");
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
                var response = await client.PostAsync(_url, content);
                var str = await response.Content.ReadAsStringAsync();
                var doc = new HtmlDocument();
                try
                {
                    var deserializeObject = JsonSerializer.Deserialize<Result>(str);
                    if (deserializeObject?.Data?.Html == null)
                        return;

                    doc.LoadHtml(deserializeObject.Data.Html);
                }
                catch
                {
                    return;
                }

                var items = doc.DocumentNode.GetByClass("visual");

                if (items.Count == 0)
                    return;
                
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
