using System;
using Org.BouncyCastle.Security;
using Random = UnityEngine.Random;

namespace link.magic.unity.sdk.Provider
{
    [Serializable]
    public class MagicRpcRequest
    {
        public int Id { get; private set; }

        public string Jsonrpc { get; private set; }

        public string Method { get; private set; }
        
        public object[] Params { get; private set; }

        public MagicRpcRequest(string method, object[] parameters)
        {
            Id = Random.Range(1, 100000);
            Jsonrpc = "2.0";
            Method = method;
            Params = parameters;
        }
    }
}