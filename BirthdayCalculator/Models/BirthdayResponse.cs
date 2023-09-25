using System.ComponentModel.DataAnnotations;

namespace BirthdayCalculator.Models
{
    public class BirthdayResponse
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter the day of your birthdate")]
        public int? Day { get; set; }

        [Required(ErrorMessage = "Please enter the year of your birthdate")]
        public int? Month { get; set; }

        [Required(ErrorMessage = "Please enter the year of your birthdate")]
        public int? Year { get; set; }

        public DateTime getBirthDate()
        {
            if (Year != null && Month != null && Day != null)
            {
                return new DateTime((int)Year, (int)Month, (int)Day); 
            }
            return DateTime.Now;
        }
    }
}
