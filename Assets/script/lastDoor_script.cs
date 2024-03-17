using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class lastDoor_script : MonoBehaviour
{
    //public bool clearAllStage = false;
    public bool stop = false;

    public GameObject MainCamera;
    public camera_move cm;

    public Coroutine Anim;

    private bool goFoward = false;
    private float PosY;
    private float PosZ;

    public GameObject itemButton;
    public GameObject MoveButton;

    public GameObject footSound;
    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        cm = MainCamera.GetComponent<camera_move>();
        sound = footSound.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (goFoward)
        {
            MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, PosY, PosZ);
        }
    }

    public void OnClick()
    {
        if (stop)
        {
            return;
        }
        stop = true;
        int clearStage = 0;
        foreach(bool clear in crossSceneInfoManage.crossSceneInfo.clear)
        {
            if (clear)
            {
                clearStage++;
            }
        }

        if(clearStage >= 20)//全ステージがクリア済みなら
        {
            cm.EndingAnimStart();
            StartCoroutine("MovetoLastDoor");
        }
        else
        {
            Anim = StartCoroutine("Kyohi");
        }
    }

    IEnumerator Kyohi()
    {
        yield return new WaitForSeconds(0.3f);
            shouldContinueAnim(new Vector3(5f, 0, 0), 1f);//中下
        yield return new WaitForSeconds(1.6f);
            shouldContinueAnim(new Vector3(5.9f, -18, 0), 0.4f);//左下
        yield return new WaitForSeconds(0.3f);
            shouldContinueAnim(new Vector3(5.9f, 18, 0), 0.5f);
        yield return new WaitForSeconds(0.4f);
            shouldContinueAnim(new Vector3(5, -6, 0), 0.5f);//中（ちょい左）下
        yield return new WaitForSeconds(0.5f);
            shouldContinueAnim(new Vector3(5, 0, 0), 0.5f);//中下
        yield return new WaitForSeconds(0.6f);
        MainCamera.transform.DORotate(new Vector3(0, 0, 0), 1).SetEase(Ease.InOutQuad);//真ん中
        yield return new WaitForSeconds(1);
        stop = false;
    }

    public void shouldContinueAnim(Vector3 dest, float duration)
    {
        Tween Rotate = MainCamera.transform.DORotate(dest, duration).SetEase(Ease.InOutQuad);
        if (cm.idouKaisuu < cm.idouKaisuuLimit)
        {
            StopCoroutine(Anim);
            Rotate.Kill();
            MainCamera.transform.DORotate(new Vector3(0, 0, 0), 2).SetEase(Ease.InOutQuad);
            stop = false;
        }
    }

    IEnumerator MovetoLastDoor()
    {
        PosY = MainCamera.transform.position.y;
        PosZ = MainCamera.transform.position.z;

        yield return new WaitForSeconds(2);
        itemButton.GetComponent<itemButton_move>().StartCoroutine("StopUsing");
        itemButton.GetComponent<CanvasGroup>().DOFade(0, 1);
        MoveButton.GetComponent<CanvasGroup>().DOFade(0, 1);

        Vector3 ThisPos = gameObject.transform.position;
        MainCamera.transform.DOMove(new Vector3(ThisPos.x, ThisPos.y, ThisPos.z - 2f), 2f);
        yield return new WaitForSeconds(2);
        yield return new WaitForSeconds(2.5f);

        DOVirtual.Float(
            MainCamera.transform.position.z,
            ThisPos.z + 0.5f,
            10,
            (tweenValue) =>
            {
                PosZ = tweenValue;
            }
            ).SetEase(Ease.InQuad);
        downY();
        sound.Play();
        goFoward = true;

        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.15f);
        upY();
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.15f);
        downY();
        sound.Play();
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.15f);
        upY();
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.15f);
        downY();
        sound.Play();
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.15f);
        upY();
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.15f);
        downY();
        sound.Play();
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.15f);
        upY();
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.15f);
        downY();
        sound.Play();
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.15f);
        upY();
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.15f);
        downY();
        sound.Play();
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.15f);
        upY();
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.15f);
        downY();
        sound.Play();
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.15f);
        upY();
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.15f);
        downY();
        sound.Play();

        yield return new WaitForSeconds(2.5f);
        yield return new WaitForSeconds(2);

        SceneManager.LoadSceneAsync("EndingScene");

    }
    void downY()
    {
        DOVirtual.Float(
        MainCamera.transform.position.y,
        MainCamera.transform.position.y - 0.02f,
        0.5f,
        (tweenValue) =>
        {
            PosY = tweenValue;
        }
        );
    }
    void upY()
    {
        DOVirtual.Float(
        MainCamera.transform.position.y,
        MainCamera.transform.position.y + 0.02f,
        0.5f,
        (tweenValue) =>
        {
            PosY = tweenValue;
        }
        );
    }
}
