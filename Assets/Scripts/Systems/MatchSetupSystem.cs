using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : MonoBehaviour
{
    [SerializeField] private List<CardData> playerDeckData;

    private void Start()
    {
        CardSystem.Instance.Setup(playerDeckData);
        DrawCardGA drawCardGA = new(5);
        ActionSystem.Instance.Perform(drawCardGA);
    }
}
