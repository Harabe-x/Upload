using System.Collections.Concurrent;
using ImageVault.ApiKeyService.Data.Interfaces.Services;

namespace ImageVault.ApiKeyService.Services;

public class DataValidationService : IDataValidationService
{
    private readonly ConcurrentDictionary<string, object> _validationRules;

    public DataValidationService()
    {
        _validationRules = new ConcurrentDictionary<string, object>();
    }

    public void AddCustomValidationRule<T>(string ruleName, Predicate<T> validationRule)
    {
        if (!_validationRules.TryAdd(ruleName, validationRule))
            throw new ArgumentException("Key already exists");
    }

    public bool ValidateData<T>(string ruleName, T validationData)
    {
        if (_validationRules.TryGetValue(ruleName, out var validationFunction) &&
            validationFunction is Predicate<T> validationRule) return validationRule.Invoke(validationData);
        throw new InvalidOperationException($"Cannot validate {typeof(T)} using the {ruleName} rule");
    }
}