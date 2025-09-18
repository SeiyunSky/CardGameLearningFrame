using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSystems : MonoBehaviour
{
    [SerializeField] private List<CardData> deckData;

    private void Start()
    {
        CardSystem.Instance.Setup(deckData);
    }


}
