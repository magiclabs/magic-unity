using System;
using System.Threading.Tasks;
using link.magic.unity.sdk.Relayer;
using Nethereum.JsonRpc.Client;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;
namespace link.magic.unity.sdk.Provider

{
    public class RpcProvider: RequestInterceptor
    {
        private WebviewController _relayer = new WebviewController();

        public RpcProvider(UrlBuilder urlBuilder)
        {
            string url = _generateBoxUrl(urlBuilder);

            // init relayer
            _relayer.Load(url);
        }

        private string _generateBoxUrl(UrlBuilder urlBuilder)
        {
            // encode options params to base 64
            string optionsJsonString = JsonUtility.ToJson(urlBuilder);
            Debug.Log(optionsJsonString);
 
            string url = $"{UrlBuilder.Host}/send/?params={urlBuilder.EncodedParams}";
            
            return url;
        }
        
        // a send function for Magic internal calls
        // public async Task<T> MagicSendAsync<T>(MagicRpcRequest request)
        // {
        //     return SendAsync<T>(request);
        // }
        
        // overrides of Nethereum sendRequestAsync to redirect paylaods to our relayer
        // public override async Task InterceptSendRequestAsync(Func<string, string, object[], Task> interceptedSendRequestAsync, string method, string route = null,
        //     params object[] paramList)
        // {
        //     // Do a request mapping to enable serialization
        //     Magic7RpcRequest magicRpcRequest = new MagicRpcRequest(method: method, parameters: paramList);
        //     return await this.SendAsync(magicRpcRequest);
        // }
        public async Task<TResult> SendAsync<TParams, TResult>(MagicRpcRequest<TParams> magicRequest)
        {
            // Wrap with Relayer params and send to relayer
            RelayerRequest<TParams> relayerRequest = new RelayerRequest<TParams>(nameof(OutboundMessageType.MAGIC_HANDLE_REQUEST), magicRequest);
            string msgStr = JsonUtility.ToJson(relayerRequest);

            var promise = new TaskCompletionSource<TResult>();

            // handle Response in the callback, so that webview is type free
            _relayer.Enqueue(msgStr, magicRequest.Id, (string responseStr) =>
            {
                TResult response = JsonUtility.FromJson<TResult>(responseStr);   
                return promise.TrySetResult(response);
            });

            return await promise.Task;
        }
    }
}