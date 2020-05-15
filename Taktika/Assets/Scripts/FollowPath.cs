using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public Player Player;
    public Wave wave;
    //Переменные для пути
    #region VariablesForPath     
    public GameObject path; //Оъект пути
    public float reachDist = 0.1f; //дистанция на которой точка пути считается достигнутой
    [SerializeField]
    private int nextpoint = 1; //Следующая точка пути
    private Transform[] pathseq; //Массив точек пути
    public Transform castle;
    #endregion

    //Параметры юнита
    #region UnitVariables
    public float speed=1.0f; //скорость
    public int life=5; //Жизни
    public int dmg=1; //Урон игроку
    public float hitDist = 1.0f; //Дистанция удара крепости
    public int bounty = 1; //награда
    #endregion

    void Start()
    {
        pathseq = path.GetComponentsInChildren<Transform>();
     //   foreach (Transform child in pathseq)
     //   {
     ////       Debug.Log(child);
     //   }
    }

    //Отрисовка пути. Для редактора
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (pathseq == null || pathseq.Length < 3)
        {
            return;
        }  
            for (int i = 2; i < pathseq.Length; i++)
            {
                Gizmos.DrawLine(pathseq[i].position, pathseq[i - 1].position);
            }
    }

    // "Смерть" юнита - true если дотиг цели и false если убит башней
    public void PlayDead(bool reach)
    {
        if (reach)
        {
            Player.Health -= dmg;
            if (Player.Health <= 0)
            {
                Player.Lose();
            }
        }
        else
        {
            Player.Gold += bounty;
            Player.kill++;
        }
        wave.left--;
        Destroy(gameObject);
    }

    void Update()
    {
        float dist = Vector3.Distance(pathseq[nextpoint].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, pathseq[nextpoint].position, Time.deltaTime * speed);
        if (dist <= reachDist && nextpoint< pathseq.Length-1)
        {
            nextpoint++;
            transform.LookAt(pathseq[nextpoint].position);
        }
        float cdist = Vector3.Distance(transform.position, castle.position);
        if (cdist <= hitDist)
        {            
            PlayDead(true);            
        }
    }


}
