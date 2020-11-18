using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KashBookStore.Models.DomainModels
{
    public class User : IdentityUser
    {
        [NotMapped]
        public IList<String> RoleNames { get; set; }
    }
}
