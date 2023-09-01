using System.Text;
using System.Text.Json;

namespace OzonContestAug23App;

public class ProblemF
{
    public void Solve()
    {
        var count = int.Parse(Console.ReadLine());
        var outArray = new List<CategoryOutput>();
        var settings = new JsonSerializerOptions()
        {
            MaxDepth = 900
        };

        for (var i = 0; i < count; i++)
        {
            var n = int.Parse(Console.ReadLine());
            var sb = new StringBuilder();
            for (var j = 0; j < n; j++)
            {
                sb.Append(Console.ReadLine());
            }



            var jsonArray = JsonSerializer.Deserialize<List<Category>>(sb.ToString(), settings);

            var output = new CategoryOutput
            {
                id = 0, name = jsonArray.First(x => x.id == 0).name, next = new List<CategoryOutput>()
            };

            CreateCategoryTree(output, jsonArray);
            outArray.Add(output);
        }

        var result = JsonSerializer.Serialize(outArray, settings);
        Console.WriteLine(result);


        void CreateCategoryTree(CategoryOutput parent, IEnumerable<Category> data)
        {
            foreach (var category in data.Where(x => x.parent == parent.id))
            {
                var newCategory = new CategoryOutput
                {
                    id = category.id,
                    name = category.name,
                    next = new List<CategoryOutput>()
                };
                parent.next.Add(newCategory);
                CreateCategoryTree(newCategory, data);
            }
        }
    }

    public class Category
        {
            public int id { get; set; }
            public string name { get; set; }
            public int? parent { get; set; }
        }
        
        public class CategoryOutput
        {
            public int id { get; set; }
            public string name { get; set; }
            public List<CategoryOutput> next { get; set; }
        }
}