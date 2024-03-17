using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class itemButton_move : MonoBehaviour
{
    public GameObject mainCam;
    public camera_move cm;

    public Vector3 startPos;
    public Vector3 movePos;

    public bool pushed = false;

    public GameObject itemMenu;

    public const float fadeTime = 1;

    //ーーー
    public GameObject nameText;
    public GameObject diskText;
    public GameObject largeImage;

    // Start is called before the first frame update
    void Start()
    {
        cm = mainCam.GetComponent<camera_move>();
    }

    void OnEnable()
    {
        float moveX = Screen.width * 0.1f; //0.1は画像のサイズによって変わる
        startPos = gameObject.transform.position;
        movePos = new Vector3(startPos.x - moveX, startPos.y, startPos.z);
        gameObject.GetComponent<CanvasGroup>().DOFade(endValue: 1,duration: 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Onclick()
    {
        float PosX = gameObject.transform.position.x;
        Debug.Log($"PosX:{PosX}, movePos:{movePos.x}, startPos:{startPos.x}");
        if (PosX != startPos.x && PosX != movePos.x)
        {
            Debug.Log("returned");
            return;
        }
        if (pushed)
        {
            pushed = false;
            gameObject.transform.DOMove(startPos, 1f).SetEase(Ease.InOutQuart);
            itemMenu.GetComponent<CanvasGroup>().DOFade(endValue: 0, duration: fadeTime).SetEase(Ease.OutQuad);
            StartCoroutine("inActiveMenu");
        }
        else if (!pushed)
        {
            pushed = true;
            gameObject.transform.DOMove(movePos, 1f).SetEase(Ease.InOutQuart);
            itemMenu.SetActive(true);
            itemMenu.GetComponent<CanvasGroup>().DOFade(endValue: 1, duration: fadeTime).SetEase(Ease.OutQuad);
        }
    }
    IEnumerator inActiveMenu()
    {
        yield return new WaitForSeconds(fadeTime);
        itemMenu.SetActive(false);

        nameText.SetActive(false);
        diskText.SetActive(false);
        largeImage.SetActive(false);
    }
    public IEnumerator StopUsing()//lastDoor_scriptから利用
    {
        gameObject.GetComponent<CanvasGroup>().DOFade(0, 1);//アイテムボタンを見えなくする
        itemMenu.GetComponent<CanvasGroup>().DOFade(endValue: 0, duration: fadeTime).SetEase(Ease.OutQuad);
        StartCoroutine("inActiveMenu");
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
