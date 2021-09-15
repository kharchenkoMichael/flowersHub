﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace FlowersHub.Model
{
    public class FlowerType
    {
        [Key]
        public string Name { get; set; }
        public string Variations { get; set; }

        public List<Flower> Flowers { get; set; } = new List<Flower>();
    }
}
