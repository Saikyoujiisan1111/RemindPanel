using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class EndingCameraMove : MonoBehaviour
{
    public GameObject first;
    public GameObject second;

    public float PosZ;
    public float PosY;

    private float WaitSeconds = 2;

    public GameObject doorWing;

    public GameObject FadePanel;

    public GameObject contents;
    public GameObject textOne;
    public GameObject textTwo;

    public GameObject footSound;
    private AudioSource walksound;
    public GameObject runSound;
    private AudioSource runsound;

    // Start is called before the first frame update
    void Start()
    {
        walksound = footSound.GetComponent<AudioSource>();
        runsound = runSound.GetComponent<AudioSource>();

        PosZ = gameObject.transform.position.z;
        PosY = gameObject.transform.position.y;

        FadePanel.GetComponent<CanvasGroup>().DOFade(0, 1);
        StartCoroutine("AnimMain");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, PosY, PosZ);
    }

    IEnumerator AnimMain()
    {

        StartCoroutine("WalkFoward");
        StartCoroutine("Yure");
        StartCoroutine("Credit");

        yield return new WaitForSeconds(5);

        gameObject.transform.DORotate(new Vector3(8, 0, 0), 2f).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(WaitSeconds);

        yield return new WaitForSeconds(1);
        gameObject.transform.DORotate(new Vector3(3, 0, 0), 2.5f).SetEase(Ease.OutQuad);
        doorWing.transform.DORotate(new Vector3(0, 180 + 130, 0), 2).SetEase(Ease.InOutQuad);//180はdoorwingのrotationのyの値
        yield return new WaitForSeconds(2);
        yield return new WaitForSeconds(1);

        yield return new WaitForSeconds(1.5f);
        crossSceneInfoManage.crossSceneInfo.played = true;
        yield return　SceneManager.LoadSceneAsync("SampleScene");
        //SceneManager.LoadSceneAsync("SampleScene");

    }

    IEnumerator WalkFoward()
    {
        //firstまで5秒で
        DOVirtual.Float(
        gameObject.transform.position.z,
        first.transform.position.z,
        5,
        (tweenValue) =>
        {
            PosZ = tweenValue;
        }
        ).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(5);

        //2秒待機
        yield return new WaitForSeconds(WaitSeconds);

        //secondまで4秒で
        DOVirtual.Float(
        gameObject.transform.position.z,
        second.transform.position.z,
        4f,
        (tweenValue) =>
        {
            PosZ = tweenValue;
        }
        ).SetEase(Ease.InQuad);
        yield return new WaitForSeconds(4.5f);
    }

    IEnumerator Yure()
    {
        float walkSpeed = 0.5f;
        yield return new WaitForSeconds(walkSpeed);
        yield return new WaitForSeconds(0.15f);
        MovePosY(walkSpeed, 0.02f);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.15f);
        MovePosY(walkSpeed, -0.02f);
        walksound.Play();
        yield return new WaitForSeconds(walkSpeed);
        yield return new WaitForSeconds(0.15f);
        MovePosY(walkSpeed, 0.02f);
        yield return new WaitForSeconds(walkSpeed);
        yield return new WaitForSeconds(0.15f);
        MovePosY(walkSpeed, -0.02f);
        walksound.Play();
        yield return new WaitForSeconds(walkSpeed);
        yield return new WaitForSeconds(0.15f);
        MovePosY(walkSpeed, 0.02f);
        yield return new WaitForSeconds(walkSpeed);
        yield return new WaitForSeconds(0.15f);
        MovePosY(walkSpeed, -0.02f);
        walksound.Play();
        yield return new WaitForSeconds(walkSpeed);
        yield return new WaitForSeconds(0.6f);
        //ここまで5秒

        yield return new WaitForSeconds(WaitSeconds);

        float runSpeed = 0.25f;
        MovePosY(runSpeed, 0.07f);
        yield return new WaitForSeconds(runSpeed);
        yield return new WaitForSeconds(0.08f);
        MovePosY(runSpeed, -0.07f);
        runsound.Play();
        yield return new WaitForSeconds(runSpeed);
        yield return new WaitForSeconds(0.08f);
        MovePosY(runSpeed, 0.07f);
        yield return new WaitForSeconds(runSpeed);
        yield return new WaitForSeconds(0.08f);
        MovePosY(runSpeed, -0.07f);
        runsound.Play();
        yield return new WaitForSeconds(runSpeed);
        yield return new WaitForSeconds(0.08f);
        MovePosY(runSpeed, 0.08f);
        yield return new WaitForSeconds(runSpeed);
        yield return new WaitForSeconds(0.08f);
        MovePosY(runSpeed, -0.09f);
        runsound.Play();
        yield return new WaitForSeconds(runSpeed);
        yield return new WaitForSeconds(0.08f);
        MovePosY(runSpeed, 0.08f);
        yield return new WaitForSeconds(runSpeed);
        yield return new WaitForSeconds(0.08f);
        MovePosY(runSpeed, -0.09f);
        runsound.Play();
        yield return new WaitForSeconds(runSpeed);
        yield return new WaitForSeconds(0.08f);

    }

    void MovePosY(float durartion, float YureHaba)
    {
        DOVirtual.Float(
        gameObject.transform.position.y,
        gameObject.transform.position.y + YureHaba,
        durartion,
        (tweenValue) =>
        {
            PosY = tweenValue;
        }
        );
    }

    IEnumerator Credit()
    {
        CanvasGroup content = contents.GetComponent<CanvasGroup>();

        /*textOne.SetActive(true);
        textTwo.SetActive(false);
        yield return new WaitForSeconds(1.5f);

        content.DOFade(1, 1f);
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(2);
        content.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);

        textOne.SetActive(false);
        textTwo.SetActive(true);
        yield return new WaitForSeconds(2);

        content.DOFade(1, 1f);
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(2);
        content.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);*/

        textOne.SetActive(true);
        textTwo.SetActive(false);
        yield return new WaitForSeconds(1.5f);

        content.DOFade(1, 1f);
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(4);
        content.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
    }
}
