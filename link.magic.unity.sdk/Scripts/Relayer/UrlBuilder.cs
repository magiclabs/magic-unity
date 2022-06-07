using System;
using JetBrains.Annotations;
using link.magic.unity.sdk.Utility;
using UnityEngine;

namespace link.magic.unity.sdk.Relayer
{
    [Serializable]
    public class UrlBuilder
    {
        public string apikey;
        internal static UrlBuilder Instance;
        // public static string Host = "https://box.magic.link";
        public static string Host = "http://192.168.1.101:3016";
        internal string EncodedParams;

        public UrlBuilder(string apikey, CustomNodeConfiguration customNode, string locale = "en-US")
        {
            CustomNodeBaseOptions options = new CustomNodeBaseOptions();
            options.ETH_NETWORK = customNode;
            options.locale = locale;
            options.API_KEY = apikey;
            
            _buildEncodeParams(this);
        }
        
        public UrlBuilder(string apikey, EthNetworkConfiguration ethNetwork, string locale = "en-US")
        {
            EthNetworkBaseOptions options = new EthNetworkBaseOptions();
            options.ETH_NETWORK = ethNetwork;
            options.locale = locale;
            options.API_KEY = apikey;

            _buildEncodeParams(this);
        }

        private void _buildEncodeParams(UrlBuilder builder)
        {
            string optionsJsonString = JsonUtility.ToJson(builder);
            EncodedParams = MagicUtility.Atob(optionsJsonString);
        }
    }
    
    [Serializable]
    internal class EthNetworkBaseOptions: IBaseOptions
    {
        internal EthNetworkConfiguration ETH_NETWORK;
        public string API_KEY { get; set; }
        public string locale { get; set; }
    }

    [Serializable]
    internal class CustomNodeBaseOptions: IBaseOptions
    {
        internal CustomNodeConfiguration ETH_NETWORK;
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
            this.network = nameof(network);
        }
    }
}



