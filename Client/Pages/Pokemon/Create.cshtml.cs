
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PokedexV2.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Net;
using System.Text;

namespace PokedexV2.Pages_Pokemon
{
    public class CreateModel : PageModel
    {

        [BindProperty]
        public Pokemon Pokemon { get; set; } = default!; 
        private static readonly HttpClient client = new HttpClient();

        private async Task<Pokemon?> PostPokemonAPI() {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
   
            try {
                string postUrl = $"{Environment.GetEnvironmentVariable("SERVER_BASE_URL")}/pokemon";
                var serializedData = JsonSerializer.Serialize(new PostPokemonRequest { Data = Pokemon });
                var content = new StringContent(serializedData);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(postUrl, content);

                if (response.IsSuccessStatusCode) {
                    return null;
                } else {
                    throw new BadHttpRequestException("Server error", (int)response.StatusCode);
                }
            } catch (WebException e) {
                throw e;
            }
        }

        public IActionResult OnPostAddAbility() {
            if (Pokemon.Abilities is null) {
                Pokemon.Abilities = new List<PokemonAbility>();
            }

            Pokemon.Abilities.Add(new PokemonAbility());
            TempData["pkmnTempData"] = JsonSerializer.Serialize<Pokemon>(Pokemon);
            return RedirectToPage("Create");
        }

        public IActionResult OnPostRemoveAbility() {
            if (Pokemon.Abilities is null) {
                Pokemon.Abilities = new List<PokemonAbility>();
            }

            if (Pokemon.Abilities.Count > 0) {
                Pokemon.Abilities.RemoveAt(Pokemon.Abilities.Count - 1);
                TempData["pkmnTempData"] = JsonSerializer.Serialize<Pokemon>(Pokemon);
                return RedirectToPage("Create");
            } else {
                return Page();
            }
        }

        public IActionResult OnGet()
        {
            var tempPokemonData = TempData["pkmnTempData"];
            if (tempPokemonData != null && !string.IsNullOrEmpty(tempPokemonData.ToString())) {
                Pokemon = JsonSerializer.Deserialize<Pokemon>(tempPokemonData.ToString());
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            try {
                await PostPokemonAPI();

                return RedirectToPage("./Index");
            } catch (WebException e) {
                Console.WriteLine(e);
                return Page();
            } catch (ArgumentException e) {
                Console.WriteLine(e);
                return Page();
            } catch (BadHttpRequestException e) {
                Console.WriteLine(e);
                return Page();
            }
        }

    }
}
