using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamShvets.Models;

public partial class PartnersImport
{
    [Key]
    [Column("partner_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PartnerId { get; set; }

    public int PartnerTypeId { get; set; }

    public string Title { get; set; } = null!;

    public string Director { get; set; } = null!;

    public string EmailPartner { get; set; } = null!;

    public string PhonePartner { get; set; } = null!;

    public string AddressPartner { get; set; } = null!;

    public long Inn { get; set; }

    public int Rating { get; set; }

    public virtual ICollection<PartnerProductsImport> PartnerProductsImports { get; set; } = new List<PartnerProductsImport>();

    public virtual PartnerTypeImport PartnerType { get; set; } = null!;
}
