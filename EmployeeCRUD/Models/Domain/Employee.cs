using System.ComponentModel.DataAnnotations;

namespace EmployeeCRUD.Models.Domain
{
	public class Employee
	{
        [Key]
		public int EmployeeId { get; set; }

		[Required(ErrorMessage = "Name is required")]
		public string EmployeeName { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Salary is required")]
		public long Salary { get; set; }

		[Required(ErrorMessage = "Date of Birth is required")]
		public DateTime DateofBirth { get; set; }

		[Required(ErrorMessage = "Department is required")]
		public string Department { get; set; }
	}
}
