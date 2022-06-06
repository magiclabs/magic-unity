using link.magic.unity.sdk.Modules.Auth;
using link.magic.unity.sdk.Modules.User;
using link.magic.unity.sdk.Provider;
using link.magic.unity.sdk.Relayer;

namespace link.magic.unity.sdk
{
    public class Magic
    {
        public UserModule User;
        public AuthModule Auth;
            
            // static instance
            public static Magic Instance;
            
            public RpcProvider Provider;

            //Constructor
            public Magic(string apikey)
            {
                Provider = new RpcProvider(apikey);
                User = new UserModule(Provider);
                Auth = new AuthModule(Provider);
            }
        }   
    public enum EthNetwork
    {
        Mainnet,
        kovan,
        rinkeby,
        rposten
    }
}