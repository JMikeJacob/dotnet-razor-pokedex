using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PokedexV2.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text.Json;

namespace PokedexV2.Pages_Pokemon
{
    public class IndexModel : PageModel
    {
        public PaginatedList<BasePokemon> Pokemon { get;set; } = default!;

        private static readonly HttpClient client = new HttpClient();
        public int currentPage;
        public static int limit = 15;
        private async Task<GetPokemonListResponse> GetPokemonAPI(int pageIndex, string searchString) {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            int basePageIndex = 1;
            if (Pokemon != null) {
                basePageIndex = Pokemon.PageIndex;
            }
         
            try {
                string getUrl = $"{Environment.GetEnvironmentVariable("SERVER_BASE_URL")}/pokemon";
                if (!string.IsNullOrEmpty(searchString)) {
                    getUrl += $"/search/{searchString}";
                }
                getUrl += $"/page/{pageIndex}";
                var streamTask = client.GetStreamAsync(getUrl);
                var pokemonDeserialized = await JsonSerializer.DeserializeAsync<GetPokemonListResponse>(await streamTask); // deserialize stream
                if (pokemonDeserialized != null) {
                    return pokemonDeserialized;
                } else {
                    return new GetPokemonListResponse();
                }
            } catch (ArgumentException e) {
                Console.WriteLine(e);
                throw e;
            }
        }

        public async Task OnGetAsync(int? pageIndex, string? query)
        {
            var pokemonResults = await GetPokemonAPI(pageIndex ?? 1, query ?? string.Empty);
            if (pokemonResults != null) {
                Pokemon = new PaginatedList<BasePokemon>(pokemonResults.Results, pokemonResults.Count, pageIndex ?? 1, limit, query ?? string.Empty);
            }
            // if (_context.Pokemon != null)
            // {
            //     Pokemon = await _context.BasePokemon.ToListAsync();
            // }
        }

        public async Task OnPostSearchAsync(string? query) {
            var pokemonResults = await GetPokemonAPI(1, query ?? string.Empty);
            if (pokemonResults != null) {
                Pokemon = new PaginatedList<BasePokemon>(pokemonResults.Results, pokemonResults.Count, 1, limit, query ?? string.Empty);
            }
        }
    }
}
