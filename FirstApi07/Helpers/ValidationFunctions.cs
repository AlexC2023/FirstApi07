using FirstApi07.Models;

namespace FirstApi07.Helpers
{
    public class ValidationFunctions
    {
        public static void ExceptionWhenDateIsNotValid(DateTime? start, DateTime? end)
        { 
        if(start.HasValue && end.HasValue && start > end)
            {
                throw new ModelValidationException(Helpers.ErrorMessagesEnum.StartEndDatesError);
            }
        }
    }
}
