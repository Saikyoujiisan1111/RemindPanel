using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class stage_script : MonoBehaviour
{
    public int donoStage; //まだ３までしかやってない。注意
    public GameObject popup;
    public GameObject cansel;
    public GameObject maincamera;
    public Vector3 doorFront;

    public camera_move cm;

    public GameObject camManager;
    public ManageCamera mc;

    public GameObject itemButton;
    public GameObject MoveButton;

    public GameObject doorSound;

    // Start is called before the first frame update
    void Start()
    {
        cansel = GameObject.Find("cansel");
        maincamera = GameObject.Find("Main Camera");
        cm = maincamera.GetComponent<camera_move>();
        camManager = GameObject.Find("CameraManager");
        mc = camManager.GetComponent<ManageCamera>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowPopup()
    {
        if(mc.whichCam != 3)
        {
            return;
        }
        cm.dSkeep = donoStage;
        popup.transform.position = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        popup.transform.GetChild(0).GetComponent<Text>().text = $"Room.{donoStage}";
        popup.SetActive(true);
        popup.GetComponent<CanvasGroup>().DOFade(endValue: 1f, duration: 1f);
        cansel.GetComponent<Image>().enabled = true;
    }
    public void HidePopup()
    {
        if (!cm.startingStage)
        {
            cansel.GetComponent<Image>().enabled = false;
            popup.GetComponent<CanvasGroup>().DOFade(endValue: 0f, duration: 0.5f);
            popup.SetActive(false);
        }
    }
    public IEnumerator StartStage()
    {
        //cm.startingStage = true;
        GameObject donodoor = GameObject.Find($"stage{cm.dSkeep}");
        doorFront = donodoor.transform.position;
        maincamera.transform.DOMove(new Vector3(doorFront.x, doorFront.y, doorFront.z - 2f), 1f);
        yield return new WaitForSeconds(1);
        Vector3 knobFront = donodoor.transform.Find("doorWing/doorKnob").gameObject.transform.position;
        maincamera.transform.DOMove(new Vector3(knobFront.x - 0.2f, maincamera.transform.position.y, knobFront.z - 0.45f), 3f);
        maincamera.transform.DORotate(new Vector3(0, 20), 3f);//.SetEase(Ease.InQuint);
        yield return new WaitForSeconds(1);

        donodoor.transform.GetChild(0).gameObject.transform.DORotate(new Vector3(0, -70), 4f).SetEase(Ease.InOutExpo);
        AudioSource sound = doorSound.GetComponent<AudioSource>();
        sound.pitch = 0.5f;//スタートドアに合わせたスピードからステージドアのスピードに合わせる。
        sound.Play();

        fade script = gameObject.transform.parent.GetComponent<fade>();
        script.FadeIn(duration: 7);

        itemButton.GetComponent<CanvasGroup>().DOFade(0, 3).SetEase(Ease.OutQuad);
        MoveButton.GetComponent<CanvasGroup>().DOFade(0, 3).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(5.5f);
        crossSceneInfoManage.crossSceneInfo.whichStage = cm.dSkeep;
        SceneManager.LoadScene("game");
    }

    public void ClearedCheck()
    {
        if (crossSceneInfoManage.crossSceneInfo.clear[donoStage])
        {
            gameObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0, -35, 0);
        }
    }
}
