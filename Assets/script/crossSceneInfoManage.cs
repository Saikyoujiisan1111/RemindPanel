using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class crossSceneInfoManage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static class crossSceneInfo
    {
        public static int whichStage { get; set; }
        public static bool played { get; set; }
        public static bool[] clear = new bool[20];
        public static bool getNewItem { get; set; }
    }
    public void BackStageSelect()
    {
        if(!crossSceneInfo.clear[crossSceneInfo.whichStage])
        {
            crossSceneInfo.clear[crossSceneInfo.whichStage] = true;
            SaveClearInfo.SaveInput();

            int clearCount = 0;
            foreach (bool stageclear in crossSceneInfo.clear)
            {
                if (stageclear)
                {
                    clearCount++;
                }
            }
            if (clearCount % 2 == 0)//偶数だったら
            {
                crossSceneInfo.getNewItem = true;
            }
        }
        crossSceneInfo.played = true;
        SceneManager.LoadScene("SampleScene");
    }
}
