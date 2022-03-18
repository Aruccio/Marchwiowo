using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Control Script/Mouse Look")]
public class PlayerNav : MonoBehaviour
{
    public float speed;
    public Camera cam;
    public Vector3 startVec;
    public Vector3 farmerStart;
    public GameObject startPoint;
    public bool cursorLocked;
    FarmerAI farmer;
    GameObject farmerGO;
    Operating opera;

    private void Start()
    {

            opera = new Operating();
            farmer = new FarmerAI();
            startVec = startPoint.transform.position;
            farmerGO = GameObject.Find("Farmer");
            farmerStart = farmerGO.transform.position;

    }

    void Update()
    {
        UpdateCursorLock();

        if (opera.endGame==false && farmer.endGame==false)
        {
            float xAxisValue = Input.GetAxis("Horizontal");
            float zAxisValue = Input.GetAxis("Vertical");
            float yRot = Input.GetAxisRaw("Mouse X");
            float xRot = Input.GetAxisRaw("Mouse Y");
            Vector3 lookY = new Vector3(0, yRot*(float)1.1, 0);
            Vector3 lookX = new Vector3(-xRot*(float)1.1, 0, 0);
            Vector3 rot = new Vector3(transform.rotation.x,
                transform.rotation.y, transform.rotation.z);
            Vector3 pos = transform.position;

            Vector3 movHorizontal = transform.right * xAxisValue;
            Vector3 movVertical = transform.forward * zAxisValue;
            Vector3 velocity = (movHorizontal + movVertical).normalized * speed;

            cam.transform.Rotate(lookX);
            transform.Rotate(lookY);
            transform.position += velocity;

            if (Input.GetKey("escape"))
                Application.Quit();
            //trochę oszukiwania w ramach testów.
            if (Input.GetKey(KeyCode.Y)) speed += (float)0.01;
            if (Input.GetKey(KeyCode.H)) speed -= (float)0.01;
        }
        else if(Input.GetKey(KeyCode.Space))
         {

            opera.ResetEverything();
            ResetEverything();
        }
    }

    public void ResetEverything()
    {

        transform.position = startVec;
        farmerGO.transform.position = farmerStart;

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Carrot")
        {
            Destroy(collision.gameObject);
            opera.AddPointPlayer();
        }
    }

    void UpdateCursorLock()
    {
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (Input.GetKeyDown(KeyCode.F))
            {
                cursorLocked = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (Input.GetKeyDown(KeyCode.F))
            {
                cursorLocked = true;
            }
        }
    }
}
