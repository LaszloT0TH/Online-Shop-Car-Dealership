namespace CarDealershipASPNETMVC.ViewModels
{
    /// <summary>
    /// In the UserRolesViewModel class, in addition to RoleId property, we have RoleName and IsSelected properties. 
    /// RoleName property is required so we can display the RoleName on the view. 
    /// IsSelected property is required to determine if the role is selected to be assigned to the given user. 
    /// We could include UserId property also in the UserRolesViewModel class, but as far as this view is concerned, 
    /// there is a one-to-many relationship from User to Roles. So, in order not to repeat UserId for each Role, 
    /// we will use ViewBag to pass UserId from controller to the view.
    /// </summary>
    public class UserRolesViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
