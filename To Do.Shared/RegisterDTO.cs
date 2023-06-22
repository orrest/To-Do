namespace To_Do.Shared;

public class RegisterDTO
{
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public RegisterDTO(
        string emailAddress, 
        string password, 
        string confirmPassword)
    {
        EmailAddress = emailAddress;
        Password = password;
        ConfirmPassword = confirmPassword;
    }
}