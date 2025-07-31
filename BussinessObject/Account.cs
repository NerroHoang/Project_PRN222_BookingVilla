using System;
using System.Collections.Generic;

namespace BussinessObject;

public partial class Account
{
    public int IdAccount { get; set; }

    public string? UserName { get; set; }

    public string? PassWord { get; set; }

    public string? Role { get; set; }

    public bool Status { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}
