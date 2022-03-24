using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.Utils.Validators
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        public virtual bool ValidateAndHandle(T instance)
        {
            var validation = this.Validate(instance);

            if (!validation.IsValid)
            {
                if (validation.Errors.Count > 1)
                {
                    var exceptions = new List<Exception>();
                    foreach (var error in validation.Errors)
                    {
                        exceptions.Add(new ArgumentException(error.ErrorMessage));
                    }

                    throw new AggregateException($"Multiple validation errors creating {typeof(T).Name}.", exceptions);
                }
                else
                {
                    throw new ArgumentException(validation.Errors.FirstOrDefault().ErrorMessage);
                }
            }

            return true;
        }
    }
}
