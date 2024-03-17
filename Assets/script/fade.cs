using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class fade : MonoBehaviour
{
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FadeIn(float duration)
    {
        panel.SetActive(true);
        panel.GetComponent<CanvasGroup>().DOFade(endValue: 2, duration: duration);
    }
    public void FadeOut(float duration)
    {
        panel.GetComponent<CanvasGroup>().DOFade(endValue: 0, duration: duration);
    }
}
