using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MagicSDK.Modules;
using MagicSDK.Provider;
using MagicSDK.Relayer;
using Unity.Services.Authentication;

namespace MagicSDK.Ext.OAuth.Modules
{
    public sealed class OAuthExtension : BaseModule
    {

        internal OAuthExtension(RpcProvider provider) : base(provider)
        {
        }

        public enum OAuthExtensionError
        {
            ParseSuccessURLError,
            UnsupportedVersions,
            UserDeniedAccess,
            UnableToStartPopup
        }

        // public Task<OAuthResponse> LoginWithPopup(OAuthConfiguration configuration)
        // {
        //     await AuthenticationService.Instance.SignInWithOpenIdConnectAsync(idProviderName, idToken);
        // }

        public async Task<OAuthResponse> LoginWithPopup(OAuthConfiguration configuration)
        {
            var oauthChallenge = new OAuthChallenge();

            // Construct OAuth URL
            var components = new UriBuilder
            {
                Scheme = "https",
                Host = "auth.magic.link",
                Path = $"/v1/oauth2/{configuration.Provider.ToString().ToLower()}/start"
            };

            // Add query items
            var queryItems = new List<string>
            {
                $"magic_api_key={Uri.EscapeDataString(UrlBuilder.Instance.apikey)}",
                $"magic_challenge={Uri.EscapeDataString(oauthChallenge.Challenge)}",
                $"state={Uri.EscapeDataString(oauthChallenge.State)}",
                $"redirect_uri={Uri.EscapeDataString(configuration.RedirectURI)}",
                $"platform=rn"
            };

            if (configuration.Scope != null && configuration.Scope.Count > 0)
            {
                queryItems.Add($"scope={string.Join(" ", configuration.Scope)}");
            }

            if (!string.IsNullOrEmpty(configuration.LoginHint))
            {
                queryItems.Add($"login_hint={Uri.EscapeDataString(configuration.LoginHint)}");
            }

            components.Query = string.Join("&", queryItems);
            var authURL = components.Uri;


                var successURL = await CreateAuthenticationSession(authURL, configuration);

                // Process success URL
                var query = new Uri(successURL).Query;
                return await SendToProviderWithConfig<OAuthConfiguration, OAuthResponse>(configuration,
                    nameof(OAuthMethod.magic_oauth_parse_redirect_result)
                );

        }

        private async Task<string> CreateAuthenticationSession(Uri authURL, OAuthConfiguration configuration)
        {
            // Implementation depends on the platform and environment
            // This is a placeholder for the authentication logic
            throw new NotImplementedException();
        }
    }
}