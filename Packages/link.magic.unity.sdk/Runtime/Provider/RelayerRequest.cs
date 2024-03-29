using System;
using UnityEngine;

namespace MagicSDK.Provider
{
    [Serializable]
    internal class RelayerRequest<T>
    {
        [SerializeField] internal string msgType;
        [SerializeField] internal MagicRpcRequest<T> payload;

        internal RelayerRequest(string msgType, MagicRpcRequest<T> payload)
        {
            this.msgType = msgType;
            this.payload = payload;
        }
    }
}