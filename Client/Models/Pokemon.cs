using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PokedexV2.Models 
{
    public class Pokemon: BasePokemon {
        [Required, Range(0, double.MaxValue, ErrorMessage = "Should be equal to or greater than 0.")]
        [JsonPropertyName("height")]
        [Column(TypeName = "decimal(5,2)")]
        public double Height { get; set; }

        [Required, Range(0, double.MaxValue, ErrorMessage = "Should be equal to or greater than 0.")]
        [JsonPropertyName("weight")]
        [Column(TypeName = "decimal(5,2)")]
        public double Weight { get; set; }

        [Required, MaxLength(500, ErrorMessage = "Should be equal to or less than 500 characters")]
        [JsonPropertyName("definition")]
        public string? Definition { get; set; } = string.Empty;

        [Display(Name = "Photo URL")]
        [Url]
        [JsonPropertyName("photoPath")]
        public string? PhotoPath { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("genus")]
        public string? Genus { get; set; } = string.Empty;

        [JsonPropertyName("habitat")]
        public string? Habitat { get; set; } = string.Empty;
        
        [Required, Range(1,9)]
        [JsonPropertyName("generation")]
        public int Generation { get; set; }

        [JsonPropertyName("abilities")]
        public List<PokemonAbility>? Abilities { get; set; }
        
        // [JsonPropertyName("moves")]
        // public List<PokemonMove>? Moves { get; set; }
    }
}