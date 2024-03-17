using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageLight : MonoBehaviour
{
    public GameObject[] Lights;
    public Light[] Setting = new Light[10];

    public float intensity = 4;
    public bool lightActive = true;
    public bool isEffect = false;

    public bool stop = false;
    public Animator anim;


    public GameObject TitleText;
    public bool lightOn = true;

    public GameObject mainCam;
    private camera_move cm;

    // Start is called before the first frame update
    void Start()
    {
        cm = mainCam.GetComponent<camera_move>();

        anim = gameObject.GetComponent<Animator>();

        Lights = GameObject.FindGameObjectsWithTag("Light");
        for (int i = 0; i < Lights.Length; i++)
        {
            Setting[i] = Lights[i].GetComponent<Light>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isEffect)
        {
            for (int i = 0; i < Setting.Length; i++)
            {
                Setting[i].intensity = intensity;
                Setting[i].enabled = lightActive;
            }
            TitleText.SetActive(lightOn);
            if (cm.start)
            {
                foreach (GameObject roomNum in cm.roomNums)
                {
                    roomNum.SetActive(lightOn);
                }
            }
        }
        else
        {
            if (!stop)
            {
                stop = true;
                StartCoroutine("Kachikachi");
            }
        }
    }

    IEnumerator Kachikachi()
    {
        float wait = Random.Range(4, 8);
        yield return new WaitForSeconds(wait);
        anim.SetBool("startAnim", true);

        //↓アニメーション中に行いたい（ゴリ押し）
        yield return new WaitForSeconds(2);
        stop = false;
        anim.SetBool("startAnim", false);
    }
}
