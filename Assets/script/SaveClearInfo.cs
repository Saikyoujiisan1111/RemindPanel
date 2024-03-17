using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveClearInfo : MonoBehaviour
{
    public TextAsset SetupClearInfo;//アプリの初起動時にしか使わない。

    //public GameObject itemManagerObject;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("ClearData"))//初起動時
        {
            string info = SetupClearInfo.text.Replace("\n", "");//改行を無くして、1行にする。
            PlayerPrefs.SetString("ClearData", info);

            int time = crossSceneInfoManage.crossSceneInfo.clear.Length;
            for(int count = 0; count < time; count++)//ちょとあやしい
            {
                crossSceneInfoManage.crossSceneInfo.clear[count] = false;
            }
            SaveOutput();
        }
        else
        {
            SaveOutput();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void SaveOutput()
    {
        string[] info = PlayerPrefs.GetString("ClearData").Split(','); //"true,false,flase" →  bool[]{true,false,false}にしたい
        //int time = 0;

        int time = crossSceneInfoManage.crossSceneInfo.clear.Length;
        for (int count = 0; count < time; count++)//ちょとあやしい
        {
            crossSceneInfoManage.crossSceneInfo.clear[count] = Convert.ToBoolean(info[count]);
        }
        /*foreach (string TrueFalse in info)
        {
            crossSceneInfoManage.crossSceneInfo.clear[time] = Convert.ToBoolean(TrueFalse);
            time++;
        }//ステージのクリア情報がbool型の配列となって扱える。*/


        GameObject ManagerObj = GameObject.Find("itemManager");
        ManagerObj.GetComponent<itemManager>().ItemManagement();

        //各ステージに、ステージがクリアされているかどうかを確認させる。（ドア半開き）
        GameObject[] stages = GameObject.FindGameObjectsWithTag("stage");
        foreach (GameObject stage in stages)
        {
            stage_script script = stage.GetComponent<stage_script>();
            script.ClearedCheck();
        }
    }
    public static void SaveInput()
    {
        string InfoInput = "";
        foreach(bool addInfo in crossSceneInfoManage.crossSceneInfo.clear)
        {
            if (InfoInput == "")//最初の一回目だけ
            {
                InfoInput = addInfo.ToString();
            }
            else
            {
                InfoInput = $"{InfoInput},{addInfo}";
            }
        }
        PlayerPrefs.SetString("ClearData", InfoInput);
        PlayerPrefs.Save();
    }

}
//