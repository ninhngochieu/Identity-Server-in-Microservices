using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IdentityMicroservices.Areas.Identity.Data;

// Add profile data for application users by adding properties to the IdentityMicroservicesUser class
public class ApplicationUser : IdentityUser
{
    public decimal Gil { get; set; }
    public DateTime CreateDate { get; set; }
}

