using System;
using System.Threading.Tasks;
using link.magic.unity.sdk.Relayer;
using Nethereum.JsonRpc.Client;
using UnityEngine;
namespace link.magic.unity.sdk.Provider

{
    public class RpcProvider: RequestInterceptor
    {
        private WebviewController _relayer = new WebviewController();

        public RpcProvider(UrlBuilder urlBuilder)
        {
            var url = _generateBoxUrl(urlBuilder);

            // init relayer
            _relayer.Load(url);
        }

        private string _generateBoxUrl(UrlBuilder urlBuilder)
        {
            // encode options params to base 64
            var optionsJsonString = JsonUtility.ToJson(urlBuilder);
            Debug.Log(optionsJsonString);
 
            var url = $"{UrlBuilder.Host}/send/?params={urlBuilder.EncodedParams}";
            
            return url;
        }

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
            var msgType = $"{nameof(OutboundMessageType.MAGIC_HANDLE_REQUEST)}-{UrlBuilder.Instance.EncodedParams}";
            var relayerRequest = new RelayerRequest<TParams>(msgType, magicRequest);
            var msgStr = JsonUtility.ToJson(relayerRequest);

            var promise = new TaskCompletionSource<TResult>();

            // handle Response in the callback, so that webview is type free
            _relayer.Enqueue(msgStr, magicRequest.id, msg =>
            {
                var relayerResponse = JsonUtility.FromJson<RelayerResponse<TResult>>(msg);

                var result = relayerResponse.response.result;
                
                return promise.TrySetResult(result);
            });

            return await promise.Task;
        }
    }
}