using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Tower_Trigger : MonoBehaviour
{
 
    public event Action<Monster> IN;
    public event Action<Monster> OUT;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        { 
            IN(other.GetComponent<Monster>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            OUT(other.GetComponent<Monster>());
        }
    }
}
