using System;
using System.Collections.Generic;

namespace BussinessObject;

public partial class Transaction
{
    public int IdTransactions { get; set; }

    public double Price { get; set; }

    public DateTime Date { get; set; }

    public virtual BookingOnline IdTransactionsNavigation { get; set; } = null!;
}
