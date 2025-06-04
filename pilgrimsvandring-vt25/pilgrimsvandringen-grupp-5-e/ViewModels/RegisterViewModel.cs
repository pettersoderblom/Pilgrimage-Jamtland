using System.ComponentModel.DataAnnotations;

namespace pilgrimsvandringen_grupp_5_e.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ange ditt användarnamn")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Fyll i din e-post")]
        [EmailAddress(ErrorMessage = "Felaktigt format på e-posten")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ditt lösenord kräver en stor bokstav, en siffra och ett specialtecken")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Du måste bekräfta ditt lösenord")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Lösenorden matchar inte")]
        public string ConfirmPassword { get; set; }
    }
}
