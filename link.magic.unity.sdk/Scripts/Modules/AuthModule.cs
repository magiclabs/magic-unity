using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using link.magic.unity.sdk.Provider;
using Nethereum.JsonRpc.Client;

namespace link.magic.unity.sdk.Modules.Auth
{
    public class AuthModule: BaseModule
    {
        internal AuthModule(RpcProvider provider): base(provider)
        {
            Provider = provider;
        }

        public async Task<string> LoginWithMagicLink(string email, bool showUI = true)
        {
            LoginWithMagicLinkConfiguration config = new LoginWithMagicLinkConfiguration(email, showUI);
            LoginWithMagicLinkConfiguration[] paramList = { config };
            MagicRpcRequest<LoginWithMagicLinkConfiguration> request = new MagicRpcRequest<LoginWithMagicLinkConfiguration>(method: nameof(AuthMethod.magic_auth_login_with_magic_link), parameters: paramList);
            return await Provider.SendAsync<LoginWithMagicLinkConfiguration, string>(request);
        }

        [Serializable]
        internal class LoginWithMagicLinkConfiguration: BaseConfiguration
        {
            public bool showUI;
            public string email;

            public LoginWithMagicLinkConfiguration(string email, bool showUI = true )
            {
                this.showUI = showUI;
                this.email = email;
            } 
        }
        
        [Serializable]
        internal class LoginWithSmsConfiguration : BaseConfiguration
        {
            public bool showUI;
            public string phoneNumber;

            public LoginWithSmsConfiguration(string phoneNumber, bool showUI = true )
            {
                this.showUI = showUI;
                this.phoneNumber = phoneNumber;
            }  
        }
        
        [Serializable]
        internal class LoginWithEmailOtpConfiguration : BaseConfiguration
        {
            public string email;

            public LoginWithEmailOtpConfiguration(string email)
            {
                this.email = email;
            }  
        }
    }

    internal enum AuthMethod
    { 
        magic_auth_login_with_magic_link,
        magic_auth_login_with_sms,
        magic_auth_login_with_email_otp
    }
}