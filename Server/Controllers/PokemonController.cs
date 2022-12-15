using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokedexV2.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly PokedexV2Context _context;

        public PokemonController(PokedexV2Context context)
        {
            _context = context;
        }

        private static PokemonListDTO ItemToPokemonListDTO(Pokemon item) =>
            new PokemonListDTO
            {
                ID = item.ID,
                Name = item.Name,
                Type1 = item.Type1 ?? default(string),
                Type2 = item.Type2 ?? default(string)
            };

        private static PokemonAbilityDTO ItemToPokemonAbilityDTO(PokemonAbility item) =>
            new PokemonAbilityDTO {
                Name = item.Name,
                Effect = item.Effect
            };

        // GET: api/Pokemon
        [HttpGet]
        public async Task<ActionResult<GetPokemonListResponse>> GetPokemon()
        {
            if (_context.Pokemon == null)
            {
                return NotFound();
            }
            
            var query = (from p in _context.Pokemon
                        select ItemToPokemonListDTO(p));
            int TotalCount = query.Count();
            return new GetPokemonListResponse {
                Count = TotalCount,
                Results = await query.ToListAsync()
            };
        }

        [HttpGet("page/{pageIndex}")]
        public async Task<ActionResult<GetPokemonListResponse>> GetPokemonPage(int pageIndex)
        {
            if (_context.Pokemon == null)
            {
                return NotFound();
            }
            
            int pageCount = 15;
            int TotalCount = _context.Pokemon.Select(x => ItemToPokemonListDTO(x)).Count();
            return new GetPokemonListResponse {
                Count = TotalCount,
                Results = await _context.Pokemon.Select(x => ItemToPokemonListDTO(x)).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToListAsync()
            };
        }

        [HttpGet("search/{searchString}/page/{pageIndex}")]
        public async Task<ActionResult<GetPokemonListResponse>> GetPokemonSearch(string searchString, int? pageIndex)
        {
            if (_context.Pokemon == null)
            {
                return NotFound();
            }

            int index = pageIndex ?? 1;
            int pageCount = 15;
            var query = _context.Pokemon.Where(p => p.Name.ToLower().Contains(searchString.ToLower())).OrderBy(p => p.ID).Select(p => ItemToPokemonListDTO(p)).Skip((index - 1) * pageCount).Take(pageCount);
            int TotalCount = _context.Pokemon.Where(p => p.Name.ToLower().Contains(searchString.ToLower())).OrderBy(p => p.ID).Select(p => ItemToPokemonListDTO(p)).Count();
            return new GetPokemonListResponse {
                Count = TotalCount,
                Results = await query.ToListAsync()
            };
        }

        // GET: api/Pokemon/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPokemon(int id)
        {
          if (_context.Pokemon == null)
          {
              return NotFound();
          }
            var pokemon = await _context.Pokemon.Select(p => new {
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
                    Abilities = _context.PokemonAbility.Where(pa => pa.PokemonId == p.ID).Select(pa => ItemToPokemonAbilityDTO(pa)).ToList()
                }).FirstOrDefaultAsync(m => m.ID == id);

            if (pokemon == null)
            {
                return NotFound();
            }

            return pokemon;
        }

        [HttpPost]
        public async Task<ActionResult<PostPokemonRequest>> CreatePokemon(PostPokemonRequest request)
        {
            Pokemon pokemon = request.Data;
            _context.Pokemon.Add(pokemon);
            await _context.SaveChangesAsync();
            Console.WriteLine(nameof(GetPokemon));
            return CreatedAtAction(
                nameof(GetPokemon),
                new { id = pokemon.ID },
                pokemon);
        }

        private bool PokemonExists(int id)
        {
            return (_context.Pokemon?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
