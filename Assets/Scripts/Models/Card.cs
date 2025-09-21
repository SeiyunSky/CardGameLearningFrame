using System.Collections.Generic;
using UnityEngine;

public class Card 
{
    public string Description => cardData.Description;
    public string Title => cardData.name;
    public Sprite Image => cardData.Image;
    public List<Effect> Effects => cardData.Effects;
    public int Mana{ get; private set; }
    private readonly CardData cardData;

    public Card(CardData cardData)
    {
        this.cardData = cardData;
        Mana = cardData.Mana;
    }
}
