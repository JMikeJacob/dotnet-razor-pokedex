using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PokedexV2.Models 
{
    public class PokemonDTO {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("type1")]
        public string? Type1 { get; set; } = string.Empty;
        [JsonPropertyName("type2")]
        public string? Type2 { get; set; } = string.Empty;

        [JsonPropertyName("height")]
        public double Height { get; set; }

        [JsonPropertyName("weight")]
        public double Weight { get; set; }

        [JsonPropertyName("definition")]
        public string? Definition { get; set; } = string.Empty;
        [JsonPropertyName("photoPath")]
        public string? PhotoPath { get; set; } = string.Empty;
        [JsonPropertyName("genus")]
        public string? Genus { get; set; } = string.Empty;
        [JsonPropertyName("habitat")]
        public string? Habitat { get; set; } = string.Empty;
        
        [Range(1, 9, ErrorMessage = "Should be between 1 to 9")]
        [JsonPropertyName("generation")]
        public int? Generation { get; set; }

        [JsonPropertyName("abilities")]
        public List<PokemonAbilityDTO>? Abilities { get; set; }
    }
}