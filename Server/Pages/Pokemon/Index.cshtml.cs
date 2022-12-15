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
    public class IndexModel : PageModel
    {
        private readonly PokedexV2Context _context;

        public IndexModel(PokedexV2Context context)
        {
            _context = context;
        }

        public IList<Pokemon> Pokemon { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Pokemon != null)
            {
                Pokemon = await _context.Pokemon.Select(p => new Pokemon {
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
                }).ToListAsync();
            }
        }
    }
}
