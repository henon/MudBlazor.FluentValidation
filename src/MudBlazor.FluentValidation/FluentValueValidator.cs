using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation;

namespace MudBlazor.FluentValidation
{

    /// <summary>
    /// Allows to create fluent validation rules for primitive type values
    ///
    /// Usage:
    /// var validator = new FluentValueValidator<string>(x => x.NotEmpty().CreditCard());
    /// var validationFunc = validator.Validation;
    /// Pass the validationFunc to a MudTextfield's Validation property
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FluentValueValidator<T> : AbstractValidator<T>
    {
        public FluentValueValidator(Action<IRuleBuilderInitial<T, T>> rule)
        {
            rule(RuleFor(x => x));
        }

        private IEnumerable<string> ValidateValue(T arg)
        {
            var result = Validate(arg);
            if (result.IsValid)
                return new string[0];
            return result.Errors.Select(e => e.ErrorMessage);
        }

        public Func<T, IEnumerable<string>> Validation => ValidateValue;
    }

}

