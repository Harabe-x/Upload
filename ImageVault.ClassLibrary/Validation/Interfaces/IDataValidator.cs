namespace ImageVault.ClassLibrary.Validation.Interfaces;

public interface IDataValidator
{
    public void AddCustomValidationRule<T>(string name, Predicate<T> validationRule);

    public bool ValidateData<T>(string name, T validationData);
}