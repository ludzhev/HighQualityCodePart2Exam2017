using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using ProjectManager.Common.Constrants;
using ProjectManager.Common.Exceptions;

namespace ProjectManager.Common.Providers
{
    public class Validator : IValidator
    {
        public void Validate<T>(T obj) where T : class
        {
            var errors = this.GetValidationErrors(obj);

            if (errors.Any())
            {
                throw new UserValidationException(errors.First());
            }
        }

        public IEnumerable<string> GetValidationErrors(object obj)
        {
            Type t = obj.GetType();
            PropertyInfo[] p = t.GetProperties();
            Type attrType = typeof(ValidationAttribute);

            foreach (var propertyInfo in p)
            {
                object[] customAttributes = propertyInfo.GetCustomAttributes(attrType, inherit: true);
                foreach (var customAttribute in customAttributes)
                {
                    var validationAttribute = (ValidationAttribute)customAttribute;
                    bool valid = validationAttribute.IsValid(propertyInfo.GetValue(obj, BindingFlags.GetProperty, null, null, null));
                    if (!valid)
                    {
                        yield return validationAttribute.ErrorMessage;
                    }
                }
            }
        }
    }
}
