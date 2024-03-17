using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectContent : MonoBehaviour
{
    const float duration = 0.4f;

    private float time = 0;

    private Vector3 v;
    private Vector3 PosBase;

    private bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            time += Time.deltaTime;
            gameObject.transform.position = v * time + PosBase;
            if(time >= duration)
            {
                Destroy(gameObject);
            }
        }

    }

    public void moveEffect( Vector3 StartPos, Vector3 targetPos, string whichEffect)
    {
        PosBase = StartPos;
        if(whichEffect == "floor" || whichEffect == "wall")
        {
            v = (targetPos - StartPos) / duration;
            start = true;
        }
    }
}
