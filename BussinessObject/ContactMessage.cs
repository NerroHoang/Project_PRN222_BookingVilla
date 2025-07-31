using System;
using System.Collections.Generic;

namespace BussinessObject;

public partial class ContactMessage
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Message { get; set; }

    public DateOnly? Date { get; set; }
}
