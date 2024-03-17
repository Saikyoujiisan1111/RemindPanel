using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class popup_move : MonoBehaviour
{
    public Vector3 ichi;
    public Vector3 limit;
    public Vector3 difference;
    public GameObject cansel;
    public GameObject stage;//基本的にstage0を参照
    public bool inrange;

    public stage_script ss;
    public camera_move cm;

    private float guidetime;
    private bool guideTimeStart = false;
    private bool stop = false;
    public GameObject guideText;

    // Start is called before the first frame update
    void Start()
    {
        cansel = GameObject.Find("cansel");
        ss = stage.GetComponent<stage_script>();
        cm = GameObject.Find("Main Camera").GetComponent<camera_move>();
    }
    void OnEnable()
    {
        inrange = false;
        ichi = gameObject.transform.position;
        limit = new Vector3(ichi.x, ichi.y + Screen.height * 0.3f, ichi.z);

        stop = false;
        guidetime = 0;
        guideText.SetActive(false);
        guideText.GetComponent<CanvasGroup>().alpha = 0;
        guideTimeStart = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (guideTimeStart)
        {
            guidetime += Time.deltaTime;
            if (guidetime >= 2 && !stop)
            {
                stop = true;
                guideText.SetActive(true);
                guideText.GetComponent<CanvasGroup>().DOFade(1, 1);
            }
        }
    }
    public void StartDrag()
    {
        difference.y = ichi.y - Input.mousePosition.y;
    }
    public void DragPopup()
    {
        float posCorrection = Input.mousePosition.y + difference.y;
        if (posCorrection >= Screen.height / 2f && !inrange)
        {
            ichi = new Vector3(ichi.x, posCorrection, ichi.z);
            gameObject.transform.position = ichi;
            if (posCorrection >= limit.y)
            {
                StartCoroutine(Hidepopup(1));
            }
        }
    }
    public void FixePosition()
    {
        if (!inrange)
        {
            difference.y = 0;
            ichi = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
            gameObject.transform.DOMove(new Vector3(Screen.width / 2f, Screen.height / 2f, 0), 0.5f).SetEase(Ease.InOutQuart);
        }
    }
    public IEnumerator Hidepopup(float time)
    {
        inrange = true;
        cm.startingStage = true;
        gameObject.transform.DOMove(new Vector3(Screen.width / 2f, Screen.height * 3f, 0), time).SetEase(Ease.InOutQuart);
        yield return new WaitForSeconds(time);
        ss.StartCoroutine("StartStage");
        //ss.StartStage();
    }
}
