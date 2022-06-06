using System;

namespace link.magic.unity.sdk.Provider
{
    [Serializable]
    public class RelayerRequest
    {
        public string MsgType;
        public MagicRpcRequest Payload;

        RelayerRequest(string msgType, MagicRpcRequest payload)
        {
            MsgType = msgType;
            Payload = payload;
        }
    }
}