using Microsoft.AspNetCore.Identity;

namespace CarDealershipASPNETMVC.Security
{
    /// <summary>
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
    /// </summary>
    public class CustomEmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
    {
    }
}
