using System.Collections.Concurrent;
using ImageVault.ClassLibrary.Validation.Interfaces;

namespace ImageVault.ClassLibrary.Validation.Classes
{
    public class DataValidator : IDataValidator
    {

        public DataValidator()
        {
            _validationRules = new ConcurrentDictionary<string, object>();
        }

        public void AddCustomValidationRule<T>(string ruleName,Predicate<T> validationRule)
        {
            if (!_validationRules.TryAdd(ruleName, validationRule))
                throw new ArgumentException("Key already exists");
        }

        public bool ValidateData<T>(string ruleName, T validationData)
        {
                if (_validationRules.TryGetValue(ruleName, out var validationFunction) && validationFunction is Predicate<T> validationRule)
                {
                    return validationRule.Invoke(validationData);
                }

            throw new InvalidOperationException($"Cannot validate {typeof(T)} using the {ruleName} rule");
        }

        private readonly ConcurrentDictionary<string, object> _validationRules;

    }
}