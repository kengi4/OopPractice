using OopPractice.Display;

namespace OopPractice1
{
    /// <summary>
    /// Implements IDisplayer by writing messages to the console.
    /// </summary>
    public class ConsoleLogger : IDisplayer
    {
        void IDisplayer.Display(string line)
        {
            throw new NotImplementedException();
        }

        void IDisplayer.Display(IDisplayable displayable)
        {
            throw new NotImplementedException();
        }
    }
}