using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null; // ������ ��� ����.
    TextManager textManager;
    AudioManager audioManager;
    UiManager uiManager;

    public List<Card> deck = new List<Card>();
    public List<CardSlot> cardSlots = new List<CardSlot>(); // ī�尡 ���� ����
    public bool[] availableCardSlots;

    public List<Image> serviceDeck = new List<Image>(); // ���� ī����� �̸�

    private List<Card> cardInSlot = new List<Card>(); // ���Կ� ī�尡 �����ϴ��� Ȯ�ο� ����Ʈ
    private List<string> hand = new List<string>(); // ���� Ȯ�ο�

    public List<Card> useCard = new List<Card>(); // ����� ī��(�� ����)

    public BoxCollider2D deckPostion; // �� ��ġ
    public deck currentDeck; // ���� ���� ��

    bool buttonClickStatus = false; // ��ư �ѹ��� Ŭ���ϰ� ����

    IEnumerator method = null;


    GameObject number; // ���� ��������Ʈ �θ� ������Ʈ

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
        number = GameObject.Find("numberSprite");
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        uiManager = GameObject.Find("UiManager").GetComponent<UiManager>();
        textManager = TextManager.instance;
        //StartCoroutine(startGame());

        uiManager.completeWindowOpen();
    }

    public IEnumerator startGame()
    {
        serviceDeck.ForEach(img =>
        {
            img.GetComponent<cardInfo>().judge(img.sprite.name);
        });
        deckPostion.enabled = true; // �� ��ư ��� ����
        init();
        yield return new WaitForSecondsRealtime(0.3f);
        StartCoroutine(autoDrawCard(3));
        buttonClickStatus = true;
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
                    audioManager.draw();
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
        textManager.chip.text = (float.Parse(textManager.chip.text) + hand.Chip).ToString();
        textManager.multiple.text = (float.Parse(textManager.multiple.text) + hand.Multiple).ToString();
    }

    public void addChip(int c)
    {
        // ��ȭī�忡 ���� �͵� �����ؾ��Ѵ�.
        audioManager.chip();
        textManager.chip.text = (float.Parse(textManager.chip.text) + c).ToString();
    }

    public void addMultiple(int m)
    {
        audioManager.chip();
        textManager.multiple.text = (float.Parse(textManager.multiple.text) + m).ToString();
    }

    public void init()
    {
        textManager.score.text = "0";
        textManager.handName.text = "";
        textManager.chip.text = "0";
        textManager.multiple.text = "0";
        textManager.handCount.text = "30";
        textManager.discardCount.text = "40";
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
        if (buttonClickStatus)
        {
            StartCoroutine(handPlayCoroutine());
            buttonClickStatus = false;
        }
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
            TextManager.instance.upStack(textManager.handName.text); // ���� Ƚ�� ����
            textManager.handCount.text = (count - 1).ToString();
        }
    }

    public void discard() // ������ ��ư
    {
        if (buttonClickStatus)
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
            buttonClickStatus = false;
        }

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

                GameObject sprite = cardInSlot[i].getSpriteObject(number);
                sprite.SetActive(true);
                sprite.transform.position = cardInSlot[i].transform.position;
                string cardName = cardInSlot[i].getSpriteName(); // �ڵ��÷����� �� ���� ȹ�� ����
                addChip(cardName != "empty" ? int.Parse(cardName.Split("-")[0]) : 0);

                //Debug.Log($"{i + 1} ��° ī�� ó����");
                yield return new WaitForSecondsRealtime(0.4f);
                sprite.SetActive(false);
                yield return new WaitForSecondsRealtime(0.3f);

                yield return StartCoroutine(upgradeCheck(cardName, cardInSlot[i].transform.position)); // ��ȭī�� ����

            }
        }
        yield return StartCoroutine(emptyCard());
        // ��������� �ڵ��÷��̿� ���� ���� ����(��ȭ ī�� ����)
        // ���⼭���� �����п� ���� ȿ�� ����
        yield return StartCoroutine(applyService());
        // ��� ī�� ���� ��
        hand.Clear();
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

    public IEnumerator emptyCard()
    {
        bool status = false; // empty ���� Ȯ��
        for (int i = 0; i < 3; i++)
        {
            if (deck.Count > 0 && !availableCardSlots[i] && cardInSlot[i].getSpriteName() == "empty")
            {
                status = true;
                break;
            }
        }

        if (!status) // �п� ������ �� Ȯ��
        {
            if (deck.Find(card => card.GetComponent<SpriteRenderer>().sprite.name == "empty") != null)
                status = true;
        }

        if (status)
        {
            GameObject sprite = number.transform.GetChild(13).gameObject;
            sprite.SetActive(true);
            addChip(30);
            yield return new WaitForSecondsRealtime(0.4f);
            sprite.SetActive(false);
            yield return new WaitForSecondsRealtime(0.3f);
        }
    }

    public IEnumerator upgradeCheck(string cardName, Vector3 slot)
    {
        char c = cardName[^1];
        int index = 0;
        if (c == 's')
        {
            index = 11;
        }
        else if (c == 'g')
        {
            index = 12;
        }

        if (index != 0)
        {
            GameObject sprite = number.transform.GetChild(index).gameObject;
            sprite.SetActive(true);
            sprite.transform.position = slot - new Vector3(0.7f, 0, 0);
            if (index == 11)
            {
                addChip(10);
            }
            else if (index == 12)
            {
                addMultiple(1);
            }
            yield return new WaitForSecondsRealtime(0.4f);
            sprite.SetActive(false);
            yield return new WaitForSecondsRealtime(0.3f);
        }
        yield return null;
    }

    IEnumerator applyService()
    {
        List<Image> serviceList = serviceDeck.FindAll(img => img.sprite.name != "-1");
        foreach (Image img in serviceList)
        {
            etcCardEffect effect = serviceAndUpgrade.checkService(hand, img.sprite.name);
            showServiceText showText = img.GetComponent<showServiceText>();
            if (showText.setText(effect))
            {
                showText.show();
                effectProcess(effect);
                yield return new WaitForSecondsRealtime(0.4f);
                showText.close();
                yield return new WaitForSecondsRealtime(0.3f);
            }
        }
    }

    void effectProcess(etcCardEffect effect)
    {
        if (effect.Chip != 0)
        {
            addChip(effect.Chip);
        }

        if (effect.Multiple != 0)
        {
            addMultiple(effect.Multiple);
        }
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
            DrawCard();
            yield return new WaitForSecondsRealtime(0.2f);
        }

        buttonClickStatus = true;
    }
}
