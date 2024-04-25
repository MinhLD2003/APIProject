using System;
using System.Collections.Generic;

namespace Project.API.Models;

public partial class TblProductImage
{
    public int ImageId { get; set; }

    public int? ProductCode { get; set; }

    public byte[]? ProductImage { get; set; }

    public virtual TblProduct? ProductCodeNavigation { get; set; }
}
