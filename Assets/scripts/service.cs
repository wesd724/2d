using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class service : MonoBehaviour
{
    public Image[] images;
    public TextMeshProUGUI[] texts;
    public GameObject serviceCardList; // ���� ī�� ����� ���� �θ� ������Ʈ
                                       // �� �������� �������� 3���� �̾� img1, 2, 3�� sprite �����Ѵ�.
    public GameObject alreadyPickList;
    public Image cardback;

    void OnEnable()
    {
        List<Transform> childs = new List<Transform>();
        foreach (Transform child in alreadyPickList.transform)
        {
            childs.Add(child);
        }
        foreach(Transform child in childs)
        {
            child.SetParent(null);
            child.SetParent(serviceCardList.transform);  // ������ ���ŵ� ���� ī�� ��ϵ� ����
        }
        childs.Clear();

        availableServiceCard();
    }

    public void setting()
    {
        //�������� ���� Ȯ���� �ſ� ���� => ���ʺ��� ���� 3����
        //�⺻ī�尡 70 % �ݻ��� ������ ��ȭī��� 20 % �ݻ�ī��� 10 % ����
        List<Transform> availableCards = new List<Transform>();
        foreach(Transform child in serviceCardList.transform)
        {
            availableCards.Add(child);  // ���� ��밡���� ����ī�� ����
        }

        int take = availableCards.Count >= 2 ? 2 : availableCards.Count == 1 ? 1 : 0;

        for(int i = 0; i < take; i++)
        {
            int index = Random.Range(0, availableCards.Count);
            Transform child = availableCards[index];
            Sprite sprite = child.GetComponent<Image>().sprite;
            images[i].enabled = true;
            images[i].sprite = sprite;

            availableCards.RemoveAt(index); // ���õ� ī�带 ����Ʈ���� �����ϰ� �θ� ����
            child.SetParent(alreadyPickList.transform);
        }

        // ��밡���� ����ī��
        //int take = serviceCardList.transform.childCount >= 2 ? 2 : serviceCardList.transform.childCount == 1 ? 1 : 0;
        ////List<int> numbers =
        //    //Enumerable.Range(0, serviceCardList.transform.childCount).OrderBy(x => Random.value).Take(take).ToList();
        //for (int i = 0; i < take; i++)
        //{
        //    int index = Enumerable.Range(0, serviceCardList.transform.childCount).OrderBy(x => Random.value).First();
        //    Transform child = serviceCardList.transform.GetChild(index);
        //    Sprite sprite = child.GetComponent<Image>().sprite;
        //    images[i].enabled = true;
        //    images[i].sprite = sprite;
        //    child.SetParent(null); // �̹� �ش� �Ͽ��� ���� ���� ī�� ����
        //    child.SetParent(alreadyPickList.transform); // �ش� �Ͽ��� �� �� ����ī�� ����

        //}

        for (int i = 0; i < 2; i++)
        {
            images[i].GetComponent<cardInfo>().judge(images[i].sprite.name);
            int price = checkPrice(images[i].sprite.name);
            texts[i].text = $"${price}";
            if (price == -1)
                images[i].enabled = false;
        }
    }

    public void back()
    {
        for (int i = 0; i < 2; i++)
        {
            images[i].enabled = true;
            images[i].sprite = cardback.sprite;
        }
    }

    void availableServiceCard()
    {
        List<Transform> temp = new List<Transform>();
        List<Image> useService = GameManager.instance.serviceDeck.FindAll(img => img.sprite.name != "-1");
        foreach (Transform child in serviceCardList.transform) // �̹� ������� ���� ī��
        {
            Sprite sprite = child.GetComponent<Image>().sprite;
            foreach (Image img in useService)
            {
                Sprite useSprite = img.sprite;
                if (sprite.name == useSprite.name)
                {
                    temp.Add(child); 
                    break;
                }
            }
        }
        temp.ForEach(child => child.SetParent(null)); // �̹� �ش� �Ͽ��� ���� ���� ī�� ����
    }

    public int checkPrice(string name)
    {
        if (name.Contains("����") || name == "������")
        {
            return Random.Range(5, 7);
        }
        else if (name == "������" || name == "����" || name == "����������")
        {
            return Random.Range(8, 11);
        }
        else if (name == "cardback")
        {
            return -1;
        }

        return Random.Range(4, 7);
    }
}
