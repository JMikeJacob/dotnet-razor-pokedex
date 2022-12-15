using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PokedexV2.Models 
{
    public class GetPokemonListResponse {
        [JsonPropertyName("count")]
        public int Count { get; set; }
        [JsonPropertyName("results")]
        public List<BasePokemon> Results { get; set; } = new List<BasePokemon>();
    }
}