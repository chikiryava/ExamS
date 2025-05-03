using System;
using System.Collections.Generic;

namespace ExamShvets.Models;

public partial class ProductTypeImport
{
    public int TypeProductId { get; set; }

    public string Title { get; set; } = null!;

    public decimal CoefTypeProduct { get; set; }

    public virtual ICollection<ProductsImport> ProductsImports { get; set; } = new List<ProductsImport>();
}
