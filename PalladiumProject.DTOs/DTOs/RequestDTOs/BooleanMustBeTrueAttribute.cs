
using System.ComponentModel.DataAnnotations;

namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs
{
    public class BooleanMustBeTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is bool boolValue)
            {
                return boolValue;
            }
            return false;
        }
    }
}