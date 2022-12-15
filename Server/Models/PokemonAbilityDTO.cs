using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PokedexV2.Models 
{
    public class PokemonAbilityDTO  {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("effect")]
        public string? Effect { get; set; } = string.Empty;
    }
}