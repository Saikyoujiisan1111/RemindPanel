using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ManageCamera : MonoBehaviour
{
    public GameObject AnimationCam;
    public GameObject GameCam;

    public CamActiveInfo activeInfo;

    public Camera cam;

    public float whichCam = 1;

    public bool stop = false;

    // Start is called before the first frame update
    void Start()
    {
        activeInfo = AnimationCam.GetComponent<CamActiveInfo>();
        cam = GameCam.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //アニメーションが終わった時の
        if (!stop)
        {
            if (activeInfo.CamActive >= 1)
            {
                stop = true;
                cam.enabled = true;
            }
        }
    }
}
