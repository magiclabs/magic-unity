using System;
using link.magic.unity.sdk.Provider;

namespace link.magic.unity.sdk.Modules.Auth
{
    public class AuthModule: BaseModule
    {
        internal AuthModule(RpcProvider provider): base(provider)
        {
            Provider = provider;
        }

        public async void loginWithMagicLink()
        {
            
        }
    }
    
    public enum AuthMethod
    { 
        magic_auth_login_with_magic_link,
        magic_auth_login_with_sms,
        magic_auth_login_with_email_otp
    }
}