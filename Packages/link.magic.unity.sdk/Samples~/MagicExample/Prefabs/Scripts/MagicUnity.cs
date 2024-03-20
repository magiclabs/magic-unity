using System;
using System.Collections;
using System.Collections.Generic;
using MagicSDK;
using Nethereum.JsonRpc.Client;
using UnityEngine;

public class MagicUnity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Magic magic = new Magic("YOUR_PUBLISHABLE_KEY");
        Magic.Instance = magic;
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
