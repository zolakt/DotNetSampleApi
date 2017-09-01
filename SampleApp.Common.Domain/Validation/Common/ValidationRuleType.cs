namespace SampleApp.Common.Domain.Validation.Common
{
    public enum ValidationRuleType
    {
        Required = 1,
        EmailFormat = 2,
        PhoneFormat = 4,
        Range = 8,
        Length = 16,
        Custom = 1024
    }
}
