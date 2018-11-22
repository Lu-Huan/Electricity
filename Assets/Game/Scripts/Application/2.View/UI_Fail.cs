using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Fail : MonoBehaviour {

    private void OnEnable()
    {
        GetComponent<Animator>().SetBool("fail", true);
    }
    private void OnDisable()
    {
        GetComponent<Animator>().SetBool("fail", false);
    }
}
