using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Identidade.API.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        public ICollection<string> Errors = new List<string>();
        protected IActionResult CustomResponse(object result = null)
        {
            return ValidOperation() switch {
                true => Ok(result),
                false => BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]> // Implements a default specified in a RFC, what says how an API should response about details of errors
                {
                    { "Messages", Errors.ToArray() }
                }))
            };
        }

        protected IActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in errors)
            {
                AddProcessErrors(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool ValidOperation()
        {
            return !Errors.Any();
        }

        protected void AddProcessErrors(string erro)
        {
            Errors.Add(erro);
        }

        protected void ClearProcessErrors()
        {
            Errors.Clear();
        }
    }
}
