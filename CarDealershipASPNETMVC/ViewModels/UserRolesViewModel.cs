namespace CarDealershipASPNETMVC.ViewModels
{
    /// <summary>
    /// EN
    /// In the UserRolesViewModel class, in addition to RoleId property, we have RoleName and IsSelected properties. 
    /// RoleName property is required so we can display the RoleName on the view. 
    /// IsSelected property is required to determine if the role is selected to be assigned to the given user. 
    /// We could include UserId property also in the UserRolesViewModel class, but as far as this view is concerned, 
    /// there is a one-to-many relationship from User to Roles. So, in order not to repeat UserId for each Role, 
    /// we will use ViewBag to pass UserId from controller to the view.
    /// GE
    /// In der Klasse UserRolesViewModel haben wir zusätzlich zur Eigenschaft RoleId die Eigenschaften RoleName und IsSelected.
    /// Die RoleName-Eigenschaft ist erforderlich, damit wir den RoleName in der Ansicht anzeigen können.
    /// Die IsSelected-Eigenschaft ist erforderlich, um zu bestimmen, ob die Rolle ausgewählt ist, um dem angegebenen Benutzer zugewiesen zu werden.
    /// Wir könnten die UserId-Eigenschaft auch in die UserRolesViewModel-Klasse aufnehmen, aber was diese Ansicht betrifft,
    /// Es besteht eine Eins-zu-Viele-Beziehung von Benutzer zu Rollen. Um also die Benutzer-ID nicht für jede Rolle zu wiederholen,
    /// Wir verwenden ViewBag, um die UserId vom Controller an die Ansicht zu übergeben.
    /// HU
    /// A UserRolesViewModel osztályban a RoleId tulajdonság mellett RoleName és IsSelected tulajdonságokkal is rendelkezünk.
    /// A RoleName tulajdonság megadása szükséges ahhoz, hogy a RoleName megjeleníthető legyen a nézetben.
    /// Az IsSelected tulajdonság szükséges annak meghatározásához, hogy a szerepkör ki van-e választva
    /// az adott felhasználóhoz való hozzárendelésre.A UserId tulajdonságot a UserRolesViewModel osztályba is belefoglalhatjuk,
    /// de ami ezt a nézetet illeti, egy-a-többhöz kapcsolat van a User és a Roles között.
    /// Tehát annak érdekében, hogy ne ismételjük meg a UserId-t minden szerepkörnél, a ViewBag segítségével
    /// átadjuk a UserId-t a vezérlőtől a nézethez.
    /// </summary>
    public class UserRolesViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
