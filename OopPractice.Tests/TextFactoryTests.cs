using OopPractice.Text;
using Xunit;

namespace OopPractice.Tests
{
    public class TextFactoryTests
    {
        [Fact]
        public void AddHeading_ShouldCreateNestedStructure()
        {
            var factory = new TextFactory("Doc Title");

            factory.AddHeading("Chapter 1");
            factory.AddParagraph("Intro text");

            string result = factory.ToString();

            Assert.Contains("Doc Title", result);
            Assert.Contains("Chapter 1", result);
            Assert.Contains("Intro text", result);
            Assert.Contains("\tChapter 1", result);
        }

        [Fact]
        public void Up_ShouldNavigateToParent()
        {
            var factory = new TextFactory("Root");

            factory.AddHeading("Level 1");
            factory.Up();
            factory.AddHeading("Level 1 Sibling");

            string result = factory.ToString();

            Assert.Contains("\tLevel 1", result);
            Assert.Contains("\tLevel 1 Sibling", result);
        }
    }
}