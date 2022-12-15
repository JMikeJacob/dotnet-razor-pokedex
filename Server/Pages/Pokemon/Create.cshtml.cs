using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PokedexV2.Models;
using System.Text.Json;

namespace Server.Pages_Pokemon
{
    public class CreateModel : PageModel
    {
        private readonly PokedexV2Context _context;

        public CreateModel(PokedexV2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var tempPokemonData = TempData["pkmnTempData"];
            if (tempPokemonData != null && !string.IsNullOrEmpty(tempPokemonData.ToString())) {
                Pokemon = JsonSerializer.Deserialize<Pokemon>(tempPokemonData.ToString());
            }

            return Page();
        }

        [BindProperty]
        public Pokemon Pokemon { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Pokemon == null || Pokemon == null)
            {
                return Page();
            }

            if (Pokemon.Abilities != null && Pokemon.Abilities.Count > 0) {
                for (int i = 0; i < Pokemon.Abilities.Count; i++) {
                    _context.PokemonAbility.Add(Pokemon.Abilities[i]);
                }
            }

            _context.Pokemon.Add(Pokemon);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
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
    }
}
