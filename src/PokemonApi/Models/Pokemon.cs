﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PokemonApi.Models
{
    public class Pokemon
    {
        public Pokemon()
        {
            this.StrongAgainst = new List<Type>();
            this.WeakAgainst = new List<Type>();
            this.ResistantTo = new List<Type>();
            this.VulnerableTo = new List<Type>();
            this.Skills = new List<string>();
        }

        public string Name { get; set; }
        public int Level { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Type Type { get; set; }

        [JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
        public IList<Type> StrongAgainst { get; set; }

        [JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
        public IList<Type> WeakAgainst { get; set; }

        [JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
        public IList<Type> ResistantTo { get; set; }

        [JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
        public IList<Type> VulnerableTo { get; set; }

        public Statistics Statistics { get; set; }

        public IList<string> Skills { get; set; }
    }
}
