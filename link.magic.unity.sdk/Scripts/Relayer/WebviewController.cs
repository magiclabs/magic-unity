using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using link.magic.unity.sdk.Provider;
using UnityEngine;

namespace link.magic.unity.sdk.Relayer
{
    public class WebviewController
    {
        private WebViewObject _webViewObject;
        private bool relayerReady;

        private Queue<string> _queue = new ();
        private Dictionary<int, Func<string, bool>> _messageHandlers = new ();
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
        internal void Enqueue(string message, int id, Func<string, bool> callback)
        {
            _queue.Enqueue(message);
            _messageHandlers.Add(id, callback);
            _dequeue();
        }

        private void _dequeue()
        {
            if (_queue.Count != 0 && relayerReady) {
                string message = _queue.Dequeue();
                _webViewObject.EvaluateJS($"window.dispatchEvent(new MessageEvent('message', {message}));");
                _dequeue();
            }
        }

        // private void _handleResponse(string )
        // {
        //
        // }
    }
}