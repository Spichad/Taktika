using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Wave : MonoBehaviour
{
    public float TimeBetweenWaves;
    public int X=5;
    public int waveN = 1;
    public int count=0;
    public int left = 0;
    public Transform spawn;
    public GameObject Enemy;
    public GameObject path;
    public Player Player;
    public Transform castle;
    public GameObject splash;
    public Text BuffTXT;
    private int[] buffs= new int[3];
    private int[] buffsTotal = new int[3];
    [SerializeField]
    private float timeBetweenSpawn = 0.5f;
    private System.Random rnd;
    private float elapsed=0;
    private bool readytospawn = false;
    private Transform[] pathseq;
    public GameObject pathmodel;
    // Start is called before the first frame update
    void Start()
    {
        rnd = new System.Random();
        //Debug.Log(PlayerPrefs.GetFloat("WaveTime"));
        TimeBetweenWaves=PlayerPrefs.GetFloat("WaveTime");
        pathseq = path.GetComponentsInChildren<Transform>();
        if (!(pathseq == null || pathseq.Length < 3))
        {

            for (int i = 2; i < pathseq.Length; i++)
            {
                SpawnPathLine(pathseq[i].position, pathseq[i - 1].position);
            }
        }
    }

    void SpawnPathLine(Vector3 startPos, Vector3 endPos)
    {
        GameObject shot = Instantiate(pathmodel, new Vector3(0, 0, 0), Quaternion.identity);
        LineRenderer line = shot.GetComponent<LineRenderer>();
        Vector3[] posit = new Vector3[2];
        posit[0] = startPos;
        posit[1] = endPos;
        line.SetPositions(posit);
    }

    void wavestart()
    {
        count =rnd.Next(waveN, waveN+X);
        left = count;
        splash.SetActive(true);
        int getBuff =rnd.Next(1,2);
        BuffTXT.text =count+" enemy with: Health +" + getBuff;
        buffs[0] +=getBuff;
        getBuff = rnd.Next(0, 2);
        BuffTXT.text = BuffTXT.text+" Damage +" + getBuff;
        buffs[1] +=getBuff;
        getBuff = rnd.Next(0, 2);
        BuffTXT.text = BuffTXT.text+" Bounty +" + getBuff;
        buffs[2] +=getBuff;
    }

    void spawnE(Transform where, GameObject what)
    {
        count--; 
        GameObject newE= Instantiate(what, where.position, Quaternion.identity);
        FollowPath pathOfE = newE.GetComponent<FollowPath>();
        pathOfE.path = path;
        pathOfE.castle = castle;
        pathOfE.wave = this;
        pathOfE.Player = Player;
        pathOfE.life += buffs[0];
        pathOfE.dmg += buffs[1];
        pathOfE.bounty += buffs[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (count > 0)
        {
            elapsed += Time.deltaTime;
            if (readytospawn)
            {
                if (elapsed > timeBetweenSpawn)
                {
                    spawnE(spawn, Enemy);
                    elapsed = 0;
                }
            }
            else
            {
                if (elapsed > TimeBetweenWaves)
                {
                    readytospawn = true;
                    elapsed = 0;
                }
            }
        }
        else
        {
            
            if (left <= 0)
            {
                readytospawn = false;
                wavestart();
              
                
            }
        }
    }
}
