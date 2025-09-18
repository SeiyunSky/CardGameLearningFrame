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
        float cardSpacing = 1f / 10f;  //���Ƽ��
        float firstCardPosition = 0.5f - (cards.Count - 1) * cardSpacing / 2f;  //��һ�ſ���λ�ã����ĵ��ȥ��������*�յ���һ��
        Spline spline = spineContainer.Spline;

        for (int i = 0; i < cards.Count; i++)
        {
            float p = firstCardPosition + i * cardSpacing;  //����λ��
            Vector3 splinePosition = spline.EvaluatePosition(p);  //��Ƭλ��
            Vector3 forward = spline.EvaluateTangent(p);  //���߷���
            Vector3 up = spline.EvaluateUpVector(p);  //���߷��߷���
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            Debug.Log($"Card {i}: Forward={forward}, Up={up}, Cross={Vector3.Cross(up, forward)}");
            //��·��ƽ���ƶ�
            cards[i].transform.DOMove(splinePosition + transform.position + 0.01f * i * Vector3.back, duration);
            cards[i].transform.DORotate(rotation.eulerAngles, duration);
        }

        //�����¼�ʱ��
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
