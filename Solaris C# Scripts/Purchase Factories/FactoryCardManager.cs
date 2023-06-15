using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FactoryCardManager : MonoBehaviour
{
    // All available cards in the deck
    public List<Card> deck = new List<Card>();

    // The Slots for cards on the board
    public List<CardSlot> board = new List<CardSlot>();

    // The cardslot used on the auction side of the ui page
    public CardSlot AuctionCardSlot;

    // A sprite that could be used to show empty tiles
    public Sprite EmptyFactoryTileSprite;

    //Card that is on the top of the deck, a weaker card to get the passing of the power plant scaling right
    private Card SetupTopCard;

    // The random card pulled out of the deck to simulate random draws
    private Card randCard;

    // Cardslot to temporarily hold Cardslots for switching in sort function
    public CardSlot BoardSlotHolder;

    //public Dictionary<int, int> PlayerCardSlots;

    public void DrawCard(bool FirstSetup)
    {
        // If statement to check if the deck has any cards left
        if(deck.Count >= 1)
        {
            // For loop to go through all cards slots and see if any are free
            for(int i = 0; i < 8; i++)
            {
                // Draws a radomly choosen Factory card
                randCard = deck[Random.Range(0, deck.Count)];

                // If the game is in first setup the first factories have to be from the cheaper range for balance
                if(FirstSetup == true)
                {
                    //While loop to reroll cards untill we have a starter card selected
                    while(randCard.SolariCost < 3 || randCard.SolariCost > 15)
                    {
                        // reroll random card
                        randCard = deck[Random.Range(0, deck.Count)];
                    }
                }

                // Check to see if a board slot is empty then place the card into it
                if(board[i].SlotAvailable == true)
                {
                    // Checks if the first setup card has been used
                    if(SetupTopCard != null)
                    {
                        randCard = SetupTopCard;
                        SetupTopCard = null;
                    }

                    // Initialize the tile to all the values of the card
                    board[i].SlotImage = randCard.CardImage;
                    board[i].SlotAvailable = false;
                    board[i].AdvancedFactory = randCard.AdvancedFactory;
                    board[i].MixedFactory = randCard.MixedFactory;
                    board[i].ConsumerGoodsProduced = randCard.ConsumerGoodsProduced;
                    board[i].FissilesCost = randCard.FissilesCost;
                    board[i].MineralCost = randCard.MineralCost;
                    board[i].RareMineralCost = randCard.RareMineralCost;
                    board[i].RecyclablesCost = randCard.RecyclablesCost;
                    board[i].SolariCost = randCard.SolariCost;
                    deck.Remove(randCard);
                    board[i].GetComponent<Image>().overrideSprite = board[i].SlotImage;
                }
            }
            // Finalize first time setup and change first time setup to false
            if(FirstSetup == true)
            {
                // Generates a top deck card for setup
                SetupTopCard = deck[Random.Range(0, deck.Count)];

                // Makes sure the card generated fits the starting card parameters
                while(SetupTopCard.SolariCost < 3 || SetupTopCard.SolariCost > 15)
                {
                        // reroll random card
                        SetupTopCard = deck[Random.Range(0, deck.Count)];
                }

                // Remove the card from the deck
                deck.Remove(SetupTopCard);

                // Ends the first time setup
                FirstSetup = false;
            }
        }
        else
        {
            Debug.Log("Deck is Empty");
        }
        // Sorts the board after the draws
        BoardSlotSorter(); 
    }

    // Sends the choosen tile to the auction side
    public void SendToAuction(int boardIndex)
    {
        // Initialize the Auction tile with all the Board tiles data
        AuctionCardSlot.SlotImage = board[boardIndex].SlotImage;
        AuctionCardSlot.SlotAvailable = false;
        AuctionCardSlot.AdvancedFactory = board[boardIndex].AdvancedFactory;
        AuctionCardSlot.MixedFactory = board[boardIndex].MixedFactory;
        AuctionCardSlot.ConsumerGoodsProduced = board[boardIndex].ConsumerGoodsProduced;
        AuctionCardSlot.FissilesCost = board[boardIndex].FissilesCost;
        AuctionCardSlot.MineralCost = board[boardIndex].MineralCost;
        AuctionCardSlot.RareMineralCost = board[boardIndex].RareMineralCost;
        AuctionCardSlot.RecyclablesCost = board[boardIndex].RecyclablesCost;
        AuctionCardSlot.SolariCost = board[boardIndex].SolariCost;

        // Reset Image of Empty Board tile back to translucent gray and reset its data
        board[boardIndex].SlotImage = null;
        board[boardIndex].SlotAvailable = true;
        board[boardIndex].AdvancedFactory = false;
        board[boardIndex].MixedFactory = false;
        board[boardIndex].ConsumerGoodsProduced = 0;
        board[boardIndex].FissilesCost = 0;
        board[boardIndex].MineralCost = 0;
        board[boardIndex].RareMineralCost = 0;
        board[boardIndex].RecyclablesCost = 0;
        board[boardIndex].SolariCost = 0;
        board[boardIndex].GetComponent<Image>().overrideSprite = EmptyFactoryTileSprite;

        // Set Auction tile to have the right factory image and become Opaque
        AuctionCardSlot.GetComponent<Image>().overrideSprite = AuctionCardSlot.SlotImage;
    }

    // Sorts the board by solari cost ascending
    public void BoardSlotSorter()
    {
        Debug.Log("Got to sorting");
        // Variable to show the buble sort if the board is sorted
        bool SwapOccured = true;

        // While loop to keep sorting untill no switch happens on a sorting pass
        while(SwapOccured == true)
        {
            Debug.Log("Got to sorting While");
            //Resets the swap happened variable
            SwapOccured = false;

            // Goes through all 8 tiles one at a time
            for(int i = 0; i < 7; i++)
            {
                // Compares the tiles if the previous one is smaller then the next they are switched
                if(board[i].SolariCost > board[i+1].SolariCost)
                {
                    // Switches boards places
                    BoardSlotHolder = BoardTileSwapper(BoardSlotHolder, board[i+1]);
                    board[i+1] = BoardTileSwapper(board[i+1], board[i]);
                    board[i] = BoardTileSwapper(board[i], BoardSlotHolder);

                    // Indicates a swap was made and the function needs to keep sorting
                    SwapOccured = true;
                }
            }
        }
        
    }

    // Swaps 2 CardSlot types data places
    public CardSlot BoardTileSwapper(CardSlot Tile1, CardSlot Tile2)
    {
        Tile1.SlotImage = Tile2.SlotImage;
        Tile1.SlotAvailable = false;
        Tile1.AdvancedFactory = Tile2.AdvancedFactory;
        Tile1.MixedFactory = Tile2.MixedFactory;
        Tile1.ConsumerGoodsProduced = Tile2.ConsumerGoodsProduced;
        Tile1.FissilesCost = Tile2.FissilesCost;
        Tile1.MineralCost = Tile2.MineralCost;
        Tile1.RareMineralCost = Tile2.RareMineralCost;
        Tile1.RecyclablesCost = Tile2.RecyclablesCost;
        Tile1.SolariCost = Tile2.SolariCost;
        Tile1.GetComponent<Image>().overrideSprite = Tile2.SlotImage;
        return Tile1;
    }

    // Start is called before the first frame update
    void Start()
    {
        DrawCard(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
