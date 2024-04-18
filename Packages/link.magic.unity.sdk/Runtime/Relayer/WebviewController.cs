using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MagicSDK.Provider;
using VoltstroStudios.UnityWebBrowser;
using VoltstroStudios.UnityWebBrowser.Core;
using VoltstroStudios.UnityWebBrowser.Shared.Core;
using VoltstroStudios.UnityWebBrowser.Core.Engines;
using VoltstroStudios.UnityWebBrowser.Communication;
using VoltstroStudios.UnityWebBrowser.Input;
using static VoltstroStudios.UnityWebBrowser.Core.Engines.Engine;

namespace MagicSDK.Relayer
{
    public class WebviewController
    {

        private readonly Dictionary<int, Func<string, bool>> _messageHandlers = new();


        private readonly Queue<string> _queue = new();
        private bool _relayerLoaded;
        private bool _relayerReady;


        // Windows and Non-Windows have different Browser implementations
#if !UNITY_EDITOR_WIN || !UNITY_STANDALONE_WIN
        // NON-WINDOWS
        private GameObject macCanvas;

        private readonly WebViewObject _webViewObject;

        public WebviewController(GameObject canvas)
        {
            // instantiate webview 
            _webViewObject = new GameObject("WebViewObject").AddComponent<WebViewObject>();
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
            macCanvas = new GameObject();
            macCanvas.AddComponent<Canvas>();
            // macCanvas = canvasContainer.GetComponent<Canvas>();
            macCanvas.transform.localScale = Vector3.zero;

            _webViewObject.canvas = macCanvas;
            if (!canvas)
            {
                Debug.LogWarning("Magic Canvas is required for OSX and Editor support");
            }

            
#endif
            _webViewObject.Init(
                cb: _cb,
                ld: (msg) =>
                {
                    _relayerLoaded = true;
                },
                httpErr: (msg) =>
                {
                    Debug.Log(string.Format("MagicUnity, LoadRelayerHttpError[{0}]", msg));
                },
                err: (msg) =>
                {
                    Debug.Log(string.Format("MagicUnity, LoadRelayerError[{0}]", msg));
                }
            );
        }

        private void ShowBrowser()
        {
            macCanvas.transform.localScale = Vector3.one;
            _webViewObject.SetVisibility(true);
        }

        private void HideBrowser()
        {
            _webViewObject.SetVisibility(false);
        }

        internal void Load(string url)
        {
            _webViewObject.LoadURL(url);
        }

        private void ExecuteJavaScript(string js)
        {
            _webViewObject.EvaluateJS(js);
        }
#else
        // WINDOWS

        private WebBrowserClient webClient;
        private RectTransform uwbRectTransform;

        public WebviewController()
        {
            GameObject webviewContainer = new GameObject("WebViewObjectContainer");

            //Create canvas
            Canvas canvas = webviewContainer.AddComponent<Canvas>();
            canvas.sortingOrder = 10;
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            // canvas.worldCamera = Camera.main;

            CanvasScaler canvasScaler = webviewContainer.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0;

            webviewContainer.AddComponent<GraphicRaycaster>();
            
            //Child object, where raw image and UWB itself will live
            GameObject uwbGameObject = new("UWBContainer");
            uwbGameObject.transform.SetParent(webviewContainer.transform);
            
            //Configure rect transform
            uwbRectTransform = uwbGameObject.AddComponent<RectTransform>();
            uwbRectTransform.anchorMin = Vector2.zero;
            uwbRectTransform.anchorMax = Vector2.one;
            uwbRectTransform.pivot = new Vector2(0.5f, 0.5f);
            uwbRectTransform.localScale = Vector3.one;
            uwbRectTransform.offsetMin = Vector2.zero;
            uwbRectTransform.offsetMax = Vector2.zero;
            
            //Add raw image
            uwbGameObject.AddComponent<RawImage>();
            
            //UWB Pre-Setup
            
            //Create engine dynamically
            EngineConfiguration engineConfig = ScriptableObject.CreateInstance<EngineConfiguration>();
            engineConfig.engineAppName = "UnityWebBrowser.Engine.Cef";
            
#if UNITY_EDITOR
            engineConfig.engineFiles = new[] { 
                new Engine.EnginePlatformFiles
                {
                    platform = Platform.Windows64,
                    engineFileLocation = "Packages/dev.voltstro.unitywebbrowser.engine.cef.win.x64/Engine~/"
                },
                new Engine.EnginePlatformFiles
                {
                    platform = Platform.Linux64,
                    engineFileLocation = "Packages/dev.voltstro.unitywebbrowser.engine.cef.linux.x64/Engine~/"
                }
            };
#endif
            
            //Create coms layer dynamically
            CommunicationLayer comsLayer = ScriptableObject.CreateInstance<TCPCommunicationLayer>();
            
            //Create input handler dynamically
            WebBrowserInputHandler inputHandler = ScriptableObject.CreateInstance<WebBrowserOldInputHandler>();
            
            //UWB Object Setup
            WebBrowserUIBasic webBrowser = uwbGameObject.AddComponent<WebBrowserUIBasic>();
            webBrowser.browserClient.engine = engineConfig;
            webBrowser.browserClient.communicationLayer = comsLayer;
            webBrowser.inputHandler = inputHandler;

            // configure browser settings
            BaseUwbClientManager clientmanager = uwbGameObject.GetComponent<BaseUwbClientManager>();
            webClient = clientmanager.browserClient;
            webClient.jsMethodManager.jsMethodsEnable = true;
            webClient.initialUrl = "https://box.magic.link";
            webClient.RegisterJsMethod<string>("_cb", _cb);

            // hide the browser by default
            uwbRectTransform.localScale = Vector3.zero;
        }

         private void ShowBrowser() {
            // this is a solution to be able to show the browser from its hidden state
            uwbRectTransform.localScale = Vector3.one;
         }

         private void HideBrowser() {
            // this is a solution to be able to hide the browser but keep it actively processing
            uwbRectTransform.localScale = Vector3.zero;
        }

        internal void Load(string url) {
            DelayLoadUrl(url);
        }

        private async void DelayLoadUrl(string url) {
            await Task.Delay(1500);
            _relayerLoaded = true;
            webClient.LoadUrl(url);
        }

        private void ExecuteJavaScript(string js) {
            webClient.ExecuteJs(js);
        }
#endif

        // Common Methods

        // callback js hooks
        private void _cb(string msg)
        {
            // Debug.Log($"MagicUnity Received Message from Relayer: {msg}");
            // Do SimRle Relayer JSON Deserialization just to fetch ids for handlers

            var res = JsonUtility.FromJson<RelayerResponse<object>>(msg);
            var msgType = res.msgType;
            var method = msgType.Split("-")[0];

            switch (method)
            {
                case nameof(InboundMessageType.MAGIC_OVERLAY_READY):
                    _relayerReady = true;
                    _dequeue();
                    break;
                case nameof(InboundMessageType.MAGIC_SHOW_OVERLAY):
                    ShowBrowser();
                    break;
                case nameof(InboundMessageType.MAGIC_HIDE_OVERLAY):
                    HideBrowser();
                    break;
                case nameof(InboundMessageType.MAGIC_HANDLE_EVENT):
                    //Todo Unsupported for now
                    break;
                case nameof(InboundMessageType.MAGIC_HANDLE_RESPONSE):
                    _handleResponse(msg, res);
                    break;
            }
        }


        /// <summary>
        ///     Queue
        /// </summary>
        internal void Enqueue(string message, int id, Func<string, bool> callback)
        {
            _queue.Enqueue(message);
            _messageHandlers.Add(id, callback);
            _dequeue();
        }

        private void _dequeue()
        {
            if (_queue.Count != 0 && _relayerReady && _relayerLoaded)
            {
                var message = _queue.Dequeue();

                // Debug.Log($"MagicUnity Send Message to Relayer: {message}");
                ExecuteJavaScript($"window.dispatchEvent(new MessageEvent('message', {{ 'data': {message} }}));");
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