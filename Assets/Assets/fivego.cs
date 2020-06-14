using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class fivego : MonoBehaviour {

    files filer = new files();
    public string levelname;
    int autoid = 0;
    void Start () {

        if (levelname=="continue")
        {
            autoid = filer.BinaryReadInt("autosave_type");
            if (autoid <1||autoid>10)
            {
                Destroy(this.gameObject);
            }
        }

        Button btn = this.GetComponent<Button> ();
        btn.onClick.AddListener (OnClick);
    }

    private void OnClick(){
        if(levelname=="Quit"||levelname=="quit")
        {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
        }
        else
        {
            if(levelname=="continue")
            {
                filer.BinaryWriteInt("needgenerate", 0);
                switch(autoid)
                {
                    case 1:
                        levelname = "normalmaze";
                        break;
                    case 2:
                        levelname = "icemaze";
                        break;
                    case 3:
                        levelname = "bignormalmaze";
                        break;
                    case 4:
                        levelname = "programmermaze";
                        break;
                    case 5:
                        levelname = "easywhitemaze";
                        break;
                    case 6:
                        levelname = "darkmaze";
                        break;
                }
            }
            else
            {
                filer.BinaryWriteInt("needgenerate", 1);
            }
           

            SceneManager.LoadScene(levelname);
        }
        
    }
}
