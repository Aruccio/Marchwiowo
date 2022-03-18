using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();

        else if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return))
        {
            StartCoroutine(Waity(1));
            SceneManager.LoadScene("exactlyGame");
        }
    }

    IEnumerator Waity(int s)
    {
        yield return new WaitForSeconds(s);
    }
}
