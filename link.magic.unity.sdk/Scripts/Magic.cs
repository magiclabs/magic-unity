using link.magic.unity.sdk.Modules.Auth;
using link.magic.unity.sdk.Modules.User;
using link.magic.unity.sdk.Provider;
using link.magic.unity.sdk.Relayer;
using link.magic.unity.sdk.Utility;
using Nethereum.JsonRpc.Client;

namespace link.magic.unity.sdk
{
    public sealed class Magic
    {
        public UserModule User;
        public AuthModule Auth;
            
            // static instance
            public static Magic Instance;

            public RpcProvider Provider;

            //Constructor
            public Magic(string apikey)
            {
                EthNetworkConfiguration config = new EthNetworkConfiguration(EthNetwork.Mainnet);
                UrlBuilder urlBuilder = new UrlBuilder(apikey: apikey, ethNetwork: config);
                UrlBuilder.Instance = urlBuilder;
                
                Provider = new RpcProvider(urlBuilder);
                User = new UserModule(Provider);
                Auth = new AuthModule(Provider);
            }
    }   
    public enum EthNetwork
    {
        Mainnet,
        Kovan,
        Rinkeby,
        Rposten
    }
}