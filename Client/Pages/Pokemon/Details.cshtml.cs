
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PokedexV2.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Net;
using System.Net.Http;

namespace PokedexV2.Pages_Pokemon
{
    public class DetailsModel : PageModel
    {
        // private readonly PokedexContext _context;

        // inject service here
        // public DetailsModel(PokedexContext context)
        // {
        //     _context = context;
        // }

      public Pokemon Pokemon { get; set; } = default!; 
        private static readonly HttpClient client = new HttpClient();

        private async Task<Pokemon?> GetPokemonAPI(int id) {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
   
            try {
                string getUrl = $"{Environment.GetEnvironmentVariable("SERVER_BASE_URL")}/pokemon/{id}";
                var response = await client.GetAsync(getUrl);

                if (response.IsSuccessStatusCode) {
                    var pokemonDeserialized = await JsonSerializer.DeserializeAsync<Pokemon>(await response.Content.ReadAsStreamAsync()); // deserialize stream
                    return pokemonDeserialized;
                } else if (response.StatusCode == HttpStatusCode.NotFound) {
                    return null;
                } else {
                    throw new BadHttpRequestException("Server error", (int)response.StatusCode);
                }
            } catch (WebException e) {
                throw e;
            }
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try {
                if (id == null)
                {
                    return NotFound();
                }

                Pokemon? getPokemonResponse = await GetPokemonAPI(id ?? default(int));

                if (getPokemonResponse == null || getPokemonResponse == new Pokemon()) {
                    return NotFound();
                } else {
                    Pokemon = getPokemonResponse;
                    return Page();
                }
            } catch (HttpRequestException e) {
                throw e;
            }
        }
    }
}
