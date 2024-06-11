using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class jokbo
{
    public static Dictionary<string, List<float>> jokboList = new Dictionary<string, List<float>>()
    {
        ["»ïÆÈ±¤¶¯"] = new List<float> { 300f, 4f },
        ["±¤¶¯"] = new List<float> { 250f, 4f },
        ["¶¯"] = new List<float> { 100f, 4f },
        ["¾Ë¸®"] = new List<float> { 80f, 3f },
        ["µ¶»ç"] = new List<float> { 60f, 3f },
        ["±¸»æ"] = new List<float> { 50f, 2f },
        ["Àå»æ"] = new List<float> { 40f, 2f },
        ["Àå»ç"] = new List<float> { 40f, 1.5f },
        ["¼¼·ú"] = new List<float> { 20f, 1.5f },
        ["²ý"] = new List<float> { 20f, 1f },
    };

    public static Hand handCheck(List<string> hand)
    {
        if (hand.Count == 0)
        {
            return new Hand("", new List<float> { 0, 0 });
        }
        
        if (hand.Any(x => x.Contains("3-1")) && hand.Any(x => x.Contains("8-1")))
        {
            return new("»ïÆÈ±¤¶¯", jokboList["»ïÆÈ±¤¶¯"]);
        }

        if (hand.Any(x => x.Contains("1-1")) && hand.Any(x => x.Contains("3-1")) ||
            hand.Any(x => x.Contains("1-1")) && hand.Any(x => x.Contains("8-1")))
        {
            return new Hand("±¤¶¯", jokboList["±¤¶¯"]);
        }

        for (int i = 10; i > 0; i--)
        {
            if (hand.Count(x => x.StartsWith($"{i}-")) == 2)
                return new Hand("¶¯", jokboList["¶¯"]);
        }

        if (hand.Any(x => x.StartsWith("1-")) && hand.Any(x => x.StartsWith("2-")))
        {
            return new Hand("¾Ë¸®", jokboList["¾Ë¸®"]);
        }

        if (hand.Any(x => x.StartsWith("1-")) && hand.Any(x => x.StartsWith("4-")))
        {
            return new Hand("µ¶»ç", jokboList["µ¶»ç"]); ;
        }

        if (hand.Any(x => x.StartsWith("1-")) && hand.Any(x => x.StartsWith("9-")))
        {
            return new Hand("±¸»æ", jokboList["±¸»æ"]);
        }

        if (hand.Any(x => x.StartsWith("1-")) && hand.Any(x => x.StartsWith("10-")))
        {
            return new Hand("Àå»æ", jokboList["Àå»æ"]);
        }

        if (hand.Any(x => x.StartsWith("4-")) && hand.Any(x => x.StartsWith("10-")))
        {
            return new Hand("Àå»ç", jokboList["Àå»ç"]);
        }

        if (hand.Any(x => x.StartsWith("4-")) && hand.Any(x => x.StartsWith("6-")))
        {
            return new Hand("¼¼·ú", jokboList["¼¼·ú"]);
        }
        if (hand.Find(x => x == "empty") != null)
        {
            return new Hand("²ý", jokboList["²ý"]);
        }
        int sum = hand.Sum(x => int.Parse(x.Split("-")[0]));
        string extra = sum.ToString()[^1].ToString();

        return new Hand("²ý", jokboList["²ý"]);
    }
}
