using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class moveEffect : MonoBehaviour
{
    public GameObject MoveAnim;
    public GameObject StopAnim;

    public GameObject WhichEffect;

    public Vector3 targetPos;
    public Quaternion rotation;

    public string dir;//direction- 進んだ向き
    public string Info;//進んだ先のマスが何か

    private float moveRange;

    [SerializeField] GameObject canvas;

    [SerializeField] GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        GameObject player = GameObject.Find("character");
        Character script = player.GetComponent<Character>();
    }
    void AnimSelect()
    {
        //↓どのマスなのか↓
        if (Info == "floor")
        {
            WhichEffect = MoveAnim;
            moveRange = 280;
        }
        else if (Info == "wall")
        {
            WhichEffect = StopAnim;
            moveRange = 200;
        }

        //
        if (dir == "left")
        {
            targetPos = new Vector3(-moveRange, 0, 0);
            rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        else if (dir == "right")
        {
            targetPos = new Vector3(moveRange, 0, 0);
            //rotation = new Quaternion(0, 0, -90, 0);
            rotation = Quaternion.Euler(new Vector3(0, 0, -90));

        }
        else if (dir == "up")
        {
            targetPos = new Vector3(0, moveRange, 0);
            //rotation = new Quaternion(0, 0, 0, 0);
            rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        }
        else if (dir == "down")
        {
            targetPos = new Vector3(0, -moveRange, 0);
            //rotation = new Quaternion(0, 0, 180, 0);
            rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
        else
        {
            return;
        }

        Vector3 startPos = new Vector3(0, 0, 0);
        if (Info == "wall")
        {
            /*if(dir == "up"||dir == "down")
            {
                startPos.y = targetPos.y / 4 * 3;
            }
            if (dir == "right" || dir == "left")
            {
                startPos.x = targetPos.x / 4 * 3;
            }*/
            startPos = targetPos / 4 * 3;
        }
        Vector3 gap = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        startPos += gap;
        targetPos += gap;


        //Vector3 center = camera.GetComponent<Camera>().WorldToScreenPoint(gap);
        Instantiate(WhichEffect, startPos, rotation, canvas.transform)
               .GetComponent<EffectContent>().moveEffect( startPos, targetPos, Info);
        //※canvasの子オブジェクトとして画面の真ん中に生成する
    }

    public void SetMoveInfo(string direction, string MasuInfo)
    {
        dir = direction;
        Info = MasuInfo;
        AnimSelect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
