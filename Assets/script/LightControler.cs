using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControler : MonoBehaviour
{
    public Light light;
    int count = 0;
    float time = 0;
    float ONOFF = 0;

    int random = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        light = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        //点滅を続ける時間
        if (time <= 100)
        {
            random = Random.Range(1, 100);

            //ONとOFFの継続時間
            if(count <= random  && 0 <= count)
            {
                light.enabled = true;
            }
            else if(random < count && count < 100)
            {
                light.enabled = false;
            }
            count++;

            ONOFF = time;

        }
        time += Time.deltaTime;
    }
}
