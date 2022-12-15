using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PokedexV2.Models;

namespace Server.Pages_Pokemon
{
    public class DeleteModel : PageModel
    {
        private readonly PokedexV2Context _context;

        public DeleteModel(PokedexV2Context context)
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
            else 
            {
                Pokemon = pokemon;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Pokemon == null)
            {
                return NotFound();
            }
            var pokemon = await _context.Pokemon.FindAsync(id);

            if (pokemon != null)
            {
                Pokemon = pokemon;
                _context.PokemonAbility.RemoveRange(_context.PokemonAbility.Where(p => p.PokemonId == pokemon.ID));
                _context.Pokemon.Remove(Pokemon);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
