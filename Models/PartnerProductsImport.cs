using System;
using System.Collections.Generic;

namespace ExamShvets.Models;

public partial class PartnerProductsImport
{
    public int SalesId { get; set; }

    public int ProductId { get; set; }

    public int PartnerId { get; set; }

    public int Count { get; set; }

    public DateOnly DataSales { get; set; }

    public virtual PartnersImport Partner { get; set; } = null!;

    public virtual ProductsImport Product { get; set; } = null!;
}
