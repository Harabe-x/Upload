namespace ImageVault.UserService.Data.Interfaces.Services;

public interface IDataValidationService
{
    public void AddCustomValidationRule<T>(string name, Predicate<T> validationRule);

    public bool ValidateData<T>(string name, T validationData);
}