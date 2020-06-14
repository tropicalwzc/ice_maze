using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stablelized : MonoBehaviour {

    float origin_x, origin_y, origin_z;
    Vector3 stablepos;
    Vector3 stableangle;
	// Use this for initialization
	void Start () {
        origin_x = this.transform.position.x;
        origin_y = this.transform.position.y;
        origin_z = this.transform.position.z;
        stablepos = new Vector3(origin_x, origin_y, origin_z);
        stableangle = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = stablepos;
        this.transform.localEulerAngles = stableangle;
	}
}
