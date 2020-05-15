using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateManager : MonoBehaviour
{
    public Player player;
    public Tower sender;
    public Text price;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BTNOk()
    {

        if (player.Gold >= sender.Price[sender.Lvl])
        {
            player.Gold -= sender.Price[sender.Lvl]; 
            if (sender.models.Length-1> sender.Lvl)
            {
                sender.models[sender.Lvl].SetActive(false);
                sender.models[sender.Lvl+1].SetActive(true);
            }          
            sender.Lvl++;
            

            gameObject.SetActive(false);
        }
        
    }

   public void BTNClose()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
