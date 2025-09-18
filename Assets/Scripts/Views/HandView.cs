using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class HandView : MonoBehaviour
{
    [SerializeField] private SplineContainer spineContainer;

    private readonly List<CardView> cards = new();

    public IEnumerator AddCard(CardView card)
    {
        cards.Add(card);
        yield return UpdateCardPositions(.15f);
    }

    private IEnumerator UpdateCardPositions(float duration)
    {
        if(cards.Count == 0) yield break;
        float cardSpacing = 1f / 10f;  //卡牌间距
        float firstCardPosition = 0.5f - (cards.Count - 1) * cardSpacing / 2f;  //第一张卡的位置：中心点减去卡牌数量*空当的一半
        Spline spline = spineContainer.Spline;

        for (int i = 0; i < cards.Count; i++)
        {
            float p = firstCardPosition + i * cardSpacing;  //参数位置
            Vector3 splinePosition = spline.EvaluatePosition(p);  //卡片位置
            Vector3 forward = spline.EvaluateTangent(p);  //切线方向
            Vector3 up = spline.EvaluateUpVector(p);  //曲线法线方向
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            Debug.Log($"Card {i}: Forward={forward}, Up={up}, Cross={Vector3.Cross(up, forward)}");
            //沿路径平滑移动
            cards[i].transform.DOMove(splinePosition + transform.position + 0.01f * i * Vector3.back, duration);
            cards[i].transform.DORotate(rotation.eulerAngles, duration);
        }

        //控制事件时间
        yield return new WaitForSeconds(duration);
    }

    public CardView RemoveCard(Card card)
    {
        CardView cardView = GetCardView(card);
        if (cardView == null) 
            return null;
        cards.Remove(cardView);
        StartCoroutine(UpdateCardPositions(.15f));
        return cardView;
    }

    private CardView GetCardView(Card card)
    {
        return cards.Where(CardView => CardView.Card == card).FirstOrDefault();
    }
}
