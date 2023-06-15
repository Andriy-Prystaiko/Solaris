using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    // Resource Values
    public int SolariCost;
    public int MineralCost;
    public int RareMineralCost;
    public int RecyclablesCost;
    public int FissilesCost;
    public int ConsumerGoodsProduced;

    // Attached Sprite
    public Sprite CardImage;

    // Indicator for wheather the card is an advanced card
    public bool AdvancedFactory;
    //Indicator of weather factory can use multiple inputs
    public bool MixedFactory;
}
