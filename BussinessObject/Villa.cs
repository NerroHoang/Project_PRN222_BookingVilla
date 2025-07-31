using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject;

public partial class Villa
{
    public int IdVilla { get; set; }

    public string? Name { get; set; }

    public string? Describe { get; set; }

    public double? Point { get; set; }

    public int AmountOfPeople { get; set; }

    public int AmountOfRoom { get; set; }

    public bool? Status { get; set; }
    [NotMapped]
    public double Price { get; set; }

    public virtual ICollection<BookingOnline> BookingOnlines { get; set; } = new List<BookingOnline>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<ImageVilla> ImageVillas { get; set; } = new List<ImageVilla>();

    public virtual ICollection<PriceVilla> PriceVillas { get; set; } = new List<PriceVilla>();
}
