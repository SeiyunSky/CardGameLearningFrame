using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : Singleton<CardSystem>
{
    [SerializeField] private HandView handView;

    [SerializeField] private Transform drawPileRoot;

    [SerializeField] private Transform disCardPileRoot;

    private readonly List<Card> drawPile = new ();
    private readonly List<Card> discardPile = new ();
    private readonly List<Card> hand = new ();

    void OnEnable()
    {
        ActionSystem.AttachPerformer<DrawCardGA>(DrawCardsPerformer);
        ActionSystem.AttachPerformer<DisCardAllCardsGA>(DisCardsAllCardsPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemeyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemeyTurnPostReaction, ReactionTiming.POST);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardGA>();
        ActionSystem.DetachPerformer<DisCardAllCardsGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemeyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemeyTurnPostReaction, ReactionTiming.POST);


    }

    #region Publics
    public void Setup(List<CardData> deckData)
    {
        foreach(var cardData in deckData)
        {
            Card card = new(cardData);
            drawPile.Add(card);
        }
    }
    #endregion

    private IEnumerator DrawCardsPerformer(DrawCardGA drawCardGA)
    {
        int actualAmount = Mathf.Min(drawCardGA.Amount, drawPile.Count);
        int notDrawAmount = drawCardGA.Amount - actualAmount;
        for (int i = 0; i < actualAmount; i++)
        {
            yield return DrawCard(); 
        }
        if(notDrawAmount > 0 && discardPile.Count > 0)
        {
            RefillDeck();
            for (int i = 0; i < notDrawAmount; i++)
            {
                yield return DrawCard();
            }
        }
    }

    private IEnumerator DisCardsAllCardsPerformer(DisCardAllCardsGA disCardAllCardsGA)
    {
        foreach(var card in hand)
        {
            discardPile.Add(card);
            CardView cardView = handView.RemoveCard(card);
            yield return DiscardCard(cardView);
        }
        hand.Clear();
    }

    #region Reactions
    private void EnemeyTurnPreReaction(EnemyTurnGA enemyTurnGA)
    {
        ActionSystem.Instance.AddReaction(new DisCardAllCardsGA());
    }
    private void EnemeyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        ActionSystem.Instance.AddReaction(new DrawCardGA(5));
    }
    #endregion

    #region Helper
    private IEnumerator DrawCard()
    {
        Card card = drawPile.Draw();
        hand.Add(card);
        CardView cardView = CardViewCreator.Instance.CreateCardView(card,drawPileRoot.position,drawPileRoot.rotation);
        yield return handView.AddCard(cardView);
    }

    private void RefillDeck()
    {
        drawPile.AddRange(discardPile);
        discardPile.Clear();
    }

    private IEnumerator DiscardCard(CardView cardView)
    {
        cardView.transform.DOScale(Vector3.zero, 0.3f);
        Tween tween = cardView.transform.DOMove(disCardPileRoot.position, 0.15f);
        yield return tween.WaitForCompletion();
        Destroy(cardView.gameObject);
    }
    #endregion
}
