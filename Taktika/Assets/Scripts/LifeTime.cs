using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    private float elapsed = 0;
    public float livetime;
    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed> livetime) { Destroy(gameObject); }
    }
}
