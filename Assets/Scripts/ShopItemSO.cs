using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    [CreateAssetMenu(fileName = "ShopMenu", menuName = "Scriptable Objects/New Shop Item", order = 1)]
public class ShopItemSO : ScriptableObject
{
   public string title;
   public string description;
   public Sprite newSprite;
   public int baseCost;
   public int baseUpgrade;
   public int baseAutoUpgrade;
   public int baseMultiplierUpgrade;
}
