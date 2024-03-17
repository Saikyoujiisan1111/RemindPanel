using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemInfo : MonoBehaviour
{
    public string[] Info = new string[5];
    const int ITEM_Name = 0;
    const int ITEM_disk = 1;
    const int ITEM_get = 2;
    const int ITEM_fontSize = 3;
    //const int ITEM_order = 4;

    public int order;
    //エディターから数字記入

    public GameObject nameText;
    public GameObject diskText;
    public GameObject largeImage;

    public GameObject firstItem;


    // Start is called before the first frame update
    void Start()
    {
        //order = int.Parse(Info[ITEM_order]);
        //itemManagerで使用
    }

    private void OnEnable()
    {
        if(Info[ITEM_get] != "true")
        {
            gameObject.GetComponent<Image>().color = Color.black;
        }
        else if(order == 1)
        {
            nameText.GetComponent<Text>().text = Info[ITEM_Name];
            diskText.GetComponent<Text>().text = Info[ITEM_disk];
            diskText.GetComponent<Text>().fontSize = int.Parse(Info[ITEM_fontSize]);
            largeImage.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;

            nameText.SetActive(true);
            diskText.SetActive(true);
            largeImage.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        if(Info[ITEM_get] == "true")
        {
            nameText.GetComponent<Text>().text = Info[ITEM_Name];
            diskText.GetComponent<Text>().text = Info[ITEM_disk];
            diskText.GetComponent<Text>().fontSize = int.Parse(Info[ITEM_fontSize]);
            largeImage.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;

            nameText.SetActive(true);
            diskText.SetActive(true);
            largeImage.SetActive(true);
        }
        else
        {
            nameText.GetComponent<Text>().text = "???";
            diskText.GetComponent<Text>().text = "それが何なのかがわからない";
            diskText.GetComponent<Text>().fontSize = 55;


            nameText.SetActive(true);
            diskText.SetActive(true);
            largeImage.SetActive(false);

        }
    }
}
