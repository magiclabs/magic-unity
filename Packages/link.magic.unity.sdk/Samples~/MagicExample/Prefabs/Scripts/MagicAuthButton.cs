using System.Collections;
using System.Collections.Generic;
using MagicSDK;
using UnityEngine;
using UnityEngine.UI;

public class MagicAuthButton : MonoBehaviour
{
    public Text result;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public async void Login()
    {
        Magic magic = Magic.Instance;
        var token = await magic.Auth.LoginWithEmailOtp("hiro@magic.link");
        result.text = $"token {token}";
    }
    
    public async void GetMetadata()
    {
        Magic magic = Magic.Instance;
        var metadata = await magic.User.GetMetadata();
        result.text = $"Metadata Email: {metadata.email} \n Public Address: {metadata.publicAddress}";
    }
    
    public async void Logout()
    {
        Magic magic = Magic.Instance;
        var isLogout = await magic.User.Logout();
        result.text = $"Logout: {isLogout.ToString()}";
    }
}
