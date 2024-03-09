using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlCara : MonoBehaviour
{
    private bool enElsuelo = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "tapete")
        {
            enElsuelo = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enElsuelo = false;
    }

    public bool compruebaSuelo()
    {
        return enElsuelo;
    }
}
