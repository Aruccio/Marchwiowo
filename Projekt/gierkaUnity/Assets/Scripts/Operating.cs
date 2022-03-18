using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Operating : MonoBehaviour
{
    static GameObject player;
    public GameObject ai;
    static GameObject farmer;
    GameObject[] carrots;
    GameObject tekst;
    public GameObject startPointFarmer;
    public GameObject startPoint;
    public int playerPoints;
    public int aiPoints;

    public Text time;
    public Vector3 startVec;

    public Vector3 farmerStart;
    float timer;

    Operating opera;
    public bool endGame;

    public static Canvas gameCanvas;
    public static Canvas winCanvas;
    public static Canvas loseCanvas;


    void Start()
    {
        gameCanvas = GameObject.Find("gameCanvas").GetComponent<Canvas>();
        winCanvas = GameObject.Find("winCanv").GetComponent<Canvas>();
        loseCanvas = GameObject.Find("loseCanv").GetComponent<Canvas>();
        player = GameObject.Find("Player");
        farmer = GameObject.Find("Farmer");

        carrots = GameObject.FindGameObjectsWithTag("Carrot");
        playerPoints = 0;
        aiPoints = 0;
        SetCarrots();
        time.text = System.Convert.ToString(0);
        startVec = startPoint.transform.position;

        farmerStart = startPointFarmer.transform.position;

        gameCanvas.enabled = true;
         loseCanvas.enabled = false;
         winCanvas.enabled = false;
        endGame = false;



    }

    void Update()
    {
        timer += Time.deltaTime;
        time.text = System.Convert.ToString(System.Math.Ceiling(timer));

        if (endGame == true)
        {
            timer = 0;
            time.text = System.Convert.ToString("0");
        }
    }


   public void AddPointPlayer()
    {
        playerPoints++;
        tekst = GameObject.Find("carrsText");
        tekst.GetComponent<Text>().text = System.Convert.ToString(playerPoints);

        if(playerPoints>=300)
        {
            gameCanvas.enabled = false;
            loseCanvas.enabled = false;
            winCanvas.enabled = true;
            endG();

        }

    }

    public void AddPointAI()
    {
        aiPoints++;
    }


    void SetCarrots()
    {
        System.Random rnd = new System.Random();
      int carLen =  carrots.Length;

        for (int i = 0; i < 740; i++)
        {
            int r = rnd.Next(carrots.Length);
            Destroy(carrots[r]);
            carrots[r].GetComponent<Renderer>().enabled = false;
            carrots[r].GetComponentInChildren<Renderer>().enabled = false;
            Destroy(carrots[r]);
        }
    }




    public void ResetEverything()
    {

        SceneManager.LoadScene("Menu");
        playerPoints = 0;

        timer = 0;

        GameObject tet = GameObject.Find("carrsTextWin");
        GameObject pts = GameObject.Find("carrsText");
        tet.GetComponent<Text>().text = "";

        tet = GameObject.Find("marchwiowo");
        tet.GetComponent<Text>().text = System.Convert.ToString("");

        tet = GameObject.Find("marchewkiWin");
        tet.GetComponent<Text>().text = System.Convert.ToString("");

        tet = GameObject.Find("dopisekWin");
        tet.GetComponent<Text>().text = System.Convert.ToString("");

        tet = GameObject.Find("pressWin");
        tet.GetComponent<Text>().text = System.Convert.ToString("");

        gameCanvas.enabled = true;
        loseCanvas.enabled = false;
        winCanvas.enabled = false;
        endGame = false;

    }

    public void endG()
    {
        GameObject tet = GameObject.Find("carrsTextWin");
        tet.GetComponent<Text>().text = System.Convert.ToString(playerPoints);

        tet = GameObject.Find("marchwiowo");
        tet.GetComponent<Text>().text = System.Convert.ToString("Marchwiowo!!!!");

       tet = GameObject.Find("marchewkiWin");
        tet.GetComponent<Text>().text = System.Convert.ToString("Twoje marchewki:");

        tet = GameObject.Find("dopisekWin");
        tet.GetComponent<Text>().text = System.Convert.ToString("Gratulacje, zyjesz!");

        tet = GameObject.Find("pressWin");
        tet.GetComponent<Text>().text = System.Convert.ToString("(wcisnij enter,\nby zagrac jeszcze raz)");

        endGame = true;

    }


}


