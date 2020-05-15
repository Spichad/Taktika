using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    private float elapsed = 0;
    public float livetime;
    void Update()
    {
        if (gameObject.active)
        {
            elapsed += Time.deltaTime;
            if (elapsed > livetime) {
                gameObject.SetActive(false);
                elapsed = 0;
            }
        }
    }
}
