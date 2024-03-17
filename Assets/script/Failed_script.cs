using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Failed_script : MonoBehaviour
{
    public bool startFailedAnim = false;
    const float duration = 2;
    private float v;
    private float time =0;

    public PostProcessVolume Volume;//自身のPostProcessVolumeを入れる。

    public GameObject failedInfoUI;

    // Start is called before the first frame update
    void Start()
    {
        v = 1 / duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (startFailedAnim)
        {
            time += Time.deltaTime;
            if (time >= duration)
            {
                failedInfoUI.SetActive(true);
                failedInfoUI.GetComponent<CanvasGroup>().DOFade(1, 1);

                startFailedAnim = false;
                return;
            }
            Volume.weight = v * time;
        }
    }

    //failedInfoUI内のImageが押された時に呼ばれるメソッド
    public void OnClickRestart()
    {
        SceneManager.LoadSceneAsync("game");
    }
    public void OnClickBack()
    {
        crossSceneInfoManage.crossSceneInfo.played = true;
        SceneManager.LoadSceneAsync("SampleScene");
    }
}
