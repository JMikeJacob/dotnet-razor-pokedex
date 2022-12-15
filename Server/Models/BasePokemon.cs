using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PokedexV2.Models 
{
    public class BasePokemon {
        [Display(Name="Index")]
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}