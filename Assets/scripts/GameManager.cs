using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null; // ������ ��� ����.
    TextManager textManager;
    AudioManager audioManager;
    UiManager uiManager;

    public List<Card> deck = new List<Card>();
    public List<CardSlot> cardSlots = new List<CardSlot>(); // ī�尡 ���� ����
    public bool[] availableCardSlots;

    private List<Card> cardInSlot = new List<Card>(); // ���Կ� ī�尡 �����ϴ��� Ȯ�ο� ����Ʈ
    private List<string> hand = new List<string>(); // ���� Ȯ�ο�

    public List<Card> useCard = new List<Card>(); // ����� ī��(�� ����)

    public BoxCollider2D deckPostion; // �� ��ġ
    public deck currentDeck; // ���� ���� ��

    IEnumerator method = null;


    GameObject parent; // ���� ��������Ʈ �θ� ������Ʈ

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
        parent = GameObject.Find("numberSprite");
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        uiManager = GameObject.Find("UiManager").GetComponent<UiManager>();
        textManager = TextManager.instance;
        //StartCoroutine(startGame());
    }

    public IEnumerator startGame()
    {
        deckPostion.enabled = true;
        init();
        yield return new WaitForSecondsRealtime(0.3f);
        StartCoroutine(autoDrawCard(3));
    }

    public IEnumerator organize()
    {
        foreach (Card card in useCard)
        {
            audioManager.restart();
            yield return StartCoroutine(card.drawAnim(card.transform.position, deck[0].transform.position, 0.15f));
            card.init();
            deck.Add(card);
            card.gameObject.SetActive(false);
        }
        useCard.Clear();
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
                    //Debug.Log($"{i + 1} ��° ī�� ��ο�!");
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;

                    StartCoroutine(randCard.drawAnim(randCard.transform.position,
                        cardSlots[i].transform.position));
                    availableCardSlots[i] = false;

                    cardInSlot.Insert(i, randCard);
                    deck.Remove(randCard);

                    useCard.Add(randCard);
                    //Debug.Log(randCard.gameObject.name);
                    return;
                }
            }
        }

        //Debug.Log("���� ��:" + deck.Count);
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

    public void addChip(string name)
    {
        int c = int.Parse(name.Split("-")[0]);
        textManager.chip.text = (float.Parse(textManager.chip.text) + c).ToString();
    }

    public void addMultiple(int m)
    {
        textManager.multiple.text = (float.Parse(textManager.multiple.text) + m).ToString();
    }

    public void init()
    {
        textManager.score.text = "0";
        textManager.handName.text = "";
        textManager.chip.text = "0";
        textManager.multiple.text = "0";
        textManager.handCount.text = "3";
        textManager.discardCount.text = "4";
        currentDeck.all();
    }

    public void nextTurn()
    {
        textManager.handName.text = "";
        textManager.chip.text = "0";
        textManager.multiple.text = "0";
    }

    public void handPlay() // �ڵ��÷��� ��ư
    {
        StartCoroutine(handPlayCoroutine());
    }

    public IEnumerator handPlayCoroutine()
    {
        int count = int.Parse(textManager.handCount.text);
        if (count > 0)
        {
            int handPlayCount = 0;
            for (int i = 0; i < cardInSlot.Count; i++)
            {
                if (cardInSlot[i].hasSelected)
                {
                    //Debug.Log($"{i + 1} ��° ī�� ����");
                    audioManager.handPlay();
                    yield return StartCoroutine(cardInSlot[i].selectCard());
                    handPlayCount++;
                }
            }
            method = handPlayProcess(handPlayCount);
            StartCoroutine(method);
            textManager.handCount.text = (count - 1).ToString();
        }
        hand.Clear();
    }

    public void discard() // ������ ��ư
    {
        int count = int.Parse(textManager.discardCount.text);
        if (count > 0)
        {
            int discardCount = 0;
            for (int i = cardInSlot.Count - 1; i >= 0; i--)
            {
                if (cardInSlot[i].hasSelected)
                {
                    //Debug.Log($"{i + 1} ��° ī�� ����");
                    cardInSlot[i].end();
                    cardInSlot.RemoveAt(i);
                    discardCount++;
                }
            }
            audioManager.discard();
            textManager.discardCount.text = (count - 1).ToString();
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
                audioManager.chip();
                sprite.transform.position = cardInSlot[i].transform.position;
                addChip(cardInSlot[i].getSpriteName());

                //Debug.Log($"{i + 1} ��° ī�� ó����");
                yield return new WaitForSecondsRealtime(0.5f);
                sprite.SetActive(false);
                yield return new WaitForSecondsRealtime(0.15f);
            }
        }
        yield return new WaitForSecondsRealtime(0.3f);
        yield return StartCoroutine(addScore());
        for (int i = cardInSlot.Count - 1; i >= 0; i--)
        {
            if (cardInSlot[i].hasSelected)
            {
                cardInSlot[i].end();
                cardInSlot.RemoveAt(i);
            }
        }
        audioManager.discard();
        StartCoroutine(autoDrawCard(count));
    }

    IEnumerator addScore()
    {
        audioManager.score();

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
        yield return StartCoroutine(check((int)target));
    }

    IEnumerator check(float score)
    {
        if (score >= int.Parse(textManager.goal.text))
        {
            yield return new WaitForSeconds(0.8f);
            nextTurn();
            StopCoroutine(method);
            deckPostion.enabled = false;
            yield return StartCoroutine(clean());
            uiManager.completeWindowOpen();
        }
        yield return null;
    }

    IEnumerator clean()
    {
        for (int i = 0; i < cardInSlot.Count; i++)
        {
            cardInSlot[i].end();
        }
        audioManager.discard();
        cardInSlot.Clear();
        yield return new WaitForSecondsRealtime(0.8f);
    }

    IEnumerator autoDrawCard(int count)
    {
        nextTurn();
        yield return new WaitForSecondsRealtime(0.3f);
        for (int i = 0; i < count; i++)
        {
            audioManager.draw();
            DrawCard();
            yield return new WaitForSecondsRealtime(0.2f);
        }
    }
}
