using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proper_ui : MonoBehaviour {

    public int proper_big_button = 40;
    public int proper_bar_height = 25;
    public int proper_text_size = 18;
    public int proper_font_size = 18;
    // Use this for initialization
    void Awake()
    {
        if (Screen.height > 1300)
        {
            proper_big_button = 100;
            proper_bar_height = 60;
            proper_text_size = 40;
            proper_font_size = 40;
        }
        else{

            if(Screen.height>900)
            {
                proper_big_button = 50;
                proper_bar_height = 30;
                proper_text_size = 25;
                proper_font_size = 25;
            }

        }

    }
    private void Start()
    {
        if (Screen.height > 1300)
        {
            proper_big_button = 100;
            proper_bar_height = 60;
            proper_text_size = 40;
            proper_font_size = 40;
        }
        else
        {

            if (Screen.height > 900)
            {
                proper_big_button = 50;
                proper_bar_height = 30;
                proper_text_size = 25;
                proper_font_size = 25;
            }

        }
    }
    public void set_proper_ui_style(GUIStyle good)
    {
        if (Screen.height <= 900)
            good.fontSize = 15;
        else
        if (Screen.height > 900 && Screen.height <= 1300)
            good.fontSize = 20;
        else
        if (Screen.height > 1300)
            good.fontSize = 40;
    }
}
