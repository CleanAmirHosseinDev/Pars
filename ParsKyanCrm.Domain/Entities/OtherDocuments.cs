using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class OtherDocuments
    {
        public int OtherDocumentId { get; set; }
        public int? CustomerId { get; set; }
        public string DocumentName { get; set; }
        public int? KindOfDocumentId { get; set; }
        public string IssuanceAuthority { get; set; }
        public string DocumentPicture { get; set; }
        public byte? IsActive { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual SystemSeting11 KindOfDocument { get; set; }
    }
}
