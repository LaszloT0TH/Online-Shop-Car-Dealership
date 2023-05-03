namespace CarDealershipASPNETMVC.ViewModels
{
    /// <summary>
    /// EN
    /// In the UserRoleViewModel class, in addition to UserId property, we have UserName and IsSelected properties. 
    /// UserName property is required so we can display the UserName on the view. 
    /// IsSelected property is required to determine if the user is selected to be a member of the role.
    /// We could include RoleId property also in the UserRoleViewModel class, but as far as this view is concerned, 
    /// there is a one-to-many relationship from Role to Users. So, in order not to repeat RoleId for each User, 
    /// we will use ViewBag to pass RoleId from controller to the view.
    /// GE
    /// In der Klasse UserRoleViewModel haben wir zusätzlich zur Eigenschaft UserId die Eigenschaften UserName und IsSelected.
    /// UserName-Eigenschaft ist erforderlich, damit wir den UserName in der Ansicht anzeigen können.
    /// Die IsSelected-Eigenschaft ist erforderlich, um zu bestimmen, ob der Benutzer als Mitglied der Rolle ausgewählt wurde.
    /// Wir könnten die Eigenschaft RoleId auch in die Klasse UserRoleViewModel aufnehmen, aber was diese Ansicht betrifft,
    /// Es besteht eine Eins-zu-Viele-Beziehung von Rolle zu Benutzern. Um die RoleId nicht für jeden Benutzer zu wiederholen,
    /// Wir verwenden ViewBag, um die RoleId vom Controller an die Ansicht zu übergeben.
    /// HU
    /// A UserRoleViewModel osztályban a UserId tulajdonságon kívül UserName és IsSelected tulajdonságokkal is rendelkezünk.
    /// A UserName tulajdonság szükséges ahhoz, hogy a Felhasználónevet megjeleníthessük a nézetben.
    /// Az IsSelected tulajdonság szükséges annak meghatározásához, hogy a felhasználó a szerepkör tagjának van-e kiválasztva.
    /// Felvehetnénk a RoleId tulajdonságot a UserRoleViewModel osztályba is, de ami ezt a nézetet illeti,
    /// a szerep és a felhasználók között egy a többhez kapcsolat van. Tehát, hogy ne ismétlődjön meg minden egyes felhasználó szerepazonosítója,
    /// a ViewBagot használjuk a RoleId átadására a vezérlőről a nézetnek.    
    /// </summary>
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
