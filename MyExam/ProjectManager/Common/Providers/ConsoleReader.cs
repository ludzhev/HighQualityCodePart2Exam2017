using System;

using ProjectManager.Common.Constrants;

namespace ProjectManager.Common.Providers
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            var input = Console.ReadLine();

            return input;
        }
    }
}
