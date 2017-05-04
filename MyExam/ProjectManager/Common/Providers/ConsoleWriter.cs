using System;

using ProjectManager.Common.Constrants;

namespace ProjectManager.Common.Providers
{
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
