using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokedexV2.Models;

    public class PokedexV2Context : DbContext
    {
        public PokedexV2Context (DbContextOptions<PokedexV2Context> options)
            : base(options)
        {
        }

        public DbSet<PokedexV2.Models.Pokemon> Pokemon { get; set; } = default!;
        public DbSet<PokedexV2.Models.PokemonAbility> PokemonAbility { get; set; } = default!;
    }
