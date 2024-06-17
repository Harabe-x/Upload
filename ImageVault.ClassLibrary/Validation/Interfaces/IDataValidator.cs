namespace ImageVault.ClassLibrary.Validation.Interfaces;

public interface IDataValidatorc
{
    public void AddValidationRule<T>(string name, Func<bool, bool> validationRule);

    public bool ValidateData<T>(string name,T validationData);

    public Func<bool, bool> GetValidationRule(string ruleName);
}