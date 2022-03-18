using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FarmerAI : MonoBehaviour
{
    private LayerMask mask;
    Rigidbody rb;
    Operating opera;
    public GameObject player;
    public Vector3 startVec;
    public Vector3 farmerStart;
    public GameObject farmer;

    public Text time;
    public GameObject startPoint;
    public GameObject startPointFarmer;
    public bool endGame;
    public static Canvas gameCanvas;
    public static Canvas winCanvas;
    public static Canvas loseCanvas;
    public float speed;
    bool found;
    public GameObject tObject;

    int widthStart;
    int widthEnd;
    int heightStart;
    int heightEnd;
    int popSize;
    bool reached;

    public GameObject target;



    void Start()
    {
        opera = new Operating();
        rb = transform.GetComponent<Rigidbody>();
        gameCanvas = GameObject.Find("gameCanvas").GetComponent<Canvas>();
        winCanvas = GameObject.Find("winCanv").GetComponent<Canvas>();
        loseCanvas = GameObject.Find("loseCanv").GetComponent<Canvas>();
        startVec = startPoint.transform.position;
        farmerStart = startPointFarmer.transform.position;
        endGame = false;
        found = true;


        widthStart = 4;
        widthEnd = 40;
        heightStart = 4;
        heightEnd = 49;
        popSize = 10;
        found = false;
        reached = false;
        

    }

    void Update()
    {
        if (endGame == true)
        {
            if (Input.GetKey("escape"))
                Application.Quit();

            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene("Menu");
                ResetEverything();
            }
        }
        else
        {

            if (!found)
            {
                Run(10, popul, carrsVec);
                found = true;
            }
            if (found)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
                if (transform.position == target.transform.position)
                {
                    reached = true;
                }
                if (reached)
                {
                    found = false;
                    reached = false;
                }
            }

            

        }
    }


    public Vector3 MoveInDirection(Vector3 wek)
    {
        return wek * (Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameCanvas.enabled = false;
            loseCanvas.enabled = true;
            winCanvas.enabled = false;


            endG();
        }
    }

    public void endG()
    {
        GameObject tet = GameObject.Find("carrsTextLose");
        Text ts = GameObject.Find("carrsText").GetComponent<Text>();
        string tss = ts.text;
        tet.GetComponent<Text>().text = tss;



        tet = GameObject.Find("zlapano");
        tet.GetComponent<Text>().text = System.Convert.ToString("Zlapano Cie!!");

        tet = GameObject.Find("marchewkiLose");
        tet.GetComponent<Text>().text = System.Convert.ToString("Twoje marchewki:");

        tet = GameObject.Find("dopisekLose");
        tet.GetComponent<Text>().text = System.Convert.ToString("(tylko co z tego,\nskoro nie zyjesz ?)");

        tet = GameObject.Find("pressLose");
        tet.GetComponent<Text>().text = System.Convert.ToString("(wcisnij enter,\nby zagrac jeszcze raz)");



        opera.endGame = true;
        endGame = true;

    }


    public void ResetEverything()
    {
        opera.playerPoints = 0;
        player.transform.position = startVec;
        farmer.transform.position = farmerStart;

        GameObject tet = GameObject.Find("carrsTextWin");
        GameObject pts = GameObject.Find("carrsText");
        tet.GetComponent<Text>().text = "";

        tet = GameObject.Find("zlapano");
        tet.GetComponent<Text>().text = System.Convert.ToString("");

        tet = GameObject.Find("marchewkiLose");
        tet.GetComponent<Text>().text = System.Convert.ToString("");

        tet = GameObject.Find("dopisekLose");
        tet.GetComponent<Text>().text = System.Convert.ToString("");

        tet = GameObject.Find("pressLose");
        tet.GetComponent<Text>().text = System.Convert.ToString("");

        gameCanvas.enabled = true;
        loseCanvas.enabled = false;
        winCanvas.enabled = false;
        endGame = false;
    }

    List<Vector3> carrsVec = new List<Vector3>();
    List<GameObject> carrs = new List<GameObject>();
    List<Vector3> popul = new List<Vector3>();






    public void Run(int it, List<Vector3> popul, List<Vector3> carrotts)
    {
        Generate(popSize, widthStart, heightStart, widthEnd, heightEnd, popul);


        for (int i = 0; i < it; i++)
        {
            for (int j = 0; j < popul.Count; j++)
            {
                popul[j] = Move(popul[j]);
            }

        }

        target.transform.position = Best(popul, carrotts);
    }
    private Vector3 Best(List<Vector3> popul, List<Vector3> carrotts)
    {
        return FindTheBest(popul, carrotts);
    }


    public void Generate(int sizePop, int szerStart, int wysStart, int szerEnd, int wysEnd, List<Vector3> popul)
    {
        for (int i = 0; i < sizePop; i++)
        {
            int tmp1 = Random.Range(szerStart, szerEnd);
            int tmp2 = Random.Range(wysEnd, wysEnd);
            popul.Add(new Vector3(tmp1, 1 / 10, tmp2));
        }

        GameObject[] carrotsAr = GameObject.FindGameObjectsWithTag("Carrot");
        foreach (GameObject ca in carrotsAr)
        {
            carrs.Add(ca);
            carrsVec.Add(ca.transform.position);
        }


    }


    public Vector3 Move(Vector3 a)
    {
        Vector3 b = a;
        int bxInt = System.Convert.ToInt16(b.x);
        int bzInt = System.Convert.ToInt16(b.z);
        if (Random.Range(0, 100) < 50)
        {
            int tmp = bxInt + System.Convert.ToInt16(System.Math.Ceiling(Levy(bxInt)));
            if (tmp >= widthStart && tmp < widthEnd) b.x = tmp;
        }
        else
        {
            int tmp = bxInt - System.Convert.ToInt16(System.Math.Ceiling(Levy(bxInt)));
            if (tmp >= widthStart && tmp < widthEnd) b.x = tmp;
        }
        if (Random.Range(0, 100) < 50)
        {
            int tmp = bzInt + System.Convert.ToInt16(System.Math.Ceiling(Levy(bzInt)));
            if (tmp >= heightStart && tmp < heightEnd) b.z = tmp;
        }
        else
        {
            int tmp = bzInt - System.Convert.ToInt16(System.Math.Ceiling(Levy(bzInt)));
            if (tmp > heightStart && tmp < heightEnd) b.z = tmp;
        }
        return b;
    }

    public Vector3 HostDecision(Vector3 a)
    {
        if (Random.Range(0, 100) < 50) return new Vector3(Random.Range(widthStart, widthEnd), (float)0.1, Random.Range(heightStart, heightEnd));
        else return a;

    }

    public int Host(int neighneighborhoodSize, Vector3 p)
    {
        int counter = 0;
        for (int i = -System.Convert.ToInt32(neighneighborhoodSize / 2); i < System.Convert.ToInt32(neighneighborhoodSize / 2) + 1; i++)
        {
            for (int j = -System.Convert.ToInt32(neighneighborhoodSize / 2); j < System.Convert.ToInt32(neighneighborhoodSize / 2) + 1; j++)
            {
                bool march = false;
                for (int k = 1; k < carrsVec.Count; k++)
                {
                    if (p.x == carrsVec[k].x && p.z == carrsVec[k].z)
                        march = true;
                    if (p.x + i >= widthStart && p.x + i < widthEnd && p.z + j >= heightStart && p.z + j < heightEnd && march != false)
                        counter++;
                }


            }
        }
        return counter;
    }

    public double FitnessFunction(Vector3 p1, Vector3 p2)
    {
        return System.Math.Sqrt(System.Math.Pow(p1.x - p2.x, 2) + System.Math.Pow(p1.z - p2.z, 2));
    }

    public double Euclidean(Vector3 p1, Vector3 p2)
    {
        return (p1.x - p2.x) + (p1.z - p2.z);
    }
    public double Manhattan(Vector3 p1, Vector3 p2)
    {
        return System.Math.Abs(p1.x - p2.z) + System.Math.Abs(p1.z - p2.x);
    }
    public double Maximum(Vector3 p1, Vector3 p2)
    {
        return System.Math.Abs(p1.x - p2.z < System.Math.Abs(p1.z - p2.x) ? System.Math.Abs(p1.x - p2.z) : System.Math.Abs(p1.z - p2.x));
    }

    public bool CheckPosition(Vector3 p)
    {
        bool pos = false;
        for (int k = 1; k < carrsVec.Count; k++)
        {
            if (p.x == carrsVec[k].x && p.z == carrsVec[k].z)
                pos = true;
        }
        return pos;
    }

    public double Levy(int x)
    {
        double c = Random.value, d = Random.value;
        double val = System.Math.Sqrt(c / (2 * System.Math.PI)) * System.Math.Exp((-c * (2 * (x - d))) / System.Math.Pow(x - d, 3 / 2)) * (System.Math.Sqrt((heightEnd - heightStart) + (widthEnd - widthStart))); //poprawne: -c / (2 * (x - d))
        return System.Math.Ceiling(val) - val > 0.5 ? System.Math.Ceiling(val) : System.Math.Floor(val);
    }

    public Vector3 FindTheBest(List<Vector3> popul, List<Vector3> carrotts)
    {
        Vector3 a = popul[0];

        for (int i = 0; i < popul.Count; i++)
        {
            if (CheckPosition(popul[i]) != false)
                if (FitnessFunction(popul[i], tObject.transform.position) < FitnessFunction(a, tObject.transform.position))
                    a = popul[i];
        }
        return a;
    }

    public void Destroy(List<Vector3> popul, List<Vector3> carrotts)
    {
        popul.Clear();
        carrotts.Clear();
    }
}


