using System;
using System.Collections.Generic;

namespace ExamShvets.Models;

public partial class PartnerTypeImport
{
    public int PartnerTypeId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<PartnersImport> PartnersImports { get; set; } = new List<PartnersImport>();
}
