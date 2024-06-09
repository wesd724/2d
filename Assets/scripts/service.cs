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
    public GameObject serviceCardList; // ��ȭ�� ī�� ����� ���� �θ� ������Ʈ
                                       // �� �������� �������� 3���� �̾� img1, 2, 3�� sprite �����Ѵ�.
    public Image cardback;

    void OnEnable()
    {
        
    }

    public void setting()
    {
        // ���簡���� �ִ� ī�� ���� �ڵ�
        // �� �����Ͽ��� ���� �͵� ����
        // hover �� �������Ѵ�!
        // hover�� �����Ҷ����� ��� ī�忡 ���� ������Ʈ�� ������ ����
        // sprite �̸����� ���� �˻��Ͽ� �ؽ�Ʈ�� �����´�
        // sprite �̸����� �� ���ӿ� ��� ��ȣ�ۿ��� ����� ���� ���Ѱ� ����.
        int[] numbers =
            Enumerable.Range(0, serviceCardList.transform.childCount).OrderBy(x => Random.value).Take(2).ToArray();

        for (int i = 0; i < 2; i++)
        {
            Transform child = serviceCardList.transform.GetChild(numbers[i]);
            Sprite sprite = child.GetComponent<Image>().sprite;
            images[i].enabled = true;
            images[i].sprite = sprite;

            int price = check(sprite.name);
            texts[i].text = $"${price}";
            images[i].GetComponent<cardInfo>().judge(sprite.name);
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

    public int check(string name)
    {
        if(name.Contains("����") || name == "������")
        {
            return Random.Range(5, 7);
        }
        else if (name == "������" || name == "����" || name == "����������")
        {
            return Random.Range(8, 11);
        }
        return Random.Range(4, 7);
    }
}
