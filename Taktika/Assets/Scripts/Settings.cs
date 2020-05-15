using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public InputField input;
    void Start()
    {
        input.text = PlayerPrefs.GetFloat("WaveTime").ToString();
    }
    public void BTNSaveAndReset()
    {
        float set=0.0f;
        if (float.TryParse(input.text,out set))
        {
         //   Debug.Log(set);
            PlayerPrefs.SetFloat("WaveTime", set);
            SceneManager.LoadScene("Scene");
        }
    }

    public void BTNClose()
    {
        gameObject.SetActive(false);
    }
}
