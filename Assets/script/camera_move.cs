using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class camera_move : MonoBehaviour
{
    public float moto;
    public float ato;
    public float time;
    public float range = 10;
    public int idouKaisuu = 0;
    public bool start;
    public bool startingStage;

    public GameObject manageCam;
    public ManageCamera mc;

    public int idouKaisuuLimit = 11;//これとlastDoor_scriptで使用。
    public bool inEnding = false;

    //public bool startingStage;

    public int dSkeep;//camera_moveとは関係ない。主にstage_scriptで使用。

    public GameObject door;

    public GameObject itemButton;
    public GameObject titleText;
    public GameObject MoveButton;

    public GameObject[] roomNums;//MangeLightでチカチカさせてる

    public GameObject doorSound;

    // Start is called before the first frame update
    void Start()
    {
        mc = manageCam.GetComponent<ManageCamera>();

        roomNums = GameObject.FindGameObjectsWithTag("roomNumber");
        foreach (GameObject roomNum in roomNums)
        {
            roomNum.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Flick();
    }
    public void Flick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            moto = Input.mousePosition.x;
        }
        if (Input.GetMouseButton(0))
        {
            time += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0))
        {
            ato = Input.mousePosition.x;
            if (time <= 0.5f)
            {
                YokoScroll();
            }
            time = 0;
        }
    }
    public void YokoScroll()
    {
        if (inEnding)
        {
            return;
        }

        GameObject obj = GameObject.Find("popup");
        GameObject itemMenu = GameObject.Find("itemMenu");
        if (start && !startingStage && obj == null && itemMenu == null)
        {
            if (moto - ato <= -range || range <= moto - ato)
            {
                if (moto - ato > 0 && idouKaisuu < idouKaisuuLimit)//廊下の長さによって変わる
                {
                    idouKaisuu++;
                    transform.DOMove(new Vector3(idouKaisuu * 4 - 0.5f, 1, -1.5f), 2);
                }
                if (0 > moto - ato && 0 < idouKaisuu)
                {
                    idouKaisuu--;
                    transform.DOMove(new Vector3(idouKaisuu * 4 - 0.5f, 1, -1.5f), 2);
                }//-0.5は初期位置のずれの分
            }
        }

    }
    public void DoMoveCamera()//buttonMoveSelectのスクリプトで使用。
    {
        transform.DOMove(new Vector3(idouKaisuu * 4 - 0.5f, 1, -1.5f), 2);
    }

    public void StartSelecting()
    {
        if (!start && mc.whichCam == 2)
        {
            StartCoroutine("StartSelectinggg");
        }
    }//startdoorのeventTriggerで呼び出してる。
    public IEnumerator StartSelectinggg()
    {
        door.transform.parent.gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.transform.DOMove(new Vector3(-0.5f, 1, gameObject.transform.position.z + 0.6f), 2).SetEase(Ease.InQuart);
        door.transform.DORotate(new Vector3(0, 90), 1f);
        doorSound.GetComponent<AudioSource>().Play();
        gameObject.transform.DOMove(new Vector3(-0.5f, 1, -1.5f), 2).SetEase(Ease.InOutQuart);

        GameObject childText = titleText.transform.GetChild(0).gameObject;
        DOVirtual.Float(1, 0, 0.8f,
            (tweenValue) =>
            {
                Color colour = titleText.GetComponent<TextMesh>().color;
                titleText.GetComponent<TextMesh>().color = new Color(colour.r, colour.g, colour.b, tweenValue);
                Color ColChild = childText.GetComponent<TextMesh>().color;
                childText.GetComponent<TextMesh>().color = new Color(ColChild.r, ColChild.g, ColChild.b, tweenValue);
            });
        while (gameObject.transform.position.z <= -1.7f) yield return null;
        start = true;

        foreach (GameObject roomNum in roomNums)
        {
            roomNum.SetActive(true);
        }

        itemButton.SetActive(true);
        MoveButton.SetActive(true);
        MoveButton.GetComponent<CanvasGroup>().DOFade(endValue: 1,duration: 1);

        mc.whichCam = 3;
    }
    public void EndingAnimStart()
    {
        inEnding = true;
    }
}


