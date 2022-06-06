namespace link.magic.unity.sdk.Relayer
{
    interface IBaseOptions
    {
        static private string host = UrlBuilder.Host;
        static private string sdk = "magic-sdk-unity";
        string API_KEY { get; set; }
        string locale { get; set; }
    }
}