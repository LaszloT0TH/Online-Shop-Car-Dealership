namespace CarDealershipASPNETMVC.Security
{
    /// <summary>
    /// EN
    /// This class holds the purpose strings required for encryption and decryption. 
    /// At the moment we have, only one, purpose string. 
    /// DE
    /// Diese Klasse enthält die für die Verschlüsselung und Entschlüsselung erforderlichen Zweckzeichenfolgen.
    /// Im Moment haben wir nur einen Purpose-String.
    /// HU
    /// Az útvonalértékek titkosításának és visszafejtésének lépései Célsztring létrehozása
    /// Ez az osztály tartalmazza a titkosításhoz és visszafejtéshez szükséges célkarakterláncokat.
    /// Jelenleg csak egy célkarakterláncunk van.
    /// </summary>
    public class DataProtectionPurposeStrings
    {
        public readonly string IdRouteValue = "IdRouteValue";
    }
}
