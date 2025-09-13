using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class CardViewCreator : Singleton<CardViewCreator>
{
    [SerializeField] private CardView cardViewPrefab;

    public CardView CreateCardView(Card card, Vector3 position,Quaternion rotation)
    {
        //初始化对应预制体
        CardView cardView = Instantiate(cardViewPrefab, position, rotation);
        //卡牌缩放动画 归零后用Dotween来实现缩放动画
        cardView.transform.localScale = Vector3.zero;
        cardView.transform.DOScale(Vector3.one, 0.15f);
        cardView.Setup(card);
        return cardView;
    }
}
