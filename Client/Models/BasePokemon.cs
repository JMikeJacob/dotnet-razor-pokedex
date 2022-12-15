using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PokedexV2.Models 
{
    public class BasePokemon {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        public string GetPaddedId() {
            string idString = ID.ToString();
            if (ID < 10) {
                return "00" + idString;
            } else if (ID < 100) {
                return "0" + idString;
            } else {
                return idString;
            }
        }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("type1")]
        public string Type1 { get; set; } = string.Empty;

        [JsonPropertyName("type2")]
        public string? Type2 { get; set; } = string.Empty;
    }
}