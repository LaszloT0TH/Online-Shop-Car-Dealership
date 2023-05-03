using Microsoft.AspNetCore.Identity;

namespace CarDealershipASPNETMVC.Security
{
    /// <summary>
    /// EN
    /// If you want to set the lifespan of just a specific type of token,
    /// you can do so by creating a custom token provider. 
    /// For example, let's set the lifespan of email confirmation token type to 3 days.
    /// Create custom email confirmation token provider options By default, 
    /// it is the built-in DataProtectionTokenProviderOptions class that controls 
    /// the token lifespan of all token types.If you want to set a specific lifespan 
    /// for just the email confirmation token type, 
    /// create a CustomEmailConfirmationTokenProviderOptions class.  
    /// Make this custom class inherit from the built-in DataProtectionTokenProviderOptions class. 
    /// There are 2 important reasons why we do this. 
    /// The TokenLifespan property is inherited from the base class and 
    /// It allows an instance of this class to be passed as an argument to the DataProtectorTokenProvider class.
    /// GE
    /// Wenn Sie die Lebensdauer nur eines bestimmten Tokentyps festlegen möchten,
    /// Sie können dies tun, indem Sie einen benutzerdefinierten Token-Anbieter erstellen.
    /// Legen wir beispielsweise die Lebensdauer des E-Mail-Bestätigungs-Tokentyps auf 3 Tage fest.
    /// Benutzerdefinierte E-Mail-Bestätigungs-Tokenanbieteroptionen erstellen Standardmäßig
    /// Es ist die integrierte DataProtectionTokenProviderOptions-Klasse, die steuert
    /// die Token-Lebensdauer aller Token-Typen. Wenn Sie eine bestimmte Lebensdauer festlegen möchten
    /// nur für den Typ des E-Mail-Bestätigungs-Tokens,
    /// Erstellen Sie eine CustomEmailConfirmationTokenProviderOptions-Klasse.
    /// Lassen Sie diese benutzerdefinierte Klasse von der integrierten DataProtectionTokenProviderOptions-Klasse erben.
    /// Es gibt 2 wichtige Gründe, warum wir dies tun.
    /// Die TokenLifespan-Eigenschaft wird von der Basisklasse und geerbt
    /// Ermöglicht die Übergabe einer Instanz dieser Klasse als Argument an die DataProtectorTokenProvider-Klasse.
    /// HU
    /// Egyéni e-mail-megerősítő jogkivonat-szolgáltató beállításainak
    /// létrehozása Alapértelmezés szerint a beépített DataProtectionTokenProviderOptions osztály
    /// szabályozza az összes jogkivonattípus jogkivonat-élettartamát.
    /// Ha csak az e-mail-megerősítő jogkivonat típusához szeretne beállítani egy adott élettartamot,
    /// hozzon létre egy CustomEmailConfirmationTokenProviderOptions osztályt.
    /// Öröklődjön ez az egyéni osztály a beépített DataProtectionTokenProviderOptions osztálytól.
    /// Ennek 2 fontos oka van.
    /// A TokenLifespan tulajdonság az alaposztályból öröklődik, és
    /// Lehetővé teszi, hogy az osztály egy példánya argumentumként legyen átadva a DataProtectorTokenProvider osztálynak.

    /// </summary>
    public class CustomEmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
    {
    }
}
