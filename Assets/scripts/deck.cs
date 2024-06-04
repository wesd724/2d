using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class deck : MonoBehaviour
{
    void OnEnable()
    {
        Image ui;
        SpriteRenderer game;
        GameObject c;
        List<Card> deck = GameManager.instance.deck;
        foreach (Card card in deck)
        {
            c = transform.Find(card.name).gameObject; // deck UI 부모 오브젝트의 자식오브젝트(해당 카드)를 검색 
            ui = c.GetComponent<Image>();
            game = card.GetComponent<SpriteRenderer>();
            ui.sprite = game.sprite;
            ui.color = game.color;
            // 이 부모 오브젝트의 자식오브젝트 스프라이트 이미지를
            // 실제 인게임 deck에 스프라이트 이미지 파일로 연결
        }
    }

    public void use()
    {
        GameObject c;
        Image image;
        GameManager.instance.useCard.ForEach(card =>
        {
            c = transform.Find(card.name).gameObject;
            image = c.GetComponent<Image>();
            image.sprite = card.GetComponent<SpriteRenderer>().sprite;
            image.color = new Color(110 / 255f, 110 / 255f, 110 / 255f);
        });
    }

    public void all()
    {
        GameObject c;
        Image image;
        GameManager.instance.useCard.ForEach(card =>
        {
            c = transform.Find(card.name).gameObject;
            image = c.GetComponent<Image>();
            image.sprite = card.GetComponent<SpriteRenderer>().sprite;
            image.color = new Color(1, 1, 1);
        });
    }
}
