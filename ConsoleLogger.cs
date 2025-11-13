using OopPractice.Characters;
using System;

namespace OopPractice1
{
    /// <summary>
    /// Implements ILogger by writing messages to the console.
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}