using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_events : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        fade script = gameObject.GetComponent<fade>();
        script.FadeOut(4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
