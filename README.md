# Magic Unity SDK

Magic empowers developers to protect their users via an innovative, passwordless authentication flow without the UX compromises that burden traditional OAuth implementations. 

[Documentation](https://magic.link/docs/login-methods/email/integration/unity)

## ⚠️ Important Changes in Version 3.0.0 ⚠️

Starting with version 3.0, the Magic Unity SDK has transitioned to using the Unity Package Manager for dependency management. This change offers a more convenient and robust method for managing dependencies.

Old Implementation (Not Supported) ❌
```csharp
using link.magic.unity.sdk
```

New Implementation (Since Version 3.0)
```csharp
using MagicSDK
```

## ⚠️ Removal of `LoginWithMagicLink()` ⚠️
As of `v2.0.0`, passcodes (ie. `LoginWithSMS()`, `LoginWithEmailOTP()`) are replacing Magic Links (ie. `LoginWithMagicLink()`) for all of our Mobile SDKs⁠. [Learn more](https://magic.link/docs/auth/login-methods/email/email-link-update-march-2023)


## Platform Compatibility
For optimal performance, we recommend using this plugin with Unity 2021.3.3f1 or later.

| Platform | Support? |
|----------|----------|
| iOS      | ✅        |
| Android  | ✅        |
| MacOS    | ✅        |
| Editor   | ✅        |
| Windows  | ✅        |
| WebGL    | ❌        |


Note: WebGL is not currently supported. For web-based projects, consider using [magic-js](https://github.com/magiclabs/magic-js) for passwordless authentication within a JavaScript environment.

## Installation

There are a number of required packages that need to be added. We recommend just adding them directly to the to the `Packages/manfifest.json`. Your final `manifest.json` should resemble the following:

```json
{
  "dependencies": {
    "link.magic.unity.sdk": "3.1.1",
    "com.nethereum.unity": "4.19.2",
    "net.gree.unity-webview": "https://github.com/gree/unity-webview.git?path=/dist/package",
    "dev.voltstro.unitywebbrowser": "2.1.1",
    "dev.voltstro.unitywebbrowser.communication.pipes": "1.0.1",
    "dev.voltstro.unitywebbrowser.engine.cef": "2.1.0-121.3.13",
    "dev.voltstro.unitywebbrowser.engine.cef.win.x64": "2.1.0-121.3.13",
    "dev.voltstro.unitywebbrowser.unix-support": "1.0.1"
  },
  "scopedRegistries": [
    {
      "name": "Voltstro UPM",
      "url": "https://upm-pkgs.voltstro.dev",
      "scopes": [
        "dev.voltstro",
        "org.nuget",
        "com.cysharp.unitask"
      ]
    },
    {
      "name": "OpenUPM",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.nethereum.unity",
        "link.magic.unity.sdk"
      ]
    }
  ]
}
```

You will likely have many other references in your `dependencies` node and you may have other `scopedRegistries` as well, but these are the ones that are required to use the Magic Unity SDK.

## Quick Start

Try import Sample in Package Manager `prefabs/MagicExample.prefab` to get a quick start!

### Instantiate Magic

Create a script with the following code and bind this to a Canvas or object that you love to start with

```c#
public class MagicUnity : MonoBehaviour
{
    // Attach this script when you start the canvas 
    void Start()
    {
        Magic magic = new Magic("YOUR_PUBLISHABLE_KEY");
        
        // Append the instance here, so that it can be shared across the project
        Magic.Instance = magic;
    }
}
```

For macOS editor support, you'll need to pass an additional parameter `macCanvas` to properly render Magic components. The object you pass in should be the name of your app's primary canvas.

```c#
public class MagicUnity : MonoBehaviour
{
    // Attach this script when you start the canvas 
    void Start()
    {
        Magic magic = new Magic("YOUR_PUBLISHABLE_KEY", macCanvas: GameObject.Find("Magic Example 1"));
        
        // Append the instance here, so that it can be shared across the project
        Magic.Instance = magic;
    }
}
```

### User Authentication

```c#
await magic.Auth.LoginWithEmailOtp("hiro@magic.link");
```

### Web3 interaction

After the user has been authenticated, now it's a good time to get the users on the blockchain. 

Magic Unity builds on top of Nethereum to enable web3 functionalities. For more detail about Nethereum, please check their official docs https://docs.nethereum.com/en/latest/ 
and their github repo about [RPC payloads](https://github.com/Nethereum/Nethereum/tree/f0f7cbd225fadfce681faff004a57e480428e62b/src/Nethereum.RPC)

```c#
        // Get Eth Account  
         var ethAccounts = new EthAccounts(Magic.Instance.Provider);
        var accounts = await ethAccounts.SendRequestAsync();
         
         // Eth sign
        var personalSign = new EthSign(Magic.Instance.Provider);
        var transactionInput = new TransactionInput{Data = "Hello world"};
        var res = await personalSign.SendRequestAsync(accounts[0], "hello world");
    
        // Send Transaction
        var transaction = new EthSendTransaction(Magic.Instance.Provider);
        var transactionInput = new TransactionInput
            { To = accounts[0], Value = new HexBigInteger(10), From = accounts[0]};
        var hash = await transaction.SendRequestAsync(transactionInput);
```

### Support
More blockchain support will be coming soon. Feel free to send your requests and issues to `support@magic.link` or via our helpdesk. Happy coding!

