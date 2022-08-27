using FluentValidation.Results;
using Mc2.CrudTest.Domain.BaseEntities;
using System.Text;

namespace Mc2.CrudTest.WebUi.Helpers
{
    public static class ErrorHtmlHelper
    {
        public static string CreateErrors(List<ValidationFailure> errors)
        {
            StringBuilder str = new();
            foreach (var err in errors)
            {
                //str.Append($"<li> /*{err.ErrorCode} :*/ {err.ErrorMessage} </li>");
                str.Append($"<li>{err.ErrorMessage} </li>");
            }

            return GenerateErrorDiv(str.ToString());
        }

        public static string CreateMessageFromBusinessException(BusinessException ex)
        {
            return GenerateErrorDiv(ex.Description);
        }

        public static string CreateMessageFromException()
        {
            return GenerateErrorDiv("<li>Internal server occured </li>");
        }

        public static string GenerateErrorDiv(string innerHtml)
        {
            return "<div class=\"alert alert-danger\"> <ul> " + innerHtml + " </ul> </div>";
        }
    }
}