namespace OzonContestAug23App;

public class ProblemG
{
    public void Solve()
    {
        //черновое решение, проходит все тесты

        var count = int.Parse(Console.ReadLine());
        
        var helper = new Dictionary<char,int>
        {
            {'2', 2}, {'3', 3},  {'4', 4},  {'5', 5},  {'6', 6},  {'7', 7}, {  '8', 8}, 
            {'9', 9}, {'T', 10}, {'J', 11}, {'Q', 12}, {'K', 13}, {'A', 14}
        };
        
        for (var i = 0; i < count; i++)
        {
            var deck = new Dictionary<char, List<char>>
            {
                {'2', new List<char>{'S', 'C', 'D', 'H'}},
                {'3', new List<char>{'S', 'C', 'D', 'H'}},
                {'4', new List<char>{'S', 'C', 'D', 'H'}},
                {'5', new List<char>{'S', 'C', 'D', 'H'}},
                {'6', new List<char>{'S', 'C', 'D', 'H'}},
                {'7', new List<char>{'S', 'C', 'D', 'H'}},
                {'8', new List<char>{'S', 'C', 'D', 'H'}},
                {'9', new List<char>{'S', 'C', 'D', 'H'}},
                {'T', new List<char>{'S', 'C', 'D', 'H'}},
                {'J', new List<char>{'S', 'C', 'D', 'H'}},
                {'Q', new List<char>{'S', 'C', 'D', 'H'}},
                {'K', new List<char>{'S', 'C', 'D', 'H'}},
                {'A', new List<char>{'S', 'C', 'D', 'H'}}
            };
            var result = new Dictionary<string, int>();
            var playersCount = int.Parse(Console.ReadLine());
            //создаем словарь со всеми комбинациями в зависимости от карт первого игрока
            var firstPlayer = Console.ReadLine().Split(' ');
            var firstPlayerHasPair = firstPlayer[0][0] == firstPlayer[1][0];
            var firstPlayerFirstCardValue = helper[firstPlayer[0][0]];
            var firstPlayerSecondCardValue = helper[firstPlayer[1][0]];
            var firstPlayerHightestCard = Math.Max(firstPlayerFirstCardValue, firstPlayerSecondCardValue);
            var isHightest = true;
            var canWinWithPairOrHightest = true;
            deck[firstPlayer[0][0]].Remove(firstPlayer[0][1]);
            deck[firstPlayer[1][0]].Remove(firstPlayer[1][1]);
            foreach (var elem in deck)
            {
                //тройки
                if (firstPlayerHasPair && elem.Key == firstPlayer[0][0])
                {
                    foreach (var val in elem.Value)     
                    {
                        result.Add($"{firstPlayer[0][0]}{val}", 3);
                    }
                    continue;
                }
                
                //пары
                if (elem.Key == firstPlayer[0][0])
                {
                    foreach (var val in elem.Value)
                    {
                        result.Add($"{firstPlayer[0][0]}{val}", 2);
                    }
                    continue;
                }
        
                if (elem.Key == firstPlayer[1][0])
                {
                    foreach (var val in elem.Value)
                    {
                        result.Add($"{firstPlayer[1][0]}{val}", 1);
                    }
                    continue;
                }
                
                //старшая
                foreach (var val in elem.Value)
                {
                    result.Add($"{elem.Key}{val}", 0);
                }
            }
            
            //остальные игроки
            for (var j = 1; j < playersCount; j++)
            {
                var cur = Console.ReadLine().Split(' ');
                var keysToRemove = new HashSet<string>();
                result.Remove(cur[0]);
                result.Remove(cur[1]);
                var curFirstCard = helper[cur[0][0]];
                var curSecondCard = helper[cur[1][0]];
                var curPlayerHasPair = cur[0][0] == cur[1][0];
                var curBest = Math.Max(curFirstCard, curSecondCard);
                if (result.TryGetValue(cur[0], out _))
                {
                    result.Remove(cur[0]);
                }
                
                if (result.TryGetValue(cur[1], out _))
                {
                    result.Remove(cur[1]);
                }
                
                if (curPlayerHasPair)
                {
                    if (!firstPlayerHasPair || (firstPlayerHasPair && firstPlayerFirstCardValue < curFirstCard))
                    {
                        foreach (var pair in result.Where(x => x.Value == 0))
                        {
                            keysToRemove.Add(pair.Key);
                        }
                    }
                    foreach (var pair in result.Where(pair => pair.Key[0] == cur[0][0]))
                    {
                        keysToRemove.Add(pair.Key);
                    }
                    if (firstPlayerFirstCardValue < curFirstCard &&
                        firstPlayerSecondCardValue < curSecondCard)
                    {
                        foreach (var pair in result.Where(x => x.Value < 3))
                        {
                            keysToRemove.Add(pair.Key);
                        }
                    }
                    if (firstPlayerFirstCardValue < curFirstCard)
                    {
                        foreach (var pair in result.Where(x => x.Value is 2))
                        {
                            keysToRemove.Add(pair.Key);
                        }
                    }
                    if (firstPlayerSecondCardValue < curFirstCard)
                    {
                        foreach (var pair in result.Where(x => x.Value is 1))
                        {
                            keysToRemove.Add(pair.Key);
                        }
                    }
        
                    if (firstPlayerHasPair && firstPlayerFirstCardValue < curFirstCard)
                    {
                        foreach (var pair in result.Where(x => x.Value is 0))
                        {
                            keysToRemove.Add(pair.Key);
                        }
                    }
                }
                else
                {
                    //выбираем карты дающие пару текущему игроку но не первому
                    foreach (var pair in result.Where(x =>
                                 x.Value == 0 && (x.Key[0] == cur[0][0] || x.Key[0] == cur[1][0])))
                    {
                        if ((firstPlayerHasPair && firstPlayerFirstCardValue < helper[pair.Key[0]]) || !firstPlayerHasPair)
                        {
                            keysToRemove.Add(pair.Key);
                        }
                    }
                    //3. удаляем все комбинации дающие страшую карту до текущей старшей карты игрока, если у первого игрока нет карты старше
                    //!!!!!вообще это максимально странное условие, т.к. на тесте вида: "2S 7H","QC 4D","3D 7S","KH 4H" правильное решение (по тестам):
                    //"2C","2D","2H","7C","7D","AS","AC","AD","AH", т.е. считается, что 7H + AS == KH + AS и т.д., что несовсем правильно
                    //т.к. это нам говорит: если в колоде есть карта старше всех карт на руках, то побеждают все
                    if (firstPlayerHightestCard < curBest && !firstPlayerHasPair)
                    {
                        foreach (var pair in result.Where(x => helper[x.Key[0]] < curBest && x.Value == 0))
                        {
                            keysToRemove.Add(pair.Key);
                        }
                    }
                }
                
                foreach (var key in keysToRemove)
                {
                    result.Remove(key);
                }
            }
            if (result.Count == 0)
                Console.WriteLine(0);
            else
            {
                Console.WriteLine(result.Count);
                foreach (var val in result)
                {
                    Console.WriteLine(val.Key);
                }
            }
        }

    }
}