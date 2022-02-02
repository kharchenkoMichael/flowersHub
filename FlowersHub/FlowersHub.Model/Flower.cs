using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlowersHub.Model
{
    public class Flower
    {
        [Key]
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public double PriceDouble { get; set; }
        public string ImageUrl { get; set; }
        public string Group { get; set; }
        public string Updater { get; set; }
        public List<FlowerType> FlowerTypes { get; set; }
        public List<ColorType> Colors { get; set; }

        public Flower()
        {
            FlowerTypes = new List<FlowerType>();
            Colors = new List<ColorType>();
        }

        public Flower(string url, string updater)
        {
            Url = url;
            Updater = updater;
            FlowerTypes = new List<FlowerType>();
            Colors = new List<ColorType>();
        }
    }
}
