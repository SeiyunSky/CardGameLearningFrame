using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSystems : MonoBehaviour
{
    [SerializeField] private HandView handView;

    [SerializeField] private CardData cardData;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Card card = new Card(cardData);
            CardView cardView = CardViewCreator.Instance.CreateCardView(card,Vector3.zero, Quaternion.identity);
            StartCoroutine(handView.AddCard(cardView));
        }
    }

}
