using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class maincontrol : MonoBehaviour
{
    public GameObject[] prefab = new GameObject[10];
    GameObject finder, plball;
    // Use this for initialization
    char[,] maze = new char[300, 600];
    char[,] answer = new char[300, 600];
    public Texture savepic;
    public Texture openpic;
    public Texture spacepic;
    public Texture pauseonpic;
    public Texture pauseoffpic;
    public Texture eyepic;
    public GUIStyle gooder;
    int catcher = 0;
    Vector3 groundpos;
    Vector3 groundrotate;
    int skying = 0;
    GameObject arrower;
    string radio = "";
    files filer = new files();
    public int maze_mode;
    int mazeminheight = 50;
    int mazeminwidth = 50;
    int mazemaxheight = 100;
    int mazemaxwidth = 100;
    int programmer_mode = 0;
    int perminentdark = 0;
    int newrecord = 0;
    float extra_speed = 0;
    float center_x, center_y;
    int sander = 0;
    int mazeheighter;
    int mazewidther;
    int aim_x, aim_y;
    int totalsec = 0, recordsec = 0;
    int secsander = 0;
    int win_now = 0;
    int lighter = 0;
    int pauseon = 0;
    int proper_fontsize;
    int proper_bigbuttonsize;
    int proper_barheight;
    int one = 0;
    hardmazer dif_hardmazer = new hardmazer();
    void setbasic(int mode)
    {
        if (mode == 4)
        {
            programmer_mode = 1;
            perminentdark = 1;
        }

        if (mode == 6)
            perminentdark = 1;
        switch (mode)
        {
            case 1:
                mazeminheight = 100;
                mazeminwidth = 100;
                mazemaxheight = 120;
                mazemaxwidth = 120;
                break;
            case 2:
                mazeminheight = 100;
                mazeminwidth = 200;
                mazemaxheight = 120;
                mazemaxwidth = 240;
                break;
            case 3:
                mazeminheight = 190;
                mazeminwidth = 570;
                mazemaxheight = 200;
                mazemaxwidth = 600;
                break;
            case 4:
                mazeminheight = 50;
                mazeminwidth = 150;
                mazemaxheight = 60;
                mazemaxwidth = 180;
                break;
            case 5:
                mazeminheight = 50;
                mazeminwidth = 50;
                mazemaxheight = 60;
                mazemaxwidth = 60;
                break;
            case 6:
                mazeminheight = 100;
                mazeminwidth = 150;
                mazemaxheight = 120;
                mazemaxwidth = 180;
                break;
        }
    }
    void Quitnow(int news)
    {
        Save_Current_Maze();
    }
    void Start()
    {
        int needgenerate = filer.BinaryReadInt("needgenerate");
        if (needgenerate != 0 && maze_mode > 0 && maze_mode < 10)
        {
            Generate_new_maze(maze_mode);
        }
        else
        {
            maze_mode = filer.BinaryReadInt("autosave_type");
            setbasic(maze_mode);
            Read_And_Set_Maze();
        }


        if (maze_mode != 6)
            anglemove(1);

        string recordname = "SavedMaze" + maze_mode;
        recordname += "_recordtime";
        recordsec = filer.BinaryReadInt(recordname);

        center_x = mazeheighter / 2 * 100f - mazewidther / 2 * 100f;
        center_y = mazewidther / 2 * 100f - mazeheighter / 2 * 100f;
    }
    void Generate_new_maze(int maze_mode)
    {
        setbasic(maze_mode);
        mazeheighter = Random.Range(mazeminheight, mazemaxheight);
        mazewidther = Random.Range(mazeminwidth, mazemaxwidth);
        dif_hardmazer.generatemaze(maze, answer, mazeheighter, mazewidther);
        set_new_maze(maze, mazeheighter, mazewidther);
    }
    void clear_scene()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");
        foreach (GameObject waller in walls)
        {
            Destroy(waller.gameObject);
        }
        GameObject[] flags = GameObject.FindGameObjectsWithTag("Flag");
        foreach (GameObject flager in flags)
        {
            Destroy(flager.gameObject);
        }
    }
    void congradulation_maze_now()
    {
        if (win_now == 1)
        {
            string recordname = "SavedMaze" + maze_mode;
            recordname += "_recordtime";

            if (recordsec <= 0 || recordsec > totalsec)
            {
                recordsec = totalsec;
                newrecord = 1;
            }
            filer.BinaryWriteLong(recordname, recordsec);
            win_now = 2;
        }
        int slowx = 0;
        if (plball.transform.position.y < 500f)
            plball.transform.position = new Vector3(center_x, plball.transform.position.y + 0.05f, center_y);
        else
        {
            if (plball.transform.position.x - center_x < 10000f && slowx == 0)
                plball.transform.position = new Vector3(plball.transform.position.x + 0.01f, plball.transform.position.y, center_y);
            else
            {
                slowx = 1;
                plball.transform.position = new Vector3(plball.transform.position.x - 0.05f, plball.transform.position.y, center_y);
                if (plball.transform.position.x - center_x < -10000f)
                {
                    slowx = 0;
                }
            }
        }
        plball.transform.localEulerAngles = new Vector3(90f, plball.transform.localEulerAngles.y + 0.01f, plball.transform.localEulerAngles.z);

    }
    void set_new_maze(char[,] maze, int mazeheight, int mazewidth)
    {
        GameObject finder;
        int Pos_on = 0;
        int S_exist = 0;

        for (int i = 0; i < mazeheighter; i++)
            for (int j = 0; j < mazewidther; j++)
            {
                if (maze[i, j] == '>')
                {
                    finder = Instantiate(prefab[0]) as GameObject;
                    finder.transform.localPosition = new Vector3((float)i * 100f - mazewidther / 2 * 100f, 10f, (float)j * 100f - mazeheighter / 2 * 100f);
                    continue;
                }
                if (maze[i, j] == '<')
                {
                    finder = Instantiate(prefab[1]) as GameObject;
                    finder.transform.localPosition = new Vector3((float)i * 100f - mazewidther / 2 * 100f, 10f, (float)j * 100f - mazeheighter / 2 * 100f);
                    continue;
                }
                if (maze[i, j] == 'S' || maze[i, j] == 'P')
                {
                    if (maze[i, j] == 'S')
                    {
                        S_exist = 1;
                        finder = GameObject.FindGameObjectWithTag("Flagin");
                        finder.transform.localPosition = new Vector3((float)i * 100f - mazewidther / 2 * 100f, 30f, (float)j * 100f - mazeheighter / 2 * 100f);
                    }
                    if (maze[i, j] == 'S' && Pos_on != 0)
                        continue;
                    Pos_on = 1;
                    if (S_exist == 0)
                    {
                        maze[i, j] = 'S';
                    }
                    finder = GameObject.FindGameObjectWithTag("MainCamera");
                    finder.transform.localPosition = new Vector3((float)i * 100f - mazewidther / 2 * 100f, 30f, (float)j * 100f - mazeheighter / 2 * 100f);
                    continue;
                }
                if (maze[i, j] == 'E')
                {
                    aim_x = i;
                    aim_y = j;
                    finder = Instantiate(prefab[3]) as GameObject;
                    finder.transform.localPosition = new Vector3((float)i * 100f - mazewidther / 2 * 100f, 10f, (float)j * 100f - mazeheighter / 2 * 100f);
                }
            }
    }
    int worldpos_to_arraypos(float worldpos, int type = 0)
    {
        if (type == 1)
            return (int)((worldpos + mazeheighter / 2 * 100f + 50f) / 99.99f);
        else
        {
            return (int)((worldpos + mazewidther / 2 * 100f + 50f) / 99.99f);
        }
    }
    void setradio(int mode = 0)
    {
        radio = "";
        if (plball == null)
            plball = GameObject.FindGameObjectWithTag("MainCamera");

        int nowx = worldpos_to_arraypos(plball.transform.localPosition.x);
        int nowy = worldpos_to_arraypos(plball.transform.localPosition.z, 1);
        for (int i = 0; i < mazeheighter; i++)
        {
            for (int j = 0; j < mazewidther; j++)
            {
                if (nowx == i && nowy == j)
                {
                    radio += 'P';
                    continue;
                }
                if (answer[i, j] == '1')
                {
                    radio += '_';
                    continue;
                }

                if (maze[i, j] == '.')
                {
                    radio += '0';
                }
                else
                {
                    if (maze[i, j] != '>' && maze[i, j] != '<')
                        radio += maze[i, j];
                    else
                    {
                        radio += '1';
                    }

                }
            }
            if (mode == 0)
                radio += "\n";
        }
    }
    private void FixedUpdate()
    {
        if (pauseon == 0)
            secsander++;

        if (secsander == 50)
        {
            secsander = 0;
            totalsec++;
            if (totalsec % 60 == 59)
            {
                Save_Current_Maze();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (one == 0)
        {
            one = 1;
            proper_fontsize = this.GetComponent<proper_ui>().proper_font_size;
            proper_bigbuttonsize = this.GetComponent<proper_ui>().proper_big_button;
            proper_barheight = this.GetComponent<proper_ui>().proper_bar_height;
        }
        sander++;
        if (extra_speed > 0f)
            extra_speed -= 0.01f;

        lighter++;
        if (lighter > 2500)
        {
            GameObject lighterer = GameObject.FindGameObjectWithTag("mainlight");
            lighterer.transform.localEulerAngles = new Vector3(Random.Range(0, 360), -30f, 0);
            if (perminentdark == 1)
            {
                lighterer.transform.localEulerAngles = new Vector3(Random.Range(-160, -20), -30f, 0);
            }
            lighter = 0;
        }
        if (plball == null)
        {
            plball = GameObject.FindGameObjectWithTag("MainCamera");
            plball.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
    int last_type = 0;
    void anglemove(int type)
    {
        if (plball == null)
            plball = GameObject.FindGameObjectWithTag("MainCamera");

        plball.transform.localEulerAngles = new Vector3(0, plball.transform.localEulerAngles.y, 0);
        if (last_type == type)
        {
            if (extra_speed < 0.5f)
                extra_speed += 0.02f;
        }
        else
        {
            extra_speed = 0;
        }
        last_type = type;
        if (skying != 0)
        {
            plball.transform.position = groundpos;
            plball.transform.localEulerAngles = groundrotate;
            skying = 0;
            if (arrower != null)
                Destroy(arrower.gameObject);
        }



        float going_x = (2.3f + extra_speed) * Mathf.Sin(Mathf.PI * plball.transform.localEulerAngles.y / 180);
        float going_z = (2.3f + extra_speed) * Mathf.Cos(Mathf.PI * plball.transform.localEulerAngles.y / 180);
        going_x /= 10;
        going_z /= 10;
        int couldmove = 0;
        if (type == 0)
        {
            float future_x = plball.transform.localPosition.x + going_x;
            float future_z = plball.transform.localPosition.z + going_z;
            int pos_x = worldpos_to_arraypos(future_x);
            int pos_z = worldpos_to_arraypos(future_z, 1);
            if (pos_x < 0 || pos_z < 0)
                return;
            if (maze[pos_x, pos_z] == 'E')
            {
                win_now = 1;
            }
            if (maze[pos_x, pos_z] == '.' || maze[pos_x, pos_z] == 'S')
            {
                couldmove = 1;
                //  setradio(pos_x, pos_z);
            }
        }
        else
        {
            float future_x = plball.transform.localPosition.x - going_x;
            float future_z = plball.transform.localPosition.z - going_z;
            int pos_x = worldpos_to_arraypos(future_x);
            int pos_z = worldpos_to_arraypos(future_z, 1);
            if (pos_x < 0 || pos_z < 0)
                return;
            if (maze[pos_x, pos_z] == 'E')
            {
                win_now = 1;
            }
            if (maze[pos_x, pos_z] == '.' || maze[pos_x, pos_z] == 'S')
            {
                couldmove = 1;
                //  setradio(pos_x, pos_z);
            }
        }
        if (couldmove == 0)
            return;


        switch (type)
        {
            case 0:
                plball.transform.localPosition = new Vector3(plball.transform.localPosition.x + going_x, 30, plball.transform.localPosition.z + going_z);
                break;
            case 1:
                plball.transform.localPosition = new Vector3(plball.transform.localPosition.x - going_x, 30, plball.transform.localPosition.z - going_z);
                break;
            default:
                break;
        }
        //  float angle = Mathf.Atan2(this.transform.localPosition.y - going_to_y, this.transform.localPosition.x - going_to_x);
        //  float rotatechange = (angle / 1.6f + 1f) * 90f;
    }
    void Read_And_Set_Maze(string filename = "autosave")
    {
        string res = filer.BinaryReadString(filename);
        string posname = filename + "_height";
        mazeheighter = filer.BinaryReadInt(posname);
        posname = filename + "_width";
        mazewidther = filer.BinaryReadInt(posname);
        filer.Unpack(maze, res, mazeheighter, mazewidther);
        string timename = filename + "_nowtime";
        totalsec = filer.BinaryReadInt(timename);
        int pl_pos_x = 0, pl_pos_y = 0;
        if (programmer_mode == 1)
        {
            for (int i = 0; i < mazeheighter; i++)
                for (int j = 0; j < mazewidther; j++)
                {
                    if (i == 0 || i == mazeheighter - 1 || j == 0 || j == mazewidther - 1)
                    {
                        if (maze[i, j] == '1')
                            maze[i, j] = '<';
                    }
                    else
                    {
                        if (maze[i, j] == '1')
                            maze[i, j] = '>';
                        else
                        {
                            if (maze[i, j] == '0')
                                maze[i, j] = '.';
                        }
                    }
                    if (maze[i, j] == 'P')
                    {
                        pl_pos_x = i;
                        pl_pos_y = j;
                    }

                    if (maze[i, j] == '_')
                    {
                        answer[i, j] = '1';
                        maze[i, j] = '.';
                    }
                    else
                    {
                        answer[i, j] = '0';
                    }
                }
        }
        else
        {
            for (int i = 0; i < mazeheighter; i++)
                for (int j = 0; j < mazewidther; j++)
                {
                    if (maze[i, j] == 'P')
                    {
                        pl_pos_x = i;
                        pl_pos_y = j;
                    }
                }
        }
        clear_scene();
        set_new_maze(maze, mazeheighter, mazewidther);

        if (programmer_mode == 1)
            setradio();

        maze[pl_pos_x, pl_pos_y] = '.';
    }
    void Save_Current_Maze(string filename = "autosave")
    {
        string pathandname = filename;
        if (programmer_mode == 1)
        {
            setradio(1);
            filer.BinaryWriteString(pathandname, radio);
            setradio(0);
        }
        else
        {
            int nowx = worldpos_to_arraypos(plball.transform.localPosition.x);
            int nowy = worldpos_to_arraypos(plball.transform.localPosition.z, 1);
            maze[nowx, nowy] = 'P';
            string tempdata = filer.Pack(maze, mazeheighter, mazewidther);
            filer.BinaryWriteString(pathandname, tempdata);
            maze[nowx, nowy] = '.';
        }
        string posname = pathandname + "_height";
        filer.BinaryWriteInt(posname, mazeheighter);
        posname = pathandname + "_width";
        filer.BinaryWriteInt(posname, mazewidther);
        string timename = pathandname + "_nowtime";
        filer.BinaryWriteLong(timename, totalsec);

        if (filename == "autosave")
        {
            string idname = filename + "_type";
            filer.BinaryWriteInt(idname, maze_mode);
        }

    }
    string transform_time(long totalseconds)
    {
        long nowminute = totalseconds / 60;
        long nowsec = totalseconds % 60;
        string timestr = "";
        if (nowminute < 10)
            timestr += "0";
        timestr += nowminute;
        timestr += " : ";
        if (nowsec < 10)
            timestr += "0";
        timestr += nowsec;

        return timestr;
    }
    void OnGUI()
    {

        GUI.skin.label.fontSize = proper_fontsize;
        GUI.skin.label.normal.textColor = new Vector4(1.0f, 1.0f, 0.95f, 1.0f);
        if (plball != null)
            GUI.Label(new Rect(7, Screen.height - proper_barheight * 4.5f, Screen.width / 4, proper_barheight), "" + worldpos_to_arraypos(plball.transform.localPosition.x) + "," + worldpos_to_arraypos(plball.transform.localPosition.z, 1));
        GUI.skin.label.normal.textColor = new Vector4(0.4f, 0.52f, 0.4f, 1.0f);
        GUI.Label(new Rect(7, Screen.height - proper_barheight * 3.4f, Screen.width / 4, proper_barheight), "" + aim_x + "," + aim_y);
        GUI.skin.label.normal.textColor = new Vector4(0.95f, 0.95f, 0.95f, 1.0f);
        GUI.Label(new Rect(7, Screen.height - proper_barheight * 2.3f, Screen.width / 4, proper_barheight), transform_time(totalsec));

        if (win_now != 0)
        {
            congradulation_maze_now();
            GUI.skin.label.fontSize = 75;
            if (newrecord == 0)
                GUI.Label(new Rect(Screen.width / 3, Screen.height / 3, Screen.width / 3 * 2, Screen.height / 3 * 2), "");
            else
            {
                GUI.Label(new Rect(Screen.width / 3, Screen.height / 3, Screen.width / 3 * 2, Screen.height / 3 * 2), "");
            }
            GUI.skin.label.fontSize = 20;
        }


        if (GUI.Button(new Rect(0, proper_bigbuttonsize, proper_bigbuttonsize, proper_bigbuttonsize), eyepic) && sander > 10)
        {
            sander = 0;
            if (skying == 0)
            {
                skying = 1;
                groundpos = plball.transform.position;
                groundrotate = plball.transform.localEulerAngles;


                if (programmer_mode == 0 || maze_mode == 6)
                {
                    arrower = Instantiate(prefab[5]) as GameObject;
                    arrower.transform.position = groundpos;
                    arrower.transform.localEulerAngles = groundrotate;
                    plball.transform.localEulerAngles = new Vector3(90f, 0, 0);
                    plball.transform.position = new Vector3(plball.transform.position.x, 800, plball.transform.position.z);
                }
            }
            else
            {
                if (arrower != null)
                    Destroy(arrower.gameObject);

                plball.transform.position = groundpos;
                plball.transform.localEulerAngles = groundrotate;
                skying = 0;
            }
        }

        if (plball != null && pauseon == 0)
        {
            if (Input.GetTouch(0).position.x > Screen.width / 3.3 && Input.GetTouch(0).position.x < Screen.width / 3.5 * 2.5) //W
            {
                anglemove(0);
            }

            if (Input.GetTouch(0).position.x < Screen.width / 3.3 && Input.GetTouch(0).position.x > proper_bigbuttonsize)
            {
                if (skying != 0)
                {
                    plball.transform.position = groundpos;
                    plball.transform.localEulerAngles = groundrotate;
                    skying = 0;
                    if (arrower != null)
                        Destroy(arrower.gameObject);
                }
                plball.transform.localEulerAngles = new Vector3(0, plball.transform.localEulerAngles.y - 0.1f, 0);
            }
            if (Input.GetTouch(0).position.x > Screen.width / 3.5 * 2.5)
            {
                if (skying != 0)
                {
                    plball.transform.position = groundpos;
                    plball.transform.localEulerAngles = groundrotate;
                    skying = 0;
                    if (arrower != null)
                        Destroy(arrower.gameObject);
                }
                plball.transform.localEulerAngles = new Vector3(0, plball.transform.localEulerAngles.y + 0.1f, 0);
            }


        }
    }
}
