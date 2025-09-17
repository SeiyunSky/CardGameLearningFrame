using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text title;

    [SerializeField] private TMP_Text description;

    [SerializeField] private TMP_Text mana;

    [SerializeField] private SpriteRenderer imageSR;

    [SerializeField] private GameObject wrapper;

    public Card Card { get; private set; }

    public void Setup(Card card)
    {
        Card = card;
        this.title.text = card.Title;
        this.description.text = card.Description;
        this.mana.text = card.Mana.ToString();
        this.imageSR.sprite = card.Image;
    }

    private void OnMouseEnter()
    {
        wrapper.SetActive(false);
        Vector3 position = new(transform.position.x , -2 , 0);
        CardViewHoverSystem.Instance.Show(Card, position);

    }

    private void OnMouseExit()
    {
        CardViewHoverSystem.Instance.Hide();
        wrapper.SetActive(true);
    }

}
