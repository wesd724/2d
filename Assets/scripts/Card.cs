using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasbeenPlayed;
    public bool hasSelected;
    public int handIndex;
    private GameManager gm;
    private bool clicked = false;

    public void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }
    public void OnMouseDown()
    {
        if(!hasSelected)
        {
            transform.position += Vector3.up * 0.6f;
            hasSelected = true;
            clicked = true;
        }
        else if(clicked)
        {
            transform.position += Vector3.down * 0.6f;
            hasSelected = false;
        }
    }

    public void OnMouseEnter()
    {
        if(!hasSelected)
        {
            transform.position += Vector3.up * 0.1f;
        }
    }

    public void OnMouseExit()
    {
        if (!hasSelected)
        {
            transform.position += Vector3.down * 0.1f;
        }
    }

    public void selectCard()
    {
        gm.availableCardSlots[handIndex] = true;
        transform.position += Vector3.up * 2.2f;
    }

    public void discard()
    {
        gm.availableCardSlots[handIndex] = true;
        gameObject.SetActive(false);
    }
}
