using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class serviceAndUpgrade
{
    public static Dictionary<string, etcCardEffect> services;
    public static Dictionary<string, string[]> titleContent;

    public static void initS()
    {
        services = new Dictionary<string, etcCardEffect>()
        {
            ["�� ��ȭ"] = new etcCardEffect(0, 4),
            ["1"] = new etcCardEffect(0, 4),
            ["2"] = new etcCardEffect(0, 4),
            ["3"] = new etcCardEffect(0, 4),
            ["4"] = new etcCardEffect(0, 4),
            ["5"] = new etcCardEffect(0, 4),
            ["6"] = new etcCardEffect(0, 4),
            ["7"] = new etcCardEffect(0, 4),
            ["8"] = new etcCardEffect(0, 4),
            ["9"] = new etcCardEffect(0, 4),
            ["10"] = new etcCardEffect(0, 4), // �� ��ȭ
            ["11r"] = new etcCardEffect(0, 6),
            ["12r"] = new etcCardEffect(0, 4), // ��
            ["an"] = new etcCardEffect(0, 6), // ����
            ["��������"] = new etcCardEffect(0, 2),
            ["������"] = new etcCardEffect(0, 2),
            ["��������"] = new etcCardEffect(0, 2),
            ["������"] = new etcCardEffect(0, 2),
            ["��������"] = new etcCardEffect(0, 2),
            ["�������"] = new etcCardEffect(0, 2),
            ["��������"] = new etcCardEffect(0, 2),
            ["�����˸�"] = new etcCardEffect(0, 2),
            ["�������"] = new etcCardEffect(0, 2), // ������ȭ
            ["����"] = new etcCardEffect(0, 4),
            ["������"] = new etcCardEffect(0, 0),
            ["coin"] = new etcCardEffect(0, 0),
            ["����������"] = new etcCardEffect(0, 7), // ������ ó��
            ["����"] = new etcCardEffect(0, 4), // ������ ó��
            ["������"] = new etcCardEffect(0, 5), // ������ ó��, ���� ���� ��

        };
    }

    public static void initU()
    {
        titleContent = new Dictionary<string, string[]>()
        {
            ["1"] = new string[] { "�ҳ���", "1���� �ش��ϴ� ī�带 �÷����ϸ�\n+4 ���" },
            ["2"] = new string[] { "��ȭ", "2���� �ش��ϴ� ī�带 �÷����ϸ�\n+4 ���" },
            ["3"] = new string[] { "����", "3���� �ش��ϴ� ī�带 �÷����ϸ�\n+4 ���" },
            ["4"] = new string[] { "���", "4���� �ش��ϴ� ī�带 �÷����ϸ�\n+4 ���" },
            ["5"] = new string[] { "����", "5���� �ش��ϴ� ī�带 �÷����ϸ�\n+4 ���" },
            ["6"] = new string[] { "���", "6���� �ش��ϴ� ī�带 �÷����ϸ�\n+4 ���" },
            ["7"] = new string[] { "�θ�", "7���� �ش��ϴ� ī�带 �÷����ϸ�\n+4 ���" },
            ["8"] = new string[] { "����", "8���� �ش��ϴ� ī�带 �÷����ϸ�\n+4 ���" },
            ["9"] = new string[] { "��ȭ", "9���� �ش��ϴ� ī�带 �÷����ϸ�\n+4 ���" },
            ["10"] = new string[] { "��ǳ", "10���� �ش��ϴ� ī�带 �÷����ϸ�\n+4 ���" }, // �� ��ȭ
            ["11r"] = new string[] { "��", "���� �ش��ϴ� ī�带 �÷����ϸ�\n+6 ���" },
            ["12r"] = new string[] { "��Ȳ", "���� �ش��ϴ� ī�带 �÷����ϸ�\n+4 ���" }, // ��
            ["an"] = new string[] { "������ȣ��", "�����׸��� �ִ� ī�带 �÷����ϸ�\n+6���" }, // ����
            ["��������"] = new string[] { "����", $"������ �÷��� �ϸ� +2���\n������ �÷��� �� ������ +1���\n(�ִ� +6���)\n���� ����:{services["��������"].Multiple - 2}\n+{services["��������"].Multiple}��� ����" },
            ["������"] = new string[] { "����", $"���� �÷��� �ϸ� +2���\n���� �÷��� �� ������ +1���\n(�ִ� +6���)\n���� ����:{services["������"].Multiple - 2}\n+{services["������"].Multiple}��� ����" },
            ["��������"] = new string[] { "����", $"���� �÷��� �ϸ� +2���\n���縦 �÷��� �� ������ +1���\n(�ִ� +6���)\n���� ����:{services["��������"].Multiple - 2}\n+{services["��������"].Multiple}��� ����" },
            ["������"] = new string[] { "�嶯", $"�� �÷��� �ϸ� +2���\n���� �÷��� �� ������ +1���\n(�ִ� +6���)\n���� ����:{services["������"].Multiple - 2}\n+{services["������"].Multiple}��� ����" },
            ["��������"] = new string[] { "����", $"���� �÷��� �ϸ� +2���\n������ �÷��� �� ������ +1���\n(�ִ� +6���)\n���� ����:{services["��������"].Multiple - 2}\n+{services["��������"].Multiple}��� ����" },
            ["�������"] = new string[] { "���", $"��� �÷��� �ϸ� +2���\n����� �÷��� �� ������ +1���\n(�ִ� +6���)\n���� ����:{services["�������"].Multiple - 2}\n+{services["�������"].Multiple}��� ����" },
            ["��������"] = new string[] { "����", $"���� �÷��� �ϸ� +2���\n������ �÷��� �� ������ +1���\n(�ִ� +6���)\n���� ����:{services["��������"].Multiple - 2}\n+{services["��������"].Multiple}��� ����" },
            ["�����˸�"] = new string[] { "�˸�", $"�˸� �÷��� �ϸ� +2���\n�˸��� �÷��� �� ������ +1���\n(�ִ� +6���)\n���� ����:{services["�����˸�"].Multiple - 2}\n+{services["�����˸�"].Multiple}��� ����" },
            ["�������"] = new string[] { "���", $"��� �÷��� �ϸ� +2���\n��縦 �÷��� �� ������ +1���\n(�ִ� +6���)\n���� ����:{services["�������"].Multiple - 2}\n+{services["�������"].Multiple}��� ����" }, // ������ȭ (������)
            ["����"] = new string[] { "�ɳ�����", "�÷����Ҷ�\n+4 ���" },
            ["������"] = new string[] { "��������", $"������ �� �� ���� +4 Ĩ\n���� ����:{GameManager.discardStack}\n\n+{4 * GameManager.discardStack}Ĩ ����" }, // ������
            ["coin"] = new string[] { "����", "������ 5�� ���� �� ������ +1 ���\n(�ִ� +5 ���)" },
            ["����������"] = new string[] { "����������", "��� ������ 2ȸ�� �÷����ϸ�\nx7 ���" },
            ["����"] = new string[] { $"���������� ��", $"������ Ƚ���� 12ȸ�� �Ǹ� x4 ���\n���� ����:{GameManager.discardStack_s}" }, // ������
            ["������"] = new string[] { "�� �Ȱ��� �� ����", "���� ī�尡 6�� �̻��̸�\nx5 ���" }, // �������� ��
            ["1s"] = new string[] { "�޺�", "�÷����Ҷ�\n+10 Ĩ" },
            ["2s"] = new string[] { "�޺�", "�÷����Ҷ�\n+10 Ĩ" },
            ["1g"] = new string[] { "�޺�", "�÷����Ҷ�\n+1 ���" },
            ["2g"] = new string[] { "�޺�", "�÷����Ҷ�\n+1 ���" }, // ���� ��, �� �Ѱ����ε� �����̸� ó�� ������
            ["ty"] = new string[] { "�ƹ��͵� ���� ī��", "������ ���Ծȵȴ�.\n�÷����Ҷ� +30 Ĩ" }, //��ȭī��
        };
    }
    public static etcCardEffect checkService(List<Card> handCard, string name)
    {
        Hand check = jokbo.handCheck(handCard);
        List<string> hand = handCard.Select(card => card.getSpriteName()).ToList();
        foreach (string card in hand)
        {
            if (int.TryParse(name, out int temp) && card.Split("-")[0] == name)
            {
                return services[name];
            }
            else if (name == "11r" && (card.Contains("1-1") || card.Contains("3-1") || card.Contains("8-1")))
            {
                return services[name];
            }
            else if (name == "12r" && (card.Contains("1-1") || card.Contains("3-1") || card.Contains("8-1")))
            {
                return services[name];
            }
            else if (name == "an" && (card.Contains("1-1") || card.Contains("2-1") || card.Contains("4-1") || card.Contains("6-1") || card.Contains("7-1") || card.Contains("8-2") || card.Contains("10-1")))
            {
                return services[name];
            }
            else if (name == "����")
            {
                return services[name];
            }
            else if (name == $"����{check.HandName}")
            {
                etcCardEffect t = services[name].deepCopy();
                services[name].addMultiple();
                return t;
            }
            else if (name == "������")
            {
                services[name].setChip(GameManager.discardStack * 4);
                return services[name];
            }
            else if (name == "coin")
            {
                int coin = int.Parse(TextManager.instance.cash.text);
                services[name].setCoin(coin / 5);
                return services[name];
            }
            else if (name == "����������" && TextManager.instance.stackCheck())
            {
                return services[name];
            }
            else if (name == "����" && GameManager.discardStack_s >= 12)
            {
                return services[name];
            }
            else if (name == "������" && same())
            {
                return services[name];
            }
        }
        return new etcCardEffect(0, 0);
    }

    static bool same()
    {
        Dictionary<string, int> countcards = new Dictionary<string, int>();
        foreach (Card card in GameManager.instance.deck)
        {
            string name = card.GetComponent<SpriteRenderer>().sprite.name;
            if (countcards.ContainsKey(name))
            {
                countcards[name]++;
                if (countcards[name] >= 6)
                {
                    return true;
                }
            }
            else
            {
                countcards[name] = 1;
            }
        }

        foreach(Card card in GameManager.instance.useCard) // ����� ��, ���� �� ��� ����
        {
            string name = card.GetComponent<SpriteRenderer>().sprite.name;
            if (countcards.ContainsKey(name))
            {
                countcards[name]++;
                if (countcards[name] >= 6)
                {
                    return true;
                }
            }
            else
            {
                countcards[name] = 1;
            }
        }

        return false;

    }


}
