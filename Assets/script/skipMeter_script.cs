using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class skipMeter_script : MonoBehaviour
{
    public GameObject skipMeter;

    private Tweener tweener;

    public GameObject AnimeCam;
    public CamActiveInfo CAI;

    public bool skipped = false;

    // Start is called before the first frame update
    void Start()
    {
        CAI = AnimeCam.GetComponent<CamActiveInfo>();
    }

    // Update is called once per frame
    void Update()
    {

        if(!skipped)
        {
            if (Input.GetMouseButtonDown(0))
            {
                skipMeter.GetComponent<Image>().fillAmount = 0;
                FollowFinger();
                skipMeter.SetActive(true);
                tweener = skipMeter.GetComponent<Image>().DOFillAmount(1, 2);
                tweener.Play();
            }
            if (Input.GetMouseButton(0))
            {
                FollowFinger();
                if (skipMeter.GetComponent<Image>().fillAmount >= 1)
                {
                    AnimeCam.GetComponent<Animator>().enabled = false;//CamActive(変数)を変えたいため
                    CAI.CamActive = 1;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                tweener.Kill();
                skipMeter.GetComponent<Image>().fillAmount = 0;
                skipMeter.SetActive(false);
            }

            float fillAmount = skipMeter.GetComponent<Image>().fillAmount;//updateでこれ回すのは...
            if(fillAmount >= 1)
            {
                AnimFinish();
            }
        }
        else
        {
            AnimFinish();
        }
    }
    void AnimFinish()
    {
        tweener.Kill();
        skipped = true;
        skipMeter.GetComponent<Image>().fillAmount = 0;
        skipMeter.SetActive(false);
    }
    void FollowFinger()
    {
        float inputX = Input.mousePosition.x;
        float inputY = Input.mousePosition.y;
        skipMeter.GetComponent<RectTransform>().position = new Vector2(inputX, inputY);
    }
}
