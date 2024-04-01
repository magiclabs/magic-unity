using MagicSDK.Modules;
using MagicSDK.Provider;
using MagicSDK.Relayer;
using UnityEngine;

namespace MagicSDK
{
    public sealed class Magic
    {
        // static instance
        public static Magic Instance;
        public readonly AuthModule Auth;

        public readonly RpcProvider Provider;
        public readonly UserModule User;

        //Constructor
        public Magic(string apikey, EthNetwork network = EthNetwork.Mainnet, string locale = "en-US", GameObject canvas = null)
        {
            var urlBuilder = new UrlBuilder(apikey, network, locale);
            UrlBuilder.Instance = urlBuilder;

            Provider = new RpcProvider(urlBuilder, canvas);
            User = new UserModule(Provider);
            Auth = new AuthModule(Provider);
        }

        public Magic(string apikey, CustomNodeConfiguration config, string locale = "en-US", GameObject canvas = null)
        {
            var urlBuilder = new UrlBuilder(apikey, config, locale);
            UrlBuilder.Instance = urlBuilder;

            Provider = new RpcProvider(urlBuilder, canvas);
            User = new UserModule(Provider);
            Auth = new AuthModule(Provider);
        }
    }

    public enum EthNetwork
    {
        Mainnet,
        Goerli,
        Sepolia
    }
}
