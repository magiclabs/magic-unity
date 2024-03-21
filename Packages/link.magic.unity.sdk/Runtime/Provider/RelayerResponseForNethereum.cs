using Nethereum.JsonRpc.Client.RpcMessages;
using Newtonsoft.Json;
using UnityEngine.Scripting;
using System;
using UnityEngine;

// This class is specifically designed to manage RpcResponseMessage objects, which does not support generic type inference for type T.
namespace MagicSDK.Provider
{
    [Serializable]
    public class RelayerResponseForNethereum
    {
        [SerializeField] internal string msgType;
        [SerializeField] internal RpcResponseMessage response;

        [JsonConstructor] [Preserve]
        internal RelayerResponseForNethereum(string msgType, RpcResponseMessage response)
        {
            this.msgType = msgType;
            this.response = response;
        }
    }
}

