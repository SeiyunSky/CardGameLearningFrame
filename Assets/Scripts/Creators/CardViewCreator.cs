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
        //��ʼ����ӦԤ����
        CardView cardView = Instantiate(cardViewPrefab, position, rotation);
        //�������Ŷ��� �������Dotween��ʵ�����Ŷ���
        cardView.transform.localScale = Vector3.zero;
        cardView.transform.DOScale(Vector3.one, 0.15f);
        cardView.Setup(card);
        return cardView;
    }
}
