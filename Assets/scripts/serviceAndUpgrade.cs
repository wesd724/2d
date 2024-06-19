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
            ["월 강화"] = new etcCardEffect(0, 4),
            ["1"] = new etcCardEffect(0, 4),
            ["2"] = new etcCardEffect(0, 4),
            ["3"] = new etcCardEffect(0, 4),
            ["4"] = new etcCardEffect(0, 4),
            ["5"] = new etcCardEffect(0, 4),
            ["6"] = new etcCardEffect(0, 4),
            ["7"] = new etcCardEffect(0, 4),
            ["8"] = new etcCardEffect(0, 4),
            ["9"] = new etcCardEffect(0, 4),
            ["10"] = new etcCardEffect(0, 4), // 월 강화
            ["11r"] = new etcCardEffect(0, 6),
            ["12r"] = new etcCardEffect(0, 4), // 광
            ["an"] = new etcCardEffect(0, 6), // 동물
            ["ㅈㄱ광땡"] = new etcCardEffect(0, 2),
            ["ㅈㄱ끗"] = new etcCardEffect(0, 2),
            ["ㅈㄱ독사"] = new etcCardEffect(0, 2),
            ["ㅈㄱ땡"] = new etcCardEffect(0, 2),
            ["ㅈㄱ구삥"] = new etcCardEffect(0, 2),
            ["ㅈㄱ장삥"] = new etcCardEffect(0, 2),
            ["ㅈㄱ세륙"] = new etcCardEffect(0, 2),
            ["ㅈㄱ알리"] = new etcCardEffect(0, 2),
            ["ㅈㄱ장사"] = new etcCardEffect(0, 2), // 족보강화
            ["ㄱㅂ"] = new etcCardEffect(0, 4),
            ["ㅂㄹㄱ"] = new etcCardEffect(0, 0),
            ["coin"] = new etcCardEffect(0, 0),
            ["ㅈㅂㅁㅅㅌ"] = new etcCardEffect(0, 7), // 마지막 처리
            ["ㅆㅇ"] = new etcCardEffect(0, 4), // 마지막 처리
            ["ㄸㄱㅇ"] = new etcCardEffect(0, 5), // 마지막 처리, 전설 서비스 패

        };
    }

    public static void initU()
    {
        titleContent = new Dictionary<string, string[]>()
        {
            ["1"] = new string[] { "소나무", "1월에 해당하는 카드를 플레이하면\n+4 배수" },
            ["2"] = new string[] { "매화", "2월에 해당하는 카드를 플레이하면\n+4 배수" },
            ["3"] = new string[] { "벚꽃", "3월에 해당하는 카드를 플레이하면\n+4 배수" },
            ["4"] = new string[] { "등나무", "4월에 해당하는 카드를 플레이하면\n+4 배수" },
            ["5"] = new string[] { "난초", "5월에 해당하는 카드를 플레이하면\n+4 배수" },
            ["6"] = new string[] { "모란", "6월에 해당하는 카드를 플레이하면\n+4 배수" },
            ["7"] = new string[] { "싸리", "7월에 해당하는 카드를 플레이하면\n+4 배수" },
            ["8"] = new string[] { "공산", "8월에 해당하는 카드를 플레이하면\n+4 배수" },
            ["9"] = new string[] { "국화", "9월에 해당하는 카드를 플레이하면\n+4 배수" },
            ["10"] = new string[] { "단풍", "10월에 해당하는 카드를 플레이하면\n+4 배수" }, // 월 강화
            ["11r"] = new string[] { "비광", "광에 해당하는 카드를 플레이하면\n+6 배수" },
            ["12r"] = new string[] { "봉황", "광에 해당하는 카드를 플레이하면\n+4 배수" }, // 광
            ["an"] = new string[] { "동물애호자", "동물그림이 있는 카드를 플레이하면\n+6배수" }, // 동물
            ["ㅈㄱ광땡"] = new string[] { "광땡", $"광땡을 플레이 하면 +2배수\n광땡을 플레이 할 때마다 +1배수\n(최대 +6배수)\n현재 스택:{services["ㅈㄱ광땡"].Multiple - 2}\n+{services["ㅈㄱ광땡"].Multiple}배수 적용" },
            ["ㅈㄱ끗"] = new string[] { "망통", $"끗을 플레이 하면 +2배수\n끗을 플레이 할 때마다 +1배수\n(최대 +6배수)\n현재 스택:{services["ㅈㄱ끗"].Multiple - 2}\n+{services["ㅈㄱ끗"].Multiple}배수 적용" },
            ["ㅈㄱ독사"] = new string[] { "독사", $"독사 플레이 하면 +2배수\n독사를 플레이 할 때마다 +1배수\n(최대 +6배수)\n현재 스택:{services["ㅈㄱ독사"].Multiple - 2}\n+{services["ㅈㄱ독사"].Multiple}배수 적용" },
            ["ㅈㄱ땡"] = new string[] { "장땡", $"땡 플레이 하면 +2배수\n땡을 플레이 할 때마다 +1배수\n(최대 +6배수)\n현재 스택:{services["ㅈㄱ땡"].Multiple - 2}\n+{services["ㅈㄱ땡"].Multiple}배수 적용" },
            ["ㅈㄱ구삥"] = new string[] { "구삥", $"구삥 플레이 하면 +2배수\n구삥을 플레이 할 때마다 +1배수\n(최대 +6배수)\n현재 스택:{services["ㅈㄱ구삥"].Multiple - 2}\n+{services["ㅈㄱ구삥"].Multiple}배수 적용" },
            ["ㅈㄱ장삥"] = new string[] { "장삥", $"장삥 플레이 하면 +2배수\n장삥을 플레이 할 때마다 +1배수\n(최대 +6배수)\n현재 스택:{services["ㅈㄱ장삥"].Multiple - 2}\n+{services["ㅈㄱ장삥"].Multiple}배수 적용" },
            ["ㅈㄱ세륙"] = new string[] { "세륙", $"세륙 플레이 하면 +2배수\n세륙을 플레이 할 때마다 +1배수\n(최대 +6배수)\n현재 스택:{services["ㅈㄱ세륙"].Multiple - 2}\n+{services["ㅈㄱ세륙"].Multiple}배수 적용" },
            ["ㅈㄱ알리"] = new string[] { "알리", $"알리 플레이 하면 +2배수\n알리를 플레이 할 때마다 +1배수\n(최대 +6배수)\n현재 스택:{services["ㅈㄱ알리"].Multiple - 2}\n+{services["ㅈㄱ알리"].Multiple}배수 적용" },
            ["ㅈㄱ장사"] = new string[] { "장사", $"장사 플레이 하면 +2배수\n장사를 플레이 할 때마다 +1배수\n(최대 +6배수)\n현재 스택:{services["ㅈㄱ장사"].Multiple - 2}\n+{services["ㅈㄱ장사"].Multiple}배수 적용" }, // 족보강화 (성장형)
            ["ㄱㅂ"] = new string[] { "꽃놀이패", "플레이할때\n+4 배수" },
            ["ㅂㄹㄱ"] = new string[] { "쓰레기통", $"버리기 할 때 마다 +4 칩\n현재 스택:{GameManager.discardStack}\n\n+{4 * GameManager.discardStack}칩 적용" }, // 성장형
            ["coin"] = new string[] { "저축", "코인을 5개 보유 할 때마다 +1 배수\n(최대 +5 배수)" },
            ["ㅈㅂㅁㅅㅌ"] = new string[] { "족보마스터", "모든 족보를 2회씩 플레이하면\nx7 배수" },
            ["ㅆㅇ"] = new string[] { $"쓰레기통의 왕", $"버리기 횟수가 12회가 되면 x4 배수\n현재 스택:{GameManager.discardStack_s}" }, // 성장형
            ["ㄸㄱㅇ"] = new string[] { "난 똑같은 게 좋아", "같은 카드가 6장 이상이면\nx5 배수" }, // 쓰레기의 왕
            ["1s"] = new string[] { "달빛", "플레이할때\n+10 칩" },
            ["2s"] = new string[] { "달빛", "플레이할때\n+10 칩" },
            ["1g"] = new string[] { "햇빛", "플레이할때\n+1 배수" },
            ["2g"] = new string[] { "햇빛", "플레이할때\n+1 배수" }, // 원래 은, 금 한개씩인데 파일이름 처리 때문에
            ["ty"] = new string[] { "아무것도 없는 카드", "족보에 포함안된다.\n플레이할때 +30 칩" }, //강화카드
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
            else if (name == "ㄱㅂ")
            {
                return services[name];
            }
            else if (name == $"ㅈㄱ{check.HandName}")
            {
                etcCardEffect t = services[name].deepCopy();
                services[name].addMultiple();
                return t;
            }
            else if (name == "ㅂㄹㄱ")
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
            else if (name == "ㅈㅂㅁㅅㅌ" && TextManager.instance.stackCheck())
            {
                return services[name];
            }
            else if (name == "ㅆㅇ" && GameManager.discardStack_s >= 12)
            {
                return services[name];
            }
            else if (name == "ㄸㄱㅇ" && same())
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

        foreach(Card card in GameManager.instance.useCard) // 사용한 패, 가진 패 모두 포함
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
