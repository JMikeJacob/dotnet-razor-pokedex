using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PokedexV2.Models;
using System.Text.Json;

namespace Server.Pages_Pokemon
{
    public class EditModel : PageModel
    {
        private readonly PokedexV2Context _context;

        public EditModel(PokedexV2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Pokemon Pokemon { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Pokemon == null)
            {
                return NotFound();
            }

            var tempPokemonData = TempData["pkmnTempData"];
            if (tempPokemonData != null && !string.IsNullOrEmpty(tempPokemonData.ToString())) {
                Pokemon = JsonSerializer.Deserialize<Pokemon>(tempPokemonData.ToString());
                return Page();
            }

            var pokemon =  await _context.Pokemon.Select(p => new Pokemon {
                    ID = p.ID,
                    Name = p.Name,
                    Type1 = p.Type1,
                    Type2 = p.Type2,
                    Height = p.Height,
                    Weight = p.Weight,
                    Definition = p.Definition,
                    PhotoPath = p.PhotoPath,
                    Genus = p.Genus,
                    Habitat = p.Habitat,
                    Generation = p.Generation,
                    Abilities = _context.PokemonAbility.Where(pa => pa.PokemonId == p.ID).ToList()
                }).FirstOrDefaultAsync(m => m.ID == id);
            if (pokemon == null)
            {
                return NotFound();
            }
            Pokemon = pokemon;

            if (Pokemon.Abilities != default(List<PokemonAbility>) && Pokemon.Abilities.Count > 0) {
                TempData["initPokemonAbilities"] = JsonSerializer.Serialize<List<PokemonAbility>>(Pokemon.Abilities);
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            List<PokemonAbility>? InitPokemonAbilities = new List<PokemonAbility>();
            var tempAbilities = TempData["initPokemonAbilities"];
            if (tempAbilities is not null && !string.IsNullOrEmpty(tempAbilities.ToString())) {
                InitPokemonAbilities = JsonSerializer.Deserialize<List<PokemonAbility>?>(tempAbilities.ToString());
            }

            if (Pokemon.Abilities != null && Pokemon.Abilities.Count > 0) {
                for (int i = 0; i < Pokemon.Abilities.Count; i++) {
                    if (Pokemon.Abilities[i].AbilityId == default(int) || InitPokemonAbilities == default(List<PokemonAbility>) || InitPokemonAbilities.Count == 0) {
                        _context.PokemonAbility.Add(Pokemon.Abilities[i]);
                    } else {
                        int InitAbilityIndex = InitPokemonAbilities.FindIndex(x => x.AbilityId == Pokemon.Abilities[i].AbilityId);
                        if (InitAbilityIndex != -1) { 
                            if (!InitPokemonAbilities[InitAbilityIndex].Equals(Pokemon.Abilities[i])) {
                                _context.Attach(Pokemon.Abilities[i]).State = EntityState.Modified;
                            }
                            InitPokemonAbilities.RemoveAt(InitAbilityIndex);
                        }
                    }
                }
            }

            if (InitPokemonAbilities != null && InitPokemonAbilities.Count > 0) {
                for (int i = 0; i < InitPokemonAbilities.Count; i++) {
                    _context.PokemonAbility.Remove(InitPokemonAbilities[i]);
                }
            }

            _context.Attach(Pokemon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PokemonExists(Pokemon.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostAddAbilityAsync() {
            if (Pokemon.Abilities is null) {
                Pokemon.Abilities = new List<PokemonAbility>();
            }

            Pokemon.Abilities.Add(new PokemonAbility());
            TempData["pkmnTempData"] = JsonSerializer.Serialize<Pokemon>(Pokemon);
            return RedirectToPage("Edit", new { id = Pokemon.ID });
        }

        public async Task<IActionResult> OnPostRemoveAbilityAsync(int abilityIndex) {
            if (Pokemon.Abilities is null) {
                Pokemon.Abilities = new List<PokemonAbility>();
            }

            if (Pokemon.Abilities.Count > 0) {
                Pokemon.Abilities.RemoveAt(Pokemon.Abilities.Count - 1);
                TempData["pkmnTempData"] = JsonSerializer.Serialize<Pokemon>(Pokemon);
                return RedirectToPage("Edit", new { id = Pokemon.ID });
            } else {
                return Page();
            }
        }

        private bool PokemonExists(int id)
        {
          return (_context.Pokemon?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
