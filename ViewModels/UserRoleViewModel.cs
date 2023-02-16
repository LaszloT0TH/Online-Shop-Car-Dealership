namespace CarDealershipASPNETMVC.ViewModels
{
    /// <summary>
    /// In the UserRoleViewModel class, in addition to UserId property, we have UserName and IsSelected properties. 
    /// UserName property is required so we can display the UserName on the view. 
    /// IsSelected property is required to determine if the user is selected to be a member of the role.
    /// We could include RoleId property also in the UserRoleViewModel class, but as far as this view is concerned, 
    /// there is a one-to-many relationship from Role to Users. So, in order not to repeat RoleId for each User, 
    /// we will use ViewBag to pass RoleId from controller to the view.
    /// </summary>
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
