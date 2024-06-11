using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class jokbo
{
    public static Dictionary<string, List<float>> jokboList = new Dictionary<string, List<float>>()
    {
        ["���ȱ���"] = new List<float> { 300f, 4f },
        ["����"] = new List<float> { 250f, 4f },
        ["��"] = new List<float> { 100f, 4f },
        ["�˸�"] = new List<float> { 80f, 3f },
        ["����"] = new List<float> { 60f, 3f },
        ["����"] = new List<float> { 50f, 2f },
        ["���"] = new List<float> { 40f, 2f },
        ["���"] = new List<float> { 40f, 1.5f },
        ["����"] = new List<float> { 20f, 1.5f },
        ["��"] = new List<float> { 20f, 1f },
    };

    public static Hand handCheck(List<string> hand)
    {
        if (hand.Count == 0)
        {
            return new Hand("", new List<float> { 0, 0 });
        }
        
        if (hand.Any(x => x.Contains("3-1")) && hand.Any(x => x.Contains("8-1")))
        {
            return new("���ȱ���", jokboList["���ȱ���"]);
        }

        if (hand.Any(x => x.Contains("1-1")) && hand.Any(x => x.Contains("3-1")) ||
            hand.Any(x => x.Contains("1-1")) && hand.Any(x => x.Contains("8-1")))
        {
            return new Hand("����", jokboList["����"]);
        }

        for (int i = 10; i > 0; i--)
        {
            if (hand.Count(x => x.StartsWith($"{i}-")) == 2)
                return new Hand("��", jokboList["��"]);
        }

        if (hand.Any(x => x.StartsWith("1-")) && hand.Any(x => x.StartsWith("2-")))
        {
            return new Hand("�˸�", jokboList["�˸�"]);
        }

        if (hand.Any(x => x.StartsWith("1-")) && hand.Any(x => x.StartsWith("4-")))
        {
            return new Hand("����", jokboList["����"]); ;
        }

        if (hand.Any(x => x.StartsWith("1-")) && hand.Any(x => x.StartsWith("9-")))
        {
            return new Hand("����", jokboList["����"]);
        }

        if (hand.Any(x => x.StartsWith("1-")) && hand.Any(x => x.StartsWith("10-")))
        {
            return new Hand("���", jokboList["���"]);
        }

        if (hand.Any(x => x.StartsWith("4-")) && hand.Any(x => x.StartsWith("10-")))
        {
            return new Hand("���", jokboList["���"]);
        }

        if (hand.Any(x => x.StartsWith("4-")) && hand.Any(x => x.StartsWith("6-")))
        {
            return new Hand("����", jokboList["����"]);
        }
        if (hand.Find(x => x == "empty") != null)
        {
            return new Hand("��", jokboList["��"]);
        }
        int sum = hand.Sum(x => int.Parse(x.Split("-")[0]));
        string extra = sum.ToString()[^1].ToString();

        return new Hand("��", jokboList["��"]);
    }
}
