using Nethereum.JsonRpc.Client.RpcMessages;
using Newtonsoft.Json;

// This class is specifically designed to manage RpcRequestMessage objects, which does not support generic type inference for type T.
namespace MagicSDK.Provider
{
    [JsonObject]
    internal class MagicRelayerRequest
    {
        [JsonProperty("msgType", Required = Required.Default)]
        internal string MsgType;

        [JsonProperty("payload", Required = Required.Default)]
        internal RpcRequestMessage Payload;

        internal MagicRelayerRequest(string msgType, RpcRequestMessage payload)
        {
            MsgType = msgType;
            Payload = payload;
        }
    }
}