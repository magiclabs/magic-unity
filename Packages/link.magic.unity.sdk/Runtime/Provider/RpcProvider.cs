using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MagicSDK.Relayer;
using Nethereum.JsonRpc.Client;
using Nethereum.JsonRpc.Client.RpcMessages;
using Newtonsoft.Json;
using UnityEngine;

namespace MagicSDK.Provider

{
    public class RpcProvider : ClientBase
    {
        // Nethereum
        private readonly JsonSerializerSettings _jsonSerializerSettings =
            DefaultJsonSerializerSettingsFactory.BuildDefaultJsonSerializerSettings();

        private readonly WebviewController _relayer = new();

        protected internal RpcProvider(UrlBuilder urlBuilder)
        {
            var url = _generateBoxUrl(urlBuilder);

            // init relayer
            _relayer.Load(url);
        }

        private string _generateBoxUrl(UrlBuilder urlBuilder)
        {
            var url = $"{UrlBuilder.Host}/send/?params={urlBuilder.EncodedParams}";

            return url;
        }

        // 
        protected override async Task<RpcResponseMessage> SendAsync(RpcRequestMessage request, string route = null)
        {
            var msgType = $"{nameof(OutboundMessageType.MAGIC_HANDLE_REQUEST)}-{UrlBuilder.Instance.EncodedParams}";
            var relayerRequest = new MagicRelayerRequest(msgType, request);
            var requestMsg = JsonConvert.SerializeObject(relayerRequest, _jsonSerializerSettings);

            var promise = new TaskCompletionSource<RpcResponseMessage>();

            // handle Response in the callback, so that webview is type free
            _relayer.Enqueue(requestMsg, (int)request.Id, responseMsg =>
            {
                var relayerResponseNethereum = JsonConvert.DeserializeObject<RelayerResponseForNethereum>(responseMsg);
                
                var result = relayerResponseNethereum?.response;
                return promise.TrySetResult(result);
            });

            return await promise.Task;
        }

        protected override async Task<RpcResponseMessage[]> SendAsync(RpcRequestMessage[] magicRequestList)
        {
            var promise = new TaskCompletionSource<RpcResponseMessage[]>();
            List<RpcResponseMessage> results = new List<RpcResponseMessage>();
        foreach (var rpcRequestMessage in magicRequestList)
            {
                RpcResponseMessage res = await SendAsync(rpcRequestMessage);
                results.Add(res);
            }
            promise.TrySetResult(results.ToArray());
            return await promise.Task;
        }

        // This function is exclusively for Magic related API, as it supports generic types for deserialization
        protected internal async Task<TResult> MagicSendAsync<TParams, TResult>(MagicRpcRequest<TParams> magicRequest)
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

                var error = relayerResponse.response.error;
                if ((error != null) & (error?.message != null))
                    return promise.TrySetException(new Exception(error.message));

                var result = relayerResponse.response.result;

                return promise.TrySetResult(result);
            });

            return await promise.Task;
        }
    }
}