using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject cam;
    public GameSceneCamera_move cm;

    public GameObject moveEffectManager;
    public moveEffect mEscript;

    public float xtap;
    public float ytap;
    public Vector3 objPos;
    public Vector3 oldPos;

    public int moveRange = 1;

    public string direction = "up";//moveEffectに向かう方向を伝えるため

    public bool onJumpMasu = false;
    public bool jumped = false;
    public bool onMasu = true;

    public bool playedStopAnim = false;

    public int onLight = 0;
    public float movedTime = 0;
    //カメラにピクトグラムの角度を合わせるのに使用。上下に動いた回数

    public int areaNow = 1;

    public GameObject clearWall;

    // Start is called before the first frame update
    void Start()
    {
        cm = cam.GetComponent<GameSceneCamera_move>();

        mEscript = moveEffectManager.GetComponent<moveEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            xtap = Input.mousePosition.x;
            xtap /= Screen.width;

            ytap = Input.mousePosition.y;
            ytap /= Screen.height;

            Idou(xtap, ytap);
        }
        if(!onMasu)
        {
            gameObject.transform.position = oldPos;
            jumped = false;
            mEscript.SetMoveInfo(direction, "wall");
            playedStopAnim = true;
        }
    }
    void Idou(float x,float y)
    {
        if (!cm.canMovePlayer)
        {
            return;
        }

        if (onJumpMasu)
        {
            onJumpMasu = false;
            moveRange = 2;
            jumped = true;
        }
        objPos = gameObject.transform.position;
        oldPos = objPos;
        if(x <= 0.3f)//left
        {
            direction = "left";
            objPos.z -= moveRange;
            gameObject.transform.position = objPos;
            moveRange = 1;
        }
        else if (0.7f <= x)//right
        {
            direction = "right";
            objPos.z += moveRange;
            gameObject.transform.position = objPos;
            moveRange = 1;
        }
        else
        {
            if(y >= 0.5f)//up
            {
                direction = "up";
                objPos.x -= moveRange;
                gameObject.transform.position = objPos;
                moveRange = 1;
                movedTime--;
            }
            if(0.5 > y)//down
            {
                direction = "down";
                objPos.x += moveRange;
                gameObject.transform.position = objPos;
                moveRange = 1;

                movedTime++;
            }
        }
    }
    void OnTriggerEnter(Collider obj)
    {
        if (onLight >= 2)
        {
            onLight--;
        }
        else if(onLight == 1)
        {
            onLight--;
            cm.LightControl("off");
        }

        string str = "none,  jump, floor,  warpA, warpB, lightMasu";//壁とゴールを除いたマスたち。
        if (str.Contains(obj.gameObject.tag))//壁かゴールか、マス以外のエフェクトなどでなければ、
        {
            WhichAreaInfo MasuInfo = obj.gameObject.GetComponent<WhichAreaInfo>();
            if (MasuInfo.whichArea != areaNow)
            {
                gameObject.transform.position = oldPos;
                jumped = false;
                mEscript.SetMoveInfo(direction, "wall");
                playedStopAnim = true;
                return;
            }
        }

        onMasu = true;
        if (obj.gameObject.tag == "areaGoal")
        {
            if (obj.GetComponent<AreaMasuInfo>().Used == false)
            {
                playedStopAnim = false;
                obj.GetComponent<AreaMasuInfo>().Used = true;
                cm.progress++;
                cm.CameraMove();
                fitPictogramRot("goal");
                areaNow++;

                Instantiate(clearWall, new Vector3(gameObject.transform.position.y -1,
                    0.5f, obj.transform.position.z), Quaternion.identity);
            }
        }

        if (obj.gameObject.tag == "wall")
        {
            if (jumped)
            {
                jumped = false;
                gameObject.transform.position = (objPos + oldPos) / 2;//進行方向に対して半分もどる
                mEscript.SetMoveInfo(direction, "wall");
            }
            else
            {
                gameObject.transform.position = oldPos;
                mEscript.SetMoveInfo(direction, "wall");
                playedStopAnim = true;
            }
            if(direction == "up")
            {
                movedTime++;
            }
            else if (direction == "down")
            {
                movedTime--;
            }
        }
        if (obj.gameObject.tag == "lightMasu")
        {
            lightMasuInfo lMInfo = obj.GetComponent<lightMasuInfo>();
            if (!lMInfo.used)
            {
                lMInfo.used = true;
                fitPictogramRot("light");
                onLight = 2;
            }
        }
        if (obj.gameObject.tag == "none")
        {
            cm.LightON();
            Failed_script Fs = GameObject.Find("FailedPost-process").GetComponent<Failed_script>();
            Fs.startFailedAnim = true;
        }
        if (obj.gameObject.tag == "floor")
        {
            if (!playedStopAnim)
            {
                mEscript.SetMoveInfo(direction, "floor");
            }
            else
            {
                playedStopAnim = false;
            }
        }
        if (obj.gameObject.tag == "Jump")
        {
            onJumpMasu = true;
            if (!playedStopAnim)
            {
                mEscript.SetMoveInfo(direction, "floor");
            }
            else
            {
                playedStopAnim = false;
            }
        }

        if(obj.gameObject.tag != "Jump")
        {
            jumped = false;
        }

    }
    private void OnTriggerExit(Collider obj)
    {
        onMasu = false;
    }

    private void fitPictogramRot(string WhichMasu)
    {
        GameObject pictogram = gameObject.transform.GetChild(0).gameObject;
        if (WhichMasu == "goal")
        {
            if(cm.progress >= 3)//一番下のゴールマスについた時
            {
                pictogram.transform.rotation = Quaternion.Euler(new Vector3(63 - movedTime * 10, 90, 0));
            }
            else
            {
                pictogram.transform.rotation = Quaternion.Euler(new Vector3(63, 90, 0));
            }
            movedTime = 0;
        }
        if (WhichMasu == "light")
        {
            pictogram.transform.rotation = Quaternion.Euler(new Vector3(63 - movedTime * 10, 90, 0));
            cm.LightControl("on");
        }
    }
}
