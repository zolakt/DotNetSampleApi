namespace SampleApp.Common.Domain.Validation.Common
{
    public interface ILengthSpecification
    {
        string Min { get; set; }

        string Max { get; set; }
    }
}
