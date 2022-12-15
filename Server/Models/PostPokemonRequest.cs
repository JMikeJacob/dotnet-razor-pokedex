using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PokedexV2.Models 
{
    public class PostPokemonRequest {
        [JsonPropertyName("data")]
        public Pokemon Data { get; set; } = default!;
    }
}