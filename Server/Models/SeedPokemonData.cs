using Microsoft.EntityFrameworkCore;

namespace PokedexV2.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PokedexV2Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<PokedexV2Context>>()))
            {
                if (context == null || context.Pokemon == null)
                {
                    throw new ArgumentNullException("Null RazorPagesMovieContext");
                }

                // Look for any movies.
                if (context.Pokemon.Any())
                {
                    return;   // DB has been seeded
                }

                List<Pokemon> seeder = SeedHelper.SeedData<Pokemon>("pokemon.json");
                context.Pokemon.AddRange(seeder);

                // // List<PokemonAbility> seederAbility = SeedHelper.SeedData<PokemonAbility>("pokemon-abilities.json");
                // context.PokemonAbility.AddRange(seeder);
                context.SaveChanges();
            }
        }
    }
}