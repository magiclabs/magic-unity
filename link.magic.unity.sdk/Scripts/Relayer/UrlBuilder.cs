using System;

namespace link.magic.unity.sdk.Relayer
{
    [Serializable]
    public class UrlBuilder
    {
        public string apikey;
        public static string Host = "https://box.magic.link";
    }
    
    [Serializable]
    internal class EthNetworkBaseOptions: IBaseOptions
    {
        private EthNetworkConfiguration ETH_NETWORK;
        public string API_KEY { get; set; }
        public string locale { get; set; }
    }

    [Serializable]
    internal class CustomNodeBaseOptions: IBaseOptions
    {
        private CustomNodeConfiguration ETH_NETWORK;
        public string API_KEY { get; set; }
        public string locale { get; set; }
    }
    
    [Serializable]
    public class CustomNodeConfiguration
    {
        string rpcUrl;
        int chainId;

        public CustomNodeConfiguration(string rpcUrl, int chainId)
        {
            this.rpcUrl = rpcUrl;
            this.chainId = chainId;
        }
    }
    
    [Serializable]
    public class EthNetworkConfiguration
    {
        string network;

        public EthNetworkConfiguration(EthNetwork network)
        {
            this.network = network.ToString();
        }
    }
}



