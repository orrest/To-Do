namespace To_Do.Helpers;

public class JwtOptions
{
    public string SigningKey { get; set; }
    public int ExpireSeconds { get; set; }
}