using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CarDealershipASPNETMVC.Security
{
    /// <summary>
    /// It is the built-in DataProtectorTokenProvider class that generates the email confirmation token.
    /// Our custom provider class gets all the functionality required to generate tokens 
    /// by inheriting from the DataProtectorTokenProvider class. 
    /// We do not have to write any special logic in this custom provider class to generate tokens. 
    /// It will be taken care by the base DataProtectorTokenProvider class. 
    /// All we need is a constructor which in turn calls the base class constructor. 
    /// Our CustomEmailConfirmationTokenProviderOptions instance is passed to the base class constructor.
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
