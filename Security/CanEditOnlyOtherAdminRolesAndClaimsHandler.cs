using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CarDealershipASPNETMVC.Security
{
    /// <summary>
    /// Creating a custom authorization handler 
    /// It is in the authorization handler that we write our logic to allow or deny access to a resource like a controller action for example.
    /// To implement a handler you inherit from AuthorizationHandler<T>, and implement the HandleRequirementAsync() method. 
    /// The generic parameter<T> on the AuthorizationHandler<T> is the type of requirement.
    /// </summary>
    public class CanEditOnlyOtherAdminRolesAndClaimsHandler :
        AuthorizationHandler<ManageAdminRolesAndClaimsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ManageAdminRolesAndClaimsRequirement requirement)
        {
            // Explanation of authorization handler code
            // The AuthorizationHandlerContext resource property returns the protected resource.
            // In our case, we use this custom requirement to protect a controller's action method.
            // So the next line returns the control action protected as AuthorizationFilterContext,
            // and provides access to HttpContext, RouteData, and everything else provided by MVC and Razor Pages.
            var authFilterContext = context.Resource as AuthorizationFilterContext;


            // If AuthorizationFilterContext is NULL, we cannot check if the requirement is met or not, so we return Task.
            // CompletedTask and the access is not authorised.
            if (authFilterContext == null)
            {
                return Task.CompletedTask;
            }

            string loggedInAdminId =
                context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            string adminIdBeingEdited = authFilterContext.HttpContext.Request.Query["userId"];

            // Our requirement is met and the authorization succeeds 
            // If the user is in the Admin role AND has Edit Role claim type with a claim value of true
            // AND the logged -in user Id is NOT EQUAL TO the Id of the Admin user being edited
            if (context.User.IsInRole("Admin") &&
                context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true") &&
                adminIdBeingEdited.ToLower() != loggedInAdminId.ToLower())
            {
                // Succeed() method specifies that the requirement is successfully evaluated.
                // A Succeed() metódus azt adja meg, hogy a követelmény kiértékelése sikeresen megtörtént.
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
