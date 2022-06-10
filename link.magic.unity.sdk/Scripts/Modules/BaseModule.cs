using System.Threading.Tasks;
using link.magic.unity.sdk.Provider;

namespace link.magic.unity.sdk.Modules
{
    public class BaseModule
    {
        internal RpcProvider Provider;

        protected BaseModule(RpcProvider provider)
        {
            Provider = provider;
        }

        internal async Task<TResult> SendToProviderWithConfig<TConfig, TResult>(TConfig config, string methodName)
        {
            TConfig[] paramList = { config };
            var request = new MagicRpcRequest<TConfig>(methodName, paramList);
            return await Provider.MagicSendAsync<TConfig, TResult>(request);
        }

        internal async Task<TResult> SendToProvider<TResult>(string methodName)
        {
            object[] paramList = { };
            var request = new MagicRpcRequest<object>(methodName, paramList);
            return await Provider.MagicSendAsync<object, TResult>(request);
        }
    }

    public class BaseConfiguration
    {
    }
}