using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonMoveSelectStage : MonoBehaviour
{
    private RectTransform rect;

    private Vector2 startPos;
    private Vector2 movedPos;
    private Vector2 startScale;
    private Vector2 movedScale;

    private Vector2 initialScale;

    public bool pushed = false;

    private bool AnimStart = false;
    private const float duration = 1;
    private float time;
    private Vector2 moveV;
    private Vector2 scaleV;

    private GameObject child;
    private CanvasGroup childFade;
    private const float fadeDuration = 0.2f;
    private float alphaSet = 0;
    private bool stopSetActive = false;

    public GameObject mainCam;
    private camera_move cm;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        rect = gameObject.GetComponent<RectTransform>();
        initialScale = rect.sizeDelta;

        child = gameObject.transform.GetChild(0).gameObject;
        childFade = child.GetComponent<CanvasGroup>();

        cm = mainCam.GetComponent<camera_move>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (AnimStart)
        {
            if (!pushed)
            {
                alphaSet = 1 - time * 1 / fadeDuration;
                childFade.alpha = alphaSet;
                if(alphaSet <= 0)
                {
                    SetActiveContent(false);
                }
            }
            if (time <= duration)
            {
                rect.anchoredPosition = moveV * time + startPos;
                rect.sizeDelta = scaleV * time + startScale;
            }
            else
            {
                if (!pushed)
                {
                    AnimStart = false;
                    stopSetActive = false;
                }
                if (pushed)
                {
                    SetActiveContent(true);
                    alphaSet = (time - 1) * 1 / fadeDuration;
                    childFade.alpha = alphaSet;
                }
            }
            if (pushed && time >= duration + fadeDuration)
            {
                AnimStart = false;
                stopSetActive = false;
            }
        }
    }
    public void OnClick()
    {
        if (AnimStart)
        {
            return;
        }
        //移動先は自分に右端、widthが２倍に

        startPos = rect.anchoredPosition;
        startScale = rect.sizeDelta;
        if (pushed)//押されてもどる
        {
            movedPos = new Vector2(startPos.x - initialScale.x * 1.2f, startPos.y);
            movedScale = new Vector2(initialScale.x, startScale.y);

            pushed = false;
            SetAnimation();
        }
        else if (!pushed)//押されて出てくる
        {
            movedPos = new Vector2(startPos.x + initialScale.x * 1.2f, startPos.y);
            movedScale = new Vector2(startScale.x * 3, startScale.y);

            pushed = true;
            SetAnimation();
        }
    }
    private void SetAnimation()
    {
        moveV = (movedPos - startPos) / duration;
        scaleV = (movedScale - startScale) / duration;
        time = 0;
        AnimStart = true;
    }
    private void SetActiveContent(bool boolean)
    {
        if (stopSetActive)
        {
            return;
        }
        stopSetActive = true;
        child.SetActive(boolean);
    }

    public void contentOnClick()
    {
        //三つのボタンの各横幅
        Vector2 eachButtonWidth = new Vector2(rect.sizeDelta.x / 3, rect.sizeDelta.y);

        Vector2 oneThird = new Vector2(rect.position.x - eachButtonWidth.x / 2, rect.position.y);
        Vector2 twoThird = new Vector2(rect.position.x + eachButtonWidth.x / 2, rect.position.y);

        Vector2 touchPos = Input.GetTouch(0).position;
        if(touchPos.x < oneThird.x)//左のボタン
        {
            OnClick();
        }
        else if (GameObject.Find("popup") != null)//ポップアップが出ている時は移動させない。
        {
            return;
        }
        else if(touchPos.x < twoThird.x)//真ん中のボタン
        {
            cm.idouKaisuu = 0;
            cm.DoMoveCamera();
            OnClick();
        }
        else//右のボタン
        {
            cm.idouKaisuu = 6;
            cm.DoMoveCamera();
            OnClick();
        }
    }
}
