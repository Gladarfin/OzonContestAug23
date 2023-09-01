namespace OzonContestAug23App;

public class ProblemH
{
    public void Solve()
    {
        var input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        var n = input[0];
        var k = input[1];
        var wrongIps = 0;
        var dict = new Dictionary<string, List<string>>();
        var result = new List<string>();
        for (var i = 0; i < n; i++)
        {
            var addr = Console.ReadLine();
            var mask = addr[..addr.LastIndexOf('.')];
            if (dict.TryGetValue(mask, out var value))
            {
                value.Add(addr);
                continue;
            }
            dict.Add(mask, new List<string>{addr});
        }
        
        if (k >= n)
        {
            Console.WriteLine(wrongIps);
            Console.WriteLine(n);
            foreach (var val in dict.SelectMany(pair => pair.Value))
            {
                Console.WriteLine(val);
            }
        }
        
        else if (dict.Count > k)
        {
            wrongIps = 65536 - dict.Sum(pair => pair.Value.Count);
            Console.WriteLine(wrongIps);
            Console.WriteLine(1);
            Console.WriteLine("100.200.0.0/16");
        }
        else
        {
            var sortedIPs = dict.OrderByDescending(x => x.Value.Count)
                .ToDictionary(x => x.Key, x => x.Value);
            
            var curFreeStrings = k;
            var leftIPs = n;
            foreach (var item in sortedIPs)
            {
                if (curFreeStrings < leftIPs)
                {
                    curFreeStrings--;
                    wrongIps += 256 - item.Value.Count;
                    leftIPs -= item.Value.Count;
                    result.Add($"100.200.{item.Key.Split('.')[2]}.0/24");
                    continue;
                }
        
                foreach (var val in item.Value)
                {
                    leftIPs--;
                    curFreeStrings--;
                    var cur = val.Split('.');
                    result.Add($"100.200.{cur[2]}.{cur[3]}");
                }
            }
            Console.WriteLine(wrongIps);
            Console.WriteLine(result.Count);
            foreach (var val in result)
            {
                Console.WriteLine(val);
            }
        }
    }
}