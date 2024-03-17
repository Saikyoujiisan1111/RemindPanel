using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

public class GameSceneCamera_move : MonoBehaviour
{
    public GameObject mpoint1;
    public GameObject mpoint2;
    public GameObject smaker;
    public stageMaker sm;

    public bool canMovePlayer = false;

    public int progress = 0;

    public GameObject PostProcess;
    public Animator effectAnim;

    public GameObject FootSound;
    private AudioSource Sound;

    //int stageClearState; //ステージのクリア状態　0:スタートから 1~2:x区画目 3:クリア

    private bool startSound = false;
    private float time = 0;
    private int Playcount = 0;
    private const float rest = 0.8f;
    private int countMax;

    //ライトマスの効果がそのエリア外で影響を与えないための変数
    private bool waitingOnAreaMasu = false;

    // Start is called before the first frame update
    void Start()
    {
        Sound = FootSound.GetComponent<AudioSource>();

        effectAnim = PostProcess.GetComponent<Animator>();

        smaker = GameObject.Find("StageMaker");
        sm = smaker.GetComponent<stageMaker>();

        mpoint1 = GameObject.Find("movePoint1");
        mpoint2 = GameObject.Find("movePoint2");

        CameraMove();

    }

    // Update is called once per frame
    void Update()
    {
        //足音を指定回数、指定の間隔で流す。
        if (startSound)
        {
            time += Time.deltaTime;
            if(Playcount * rest < time)
            {
                Playcount++;
                Sound.Play();
                if(countMax <= Playcount)
                {
                    startSound = false;
                }
            }
        }

    }
    public void CameraMove()
    {
        switch (progress)
        {
            case 0:
                StartCoroutine("StartMove");
                break;

            case 1:
                LightON();
                transform.DOMove(new Vector3(transform.position.x + sm.yokoHaba, transform.position.y, transform.position.z), 2);
                waitingOnAreaMasu = true;
                StartCoroutine("LightOFF");
                break;

            case 2:
                LightON();
                waitingOnAreaMasu = false;
                transform.DOMove(new Vector3(transform.position.x + sm.yokoHaba, transform.position.y, transform.position.z), 2);
                waitingOnAreaMasu = true;
                StartCoroutine("LightOFF");
                break;

            case 3:
                waitingOnAreaMasu = true;
                LightON();
                effectAnim.SetBool("goal", true);
                break;

            default:
                LightON();
                break;

        }
    }
    public IEnumerator LightOFF()
    {
        yield return new WaitForSeconds(5);
        fade script = gameObject.GetComponent<fade>();
        script.FadeIn(0.5f);
        canMovePlayer = true;
    }
    public void LightON()
    {
        fade script = gameObject.GetComponent<fade>();
        script.FadeOut(0.5f);
        canMovePlayer = false;
    }
    public IEnumerator StartMove()
    {
        float mtime = 3;
        countMax = (int)(mtime / rest);
        startSound = true;

        transform.DOMove(mpoint1.transform.position, mtime); 
        transform.DORotate(mpoint1.transform.localEulerAngles, mtime);
        yield return new WaitForSeconds(mtime);
        gameObject.GetComponent<PostProcessLayer>().volumeTrigger = PostProcess.transform;


        transform.DOMove(mpoint2.transform.position, mtime);
        transform.DORotate(mpoint2.transform.localEulerAngles, mtime);

        yield return new WaitForSeconds(mtime);
        StartCoroutine("LightOFF");
    }

    public void LightControl(string OnOff)//character(script)から使用
    {
        if(OnOff == "on")
        {
            fade script = gameObject.GetComponent<fade>();
            script.panel.GetComponent<CanvasGroup>().alpha = 0;
            StartCoroutine("lightMasuLimit");
        }
        if (OnOff == "off")
        {
            fade script = gameObject.GetComponent<fade>();
            script.panel.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    private IEnumerator lightMasuLimit()
    {
        yield return new WaitForSeconds(5);
        if (!waitingOnAreaMasu)
        {
            fade script = gameObject.GetComponent<fade>();
            script.panel.GetComponent<CanvasGroup>().alpha = 1;
        }
    }
}
