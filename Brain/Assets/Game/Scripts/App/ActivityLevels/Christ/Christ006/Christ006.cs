using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using BaseFramework;

public class Christ006 : ChristBaseLevel
{

    public Sprite closeSprite;
    public Sprite[] openSprites;
    public Text stepNumText;

    private Christ006Card[] _cards;

    private readonly int[] _cardIndexs = new int[] { 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4 };

    private Christ006Card _showedCard;

    private int _step = 30;
    private int _collectCardCount = 0;

    protected override void Start()
    {
        base.Start();
        _cards = this.GetComponentsInChildren<Christ006Card>();

        RefreshCard();

        foreach (Christ006Card card in _cards)
        {
            card.onClick = OnCardClick;
        }

        stepNumText.text = _step.ToString();
    }

    private void RefreshCard()
    {
        _cardIndexs.Shuffle();
        for (int i = 0; i < _cards.Length; ++i)
        {
            int index = _cardIndexs[i];
            _cards[i].Init(index, closeSprite, openSprites[index]);
        }
        _collectCardCount = 0;
    }

    public override void Refresh()
    {
        base.Refresh();

        RefreshCard();
        _showedCard = null;

        _step = 30;
        stepNumText.text = _step.ToString();
    }

    protected override void OnTipsDialogCloseWithTipsUsed()
    {
        base.OnTipsDialogCloseWithTipsUsed();
        Add5Step();
    }

    private void Add5Step()
    {
        _step += 5;
        stepNumText.text = _step.ToString();
    }

    private void RemoveCards()
    {
        int removeCount = _cards.Length - _collectCardCount;
        removeCount = removeCount >= 4 ? 4 : removeCount;
        _collectCardCount += removeCount;
        List<Christ006Card> removeCards = GetRemoveCards(removeCount);
        //Log.E(this, $"{removeCount}, {removeCards.Count}");
        for (int i = 0; i < removeCards.Count; ++i)
        {
            int j = i;
            removeCards[j].Show(() => {
                removeCards[j].Collect(() => {
                    CheckFinish();
                });
            });
        }
    }

    private List<Christ006Card> GetRemoveCards(int count)
    {
        HashSet<Christ006Card> set = new HashSet<Christ006Card>();
        while (count >= 2)
        {
            Christ006Card card1 = _cards.GetRandomItem(it => it.state != Christ006Card.State.Collect && !set.Contains(it));
            if (card1 != null)
            {
                set.Add(card1);
            }
            Christ006Card card2 = _cards.GetRandomItem(it => it.state != Christ006Card.State.Collect && !set.Contains(it) && it.id == card1.id);
            if (card2 != null)
            {
                set.Add(card2);
            }
            //Log.E(this, $"GetRandom card {card1?.name}, {card2?.name}");
            count -= 2;
        }
        return new List<Christ006Card>(set);
    }

    private void OnCardClick(Christ006Card card)
    {
        if (card.state == Christ006Card.State.Hide && _step > 0)
        {
            --_step;
            stepNumText.text = _step.ToString();
            card.Show(() => {
                OnCardShowed(card);
            });
        }
    }

    private void OnCardShowed(Christ006Card card)
    {
        if (_showedCard == null)
        {
            _showedCard = card;
            CheckFinish();
        }
        else if (_showedCard.id == card.id)
        {
            _collectCardCount += 2;
            _showedCard.Collect();
            card.Collect(CheckFinish);
            _showedCard = null;
        }
        else
        {
            _showedCard.Hide();
            card.Hide(CheckFinish);
            _showedCard = null;
        }
    }

    private void CheckFinish()
    {
        if (_collectCardCount >= _cards.Length)
        {
            Completion();
        }
        else if (_step <= 0)
        {
            ShowError();
            After(Refresh, 0.4f);
        }
    }
}
