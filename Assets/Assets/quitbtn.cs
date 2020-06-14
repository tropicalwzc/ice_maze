using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class quitbtn : MonoBehaviour
{

    private GameObject finder;

    public Texture btnchahao;
    public Texture btnretry;
    public Texture backtomenu;
    public string return_name;
    public GUIStyle gooder;
    private int sander = 0;
    int ready_to_quit = 0;
    int ready_go_to_menu = 0;
    int proper_fontsize;
    int proper_bigbuttonsize;
    int proper_barheight;
    int one = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        sander++;
        if (one == 0)
        {
            one = 1;
            proper_fontsize = this.GetComponent<proper_ui>().proper_font_size;
            proper_bigbuttonsize = this.GetComponent<proper_ui>().proper_big_button;
            proper_barheight = this.GetComponent<proper_ui>().proper_bar_height;
        }
        if (ready_to_quit == 1 && sander > 5)
        {
            Quit();
        }
        if (ready_go_to_menu == 1 && sander > 5)
        {
            SceneManager.LoadScene("mainmenu");
        }

    }
    public void Quit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    float oldx, oldy;


    void OnGUI()
    {
        GUI.skin.label.fontSize = 22;
        GUI.skin.button.fontSize = 22;




        if (GUI.Button(new Rect(0, 0, proper_bigbuttonsize, proper_bigbuttonsize), btnretry) || Input.GetKeyDown(KeyCode.R) && sander > 10)
        {
            sander = 0;
            files filer = new files();
            filer.BinaryWriteInt("needgenerate", 1);

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
                    levelname = "programmermaze";
                    break;

                default:
                    levelname = "normalmaze";
                    break;
            }
            SceneManager.LoadScene(levelname);
            // Application.LoadLevel ("icemaze");
        }
    }
}
