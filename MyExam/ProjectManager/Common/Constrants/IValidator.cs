using System.Collections.Generic;

namespace ProjectManager.Common.Constrants
{
    public interface IValidator
    {
        void Validate<T>(T obj) where T : class;

        IEnumerable<string> GetValidationErrors(object obj);
    }
}
