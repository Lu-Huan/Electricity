using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Dead : MonoBehaviour {
    private void OnEnable()
    {
        GetComponent<Animator>().SetBool("dead", true);
    }
    private void OnDisable()
    {
        GetComponent<Animator>().SetBool("dead", false);
    }
}
