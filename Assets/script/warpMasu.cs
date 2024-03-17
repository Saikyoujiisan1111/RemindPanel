using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warpMasu : MonoBehaviour
{
    public bool used = false;
    public int whichGroup;

    private string dest;
    private GameObject[] objs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.name != "character")
        {
            return;
        }
        WhichAreaInfo MasuInfo = gameObject.GetComponent<WhichAreaInfo>();
        if (MasuInfo.whichArea != obj.GetComponent<Character>().areaNow)//キャラのいるエリアとこのワープマスがあるエリアが違うなら、
        {
            return;
        }

        if (gameObject.tag == "warpA")
        {
            objs = GameObject.FindGameObjectsWithTag("warpB");
            WarpTo("B");
        }
        else if (gameObject.tag == "warpB")
        {
            objs = GameObject.FindGameObjectsWithTag("warpA");
            WarpTo("A");
        }
    }
    private void WarpTo(string dest)
    {
        foreach(GameObject obj in objs)
        {
            warpMasu objScript = obj.GetComponent<warpMasu>();
            warpMasu thisScript = gameObject.GetComponent<warpMasu>();
            if (!objScript.used && !thisScript.used)
            {
                if (objScript.whichGroup == thisScript.whichGroup)
                {
                    Vector3 objPos = obj.transform.position;
                    Vector3 destination = new Vector3(objPos.x, objPos.y + 1, objPos.z);// 1はcharacterとブロックの高さの差分
                    GameObject.Find("character").transform.position = destination;

                    used = true;
                }
            }
        }
    }
}
