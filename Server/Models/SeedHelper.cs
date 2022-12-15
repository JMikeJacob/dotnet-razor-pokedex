using System.Text.Json;

namespace PokedexV2.Models {
    public static class SeedHelper
    {
        public static List<TEntity> SeedData<TEntity>(string fileName)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string path = "wwwroot/seed";
            string fullPath = Path.Combine(currentDirectory, path, fileName);

            var result = new List<TEntity>();
            using (StreamReader reader = new StreamReader(fullPath))
            {
                string json = reader.ReadToEnd();
                result = JsonSerializer.Deserialize<List<TEntity>>(json);
            }

            return result;
        }
    }
}