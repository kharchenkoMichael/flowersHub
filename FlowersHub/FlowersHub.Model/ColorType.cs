using System.ComponentModel.DataAnnotations;

namespace FlowersHub.Model
{
    public class ColorType
    {
        [Key]
        public string Name { get; set; }
        public string Variations { get; set; }
    }
}
