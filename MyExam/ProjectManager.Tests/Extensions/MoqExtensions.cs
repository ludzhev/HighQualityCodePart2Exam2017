using System;
using System.Linq.Expressions;
using Moq;

namespace ProjectManager.Tests.Extensions
{
    internal static class MoqExtensions
    {
        public static void SetupMany<TSvc, TReturn>(this Mock<TSvc> mock,
            Expression<Func<TSvc, TReturn>> expression,
            params TReturn[] args)
            where TSvc : class
        {
            if (args.Length == 0)
            {
                mock.Setup(expression)
                    .Returns(null);

                return;
            }

            var numCalls = 0;

            mock.Setup(expression)
                .Returns(() => numCalls < args.Length ? args[numCalls] : args[args.Length - 1])
                .Callback(() => numCalls++);
        }
    }
}
