using System.ComponentModel.DataAnnotations;

namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs
{
    public class CreateCompanyRequest
    {
        [Display(Name = "User name")]
        public string UserName { get; set; } = null!;

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last name")]
        public string LastName { get; set; } = null!;

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; } = null!;

        [Display(Name = "Country Name")]
        public string Country { get; set; } = null!;

        [Required]
        [RegularExpression(pattern: "^0*?[1-9]\\d*$", ErrorMessage = "Please enter a valid {0}")]
        [Display(Name = "No Of Employee")]
        public int NoOfEmployee { get; set; }

        [Required]
        [Display(Name = "Contact No")]
        [RegularExpression(pattern: "^\\+?\\d{10,12}$", ErrorMessage = "Please enter valid Contact No")]
        public string ContactNo { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email address")]
        public string Email { get; set; } = null!;

        [Display(Name = "Created by")]
        public string CreatedBy { get; set; } = null!;

        [Display(Name = "Created date")]
        public string CreatedDate { get; set; } = null!;

        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare(otherProperty: "Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        [BooleanMustBeTrue(ErrorMessage = "Please Accept Terms And Conditions")]
        public bool Term { get; set; }

        public string RoleId { get; set; } = null!;
        public long EmployeeId { get; set; }
        public string EmployeeCode { get; set; } = null!;
        public List<string> Countries { get; set; } = new List<string>();
    }
}
