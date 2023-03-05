using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PowerStationDomain.models.AD
{
    public class PasswordSet
    {
        [Required(ErrorMessage = "Password is required")]
        [StringLength(12, ErrorMessage = "Must be between 5 and 12 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(12, ErrorMessage = "Must be between 5 and 12 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are not the same")]
        public string ConfirmPassword { get; set; }


        public bool AreEqual()
        {
            if (Password.Equals(ConfirmPassword)) return true;
            return false;
        }

    }


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public sealed class PropertyMustMatchAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "'{0}' and '{1}' do not match.";

        public PropertyMustMatchAttribute(string originalProperty)
            : base(_defaultErrorMessage)
        {
            OriginalProperty = originalProperty;
        }

        public string OriginalProperty { get; }

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var properties = TypeDescriptor.GetProperties(validationContext.ObjectInstance);
            var originalProperty = properties.Find(OriginalProperty, false);
            var originalValue = originalProperty.GetValue(validationContext.ObjectInstance);
            var confirmProperty = properties.Find(validationContext.MemberName, false);
            var confirmValue = confirmProperty.GetValue(validationContext.ObjectInstance);
            if (originalValue == null)
            {
                if (confirmValue == null)
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult(ErrorMessage,
                    new[] { validationContext.MemberName });
            }

            if (originalValue.Equals(confirmValue))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage,
                new[] { validationContext.MemberName });
        }
    }


    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    //public sealed class PropertiesMustMatchAttribute : ValidationAttribute
    //{
    //    private const string _defaultErrorMessage = "'{0}' and '{1}' do not match.";
    //    private readonly object _typeId = new object();

    //    public PropertiesMustMatchAttribute(string originalProperty, string confirmProperty)
    //        : base(_defaultErrorMessage)
    //    {
    //        OriginalProperty = originalProperty;
    //        ConfirmProperty = confirmProperty;
    //    }

    //    public string ConfirmProperty { get; private set; }
    //    public string OriginalProperty { get; private set; }

    //    public override object TypeId
    //    {
    //        get
    //        {
    //            return _typeId;
    //        }
    //    }

    //    public override string FormatErrorMessage(string name)
    //    {
    //        return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
    //            OriginalProperty, ConfirmProperty);
    //    }

    //    public override bool IsValid(object value)
    //    {
    //        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
    //        object originalValue = properties.Find(OriginalProperty, true /* ignoreCase */).GetValue(value);
    //        object confirmValue = properties.Find(ConfirmProperty, true /* ignoreCase */).GetValue(value);
    //        return Object.Equals(originalValue, confirmValue);
    //    }
    //}

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class IsEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var inputValue = value as string;
            return !string.IsNullOrEmpty(inputValue);
        }
    }
}
