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
        private readonly WebViewObject _webViewObject;
        private bool _relayerReady;
        private bool _relayerLoaded;

        private Queue<string> _queue = new ();
        private Dictionary<int, Func<string, bool>> _messageHandlers = new ();
        public WebviewController()
        {
            // instantiate webview 
            _webViewObject = (new GameObject("WebViewObject")).AddComponent<WebViewObject>();
            _webViewObject.Init(
                cb: _cb,
                ld: _onLoad
            );
        }
        internal void Load(string url)
        {
            _webViewObject.LoadURL(url);
        }

        // onLoad hooks 
        private void _onLoad(string msg)
        {
            _relayerLoaded = true;
        }
        
        // callback js hooks
        private void _cb(string msg)
        {
            // Do Simple Relayer JSON Deserialization just to fetch ids for handlers
            RelayerResponse<object> res = JsonUtility.FromJson<RelayerResponse<object>>(msg);
            string msgType = res.msgType;

            var method = msgType.Split("-")[0];
            
            switch (method)
            {
                case nameof(InboundMessageType.MAGIC_OVERLAY_READY):
                    _relayerReady = true;
                    _dequeue();
                    break;
                case nameof(InboundMessageType.MAGIC_SHOW_OVERLAY):
                    _webViewObject.SetVisibility(true);
                    break;
                case nameof(InboundMessageType.MAGIC_HIDE_OVERLAY):
                    _webViewObject.SetVisibility(false);
                    break;
                case nameof(InboundMessageType.MAGIC_HANDLE_EVENT):
                    //Todo Unsupported for now
                    break;
                case nameof(InboundMessageType.MAGIC_HANDLE_RESPONSE):
                    _handleResponse(msg, res);
                    break;
                default:
                    //Todo add error
                    break;
            }
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
            if (_queue.Count != 0 && _relayerReady && _relayerLoaded) {
                string message = _queue.Dequeue();

                _webViewObject.EvaluateJS($"window.dispatchEvent(new MessageEvent('message', {{ 'data': {message} }}));");
                
                _dequeue();
            }
        }

        private void _handleResponse(string originalMsg, RelayerResponse<object> relayerResponse)
        {
            var payloadId = relayerResponse.response.id;
            var handler = _messageHandlers[payloadId];
            handler(originalMsg);
            _messageHandlers.Remove(payloadId);
        }
    }
}