using Moq;
using OopPractice.Characters;
using OopPractice.Display;
using OopPractice.Infra;
using Xunit;

namespace OopPractice.Tests
{
    public class GenshinApiClientTests
    {
        [Fact]
        public async Task GetCharacter_ShouldReturnCharacter_WhenNameIsValid()
        {
            var mockDisplayer = new Mock<IDisplayer>();
            var client = new GenshinApiClient(mockDisplayer.Object);

            Character character = await client.GetCharacterAsync("diluc");

            Assert.NotNull(character);
            Assert.Equal("Diluc", character.Name);
            Assert.True(character.AttackPower > 0);
        }

        [Fact]
        public async Task GetCharactersList_ShouldReturnNonEmptyList()
        {
            var mockDisplayer = new Mock<IDisplayer>();
            var client = new GenshinApiClient(mockDisplayer.Object);

            List<string> characters = await client.GetCharactersListAsync();

            Assert.NotNull(characters);
            Assert.NotEmpty(characters);
            Assert.Contains("diluc", characters);
        }
    }
}