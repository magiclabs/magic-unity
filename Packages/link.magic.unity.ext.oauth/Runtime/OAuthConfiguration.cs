using System;
using MagicSDK.Modules;
using System.Collections.Generic;
// Assuming MagicSDK is a library you're using in C#, include it here
// using MagicSDK;

[Serializable]
public class OAuthConfiguration : BaseConfiguration
{
    public OAuthProvider Provider { get; set; }
    public string RedirectURI { get; set; }
    public List<string> Scope { get; set; }
    public string LoginHint { get; set; }

    public OAuthConfiguration(OAuthProvider provider, string redirectURI, List<string> scope = null, string loginHint = null)
    {
        Provider = provider;
        RedirectURI = redirectURI;
        Scope = scope ?? new List<string>();
        LoginHint = loginHint;
    }
}

public enum OAuthProvider
{
    GOOGLE,
    FACEBOOK,
    GITHUB,
    APPLE,
    LINKEDIN,
    BITBUCKET,
    GITLAB,
    TWITTER,
    DISCORD,
    TWITCH,
    MICROSOFT
}