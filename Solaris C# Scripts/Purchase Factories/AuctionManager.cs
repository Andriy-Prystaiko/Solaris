using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AuctionManager : MonoBehaviour
{
    public CardSlot AuctionedFactory;
    public GameObject AuctionWindow;
    public TextMeshProUGUI CurrentBid;
    public List<ResourceManager> PlayerResources = new List<ResourceManager>();

    // Initialize the price of the auction based on starting price of Factory
    public void AuctionStart()
    {
        CurrentBid.SetText("Current Bid: "+AuctionedFactory.SolariCost);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
