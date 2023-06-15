using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelector : MonoBehaviour
{
    //declare all cameras to cycle through
    public GameObject Main_Cam;
    public GameObject Earth_Cam;
    public GameObject Moon_Cam;
    public GameObject Mars_Cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // When left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Create a line impact variable
            RaycastHit hit;
            // Creates a ray shoot out from the player view
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Detects if the ray was cast out to 1000 units
            if (Physics.Raycast(ray, out hit, 10000.0f))
            {
                // if the ray hit an object
                if (hit.transform != null)
                {
                    // Print the name of the object hit
                    PrintName(hit.transform.gameObject);

                    // 4 if statements seeing if an object of intrest(Sun, Earth, Moon, and Mars) were hit and if so switches to the camera responsible for each
                    if (hit.transform.gameObject.name == "Earth")
                    {
                        Main_Cam.SetActive(false);
                        Earth_Cam.SetActive(true);
                        Moon_Cam.SetActive(false);
                        Mars_Cam.SetActive(false);
                    }
                    if (hit.transform.gameObject.name == "Moon")
                    {
                        Main_Cam.SetActive(false);
                        Earth_Cam.SetActive(false);
                        Moon_Cam.SetActive(true);
                        Mars_Cam.SetActive(false);
                    }
                    if (hit.transform.gameObject.name == "Mars")
                    {
                        Main_Cam.SetActive(false);
                        Earth_Cam.SetActive(false);
                        Moon_Cam.SetActive(false);
                        Mars_Cam.SetActive(true);
                    }
                    if (hit.transform.gameObject.name == "Sun")
                    {
                        Main_Cam.SetActive(true);
                        Earth_Cam.SetActive(false);
                        Moon_Cam.SetActive(false);
                        Mars_Cam.SetActive(false);
                    }
                    
                }
            }
        }
        // When escape is pressed switches view back to the solar camera
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Main_Cam.SetActive(true);
            Earth_Cam.SetActive(false);
            Moon_Cam.SetActive(false);
            Mars_Cam.SetActive(false);
        }
    }

    // print function which takes a game object and prints its name
    private void PrintName(GameObject go)
    {
        print(go.name);
    }
}
