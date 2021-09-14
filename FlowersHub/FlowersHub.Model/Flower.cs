using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FlowersHub.Model
{
    public class Flower
    {
        [Key]
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string Price { get; set; }
        public string ImageUrl { get; set; }
        public List<FlowerType> FlowerTypes { get; set; }
        public List<ColorType> Colors { get; set; }
    }
}
