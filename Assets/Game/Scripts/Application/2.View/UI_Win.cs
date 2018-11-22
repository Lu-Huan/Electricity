using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Win : MonoBehaviour {
    
    private void OnEnable()
    {
        GetComponent<Animator>().SetTrigger("win");
    }
}
