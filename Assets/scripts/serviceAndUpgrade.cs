using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class serviceAndUpgrade
{
    public static Dictionary<string, etcCardEffect> services = new Dictionary<string, etcCardEffect>()
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
        ["11r"] = new etcCardEffect(0, 4),
        ["12r"] = new etcCardEffect(0, 6), // ��
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
        ["������"] = new etcCardEffect(4, 0),
        ["����"] = new etcCardEffect(0, 4),
        ["coin"] = new etcCardEffect(0, 1),
        ["����������"] = new etcCardEffect(0, 7),
        ["����"] = new etcCardEffect(4, 0),
        ["������"] = new etcCardEffect(0, 5), // ���� ���� ��

    };

    public static Dictionary<string, string[]> titleCotent = new Dictionary<string, string[]>()
    {
        ["1"] = new string[] { "�ҳ���", "1���� �ش��ϴ� ī�带 �÷����ϸ� +4 ���" },
        ["2"] = new string[] { "��ȭ", "2���� �ش��ϴ� ī�带 �÷����ϸ� +4 ���" },
        ["3"] = new string[] { "����", "3���� �ش��ϴ� ī�带 �÷����ϸ� +4 ���" },
        ["4"] = new string[] { "���", "4���� �ش��ϴ� ī�带 �÷����ϸ� +4 ���" },
        ["5"] = new string[] { "����", "5���� �ش��ϴ� ī�带 �÷����ϸ� +4 ���" },
        ["6"] = new string[] { "���", "6���� �ش��ϴ� ī�带 �÷����ϸ� +4 ���" },
        ["7"] = new string[] { "�θ�", "7���� �ش��ϴ� ī�带 �÷����ϸ� +4 ���" },
        ["8"] = new string[] { "����", "8���� �ش��ϴ� ī�带 �÷����ϸ� +4 ���" },
        ["9"] = new string[] { "��ȭ", "9���� �ش��ϴ� ī�带 �÷����ϸ� +4 ���" },
        ["10"] = new string[] { "��ǳ", "10���� �ش��ϴ� ī�带 �÷����ϸ� +4 ���" }, // �� ��ȭ
        ["11r"] = new string[] { "��", "���� �ش��ϴ� ī�带 �÷����ϸ� +6 ���" },
        ["12r"] = new string[] { "��Ȳ", "���� �ش��ϴ� ī�带 �÷����ϸ� +4 ���" }, // ��
        ["an"] = new string[] { "������ȣ��", "�����׸��� �ִ� ī�带 �÷����ϸ� +4���" }, // ����
        ["��������"] = new string[] { "����", "������ �÷��� �ϸ� +2���\n������ �÷��� �� ������ +1��� (�ִ� +6���)" },
        ["������"] = new string[] { "����", "���� �÷��� �ϸ� +2���\n���� �÷��� �� ������ +1��� (�ִ� +6���)" },
        ["��������"] = new string[] { "����", "���� �÷��� �ϸ� +2���\n���縦 �÷��� �� �� ���� +1��� (�ִ� +6���)" },
        ["������"] = new string[] { "�嶯", "�� �÷��� �ϸ� +2���\n���� �÷��� �� ������ +1��� (�ִ� +6���)" },
        ["��������"] = new string[] { "����", "���� �÷��� �ϸ� +2���\n������ �÷��� �� ������ +1��� (�ִ� +6���)" },
        ["�������"] = new string[] { "���", "��� �÷��� �ϸ� +2���\n����� �÷��� �� ������ +1��� (�ִ� +6���)" },
        ["��������"] = new string[] { "����", "���� �÷��� �ϸ� +2���\n������ �÷��� �� ������ +1��� (�ִ� +6���)" },
        ["�����˸�"] = new string[] { "�˸�", "�˸� �÷��� �ϸ� +2���\n�˸��� �÷��� �� ������ +1��� (�ִ� +6���)" },
        ["�������"] = new string[] { "���", "��� �÷��� �ϸ� +2���\n��縦 �÷��� �� ������ +1��� (�ִ� +6���)" }, // ������ȭ (������)
        ["������"] = new string[] { "��������", "������ �� �� ���� +4 Ĩ" }, // ������
        ["����"] = new string[] { "�ɳ�����", "�÷����Ҷ� +4 ���" },
        ["coin"] = new string[] { "����", "������ 5�� ���� �� ������ +1 ��� (�ִ� +5 ���)" },
        ["����������"] = new string[] { "����������", "��� ������ 2ȸ�� �÷����ϸ� x7 ���" },
        ["����"] = new string[] { "���������� ��", "������ Ƚ���� 12ȸ�� �Ǹ� x4 ���" },
        ["������"] = new string[] { "�� �Ȱ��� �� ����", "���� ī�尡 12�� �̻��̸� x5 ���" }, // �������� ��
        ["1s"] = new string[] { "�޺�", "�÷����Ҷ� +10 Ĩ" },
        ["2s"] = new string[] { "�޺�", "�÷����Ҷ� +10 Ĩ" },
        ["1g"] = new string[] { "�޺�", "�÷����Ҷ� +1 ���" },
        ["2g"] = new string[] { "�޺�", "�÷����Ҷ� +1 ���" }, // ���� ��, �� �Ѱ����ε� �����̸� ó�� ������
        ["ty"] = new string[] { "�ƹ��͵� ���� ī��", "������ ���Ծȵǰ�, �� �Ǵ� �п� ������ ������ +30 Ĩ" }, //��ȭī��
    };

    public static etcCardEffect checkService(List<string> hand, string name)
    {
        Hand check = jokbo.handCheck(hand);
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
            else if (name == "an" && (card.Contains("1-1") || card.Contains("2-1") || card.Contains("4-1") || card.Contains("6-1") || card.Contains("7-1") || card.Contains("10-1")))
            {
                return services[name];
            }
            else if(name == "����")
            {
                return services[name];
            }
            else if(name == $"����{check.HandName}")
            {
                string cardName = $"����{check.HandName}";
                services[cardName].addMultiple(1);
                return services[cardName];
            }
        }
        return new etcCardEffect(0, 0);
    }

    
}
