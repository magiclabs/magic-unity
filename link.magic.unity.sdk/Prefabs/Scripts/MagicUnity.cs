using System;
using System.Collections;
using System.Collections.Generic;
using link.magic.unity.sdk;
using Nethereum.JsonRpc.Client;
using UnityEngine;

public class MagicUnity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Magic magic = new Magic("pk_live_BF42F29844DD0026", EthNetwork.Rinkeby);
        Magic.Instance = magic;
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
