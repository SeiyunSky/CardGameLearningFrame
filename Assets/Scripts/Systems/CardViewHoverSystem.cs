using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardViewHoverSystem : Singleton<CardViewHoverSystem>
{
    [SerializeField] private CardView cardViewHover;

    public void Show(Card card,Vector3 position)
    {
        cardViewHover.gameObject.SetActive(true);
        cardViewHover.Setup(card);
        cardViewHover.transform.position = position;

        DOTween.Kill(cardViewHover.transform);

        cardViewHover.transform.DOMoveY(position.y + 0.3f, 0.5f)
        .SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.InOutSine);
    }

    public void Hide()
    {
        cardViewHover.gameObject.SetActive(false);
    }

}
