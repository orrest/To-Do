namespace To_Do.API.Models;

public class RegisterResult
{
    public bool Successful { get; set; }
    public IEnumerable<string> Errors { get; set; }
}