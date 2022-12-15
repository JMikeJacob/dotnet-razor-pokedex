using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Collections.ObjectModel;

namespace PokedexV2.Models 
{
    public class Pokemon: BasePokemon {
        [Required]
        [JsonPropertyName("type1")]
        public string? Type1 { get; set; } = string.Empty;
        [JsonPropertyName("type2")]
        public string? Type2 { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Should be equal to or greater than 0.")]
        [Required]
        [JsonPropertyName("height")]
        public double Height { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Should be equal to or greater than 0.")]
        [Required]
        [JsonPropertyName("weight")]
        public double Weight { get; set; }

        [JsonPropertyName("definition")]
        public string? Definition { get; set; } = string.Empty;
        [JsonPropertyName("photoPath")]

        [Url]
        public string? PhotoPath { get; set; } = string.Empty;
        [JsonPropertyName("genus")]
        public string? Genus { get; set; } = string.Empty;
        [JsonPropertyName("habitat")]
        public string? Habitat { get; set; } = string.Empty;
        
        [Range(1, 9, ErrorMessage = "Should be between 1 to 9")]
        [JsonPropertyName("generation")]
        public int? Generation { get; set; }

        [JsonPropertyName("abilities")]
        public List<PokemonAbility>? Abilities { get; set; }

        // public List<PokemonMove>? Moves { get; set; }
    }
}