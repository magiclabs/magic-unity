using System.Collections.Generic;
using MagicSDK.Modules;

namespace MagicSDK.Ext.OAuth.Modules
{
    public class OAuthResponse
    {

            public OauthPartialResult OAuth { get; set; }
            public MagicPartialResult Magic { get; set; }

        public class OauthPartialResult
        {
            public string Provider { get; set; }
            public List<string> Scope { get; set; }
            public string AccessToken { get; set; }
            public string UserHandle { get; set; }
            public OpenIDConnectProfile UserInfo { get; set; }
        }

        public class MagicPartialResult
        {
            public string IdToken { get; set; }
            public UserMetadata UserMetadata { get; set; }
        }
    }
}