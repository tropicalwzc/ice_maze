using UnityEngine;
using System.Collections;

public class hardmazer
{
    /*
     * Though this generater has a slower speed , but its maze within size of 75*75 is rather harsh
     * */
    //int startpointx,startpointy;
    //int endpointx,endpointy;
    //int havebuild=0;
    int isend(int nowx, int nowy, int endx, int endy)
    {
        int result = 0;

        if (nowx == endx && (nowy - endy >= -1 && nowy - endy <= 1))
            result = 1;
        if (nowy == endy && (nowx - endx >= -1 && nowx - endx <= 1))
            result = 1;


        return result;
    }
    int rand()
    {
        return Random.Range(0, 1000);
    }
    int isempty(char[,] maze, int nowx, int nowy, int width, int height)
    {
        int counter = 0;

        if (maze[nowx - 1, nowy] - '>' == 0)
            counter++;
        if (maze[nowx + 1, nowy] - '>' == 0)
            counter++;
        if (maze[nowx, nowy + 1] - '>' == 0)
            counter++;
        if (maze[nowx, nowy - 1] - '>' == 0)
            counter++;

        if (counter == 3)
        {
            maze[nowx, nowy] = '.';
            return 1;
        }

        return 0;
    }

    int wayerror(int[] label)
    {
        int sum = 0;
        for (int i = 0; i < 4; i++)
            sum += label[i];
        if (sum <= -4)
            return 1;
        else
            return 0;
    }


    void copymaze(char[,]maze,char[,]newmaze,int mazeheight,int mazewidth)
    {
        for (int i = 0; i < mazeheight; i++)
            for (int j = 0; j < mazewidth; j++)
                maze[i, j] = newmaze[i, j];
    }
    int nowxp, nowyp;
    void digdoor(char[,] maze, int stx, int sty, int mazeheight, int mazewidth)
    {
        if (stx == 0)
            stx += 1;
        if (stx == mazeheight - 1)
            stx -= 1;
        if (sty == 0)
            sty += 1;
        if (sty == mazewidth - 1)
            sty -= 1;

        nowxp = stx;
        nowyp = sty;
        maze[stx, sty] = '.';
    }
    int perfectconnect(char[,]maze,char[,]originmaze,int startx,int starty,int endx_s,int endy_s,int mazeheight,int mazewidth)
    {
        int partical = Random.Range(30, 33);
        if(starty-endy_s<-partical || starty-endy_s> partical || startx-endx_s<-partical || startx-endx_s> partical)
        {
            int midingx = (startx+endx_s)/2;
            int midingy = (starty+endy_s)/2;
            char[,] originchange = new char[300, 600];
            copymaze(originchange, originmaze, mazeheight, mazewidth);
            int connectfirst = perfectconnect(maze,originmaze, startx, starty, midingx, midingy, mazeheight, mazewidth);
            int foxtime = 0;
            while(connectfirst == 0)
            {
                foxtime++;
                if (foxtime >= 17)
                {
                    copymaze(originmaze, originchange, mazeheight, mazewidth);
                    copymaze(maze, originmaze, mazeheight, mazewidth);
                    return 0;
                }
                   
                copymaze(originmaze, originchange, mazeheight, mazewidth);
                int polyx = Random.Range(-13, 14);
                int polyy = Random.Range(-13, 14);
                while (polyx>-8&&polyx<8&& polyy > -8 && polyy < 8)
                {
                    polyx = Random.Range(-13, 14);
                    polyy = Random.Range(-13, 14);
                }

                midingx = (startx + endx_s) / 2 + polyx;
                if (midingx < 1)
                    midingx = 1;
                if (midingx > mazeheight - 1)
                    midingx = mazeheight - 1;
                midingy = (starty + endy_s) / 2 + polyy;
                if (midingy < 1)
                    midingy = 1;
                if (midingy > mazewidth - 1)
                    midingy = mazewidth - 1;
                connectfirst = perfectconnect(maze, originmaze, startx, starty, midingx, midingy, mazeheight, mazewidth);
               
                if (maze[endx_s, endy_s] == '.' || endx_s - 1 >= 0 && maze[endx_s - 1, endy_s] == '.' || endx_s + 1 <= mazeheight - 1 && maze[endx_s + 1, endy_s] == '.' || endy_s - 1 >= 0 && maze[endx_s, endy_s - 1] == '.' || endy_s + 1 <= mazewidth - 1 && maze[endx_s, endy_s + 1] == '.')
                {
                    if (connectfirst == 1)
                    {
                        copymaze(originmaze, originchange, mazeheight, mazewidth);
                    }
                    connectfirst = 0;
                }
            }

            int connectsecond = perfectconnect(maze, originmaze, midingx, midingy, endx_s, endy_s, mazeheight, mazewidth);
            int failtime = 0;
            while(connectsecond == 0)
            {
                failtime++;
                if (failtime >= 17)
                    break;
                connectsecond = perfectconnect(maze, originmaze, midingx, midingy, endx_s, endy_s, mazeheight, mazewidth);
            }
            if(connectsecond==0)
            {
                copymaze(originmaze, originchange, mazeheight, mazewidth);
                copymaze(maze, originmaze, mazeheight, mazewidth);
                return 0;
            }
            copymaze(originmaze, maze, mazeheight, mazewidth);
            return 1;
        }
        
        int during = 0;
        int restart_now = 0;
        int[] label = new int[4];
        while (restart_now == 0)
        {
            restart_now = 1;
            copymaze(maze, originmaze, mazeheight, mazewidth);
            int whichdirect;
            int symbol = 0;
            int endx = endx_s;
            int endy = endy_s;
            int nowx = startx;
            int nowy = starty;
            for (int i = 0; i < 4; i++)
                label[i] = 0;

            maze[startx, starty] = '.';
            
            // int from = 0;
            while (restart_now == 1)
            {
                if (isend(nowx + 1, nowy, endx, endy) == 1)
                {
                    maze[nowx + 1, nowy] = '.';
                    break;
                }
                if (isend(nowx - 1, nowy, endx, endy) == 1)
                {
                    maze[nowx - 1, nowy] = '.';
                    break;
                }
                if (isend(nowx, nowy + 1, endx, endy) == 1)
                {
                    maze[nowx, nowy + 1] = '.';
                    break;
                }
                if (isend(nowx, nowy - 1, endx, endy) == 1)
                {
                    maze[nowx, nowy - 1] = '.';
                    break;
                }
                symbol = 0;
                while (symbol == 0 && restart_now == 1)
                {
                    during++;
                    if (during > 10000)
                    {
                        copymaze(maze, originmaze, mazeheight, mazewidth);
                        return 0;
                    }
                    symbol = 0;
                    if (wayerror(label) == 1)
                    {
                        copymaze(maze, originmaze, mazeheight, mazewidth);
                        return 0;
                    }
                    whichdirect = Random.Range(0, 4);
                    if(label[whichdirect]==-1)
                        whichdirect = Random.Range(0, 4);
                    switch (whichdirect)
                    {
                        //up
                        case 0:
                            //   from = 0;
                            if (nowx <= 1)
                            {
                                label[0] = -1;
                                break;
                            }
                            nowx--;
                            symbol = isempty(maze, nowx, nowy, mazewidth, mazeheight);
                            if (symbol == 0)
                            {
                                label[0] = -1;
                                nowx++;
                            }
                            break;
                        //down
                        case 1:
                            //    from = 1;
                            if (nowx >= mazeheight - 2)
                            {
                                label[1] = -1;
                                break;
                            }

                            nowx++;
                            symbol = isempty(maze, nowx, nowy, mazewidth, mazeheight);
                            if (symbol == 0)
                            {
                                label[1] = -1;
                                nowx--;
                            }
                            break;
                        //right
                        case 2:
                            //    from = 2;
                            if (nowy >= mazewidth - 2)
                            {
                                label[2] = -1;
                                break;
                            }

                            nowy++;
                            symbol = isempty(maze, nowx, nowy, mazewidth, mazeheight);
                            if (symbol == 0)
                            {
                                label[2] = -1;
                                nowy--;
                            }
                            break;
                        //left
                        case 3:
                            //   from = 3;
                            if (nowy <= 1)
                            {
                                label[3] = -1;
                                break;
                            }

                            nowy--;
                            symbol = isempty(maze, nowx, nowy, mazewidth, mazeheight);
                            if (symbol == 0)
                            {
                                label[3] = -1;
                                nowy++;
                            }
                            break;
                    }

                }
            }
        }
        copymaze(originmaze, maze, mazeheight, mazewidth);
        return 1;
    }
    void set_starter(char[,]maze,int[,]startpoint,int mazeheight,int mazewidth)
    {
        int oop;
        for (int i = 0; i < mazeheight; i++)
            for (int k = 0; k < mazewidth; k++)
            {
                if (i == 0 || k == 0 || i == mazeheight - 1 || k == mazewidth - 1)
                    maze[i, k] = '<';
                else
                {
                    maze[i, k] = '>';
                }
            }
        int widrandom, heightrandom;
        widrandom = mazewidth - 3;
        heightrandom = mazeheight - 3;
        int startwhere;
        int firstland = 2;
        for (oop = 0; oop < 2; oop++)
        {
            if (oop == 0)
            {
                startwhere = rand() % 4;
                firstland = startwhere;
            }
            else
            {
                if (firstland == 1 || firstland == 0)
                    startwhere = firstland + 2;
                else startwhere = firstland - 2;
            }

            switch (startwhere)
            {
                case 0:
                    startwhere = rand() % widrandom + 2;
                    maze[0, startwhere] = '.';
                    startpoint[oop, 0] = 0;
                    startpoint[oop, 1] = startwhere;
                    break;
                case 1:
                    startwhere = rand() % heightrandom + 2;
                    maze[startwhere, mazewidth - 1] = '.';
                    startpoint[oop, 0] = startwhere;
                    startpoint[oop, 1] = mazewidth - 1;
                    break;
                case 2:
                    startwhere = rand() % widrandom + 2;
                    maze[mazeheight - 1, startwhere] = '.';
                    startpoint[oop, 0] = mazeheight - 1;
                    startpoint[oop, 1] = startwhere;
                    break;
                case 3:
                    startwhere = rand() % heightrandom + 2;
                    maze[startwhere, 0] = '.';
                    startpoint[oop, 0] = startwhere;
                    startpoint[oop, 1] = 0;
                    break;
            }
            if (startpoint[1, 0] == startpoint[0, 0] && startpoint[1, 1] == startpoint[1, 1])
                oop = 0;
            if (oop == 1)
            {
                if (startpoint[0, 0] - startpoint[1, 0] > -mazeheight / 1.6f && startpoint[0, 0] - startpoint[1, 0] < mazeheight / 1.6f || startpoint[0, 1] - startpoint[1, 1] > -mazewidth / 1.6f && startpoint[0, 1] - startpoint[1, 1] < mazewidth / 1.6f)
                {
                    oop = 0;
                }
            }
            if(oop == 0)
            {
                if(startpoint[0,0]>mazeheight/5&&startpoint[0,0]<mazeheight/5*4||startpoint[0,1]>mazewidth/5&&startpoint[0,1]<mazewidth/5*4)
                {
                    oop = -1;
                }
            }
        }
        maze[startpoint[0, 0], startpoint[0, 1]] = 'S';
        maze[startpoint[1, 0], startpoint[1, 1]] = 'E';
        digdoor(maze, startpoint[0, 0], startpoint[0, 1], mazeheight, mazewidth);
    }
    int recursive_empty(char[,] maze, int nowx, int nowy, int width, int height)
    {
        int counter = 0;

        if (maze[nowx - 1, nowy] - '>' == 0)
            counter++;

        if (maze[nowx + 1, nowy] - '>' == 0)
            counter++;

        if (maze[nowx, nowy + 1] - '>' == 0)
            counter++;

        if (maze[nowx, nowy - 1] - '>' == 0)
            counter++;

        if (counter == 3)
        {
            maze[nowx, nowy] = '.';
            int direction = Random.Range(0, 4);
            switch (direction)
            {
                case 0:
                    if (nowx > 2)
                        nowx--;
                    else
                    {
                        return 1;
                    }
                    break;
                case 1:
                    if (nowy > 2)
                        nowy--;
                    else
                    {
                        return 1;
                    }
                    break;
                case 2:
                    if (nowx < height - 2)
                        nowx++;
                    else
                    {
                        return 1;
                    }
                    break;
                case 3:
                    if (nowy < width - 2)
                        nowy++;
                    else
                    {
                        return 1;
                    }
                    break;

            }
            recursive_empty(maze, nowx, nowy, width, height);
            return 1;
        }
        return 0;
    }
    public void generatemaze(char[,] maze, char[,] answer, int mazeheight, int mazewidth)
    {
        int[,] startpoint = new int[2, 2];
        int res = 0;
         while(res == 0)
         {
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    startpoint[i, j] = 0;

            char[,] originmaze = new char[300, 600];
            set_starter(maze, startpoint, mazeheight, mazewidth);
            copymaze(originmaze, maze, mazeheight, mazewidth);
            res = perfectconnect(maze, originmaze, nowxp, nowyp, startpoint[1, 0], startpoint[1, 1], mazeheight, mazewidth);
         }
         
            for (int i = 1; i < mazeheight - 1; i++)
                for (int k = 1; k < mazewidth - 1; k++)
                {
                    if (maze[i, k] == '.')
                    {
                        answer[i, k] = '1';
                    }
                }
        
        long toyoo = mazeheight*mazewidth*100;

        while (toyoo > 0)
        {
           recursive_empty(maze, Random.Range(1,mazeheight-1), Random.Range(1, mazewidth - 1), mazewidth, mazeheight);
            toyoo--;
        }
        int aim_xf,aim_yf;
        for (int p = 0; p < mazeheight * mazewidth / 13; p++)
        {
            aim_xf = Random.Range(1, mazeheight - 1);
            aim_yf = Random.Range(1, mazewidth - 1);
            if (maze[aim_xf, aim_yf] == '.')
                continue;
            if (answer[aim_xf - 1, aim_yf] == '1' && answer[aim_xf + 1, aim_yf] == '1' || answer[aim_xf, aim_yf - 1] == '1' && answer[aim_xf, aim_yf + 1] == '1')
                continue;
            maze[aim_xf, aim_yf] = '.';
        }
        for (int di = 2; di < mazeheight - 2; di++)
            for (int j = 2; j < mazewidth - 2; j++)
            {
                int adder = 0;
                for (int ei = di - 1; ei < di + 2; ei++)
                    for (int ej = j - 1; ej < j + 2; ej++)
                    {
                        if (maze[ei, ej] == '.')
                            adder++;
                    }
                if (answer[di, j] == '1')
                    continue;
                if (adder > 7)
                {
                    maze[di, j] = '>';
                }
                if (answer[di - 1, j] == '1' && answer[di + 1, j] == '1' || answer[di, j - 1] == '1' && answer[di, j + 1] == '1')
                    maze[di, j] = '>';
            }
        maze[startpoint[0, 0], startpoint[0, 1]] = 'S';
        maze[startpoint[1, 0], startpoint[1, 1]] = 'E';
    }

    // Use this for initialization

}
