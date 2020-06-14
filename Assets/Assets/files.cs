using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System;
using System.IO;
using System.Diagnostics;

public class files  {
 
  string filePath;
  public void open_outspace_file(string fileName)
  {
        return;
        filePath = Path.Combine("icemaze_Data", fileName);
  Process.Start(filePath);
  }
  public string get_inputfield_text(InputField searcher)
  {
     string textn=searcher.GetComponent<InputField>().text;
	 return textn;
  }
    
  public string Pack(char [,]array,int height,int width,int difficulty=0)
  {
  	  string weller="";
      
      for(int i=0;i< height; i++)
        {
            char[] temparray = new char[width];
            for (int j = 0; j < width; j++)
                temparray[j] = array[i, j];
            string frog = new string(temparray);
            weller += frog;
        }

	  return weller;
  }
  public void Unpack(char[,]array,string weller,int height,int width)
  {
        int foxtime = 0;

        if (weller == "NULL")
            return;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
               array[i, j] = weller[foxtime];
               foxtime++;                  
            }
        }
  }
  public void Write(string pathAndName,string stringData)
    {
        return;
        FileInfo textFile=new FileInfo(pathAndName);
        if(textFile.Exists)
            textFile.Delete();
        StreamWriter writer;
        writer=textFile.CreateText();
        writer.Write(stringData);

        writer.Close();
    }
  public void BinaryWriteString(string filename,string stringData)
    {
        return;
        BinaryWriter bw;
        string path= Path.Combine("icemaze_Data","icesave");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        bw = new BinaryWriter(new FileStream("icemaze_Data/icesave/"+ filename, FileMode.Create));
        try
        {
            bw.Write(stringData);
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message + "\n Cannot open file.");
        }
        bw.Close();
       
    }
    public void BinaryWriteInt(string filename, int intData )
    {
        return;

        BinaryWriter bw;
        string path = Path.Combine("icemaze_Data", "icesave");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        bw = new BinaryWriter(new FileStream("icemaze_Data/icesave/" + filename, FileMode.Create));
        try
        {
            bw.Write(intData);
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message + "\n Cannot open file.");
        }
        bw.Close();
    }
    public void BinaryWriteLong(string filename, long intData)
    {
        return;

        BinaryWriter bw;

        string path = Path.Combine("icemaze_Data", "icesave");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        bw = new BinaryWriter(new FileStream("icemaze_Data/icesave/" + filename, FileMode.Create));
        try
        {
            bw.Write(intData);
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message + "\n Cannot open file.");
        }
        bw.Close();
    }
    public string BinaryReadString(string filename)
    {
        string readstring="Fail";
        /*
        BinaryReader br;
        try
        {
            br = new BinaryReader(new FileStream("icemaze_Data/icesave/" + filename, FileMode.Open));
            readstring = br.ReadString();
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message + "\n Cannot open file.");
            BinaryWriteString(filename, "NULL");
            return "Fail";
        }
        br.Close();
        */
        return readstring;

    }
    public int BinaryReadInt(string filename)
    {
        int readint = -1;
        /*
        BinaryReader br;
        try
        {
            br = new BinaryReader(new FileStream("icemaze_Data/icesave/" + filename, FileMode.Open));
            readint = br.ReadInt32();
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message + "\n Cannot open file.");
            BinaryWriteInt(filename, -1);
            return -1;
        }
        br.Close();
        */
        return readint;
    }
    public long BinaryReadLong(string filename)
    {
        long readint = -1;
        /*
        BinaryReader br;
        try
        {
            br = new BinaryReader(new FileStream("icemaze_Data/icesave/" + filename, FileMode.Open));
            readint = br.ReadInt64();
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message + "\n Cannot open file.");
            BinaryWriteLong(filename, -1);
            return -1;
        }
        br.Close();
        */
        return readint;
    }
    public string Read(string pathAndName,string fileName="temper.txt")
    {
        string dataAsString="NULL";
        /*
        try{
            StreamReader textReader=File.OpenText(pathAndName);
            dataAsString=textReader.ReadToEnd();
            textReader.Close();
        }
        catch(Exception e)// can not read or find the file
        {
            Console.WriteLine(e.Message + "\n Cannot open file.");
            string filePath ="";
			filePath= Path.Combine("icemaze_Data", fileName);
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("NULL");
			dataAsString="NULL";
            sw.Close();
        }
        */
        return dataAsString;
    }
}
