using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class newItemImage_script : MonoBehaviour
{
    private GameObject oya;

    private bool fadeStart = false;
    private float time = 0;
    private float duration = 3f;
    private float rgbValue = 0;
    private float v;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeStart)
        {
            time += Time.deltaTime;
            rgbValue = v * time;
            oya.GetComponent<Image>().color = new Color(rgbValue, rgbValue, rgbValue, 1);
            if(duration <= time)
            {
                fadeStart = false;
                time = 0;
            }
        }
    }
    private void OnEnable()
    {
        oya = gameObject.transform.parent.gameObject;
        v = 1 / duration;
        fadeStart = true;
    }

    public void Onclick()
    {
        StartCoroutine(HideItemImage(oya));
    }

    IEnumerator HideItemImage(GameObject oya)
    {
        float duration = 1.5f;
        oya.GetComponent<CanvasGroup>().DOFade(0, duration);
        yield return new WaitForSeconds(duration);
        oya.SetActive(false);
        oya.GetComponent<CanvasGroup>().alpha = 1;//リセット
        oya.GetComponent<Image>().color = new Color(1, 1, 1, 1);

    }
}
