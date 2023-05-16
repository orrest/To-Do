using System;

namespace To_Do.Models;

public class BaseModel
{
    public int Id { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime? UpdateTime { get; set; }
}