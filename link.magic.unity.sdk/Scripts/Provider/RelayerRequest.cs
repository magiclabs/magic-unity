using System;

namespace link.magic.unity.sdk.Provider
{
    [Serializable]
    internal class RelayerRequest<T>
    {
        public string msgType;
        public MagicRpcRequest<T> payload;

        internal RelayerRequest(string msgType, MagicRpcRequest<T> payload)
        {
            this.msgType = msgType;
            this.payload = payload;
        }
    }
}