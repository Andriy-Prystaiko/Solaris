using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    // Find all the text  objects and import them into this script
    // Create a new private variable for each resource in this script
    public TextMeshProUGUI solariText;
    private int solari;
    public TextMeshProUGUI marketsText;
    private int markets;
    public TextMeshProUGUI mineralsText;
    private int minerals;
    public TextMeshProUGUI rareMineralsText;
    private int rareMinerals;
    public TextMeshProUGUI recyclablesText;
    private int recyclables;
    public TextMeshProUGUI fissilesText;
    private int fissiles;
    
    // Start is called before the first frame update
    void Start()
    {
        // Declare all starting resource values and set the resources appropriate text
       solari = 50; 
       solariText.SetText("Solari: " + solari);
       markets = 0; 
       marketsText.SetText("Markets: " + markets);
       minerals = 0; 
       mineralsText.SetText("Minerals: " + minerals);
       rareMinerals = 0; 
       rareMineralsText.SetText("Rare Minerals: " + rareMinerals);
       recyclables = 0; 
       recyclablesText.SetText("Recyclables: " + recyclables);
       fissiles = 0; 
       fissilesText.SetText("Fissiles: " + fissiles);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
