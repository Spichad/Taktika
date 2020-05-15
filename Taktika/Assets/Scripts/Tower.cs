using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //Переменные требующие задания в редакторе 
    #region REQUIRED!
    public GameObject updatemenu = null;
    public UpdateManager updman = null;
    public GameObject shotmodel;
    public Transform[] spherepos;
    #endregion
    //Переменные для работы башни
    #region Variables    
    public GameObject Target=null; //сохранение текущей цели
    public FollowPath TargetScr = null; //скрипт текущей цели   
    private float elapsed = 0; //таймер для атаки
    #endregion


    //Параметры Башни
    #region TowerVariables
    public float[] attSpeed; //промежуток между атаками
    public int[] dmg; //Урон
    public float[] hitDist; //Дистанция удара
    public int[] Price; // цена улучшения
    public int Lvl = 0; // уровень улучшения
    public GameObject[] models;
    #endregion

    #if UNITY_EDITOR
    [SerializeField]
    private bool ShowAttackDistance = false;
    #endif

    #if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (ShowAttackDistance) {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, hitDist[Lvl]);
        }
    }
    #endif

    private GameObject FindTarget(string Tag, GameObject sender)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(Tag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = sender.transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    void OnMouseDown()
    {
        if (Lvl + 1 < Mathf.Min(attSpeed.Length, dmg.Length, hitDist.Length))
        {
            Debug.Log("Update");
            updatemenu.SetActive(true);
            updman.sender = this;
            updman.price.text =Price[Lvl].ToString();
        }
        else
        {
            Debug.Log("Max updated");
        }
    }

    void SpawnShot(Vector3 startPos, Vector3 endPos)
    {
        GameObject shot = Instantiate(shotmodel,new Vector3(0,0,0),Quaternion.identity);
        LineRenderer line = shot.GetComponent<LineRenderer>();
        Vector3[] posit = new Vector3[2];
        posit[0] = startPos;
        posit[1] = endPos;
        line.SetPositions(posit);
    }


    void Update()
    {
        elapsed += Time.deltaTime;
        
        if (Target != null)
         {
            // отошел от башни
        if ((Vector3.Distance(Target.transform.position,transform.position) > hitDist[Lvl]))
            {
                Target = FindTarget("Enemy", gameObject);
                if (Target != null) TargetScr = Target.GetComponent<FollowPath>();
                return;
            }

                if (elapsed>= attSpeed[Lvl])
            {

                //выстрел      
                SpawnShot(Target.transform.position,spherepos[Lvl].position);         
                TargetScr.life -=dmg[Lvl];
                if (TargetScr.life <= 0)
                {
                    TargetScr.PlayDead(false);
                }
                //конец выстрел

                elapsed = 0;
            }
            
        }
        else
        {
            Target = FindTarget("Enemy",gameObject);
            if (Target!= null) TargetScr = Target.GetComponent<FollowPath>();
        }
        if (elapsed >= attSpeed[Lvl])
        {
            elapsed = attSpeed[Lvl];
        }
    }
}
