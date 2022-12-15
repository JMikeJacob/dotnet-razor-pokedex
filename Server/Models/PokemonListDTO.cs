using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PokedexV2.Models 
{
    public class PokemonListDTO {
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Type1 { get; set; } = string.Empty;

        public string? Type2 { get; set; } = string.Empty;
    }
}