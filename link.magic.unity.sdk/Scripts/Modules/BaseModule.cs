using link.magic.unity.sdk.Provider;

namespace link.magic.unity.sdk.Modules
{
    public class BaseModule
    {
        internal RpcProvider Provider;
        
        public BaseModule(RpcProvider provider)
        {
            Provider = provider;
        }
    }

    public class BaseConfiguration
    {
        
    }
}