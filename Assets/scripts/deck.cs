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
            c = transform.Find(card.name).gameObject; // deck UI �θ� ������Ʈ�� �ڽĿ�����Ʈ(�ش� ī��)�� �˻� 
            ui = c.GetComponent<Image>();
            game = card.GetComponent<SpriteRenderer>();
            ui.sprite = game.sprite;
            ui.color = game.color;
            // �� �θ� ������Ʈ�� �ڽĿ�����Ʈ ��������Ʈ �̹�����
            // ���� �ΰ��� deck�� ��������Ʈ �̹��� ���Ϸ� ����
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
