namespace ImageVault.ApiKeyService.Data.Interfaces.Services;


/// <summary>
///  Service responsible for validating data
/// </summary>
public interface IDataValidationService
{
    /// <summary>
    /// Registers a custom validation rule for a specific data type.
    /// </summary>
    /// <typeparam name="T">The type of data that the validation rule will be applied to.</typeparam>
    /// <param name="name">A unique name or identifier for the custom validation rule. This name is used to refer to the rule when validating data.</param>
    /// <param name="validationRule">The type of data that the validation rule will be applied to. predicate function that defines the validation logic . This function should return `true` if the data is valid `true` otherwise`false`.</param>
    public void AddCustomValidationRule<T>(string name, Predicate<T> validationRule);

    /// <summary>
    /// Validates the provided data using a custom validation rule identified by the given name.
    /// </summary>
    /// <typeparam name="T">The type of the data to be validated.</typeparam>
    /// <param name="name">The name or identifier of the custom validation rule to apply.</param>
    /// <param name="validationData">The data to be validated against the specified validation rule.</param>
    /// <returns>Returns `true` if the data passes the validation rule, otherwise `false`.</returns>
    public bool ValidateData<T>(string name, T validationData);

}