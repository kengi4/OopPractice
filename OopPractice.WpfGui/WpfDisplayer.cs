using OopPractice.Display;
using System;
using System.Windows.Controls;

namespace OopPractice.WpfGui
{
    public class WpfDisplayer : IDisplayer
    {
        private readonly TextBox _outputBox;

        public WpfDisplayer(TextBox outputBox)
        {
            _outputBox = outputBox;
        }

        public void Display(string message)
        {
            _outputBox.Dispatcher.Invoke(() =>
            {
                _outputBox.AppendText(message + "\n");
                _outputBox.ScrollToEnd();
            });
        }

        public void Display(IDisplayable item)
        {
            Display(item.Render());
        }
    }
}