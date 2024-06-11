using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null; // 아직은 사용 안함.
    TextManager textManager;

    UiManager uiManager;

    public List<Card> deck = new List<Card>();
    public List<CardSlot> cardSlots = new List<CardSlot>(); // 카드가 놓일 슬롯
    public bool[] availableCardSlots;

    public List<Image> serviceDeck = new List<Image>(); // 서비스 카드들의 이름

    private List<Card> cardInSlot = new List<Card>(); // 슬롯에 카드가 존재하는지 확인용 리스트
    private List<string> hand = new List<string>(); // 족보 확인용

    public List<Card> useCard = new List<Card>(); // 사용한 카드(패 포함)

    public BoxCollider2D deckButton; // 덱 보기 버튼
    public deck currentDeck; // 현재 보유 덱

    bool buttonClickStatus = false; // 버튼 한번만 클릭하게 설정

    IEnumerator method = null;

    // 전역
    public static bool d1 = false;
    public static int discardStack = 0; // 버리기카드 누적
    public static bool d2 = false;
    public static int discardStack_s = 0; // 쓰레기통의 왕 카드 누적
    public static int wave = 0; // 한 웨이브당 3라운드 => 2,  round 1 설정
    public static int round = -1;

    GameObject number; // 점수 스프라이트 부모 오브젝트

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
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
        uiManager = GameObject.Find("UiManager").GetComponent<UiManager>();
        textManager = TextManager.instance;
        textManager.test(); // 테스트 함수
        serviceAndUpgrade.initS();
        updateServiceContent();
        StartCoroutine(startGame());

        //uiManager.completeWindowOpen();
    }

    public IEnumerator startGame()
    {
        deckButton.enabled = true; // 덱 버튼 사용 가능
        init();
        yield return new WaitForSecondsRealtime(0.3f);
        StartCoroutine(autoDrawCard(3));
        buttonClickStatus = true;
    }

    public IEnumerator organize()
    {
        foreach (Card card in useCard)
        {
            AudioManager.instance.restart();
            card.init();
            yield return StartCoroutine(card.drawAnim(card.transform.position, deck[0].transform.position, 0.15f));
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
                    AudioManager.instance.draw();
                    //Debug.Log($"{i + 1} 번째 카드 드로우!");
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
        // 강화카드에 대한 것도 적용해야한다.
        AudioManager.instance.chip();
        textManager.chip.text = (float.Parse(textManager.chip.text) + c).ToString();
    }

    public void addMultiple(int m)
    {
        AudioManager.instance.chip();
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

    public void handPlay() // 핸드플레이 버튼
    {
        if (buttonClickStatus && hand.Count > 0)
        {
            buttonClickStatus = false;
            updateServiceContent();
            StartCoroutine(handPlayCoroutine());
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
                    //Debug.Log($"{i + 1} 번째 카드 선택");
                    AudioManager.instance.handPlay();
                    yield return StartCoroutine(cardInSlot[i].selectCard());
                    handPlayCount++;
                }
            }
            method = handPlayProcess(handPlayCount);
            StartCoroutine(method);
            TextManager.instance.upStack(textManager.handName.text); // 족보 횟수 증가
            textManager.handCount.text = (count - 1).ToString();
        }
    }

    public void discard() // 버리기 버튼
    {
        if (buttonClickStatus && hand.Count > 0)
        {
            int count = int.Parse(textManager.discardCount.text);
            if (count > 0)
            {
                discardService();
                updateServiceContent();
                int discardCount = 0;
                AudioManager.instance.discard();
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
                textManager.discardCount.text = (count - 1).ToString();
                buttonClickStatus = false;
                StartCoroutine(autoDrawCard(discardCount));
            }
            hand.Clear();
        }

    }

    void discardService()
    {
        if (d1) discardStack += 1;
        if (d2) discardStack_s += 1;
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
                string cardName = cardInSlot[i].getSpriteName(); // 핸드플레이할 때 점수 획득 과정
                addChip(cardName != "empty" ? int.Parse(cardName.Split("-")[0]) : 0);

                //Debug.Log($"{i + 1} 번째 카드 처리중");
                yield return new WaitForSecondsRealtime(0.4f);
                sprite.SetActive(false);
                yield return new WaitForSecondsRealtime(0.3f);

                yield return StartCoroutine(upgradeCheck(cardName, cardInSlot[i].transform.position)); // 강화카드 적용

            }
        }
        //yield return StartCoroutine(emptyCard());
        // 여기까지가 핸드플레이에 대한 점수 적용(강화 카드 포함)
        // 여기서부터 서비스패에 대해 효과 적용
        yield return StartCoroutine(applyService());
        // 모든 카드 적용 끝
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
        AudioManager.instance.discard();
        StartCoroutine(autoDrawCard(count));
    }

    public IEnumerator emptyCard()
    {
        bool status = false; // empty 존재 확인
        for (int i = 0; i < 3; i++)
        {
            if (deck.Count > 0 && !availableCardSlots[i] && cardInSlot[i].getSpriteName() == "empty")
            {
                status = true;
                break;
            }
        }

        if (!status) // 패에 없으면 덱 확인
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
        Debug.Log(c);
        int index = 0;
        if (c == 's')
        {
            index = 11;
        }
        else if (c == 'g')
        {
            index = 12;
        }
        else if (c == 'y')
        {
            index = 13;
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
            else if (index == 13)
            {
                addChip(30);
            }
            yield return new WaitForSecondsRealtime(0.4f);
            sprite.SetActive(false);
            yield return new WaitForSecondsRealtime(0.3f);
        }
        yield return new WaitForSecondsRealtime(0.05f);
    }

    IEnumerator applyService()
    {
        List<Image> serviceList = serviceDeck.FindAll(img => img.sprite.name != "-1" && img.sprite.name != "ㅈㅂㅁㅅㅌ" && img.sprite.name != "ㅆㅇ" && img.sprite.name != "ㄸㄱㅇ");
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
        // 일반 서비스패

        List<Image> serviceList2 = serviceDeck.FindAll(img => img.sprite.name == "ㅈㅂㅁㅅㅌ" || img.sprite.name == "ㅆㅇ" || img.sprite.name == "ㄸㄱㅇ");
        foreach (Image img in serviceList2)
        {
            etcCardEffect effect = serviceAndUpgrade.checkService(hand, img.sprite.name);
            showServiceText showText = img.GetComponent<showServiceText>();
            if (showText.setText2(effect))
            {
                showText.show();
                effectProcess2(effect);
                yield return new WaitForSecondsRealtime(0.4f);
                showText.close();
                yield return new WaitForSecondsRealtime(0.3f);
            }
        }
        //전설 서비스패
        yield return new WaitForSecondsRealtime(0.05f);
    }

    public void updateServiceContent()
    {
        serviceAndUpgrade.initU();
        serviceDeck.ForEach(img =>
        {
            string name = img.sprite.name;
            if(name == "ㅂㄹㄱ")
            {
                d1 = true;
            } else if(name == "ㅆㅇ")
            {
                d2 = true;
            }
            img.GetComponent<cardInfo>().judge(img.sprite.name);
        });
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

    void effectProcess2(etcCardEffect effect)
    {
        AudioManager.instance.legend();
        textManager.multiple.text = (float.Parse(textManager.multiple.text) * effect.Multiple).ToString();
    }

    IEnumerator addScore()
    {
        AudioManager.instance.score();

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
        textManager.score.text = ((int)target).ToString();
        yield return StartCoroutine(check((int)target));
    }

    IEnumerator check(float score)
    {
        if (score >= int.Parse(textManager.goal.text))
        {
            yield return new WaitForSeconds(0.8f);
            nextTurn();
            StopCoroutine(method);
            deckButton.enabled = false;
            yield return StartCoroutine(clean());
            if (wave == 2 && round == 2)
            {
                uiManager.endWindowOpen();
            }
            else
            {
                checkRound();
                uiManager.completeWindowOpen();
            }
        }
        else if (int.Parse(textManager.handCount.text) <= 0)
        {
            StopCoroutine(method);
            yield return StartCoroutine(clean());
            uiManager.failWindowOpen();
        }
        yield return null;
    }

    void checkRound()
    {
        if (round < 2)
        {
            round += 1;
        }
        else
        {
            wave += 1;
            round = 0;
        }
    }

    IEnumerator clean()
    {
        for (int i = 0; i < cardInSlot.Count; i++)
        {
            cardInSlot[i].end();
        }
        AudioManager.instance.discard();
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

    public void watchDeck(bool status)
    {
        foreach(Card card in cardInSlot)
        {
            card.GetComponent<BoxCollider2D>().enabled = status;
        }
    }
}
