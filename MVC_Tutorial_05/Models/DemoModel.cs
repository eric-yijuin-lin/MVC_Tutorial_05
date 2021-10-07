using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Tutorial_05.Models
{
    public class DemoModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "TEST")]
        public string DemoString { get; set; }
        [Range(1, 10)]
        public int range { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "測試郵件錯誤")]
        public string email { get; set; }
        [Required]
        [Phone]
        public string phone_number { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }

    public class DemoModelValidationResult
    {
        public bool IsValid { get; set; }
        public List<ValidationError> Errors { get; set; }
    }

    public class ValidationError
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }

    public class UserPreference
    {
        public string Theme { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
    }

    public class DemoListItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
