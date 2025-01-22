using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Html_Serializer
{
    internal class HtmlHelper
    {
        private readonly static HtmlHelper _instance=new HtmlHelper();
        public static HtmlHelper Instance => _instance;
        public List<string> selectors {  get; set; }
       public List<string> Singleselectors {  get; set; }
        private HtmlHelper()
        {
            
            string jsonString = File.ReadAllText("HtmlTags.json");
             selectors = JsonSerializer.Deserialize<List<string>>(jsonString);
            string  jsonStringB = File.ReadAllText("HtmlVoidTag.json");
            Singleselectors= JsonSerializer.Deserialize<List<string>>(jsonStringB);
        }
    }
}
