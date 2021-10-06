using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace FlowersHub.Model
{
    public class GroupType
    {
        [Key]
        public string Name { get; set; }
    }
}
