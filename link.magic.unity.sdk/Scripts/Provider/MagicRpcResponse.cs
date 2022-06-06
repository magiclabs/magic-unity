using System;
using JetBrains.Annotations;

namespace link.magic.unity.sdk.Provider
{
    [Serializable]
    public class MagicRpcResponse<T>
    {
        public int Id { get; private set; }

        public string Jsonrpc { get; private set; }

        [CanBeNull] public T Result { get; private set; }
        
        [CanBeNull] public Error Error { get; private set; }
        
        public MagicRpcResponse(int id, string jsonrpc, T result)
        {
            Id = id;
            Jsonrpc = jsonrpc;
            Result = result;
        }
    }

    [Serializable]
    public class Error
    {
        public int code;
        public string message;
    }
}