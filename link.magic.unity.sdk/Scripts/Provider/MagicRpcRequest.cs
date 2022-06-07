using System;
using Org.BouncyCastle.Security;
using Random = UnityEngine.Random;

namespace link.magic.unity.sdk.Provider
{
    [Serializable]
    public class MagicRpcRequest<T>
    {
        public int Id { get; private set; }

        public string Jsonrpc { get; private set; }

        public string Method { get; private set; }
        
        public T[] Params { get; private set; }

        public MagicRpcRequest(string method, T[] parameters)
        {
            Id = Random.Range(1, 100000);
            Jsonrpc = "2.0";
            Method = method;
            Params = parameters;
        }
    }
}