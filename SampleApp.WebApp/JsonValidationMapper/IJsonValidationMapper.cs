using SampleApp.Common.Domain.Validation;

namespace SampleApp.WebApp.JsonValidationMapper
{
    public interface IJsonValidationMapper
    {
        string Format(ValidationRulesMap validationMap, string prefix = null);
    }
}
