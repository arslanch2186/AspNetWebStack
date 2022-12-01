using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Models.ValidationAttributes
{
    public class ValidateListAttribute : ValidationAttribute
    {
        public int MaximumLength { get; set; }
        public int MinimumLength { get; set; }

        protected readonly List<ValidationResult> validationResults = new List<ValidationResult>();

        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list == null) return false;
            if (list.Count < MinimumLength || list.Count > MaximumLength) return false;

            var isValid = true;

            foreach (var item in list)
            {
                var validationContext = new ValidationContext(item);
                var isItemValid = Validator.TryValidateObject(item, validationContext, validationResults, true);
                isValid &= isItemValid;
            }
            return isValid;
        }
    }
}
