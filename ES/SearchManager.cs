using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES
{
    public class SearchManager : ESManager
    {
        private readonly string indexName = "product";
        public string CreateIndex()
        {
            if (Client.IndexExists(indexName).Exists)
                return "bu index ekli";

            var indexResponse = Client.CreateIndex(indexName, c => c
            .Mappings(m => m.Map<Product>(m => m.Properties(props => props
                     .Completion(s => s
                         .Name(p => p.Suggest)
                         .Analyzer("simple")
                         .SearchAnalyzer("simple")
                         .MaxInputLength(30)
                         .PreservePositionIncrements()
                         .PreserveSeparators()
                         .Contexts(c => c.Category(ca => ca.Name("category_url")).Category(ca => ca.Name("image")).Category(ca => ca.Name("price")).Category(ca => ca.Name("language")
                     )))
                     ))));
            return "eklendi";
        }
        public string DeleteIndex(string indexName)
        {
            if (!Client.IndexExists(indexName).Exists)
                return "bu index yok";

            var indexResponse = Client.DeleteIndex(indexName);
            return "silindi";
        }
        public string AddData()
        {
            List<Product> data = new List<Product>();
            Dictionary<string, IEnumerable<string>> sd = new Dictionary<string, IEnumerable<string>>();
            sd.Add("category_url", new List<string>() { "www.defacto.com.tr/kürk", "Kürk Ceket", "www.defacto.com.tr/kürk-ceket" });
            sd.Add("price", new List<string>() { "30.05" });
            sd.Add("image", new List<string>() { "www.defacto.com.tr/kürk" });
            sd.Add("language", new List<string>() { "en", "tr" });
            data.Add(new Product()
            {
                Id = 1,
                Name = "Kürk Ceket Kahverengi",
                Suggest = new CompletionField()
                {
                    Weight = 13,
                    Contexts = sd
                ,
                    Input = new List<string>() { "Kürk Ceket Kahverengi", "kürk", "ceket", "kahverengi" }
                }

            });
            sd = new Dictionary<string, IEnumerable<string>>();
            sd.Add("category_url", new List<string>() { "www.defacto.com.tr/deri", "Deri Ceket", "www.defacto.com.tr/deri-ceket" });
            sd.Add("price", new List<string>() { "99.85" });
            sd.Add("image", new List<string>() { "www.defacto.com.tr/deri" });
            sd.Add("language", new List<string>() { "tr" });
            data.Add(new Product()
            {
                Id = 2,
                Name = "Deri Ceket Kahverengi",
                Suggest = new CompletionField()
                {
                    Weight = 14,
                    Contexts = sd
                ,
                    Input = new List<string>() { "Deri Ceket Kahverengi", "Ceket", "Deri", "Kahverengi" }
                }

            });
            sd = new Dictionary<string, IEnumerable<string>>();
            sd.Add("category_url", new List<string>() { "www.defacto.com.tr/Blazzer", "Blazzer Ceket", "www.defacto.com.tr/Blazzer-ceket" });
            sd.Add("price", new List<string>() { "57.78" });
            sd.Add("image", new List<string>() { "www.defacto.com.tr/Blazzer" });
            sd.Add("language", new List<string>() { "en" });
            data.Add(new Product()
            {
                Id = 3,
                Name = "Blazzer Ceket Siyah",
                Suggest = new CompletionField()
                {
                    Weight = 15,
                    Contexts = sd
                ,
                    Input = new List<string>() { "Blazzer Ceket Siyah", "Ceket", "Blazzer", "Siyah" }
                }

            });
            sd = new Dictionary<string, IEnumerable<string>>();
            sd.Add("category_url", new List<string>() { "www.defacto.com.tr/Kot", "Kot Ceket", "www.defacto.com.tr/Kot-ceket" });
            sd.Add("price", new List<string>() { "51.12" });
            sd.Add("image", new List<string>() { "www.defacto.com.tr/Kot" });
            sd.Add("language", new List<string>() { "en", "tr" });
            data.Add(new Product()
            {
                Id = 4,
                Name = "Kot Ceket Mavi",
                Suggest = new CompletionField()
                {
                    Weight = 11,
                    Contexts = sd
                ,
                    Input = new List<string>() { "Kot Ceket Mavi", "Ceket", "Kot", "Mavi" }
                }

            });
            sd = new Dictionary<string, IEnumerable<string>>();
            sd.Add("category_url", new List<string>() { "www.defacto.com.tr/Mont", "Erkek Mont", "www.defacto.com.tr/Erkek-mont" });
            sd.Add("price", new List<string>() { "123.23" });
            sd.Add("image", new List<string>() { "www.defacto.com.tr/Mont" });
            sd.Add("language", new List<string>() { "en" });
            data.Add(new Product()
            {
                Id = 5,
                Name = "Erkek Mont Mavi",
                Suggest = new CompletionField()
                {
                    Weight = 23,
                    Contexts = sd
                ,
                    Input = new List<string>() { "Erkek Mont Mavi", "Mont", "Erkek", "Mavi" }
                }

            });
            sd = new Dictionary<string, IEnumerable<string>>();
            sd.Add("category_url", new List<string>() { "www.defacto.com.tr/Mont", "Kadın Mont", "www.defacto.com.tr/Kadın-mont" });
            sd.Add("price", new List<string>() { "112.23" });
            sd.Add("image", new List<string>() { "www.defacto.com.tr/Mont" });
            sd.Add("language", new List<string>() { "en", "tr" });
            data.Add(new Product()
            {
                Id = 6,
                Name = "Kadın Mont Mavi",
                Suggest = new CompletionField()
                {
                    Weight = 21,
                    Contexts = sd
                ,
                    Input = new List<string>() { "Kadın Mont Mavi", "Mont", "Kadın", "Mavi" }
                }

            });
            foreach (var item in data)
            {
                Client.Index(item, i => i
                       .Index(indexName)
                       .Type("product")
                       .Id(item.Id)
                       //.Refresh()
                       );
            }

            return "eklendi";
        }
        public List<SuggestRespnse> NativeSuggest(string term, string language)
        {
            var response2 = Client.Search<Product>(s => s
            .Index(indexName)
            .Suggest(
                su => su
            .Completion("suggest", cs => cs
                .Field(f => f.Suggest)
                .Prefix(term)
                .Contexts(c => c.Context("language", con => con.Context(language)))
                //elastic kendi beceresine göre benzerlik göstereni getiyor ona göre score veriyor.
                .Fuzzy(f => f
                    .Fuzziness(Fuzziness.EditDistance(1))
                )
                .Size(10)
                .Analyzer("simple")
            )
        )
      );
            var suggestions =
                from suggest in response2.Suggest["suggest"]
                from option in suggest.Options
                select new
                {
                    Id = option.Source.Id,
                    Text = option.Source.Name,
                    Score = option.Score,
                    SearchTerm = option.Text,
                    Context = option.Source.Suggest.Contexts,
                    Weight = option.Source.Suggest.Weight
                };
            List<SuggestRespnse> response = new List<SuggestRespnse>();
            foreach (var item in suggestions)
            {
                SuggestRespnse resp = new SuggestRespnse();
                resp.Id = item.Id;
                resp.Score = item.Score;
                resp.SearchTerm = item.SearchTerm;
                resp.Text = item.Text;
                resp.Context = item.Context;
                resp.Weight = (int)item.Weight;
                response.Add(resp);
            }
            return response;
        }
        public class SuggestRespnse
        {
            public Nest.Id Id { get; set; }
            public string Text { get; set; }
            public double Score { get; set; }
            public int Weight { get; set; }
            public string SearchTerm { get; set; }
            public System.Collections.Generic.IDictionary<string, System.Collections.Generic.IEnumerable<string>> Context { get; set; }
        }
        public class Product
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public string Name { get; set; }
            public CompletionField Suggest { get; set; }
        }
    }
}
