using System;
using System.Collections.Generic;

namespace ExamShvets.Models;

public partial class MaterialTypeImport
{
    public int MaterialTypeId { get; set; }

    public string MaterialType { get; set; } = null!;

    public string PercentageOfDefects { get; set; } = null!;

    public virtual ICollection<ProductsImport> ProductsImports { get; set; } = new List<ProductsImport>();
}
