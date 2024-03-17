using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backStageSelect : MonoBehaviour
{
    public bool startFade = false;
    public bool moveScene = false;
    //どっちもAnimationからtrueにしてる。

    public GameObject crossInfoManager;
    public crossSceneInfoManage csInfo;

    // Start is called before the first frame update
    void Start()
    {
        csInfo = crossInfoManager.GetComponent<crossSceneInfoManage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startFade)
        {
            fade script = gameObject.GetComponent<fade>();
            script.FadeIn(5);
        }
        if (moveScene)
        {
            csInfo.BackStageSelect();
        }
    }
}
