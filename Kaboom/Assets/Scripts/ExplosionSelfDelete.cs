using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSelfDelete : MonoBehaviour
{
    private float startTime;
    private float selfDeleteTime = 4.0f;

    // Use this for initialization
    void Start()
    {
        this.startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= this.startTime + this.selfDeleteTime)
            Object.Destroy(this.gameObject);
    }
}
