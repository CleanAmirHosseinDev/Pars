using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Domain.Entities.Users
{
    public class RequestForReating
    {

        [Key]
        public int RequestID { get; set; }

        public int? RequestNo { get; set; }

        [ForeignKey("Customer")]
        public int? CustomerID { get; set; }

        [ForeignKey("User")]
        public int? UserID { get; set; }

        public DateTime? DateOfRequest { get; set; }

        public DateTime? DateOfAssignUsers { get; set; }

        public DateTime? DateOfAcceptRequest { get; set; }

        public DateTime? DateOfConfirmed { get; set; }

        public int? Status { get; set; }

        public virtual Customers Customer { get; set; }

        public virtual Users User { get; set; }

    }
}
