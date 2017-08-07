namespace SampleApp.Common.Service
{
    public abstract class IntegerIdRequest : ServiceRequestBase
    {
        public IntegerIdRequest(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}