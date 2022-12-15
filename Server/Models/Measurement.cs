using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PokedexV2.Models 
{
    public class Measurement {
        public int value { get; set; }
        public double? Convert() {
            return value/10;
        }
    }
}