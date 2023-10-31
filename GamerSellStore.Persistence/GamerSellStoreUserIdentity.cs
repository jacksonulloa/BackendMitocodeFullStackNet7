using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Persistence
{
    public class GamerSellStoreUserIdentity : IdentityUser
    {
        [StringLength(100)]
        public string FirstName { get; set; } = "";
        [StringLength(100)]
        public string LastName { get; set; } = "";
        public DocumentTypeEnum DocumentType { get; set; } = DocumentTypeEnum.Dni;
        public int Age { get; set; } = 0;
        [StringLength(20)]
        public string DocumentNumber { get; set; } = "";
    }

    public enum DocumentTypeEnum : short
    {
        Dni,
        Pasaporte,
        CarnetExtranjeria
    }
}
