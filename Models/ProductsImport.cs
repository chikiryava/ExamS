using System;
using System.Collections.Generic;

namespace ExamShvets.Models;

public partial class ProductsImport
{
    public int ProductId { get; set; }

    public int TypeId { get; set; }

    public string Title { get; set; } = null!;

    public int Article { get; set; }

    public decimal MinCostForPartner { get; set; }

    public int MaterialId { get; set; }

    public virtual MaterialTypeImport Material { get; set; } = null!;

    public virtual ICollection<PartnerProductsImport> PartnerProductsImports { get; set; } = new List<PartnerProductsImport>();

    public virtual ProductTypeImport Type { get; set; } = null!;
}
