using UnityEngine;
using System.Collections;

public class randcolor : MonoBehaviour {

    // Use this for initialization
    int sander = 0;
    public int opique = 1;
    public int randrotate = 1;
    public int onlyrotate = 0;
    float aa, bb, cc, dd;
	void Start () {

        if(onlyrotate==0)
        {
            aa = Random.Range(0.0f, 255f);
            bb = Random.Range(0.0f, 255f);
            cc = Random.Range(0.0f, 255f);
            dd = 1f;
            if (opique == 0)
                dd = Random.Range(0.75f, 1f);
            this.GetComponent<Renderer>().material.color = new Vector4(aa / 255f, bb / 255f, cc / 255f, dd);
        }

        

        if (randrotate == 1)
        {
        this.transform.localEulerAngles = new Vector3(Random.Range(-1, 2), Random.Range(0, 360), Random.Range(-1, 2));
        }
       
		   
	}
	
	// Update is called once per frame
	void Update () {
        sander++;

        if(sander>1000&&onlyrotate==0)
        {
            sander = 0;
            int color_change = Random.Range(0, 4);
            if(color_change==1)
            {

                aa = Random.Range(0.0f, 255f );
                bb = Random.Range(0.0f, 255f );
                cc = Random.Range(0.0f, 255f );
                dd = 1f;

                if (opique == 0)
                dd = Random.Range(0.75f, 1f);
                this.GetComponent<Renderer>().material.color = new Vector4(aa / 255f, bb / 255f, cc / 255f, dd);
                
            }
        }
	}
}
