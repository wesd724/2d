using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasSelected = false;
    public int slotIndex = -1; // 슬롯에 있는 위치 인덱스
    public GameManager gm;
    private bool clicked = false;
    private bool mouseOver = false;

    Vector3 origin; // 원래 카드 위치(슬롯 위)

    void OnEnable()
    {
        
    }

    public void OnMouseDown()
    {
        if (!hasSelected && gm.handCount() < 2)
        {
            gm.addHand(this);
            transform.position = new Vector3(transform.position.x, -2.52f, transform.position.z);
            AudioManager.instance.select();
            hasSelected = true;
            clicked = true;
        }
        else if (hasSelected)
        {
            gm.cancelHand(this);
            transform.position = origin + Vector3.up * 0.1f;
            hasSelected = false;
            clicked = false;
        }

        Hand hand = jokbo.handCheck(gm.getHand());
        gm.updateHand(hand);
    }

    public void init()
    {
        hasSelected = false;
        slotIndex = -1;
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
        return GetComponent<SpriteRenderer>().sprite.name;
    }

    public IEnumerator selectCard()
    {
        gm.availableCardSlots[slotIndex] = true;
        yield return StartCoroutine(handAnim(transform.position, transform.position + Vector3.up * 2.2f));
    }

    public void end()
    {
        gm.availableCardSlots[slotIndex] = true;
        StartCoroutine(drawAnim(transform.position, transform.position + new Vector3(10, 0, 0), 0.3f, true));
    }

    public void scaleUp(float d)
    {
        transform.position += Vector3.up * 0.1f;
    }

    public GameObject getSpriteObject(GameObject parent)
    {
        if(GetComponent<SpriteRenderer>().sprite.name != "empty")
        {
            string name = GetComponent<SpriteRenderer>().sprite.name.Split("-")[0];
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
