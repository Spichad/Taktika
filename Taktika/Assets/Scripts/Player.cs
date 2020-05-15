using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int Health = 100;
    public int Gold = 0;
    public int kill = 0;
    [SerializeField]
    public GameObject loseScreen;
    [SerializeField]
    private Text HealthTxt;
    [SerializeField]
    private Text GoldTxt;
    [SerializeField]
    private Text killTxt;
    [SerializeField]
    private Text lkillTxt;

    void Update()
    {
        HealthTxt.text = Health.ToString();
        GoldTxt.text = Gold.ToString();
        killTxt.text = kill.ToString();
    }

    public void Lose()
    {
        loseScreen.SetActive(true);
        lkillTxt.text = killTxt.text;
    }
}
