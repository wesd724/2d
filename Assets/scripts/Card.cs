using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasSelected = false;
    public int handIndex;
    private GameManager gm;
    private AudioManager am;
    private bool clicked = false;
    private bool mouseOver = false;

    Vector2 origin; // 원래 카드 위치(슬롯 위)

    private Sprite sprite;

    public void Start()
    {
        gm = FindObjectOfType<GameManager>();
        am = FindObjectOfType<AudioManager>();
        sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }
    public void OnMouseDown()
    {
        if (!hasSelected && gm.handCount() < 2)
        {
            transform.position += Vector3.up * 0.6f;
            am.select();
            hasSelected = true;
            clicked = true;
            gm.addHand(sprite.name);
        }
        else if (clicked)
        {
            transform.position += Vector3.down * 0.6f;
            hasSelected = false;
            clicked = false;
            gm.cancelHand(sprite.name);
        }

        Hand hand = jokbo.handCheck(gm.getHand());
        gm.updateHand(hand);
    }

    public void init()
    {
        hasSelected = false;
        handIndex = -1;
        clicked = false;
        mouseOver = false;
    }

    public void OnMouseEnter()
    {
        if (!hasSelected)
        {
            transform.position += Vector3.up * 0.1f;
        }
        mouseOver = true;
    }

    public void OnMouseExit()
    {
        if (!hasSelected)
        {
            transform.position = origin;
        }
        mouseOver = false;
    }

    public string getSpriteName()
    {
        return sprite.name;
    }

    public IEnumerator selectCard()
    {
        gm.availableCardSlots[handIndex] = true;
        yield return StartCoroutine(handAnim(transform.position, transform.position + Vector3.up * 2.2f));
    }

    public void end()
    {
        gm.availableCardSlots[handIndex] = true;
        StartCoroutine(drawAnim(transform.position, transform.position + new Vector3(10, 0, 0), 0.3f, true));
    }

    public void scaleUp(float d)
    {
        transform.position += Vector3.up * 0.1f;
    }

    public GameObject getSpriteObject(GameObject parent)
    {
        if(sprite.name != "empty")
        {
            string name = sprite.name.Split("-")[0];
            return parent.transform.GetChild(int.Parse(name)).gameObject;
        }
        return parent.transform.GetChild(0).gameObject;
    }

    public IEnumerator drawAnim(Vector3 start, Vector3 target, float endTime = 0.3f, bool isEnd = false)
    {
        origin = target;
        float startTime = 0f;
        Quaternion startRotation, endRotation;
        if (isEnd)
        {
            startRotation = Quaternion.Euler(0, 0, 0);
            endRotation = Quaternion.Euler(-80, 0, 70);
        }
        else
        {
            startRotation = Quaternion.Euler(-80, 0, 70);
            endRotation = Quaternion.Euler(0, 0, 0);
        }
        while (startTime <= endTime)
        {
            transform.position = Vector3.Lerp(start, target, startTime / endTime);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, startTime / endTime);
            startTime += Time.deltaTime;
            yield return null;
        }
        transform.position = target;
        transform.rotation = endRotation; // 미세한 오차를 제거
        if (mouseOver) transform.position += Vector3.up * 0.1f;
        //if (isEnd) gameObject.SetActive(false);
    }

    public IEnumerator handAnim(Vector3 start, Vector3 target)
    {
        float startTime = 0f;
        float endTime = 0.1f;
        while (startTime <= endTime)
        {
            transform.position = Vector3.Lerp(start, target, startTime / endTime);
            startTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target; // 미세한 오차를 제거
        yield return new WaitForSecondsRealtime(0.3f);
    }
}
