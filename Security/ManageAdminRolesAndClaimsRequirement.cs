using Microsoft.AspNetCore.Authorization;

namespace CarDealershipASPNETMVC.Security
{
    /// <summary>
    /// Create a custom authorization requirement
    /// create a class that implements the IAuthorizationRequirement interface.
    /// This is an empty marker surface, which means there is nothing on this surface,
    /// which our custom requirements class should implement.
    /// </summary>
    public class ManageAdminRolesAndClaimsRequirement : IAuthorizationRequirement
    { 

    }
}
