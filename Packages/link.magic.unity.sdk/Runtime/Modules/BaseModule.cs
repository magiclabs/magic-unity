using System.Threading.Tasks;
using MagicSDK.Provider;

namespace MagicSDK.Modules
{
    public class BaseModule
    {
        internal RpcProvider Provider;

        protected BaseModule(RpcProvider provider)
        {
            Provider = provider;
        }

        protected async Task<TResult> SendToProviderWithConfig<TConfig, TResult>(TConfig config, string methodName)
        {
            TConfig[] paramList = { config };
            var request = new MagicRpcRequest<TConfig>(methodName, paramList);
            return await Provider.MagicSendAsync<TConfig, TResult>(request);
        }

        internal async Task<TResult> SendToProvider<TResult>(string methodName)
        {
            // Don't use object, otherwise list will not be serialized
            int[] paramList = { };
            var request = new MagicRpcRequest<int>(methodName, paramList);
            return await Provider.MagicSendAsync<int, TResult>(request);
        }
    }

    public class BaseConfiguration
    {
    }
}