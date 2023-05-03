using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CarDealershipASPNETMVC.Security
{
    /// <summary>
    /// EN
    /// It is the built-in DataProtectorTokenProvider class that generates the email confirmation token.
    /// Our custom provider class gets all the functionality required to generate tokens 
    /// by inheriting from the DataProtectorTokenProvider class. 
    /// We do not have to write any special logic in this custom provider class to generate tokens. 
    /// It will be taken care by the base DataProtectorTokenProvider class. 
    /// All we need is a constructor which in turn calls the base class constructor. 
    /// Our CustomEmailConfirmationTokenProviderOptions instance is passed to the base class constructor.
    /// GE
    /// Es ist die integrierte DataProtectorTokenProvider-Klasse, die das E-Mail-Bestätigungstoken generiert.
    /// Unsere benutzerdefinierte Anbieterklasse erhält alle Funktionen, die zum Generieren von Token erforderlich sind
    /// durch Erben von der DataProtectorTokenProvider-Klasse.
    /// Wir müssen keine spezielle Logik in diese benutzerdefinierte Anbieterklasse schreiben, um Token zu generieren.
    /// Es wird von der Basisklasse DataProtectorTokenProvider übernommen.
    /// Alles, was wir brauchen, ist ein Konstruktor, der wiederum den Konstruktor der Basisklasse aufruft.
    /// Unsere CustomEmailConfirmationTokenProviderOptions-Instanz wird an den Konstruktor der Basisklasse übergeben.
    /// HU
    /// Ez a beépített DataProtectorTokenProvider osztály, amely létrehozza az e-mail megerősítő jogkivonatot.
    /// Az egyéni szolgáltatói osztály a jogkivonatok létrehozásához szükséges összes funkciót megkapja
    /// a DataProtectorTokenProvider osztályból való örökléssel.
    /// A jogkivonatok létrehozásához nem kell speciális logikát írnunk ebben az egyéni szolgáltatói osztályban.
    /// Ezt az alap DataProtectorTokenProvider osztály fogja gondoskodni.Csak egy konstruktorra van szükségünk,
    /// amely viszont az alaposztály konstruktorának nevezi.
    /// A CustomEmailConfirmationTokenProviderOptions példányt a rendszer átadja az alaposztály konstruktorának.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>

    public class CustomEmailConfirmationTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public CustomEmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider,
                                        IOptions<CustomEmailConfirmationTokenProviderOptions> options,
                                        ILogger<DataProtectorTokenProvider<TUser>> logger)
            : base(dataProtectionProvider, options, logger)
        { }
    }
}
