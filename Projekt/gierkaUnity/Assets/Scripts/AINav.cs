using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINav : MonoBehaviour
{

    bool found = false;
    bool reached = false;
    public GameObject tObject;
    float step;
    float time;
    List<Vector3> carrsVec = new List<Vector3>();
    List<GameObject> carrs = new List<GameObject>();
    List<Vector3> popul = new List<Vector3>();
    public float speed;
    Vector3 target;

    int szerStart;
    int szerEnd;
    int wysStart;
    int wysEnd;
    int popSize;



    void Start()
    {
        szerStart = 7;
        szerEnd = 17;
        wysStart = 4;
        wysEnd = 12;
        popSize = 20;
        found = false;
        reached = false;




    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        target = transform.position;
        if (!found)
        {
            Run(20, popul, carrsVec);
            found = true;
        }
        if (found)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }




    }
    public void Generate(int sizePop, int szerStart, int wysStart, int szerEnd, int wysEnd, List<Vector3> popul)
    {
        for (int i = 0; i < sizePop; i++)
        {
            int tmp1 = Random.Range(szerStart, szerEnd);
            int tmp2 = Random.Range(wysEnd, wysEnd);
            popul.Add(new Vector3(tmp1, 1/10, tmp2 ));
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
            if (tmp >= szerStart && tmp < szerEnd) b.x = tmp;
        }
        else
        {
            int tmp = bxInt - System.Convert.ToInt16(System.Math.Ceiling(Levy(bxInt)));
            if (tmp >= szerStart && tmp < szerEnd) b.x = tmp;
        }
        if (Random.Range(0, 100) < 50)
        {
            int tmp = bzInt + System.Convert.ToInt16(System.Math.Ceiling(Levy(bzInt)));
            if (tmp >= wysStart && tmp < wysEnd) b.z = tmp;
        }
        else
        {
            int tmp = bzInt - System.Convert.ToInt16(System.Math.Ceiling(Levy(bzInt)));
            if (tmp > wysStart && tmp < wysEnd) b.z = tmp;
        }
        return b;
    }

    public Vector3 HostDecision(Vector3 a)
    {
        if (Random.Range(0, 100) < 50) return new Vector3(Random.Range(szerStart, szerEnd), (float)0.1, Random.Range(wysStart, wysEnd));
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
                    if(p.x == carrsVec[k].x && p.z == carrsVec[k].z)
                        march = true;
                    if (p.x + i >= szerStart && p.x + i < szerEnd && p.z + j >= wysStart && p.z + j < wysEnd && march != false)
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
        double val = System.Math.Sqrt(c / (2 * System.Math.PI)) * System.Math.Exp((-c * (2 * (x - d))) / System.Math.Pow(x - d, 3 / 2)) * (System.Math.Sqrt((wysEnd-wysStart)+(szerEnd-szerStart))); //poprawne: -c / (2 * (x - d))
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

    public void Run(int it, List<Vector3> popul, List<Vector3> carrotts)
    {
        Generate(popSize, szerStart, wysStart, szerEnd, wysEnd, popul);


        for (int i = 0; i < it; i++)
        {
            for (int j = 0; j < popul.Count; j++)
            {
                popul[j] = Move(popul[j]);
            }

        }
      target = Best(popul, carrotts);
    }


    private Vector3 Best(List<Vector3> popul, List<Vector3> carrotts)
    {
        return FindTheBest(popul, carrotts);
    }

}

