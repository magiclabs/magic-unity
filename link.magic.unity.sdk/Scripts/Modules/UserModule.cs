using System.Runtime.CompilerServices;
using System.Transactions;
using link.magic.unity.sdk.Provider;

namespace link.magic.unity.sdk.Modules.User
{
    public class UserModule : BaseModule
    {
        internal UserModule(RpcProvider provider) : base(provider)
        {
            Provider = provider;
        }
    }
}