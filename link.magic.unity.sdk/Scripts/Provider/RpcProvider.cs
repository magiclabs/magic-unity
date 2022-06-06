using System;
using link.magic.unity.sdk.Relayer;
using UnityEngine;

namespace link.magic.unity.sdk.Provider
{
    public class RpcProvider
    {
        private WebviewController _relayer = new WebviewController();

        public RpcProvider(string apikey)
        {
            string url = _generateBoxUrl(apikey);

            // init relayer
            _relayer.Load(url);
        }

        private string _generateBoxUrl(string apikey)
        {
            UrlBuilder urlBuilder = new UrlBuilder();
            urlBuilder.apikey = apikey;
            // encode options params to base 64
            string optionsJsonString = JsonUtility.ToJson(urlBuilder);
            Debug.Log(optionsJsonString);
            byte[] optionsInBytes = System.Text.Encoding.UTF8.GetBytes(optionsJsonString);
            string url = $"{UrlBuilder.Host}/send/?params={Convert.ToBase64String(optionsInBytes)}";
            
            return url;
        }

        public async void SendAsync()
        {
            _relayer.enqueue();
        }
        
        
        
    }
}