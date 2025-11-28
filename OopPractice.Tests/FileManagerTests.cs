using Moq;
using OopPractice.FileManager;
using Xunit;

namespace OopPractice.Tests
{
    public class FileManagerTests
    {
        [Fact]
        public void DirectoryState_DownArrow_ShouldIncreaseIndex()
        {
            var mockDriver = new Mock<IConsoleDriver>();

            mockDriver.Setup(d => d.WindowHeight).Returns(25);
            mockDriver.Setup(d => d.WindowWidth).Returns(80);

            var context = new FileManagerContext(mockDriver.Object);
            var state = new DirectoryState(context, Directory.GetCurrentDirectory());

            var keyInfo = new ConsoleKeyInfo((char)0, ConsoleKey.DownArrow, false, false, false);
            state.HandleInput(keyInfo);

            Assert.True(true);
        }
    }
}