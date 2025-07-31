using System;
using System.Collections.Generic;

namespace BussinessObject;

public partial class ImageVilla
{
    public int IdImgVilla { get; set; }

    public string? Image { get; set; }

    public int? IdVilla { get; set; }

    public virtual Villa? IdVillaNavigation { get; set; }
}
