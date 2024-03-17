using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamActiveInfo : MonoBehaviour
{
    public float CamActive;//0は動いてる。1は止まってる。
    public bool stop = false;

    public GameObject CamManager;
    public ManageCamera mc;

    public GameObject skipsensor;
    public skipMeter_script skipScript;

    // Start is called before the first frame update
    void Start()
    {
        mc = CamManager.GetComponent<ManageCamera>();
        skipScript = skipsensor.GetComponent<skipMeter_script>();

        if (crossSceneInfoManage.crossSceneInfo.played)
        {
            stop = true;//この時に行わないと、アニメが始まってしまう。
            skipScript.skipped = true;
            StartCoroutine("CameraSwichTwo");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (stop)
        {
            return;
        }
        else {
            if (1 <= CamActive)
            {
                skipScript.skipped = true;
                StartCoroutine("CameraSwichOne");
            }
            if (CamActive == 0)
            {
                gameObject.GetComponent<Camera>().enabled = true;

            }
        }
    }
    public IEnumerator CameraSwichOne()
    {
        stop = true;

        fade script = gameObject.GetComponent<fade>();
        script.FadeIn(2);
        yield return new WaitForSeconds(2);
        StartCoroutine("CameraSwichTwo");
    }
    public IEnumerator CameraSwichTwo()
    {
        gameObject.GetComponent<Camera>().enabled = false;

        fade script = gameObject.GetComponent<fade>();

        script.panel.SetActive(true);
        script.panel.GetComponent<CanvasGroup>().alpha = 1;
        //※Twoの方から始めたい時があるから。

        script.FadeOut(2);
        yield return new WaitForSeconds(2);
        script.panel.SetActive(false);
        mc.whichCam = 2;
    }
}
