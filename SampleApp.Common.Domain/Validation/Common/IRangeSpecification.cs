namespace SampleApp.Common.Domain.Validation.Common
{
    public interface IRangeSpecification
    {
        object Min { get; set; }

        object Max { get; set; }
    }
}
