using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<CardSlot> cardSlots = new List<CardSlot>();
    public bool[] availableCardSlots;

    private List<Card> cardInSlot = new List<Card>();

    public TMP_Text handCountText;
    public TMP_Text discardCountText;

    void Start()
    {
        handCountText.text = "3";
        discardCountText.text = "3";
        StartCoroutine(autoDrawCard(5));
    }

    public void DrawCard()
    {
        if (deck.Count >= 1)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];

            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i] == true)
                {
                    Debug.Log($"{i + 1} 번째 카드 드로우!");
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;

                    randCard.transform.position = cardSlots[i].transform.position;
                    availableCardSlots[i] = false;

                    randCard.hasbeenPlayed = false; // 아직 안씀

                    cardInSlot.Insert(i, randCard);
                    deck.Remove(randCard);
                    return;
                }
            }
        }

        Debug.Log("남은 덱:" + deck.Count);
    }

    public void handPlay()
    {
        int count = int.Parse(handCountText.text);
        if (count > 0)
        {
            int handPlayCount = 0;
            for (int i = 0; i < cardInSlot.Count; i++)
            {
                if (cardInSlot[i].hasSelected)
                {
                    Debug.Log($"{i + 1} 번째 카드 선택");
                    cardInSlot[i].selectCard();
                    handPlayCount++;
                }
            }

            StartCoroutine(handPlayProcess(handPlayCount));

            handCountText.text = (count - 1).ToString();
        }
    }

    public void discard()
    {
        int count = int.Parse(discardCountText.text);
        if (count > 0)
        {
            int discardCount = 0;
            for (int i = cardInSlot.Count - 1; i >= 0; i--)
            {
                if (cardInSlot[i].hasSelected)
                {
                    Debug.Log($"{i + 1} 번째 카드 선택");
                    cardInSlot[i].discard();
                    cardInSlot.RemoveAt(i);
                    discardCount++;
                }
            }

            discardCountText.text = (count - 1).ToString();

            StartCoroutine(autoDrawCard(discardCount));
        }
    }

    IEnumerator handPlayProcess(int count)
    {
        yield return new WaitForSecondsRealtime(0.7f);
        for (int i = 0; i < cardInSlot.Count; i++)
        {
            if (cardInSlot[i].hasSelected)
            {
                cardInSlot[i].gameObject.SetActive(false);

                Debug.Log($"{i + 1} 번째 카드 처리중");
                yield return new WaitForSecondsRealtime(0.6f);
            }
        }

        for (int i = cardInSlot.Count - 1; i >= 0; i--)
        {
            if (cardInSlot[i].hasSelected)
            {
                cardInSlot.RemoveAt(i);
            }
        }

        StartCoroutine(autoDrawCard(count));
    }

    IEnumerator autoDrawCard(int count)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        for (int i = 0; i < count; i++)
        {
            DrawCard();
            yield return new WaitForSecondsRealtime(0.3f);
        }
    }
}
