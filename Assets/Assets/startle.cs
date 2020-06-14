using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class startle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string levelname;
        int autoid = Random.Range(0, 4);
        switch (autoid)
        {
            case 0:
                levelname = "normalmaze";
                break;
            case 1:
                levelname = "icemaze";
                break;
            case 2:
                levelname = "easywhitemaze";
                break;
            case 3:
                levelname = "darkmaze";
                break;
            default:
                levelname = "normalmaze";
                break;
        }
        SceneManager.LoadScene(levelname);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
