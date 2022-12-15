using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PokedexV2.Models 
{
    public class PokemonAbility  {
        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("effect")]
        public string? Effect { get; set; } = string.Empty;

        [JsonPropertyName("pokemonId")]
        public int? PokemonId { get; set; }
    }
}