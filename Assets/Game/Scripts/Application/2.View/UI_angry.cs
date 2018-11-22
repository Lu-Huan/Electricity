using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_angry : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    private void OnEnable()
    {
        Invoke("Recovery", 1f);
    }
    public void Recovery()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
