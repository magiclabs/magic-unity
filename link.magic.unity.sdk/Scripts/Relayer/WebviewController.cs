using System.Collections.Generic;
using link.magic.unity.sdk.Provider;
using UnityEngine;

namespace link.magic.unity.sdk.Relayer
{
    public class WebviewController
    {
        private WebViewObject _webViewObject;
        private bool relayerReady = false;

        private string[] _queue = { };
        public WebviewController()
        {
            // instantiate webview 
            _webViewObject = (new GameObject("WebViewObject")).AddComponent<WebViewObject>();
            _webViewObject.Init(
                ld: _onLoad
            );
            
 
            // webViewObject.SetVisibility(true);
            Debug.Log("create webview");
        }
        internal void Load(string url)
        {
            _webViewObject.LoadURL(url);
        }

        // onLoad hooks 
        private void _onLoad(string msg)
        {
            Debug.Log("Loaded");
            relayerReady = true;
        }

        /// <summary>
        /// Queue
        /// </summary>
        internal void enqueue(RelayerRequest request)
        {
            
        }

        private void dequeue()
        {
            
        }
    }
}