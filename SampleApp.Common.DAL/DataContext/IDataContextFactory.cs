namespace SampleApp.Common.DAL.DataContext
{
	public interface IDataContextFactory<TContextType> where TContextType : IDataContext
    {
        TContextType Create();
	}
}
