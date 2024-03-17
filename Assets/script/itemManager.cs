using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemManager : MonoBehaviour
{
    public GameObject content;
    public GameObject newItemImg;
    public int items_playerHas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemManagement()
    {
        //アイテムを順番に並べる
        GameObject[] items = new GameObject[content.transform.childCount];
        for (var i = 0; i < items.Length; i++)
        {
            GameObject item = content.transform.GetChild(i).gameObject;
            itemInfo itemInfo = item.GetComponent<itemInfo>();

            items[itemInfo.order - 1] = item; //orderを0からじゃなく1から10で決めてたので-1するねん。
        }

        //クリア回数から解放するアイテムの個数を計算
        int clearCount = 0;
        foreach (bool stageclear in crossSceneInfoManage.crossSceneInfo.clear)
        {
            if (stageclear)
            {
                clearCount++;
            }
        }
        items_playerHas = clearCount / 2;
        for (int i = 1; i <= items_playerHas; i++)
        {
            itemInfo iInfo = items[i - 1].GetComponent<itemInfo>();
            iInfo.Info[2] = "true";//取得済みかの情報の書き換え
        }

        //ーーーー
        if (crossSceneInfoManage.crossSceneInfo.getNewItem)
        {
            crossSceneInfoManage.crossSceneInfo.getNewItem = false;

            Sprite Img = items[items_playerHas - 1].GetComponent<Image>().sprite;
            newItemImg.GetComponent<Image>().sprite = Img;
            newItemImg.SetActive(true);
        }
    }
}

