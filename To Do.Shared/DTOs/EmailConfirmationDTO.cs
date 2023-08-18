namespace To_Do.Shared;

public class EmailConfirmationDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string EmailConfirmationToken { get; set; }
}