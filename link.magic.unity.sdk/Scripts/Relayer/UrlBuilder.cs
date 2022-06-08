using System;
using JetBrains.Annotations;
using link.magic.unity.sdk.Modules;
using link.magic.unity.sdk.Utility;
using UnityEngine;

namespace link.magic.unity.sdk.Relayer
{
    public class UrlBuilder
    {
        public string apikey;
        internal static UrlBuilder Instance;
        // public static string Host = "https://box.magic.link";
        public static string Host = "http://192.168.1.101:3016";
        internal string EncodedParams;

        public UrlBuilder(string apikey, CustomNodeConfiguration customNode, string locale = "en-US")
        {
            CustomNodeOptions options = new CustomNodeOptions();
            options.ETH_NETWORK = customNode;
            options.locale = locale;
            options.API_KEY = apikey;
            
            string optionsJsonString = JsonUtility.ToJson(options);
            EncodedParams = MagicUtility.BtoA(optionsJsonString);
        }
        
        public UrlBuilder(string apikey, EthNetworkConfiguration ethNetwork, string locale = "en-US")
        {
            EthNetworkOptions options = new EthNetworkOptions();
            options.ETH_NETWORK = ethNetwork;
            options.locale = locale;
            options.API_KEY = apikey;

            string optionsJsonString = JsonUtility.ToJson(options);
            EncodedParams = MagicUtility.BtoA(optionsJsonString);
        }
    }
    
    [Serializable]
    internal class EthNetworkOptions: BaseOptions
    {
        public EthNetworkConfiguration ETH_NETWORK;
    }

    [Serializable]
    internal class CustomNodeOptions: BaseOptions
    {
        public CustomNodeConfiguration ETH_NETWORK;
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

    [Serializable]
    public class BaseOptions
    {
        public string host = UrlBuilder.Host;
        public string sdk = "magic-sdk-unity";
        public string API_KEY;
        public string locale;
    }
    
}



