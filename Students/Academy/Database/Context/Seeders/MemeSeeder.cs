using Api.Models.Response.Seeder;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Database.Context.Seeders
{
    public class MemeSeeder : ISeeder
    {
        public async Task SeedAsync(AcademyContext dbContext)
        {
            const int MemesToSeed = 1000;

            if (await dbContext.Memes.IgnoreQueryFilters().AnyAsync())
            {
                return;
            }

            using HttpClient httpClient = new();

            var response = await httpClient.GetAsync($"https://meme-api.herokuapp.com/gimme/{MemesToSeed}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Unable to fetch memes from API");
            }

            string memesJsonResponse = await response.Content.ReadAsStringAsync();
            MemesApiResponseModel? memesApiResponse = JsonSerializer.Deserialize<MemesApiResponseModel?>(memesJsonResponse);

            if (memesJsonResponse == null)
            {
                throw new Exception("Invalid API response");
            }

           var existingDbMemes = await dbContext.Memes.ToListAsync();

            foreach (MemeApiResponseModel meme in memesApiResponse.Memes ?? Array.Empty<MemeApiResponseModel>())
            {
                if (!existingDbMemes.Any(x => x.Title == meme.Title && x.Url == meme.Url))
                {
                    Meme memeEntity = meme.ToEntity();
                    dbContext.Memes.Add(memeEntity);
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
