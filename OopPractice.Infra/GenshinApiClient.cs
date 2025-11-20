using System.Net.Http.Json;
using OopPractice.Characters;
using OopPractice.Display;

namespace OopPractice.Infra
{
    public class GenshinApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IDisplayer _displayer;

        public GenshinApiClient(IDisplayer displayer)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://genshin.jmp.blue/") };
            _displayer = displayer;
        }

        public async Task<Character> GetCharacterAsync(string name)
        {
            var response = await _httpClient.GetAsync($"characters/{name.ToLower()}");
            response.EnsureSuccessStatusCode();

            var dto = await response.Content.ReadFromJsonAsync<GenshinCharacterDto>();
            if (dto == null) throw new Exception("Failed to load character data.");

            var rand = new Random();
            int hp = rand.Next(80, 150);
            int armor = rand.Next(5, 15);
            int ap = rand.Next(10, 25);

            Character character;
            if (dto.weapon.ToLower().Contains("catalyst"))
            {
                character = new Mage(dto.name, _displayer);
            }
            else
            {
                character = new Character(dto.name, hp, armor, ap, _displayer);
            }

            character.RestoreState(hp, armor, ap);

            return character;
        }

        public async Task<List<string>> GetCharactersListAsync()
        {
            var response = await _httpClient.GetAsync("characters");
            response.EnsureSuccessStatusCode();

            var list = await response.Content.ReadFromJsonAsync<List<string>>();
            return list ?? new List<string>();
        }
    }
}