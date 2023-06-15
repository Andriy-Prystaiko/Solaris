using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonVanisher : MonoBehaviour
{
    // Variable storing main Camera to check if it is active or not
    public GameObject Main_Cam;

    // Variable storing the button we would like to disable if main cam is active
    public Button newButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // statement checking if the main cam is active
        if (Main_Cam.activeInHierarchy == true)
        {
            // disables button
            newButton.interactable = false;
        }
        else
        {
            // if main cam is not active enable button
            newButton.interactable = true;
        }
    }
}
