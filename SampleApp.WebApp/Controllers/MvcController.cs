using SampleApp.Common.Domain.Exceptions;
using SampleApp.Common.Service;
using System.Linq;
using System.Web.Mvc;

namespace SampleApp.WebApp.Controllers
{
    public abstract class MvcController : Controller
    {
        protected bool HandleServiceException<TResult>(ServiceResponseBase<TResult> response)
        {
            if (response.Exception != null)
            {
                var validationException = response.Exception as ValidationException;

                if (validationException != null && validationException.Errors.Any())
                {
                    foreach (var error in validationException.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }

                    return false;
                }
                else
                {
                    throw response.Exception;
                }
            }

            return true;
        }
    }
}