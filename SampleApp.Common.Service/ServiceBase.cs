using System;
using SampleApp.Common.DAL.UnitOfWork;

namespace SampleApp.Common.Service
{
    public abstract class ServiceBase
    {
        protected readonly IUnitOfWork _uow;

        protected ServiceBase(IUnitOfWork uow)
        {
            if (uow == null)
            {
                throw new ArgumentNullException("uow");
            }

            _uow = uow;
        }
    }
}