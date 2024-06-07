using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class service : MonoBehaviour
{
    public Image[] images;
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
            Sprite sprite = serviceCardList.transform.GetChild(numbers[i]).GetComponent<Image>().sprite;
            images[i].enabled = true;
            images[i].sprite = sprite;
        }
    }

    public void back()
    {
        for (int i = 0; i < 2; i++)
        {
            images[i].sprite = cardback.sprite;
        }
    }
}
