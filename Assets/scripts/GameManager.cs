using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null; // 아직은 사용 안함.
    TextManager textManager;

    public List<Card> deck = new List<Card>();
    public List<CardSlot> cardSlots = new List<CardSlot>(); // 카드가 놓일 슬롯
    public bool[] availableCardSlots;

    private List<Card> cardInSlot = new List<Card>(); // 슬롯에 카드가 존재하는지 확인용 리스트
    private List<string> hand = new List<string>(); // 족보 확인용

    public List<Card> useCard = new List<Card>(); // 사용한 카드(패 포함)

    public Transform deckPostion; // 덱 위치


    GameObject parent;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }

    void Start()
    {
        textManager = TextManager.instance;
        StartCoroutine(autoDrawCard(3));
        parent = GameObject.Find("numberSprite");
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
                    //Debug.Log($"{i + 1} 번째 카드 드로우!");
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;

                    StartCoroutine(randCard.drawAnim(randCard.transform.position,
                        cardSlots[i].transform.position));
                    availableCardSlots[i] = false;

                    randCard.hasbeenPlayed = false; // 아직 안씀

                    cardInSlot.Insert(i, randCard);
                    deck.Remove(randCard);

                    useCard.Add(randCard);
                    //Debug.Log(randCard.gameObject.name);
                    return;
                }
            }
        }

        //Debug.Log("남은 덱:" + deck.Count);
    }


    public void addHand(string name)
    {
        hand.Add(name);
    }

    public void cancelHand(string name)
    {
        hand.Remove(name);
    }

    public int handCount()
    {
        return hand.Count;
    }

    public List<string> getHand()
    {
        return hand;
    }

    public void updateHand(Hand hand)
    {
        textManager.handName.text = hand.HandName;
        textManager.chip.text = (hand.Chip).ToString();
        textManager.multiple.text = (hand.Multiple).ToString();
    }

    public void addChip(int c)
    {
        textManager.chip.text = (float.Parse(textManager.chip.text) + c).ToString();
    }

    public void addMultiple(int m)
    {
        textManager.multiple.text = (float.Parse(textManager.multiple.text) + m).ToString();
    }

    public void init()
    {
        textManager.handName.text = "";
        textManager.chip.text = "0";
        textManager.multiple.text = "0";
    }

    public void handPlay()
    {
        int count = int.Parse(textManager.handCountText.text);
        if (count > 0)
        {
            int handPlayCount = 0;
            for (int i = 0; i < cardInSlot.Count; i++)
            {
                if (cardInSlot[i].hasSelected)
                {
                    //Debug.Log($"{i + 1} 번째 카드 선택");
                    cardInSlot[i].selectCard();
                    handPlayCount++;
                }
            }

            StartCoroutine(handPlayProcess(handPlayCount));
            textManager.handCountText.text = (count - 1).ToString();
        }
        hand.Clear();
    }

    public void discard()
    {
        int count = int.Parse(textManager.discardCountText.text);
        if (count > 0)
        {
            int discardCount = 0;
            for (int i = cardInSlot.Count - 1; i >= 0; i--)
            {
                if (cardInSlot[i].hasSelected)
                {
                    //Debug.Log($"{i + 1} 번째 카드 선택");
                    cardInSlot[i].end();
                    cardInSlot.RemoveAt(i);
                    discardCount++;
                }
            }
            textManager.discardCountText.text = (count - 1).ToString();
            StartCoroutine(autoDrawCard(discardCount));
        }
        hand.Clear();
    }

    IEnumerator handPlayProcess(int count)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        for (int i = 0; i < cardInSlot.Count; i++)
        {
            if (cardInSlot[i].hasSelected)
            {
                for (int k = 1; k < 3; k++)
                {
                    cardInSlot[i].scaleUp(0.01f);
                    yield return new WaitForSecondsRealtime(0.02f);
                }

                GameObject sprite = cardInSlot[i].getSpriteObject(parent);
                sprite.SetActive(true);
                sprite.transform.position = cardInSlot[i].transform.position;
                addChip(int.Parse(cardInSlot[i].name.Split("-")[0]));

                //Debug.Log($"{i + 1} 번째 카드 처리중");
                yield return new WaitForSecondsRealtime(0.6f);
                sprite.SetActive(false);
                yield return new WaitForSecondsRealtime(0.35f);
            }
        }
        yield return StartCoroutine(addScore());
        yield return new WaitForSecondsRealtime(0.5f);
        for (int i = cardInSlot.Count - 1; i >= 0; i--)
        {
            if (cardInSlot[i].hasSelected)
            {
                cardInSlot[i].end();
                cardInSlot.RemoveAt(i);
            }
        }
        StartCoroutine(autoDrawCard(count));
    }

    IEnumerator addScore()
    {
        float current = float.Parse(textManager.score.text);
        float target = current + float.Parse(textManager.chip.text) * float.Parse(textManager.multiple.text);

        float duration = 0.5f;
        float offset = (target - current) / duration;

        while (current < target)
        {
            current += (offset * Time.deltaTime);
            textManager.score.text = ((int)current).ToString();
            yield return null;
        }
        textManager.score.text = target.ToString();
    }

    IEnumerator autoDrawCard(int count)
    {
        init();
        yield return new WaitForSecondsRealtime(0.5f);
        for (int i = 0; i < count; i++)
        {
            DrawCard();
            yield return new WaitForSecondsRealtime(0.3f);
        }
    }
}
