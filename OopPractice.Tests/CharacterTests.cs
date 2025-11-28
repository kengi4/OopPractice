using Moq;
using OopPractice.Characters;
using OopPractice.Display;
using Xunit;

namespace OopPractice.Tests
{
    public class CharacterTests
    {
        [Fact]
        public void TakeDamage_ShouldReduceHealth_WhenDamageIsApplied()
        {
            var mockDisplayer = new Mock<IDisplayer>();
            var character = new Character("TestHero", 100, 0, 10, mockDisplayer.Object);
            int damage = 20;

            character.TakeDamage(damage);

            Assert.Equal(80, character.Health);
        }

        [Fact]
        public void TakeDamage_ShouldAccountForArmor()
        {
            var mockDisplayer = new Mock<IDisplayer>();
            int armor = 5;
            var character = new Character("ArmoredHero", 100, armor, 10, mockDisplayer.Object);
            int damage = 20;

            character.TakeDamage(damage);

            Assert.Equal(85, character.Health);
        }

        [Fact]
        public void Heal_ShouldIncreaseHealth()
        {
            var mockDisplayer = new Mock<IDisplayer>();
            var character = new Character("HealTarget", 50, 0, 10, mockDisplayer.Object);

            character.Heal(20);

            Assert.Equal(70, character.Health);
        }

        [Fact]
        public void Heal_ShouldNotExceedMaxHealth()
        {
            var mockDisplayer = new Mock<IDisplayer>();
            var character = new Character("AlmostFull", 90, 0, 10, mockDisplayer.Object);

            character.Heal(50);

            Assert.Equal(100, character.Health);
        }
    }
}