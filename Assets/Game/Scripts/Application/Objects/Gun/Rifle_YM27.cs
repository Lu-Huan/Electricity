using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle_YM27 : Gun {

    public override void Shoot(Role Target)
    {
        Vector3 offet = Target.Position + new Vector3(0, 0.2f, 0) - transform.position;
        float angle = Vector3.Angle(offet, Vector3.up);
        angle = Mathf.Clamp(angle, 90, 100);
        transform.localEulerAngles = new Vector3(angle, 0, 0);
        transform.GetComponent<XLine>().CanShoot();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
