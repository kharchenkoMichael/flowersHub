using System.ComponentModel.DataAnnotations;

namespace FlowersHub.Model
{
    public class FlowerType
    {
        [Key]
        public string Name { get; set; }
        public string Variations { get; set; }
    }
}
