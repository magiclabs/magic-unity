using System;

namespace link.magic.unity.sdk.Provider
{
    [Serializable]
    public class RelayerResponse<T>
    {
        public string msgType;
        public MagicRpcResponse<T> response;
        
        internal RelayerResponse(string msgType, MagicRpcResponse<T> response)
        {
            this.msgType = msgType;
            this.response = response;
        }
    }
}