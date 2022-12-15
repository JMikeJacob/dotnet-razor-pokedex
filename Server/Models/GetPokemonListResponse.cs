using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PokedexV2.Models 
{
    public class GetPokemonListResponse {
        public int Count { get; set; }
        public List<PokemonListDTO> Results { get; set; } = new List<PokemonListDTO>();
    }
}