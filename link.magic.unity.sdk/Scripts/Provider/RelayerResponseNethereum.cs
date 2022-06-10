using System;
using Nethereum.JsonRpc.Client.RpcMessages;
using Newtonsoft.Json;
using UnityEngine;

namespace link.magic.unity.sdk.Provider
{
    [JsonObject]
    public class RelayerResponseNethereum
    {
        [JsonProperty("msgType", Required = Required.Default)] 
        internal string MsgType;
        [JsonProperty("response", Required = Required.Default)] 
        internal RpcResponseMessage Response;
        
        [JsonConstructor]
        internal RelayerResponseNethereum(string msgType, RpcResponseMessage response)
        {
            this.MsgType = msgType;
            this.Response = response;
        }
    }
}