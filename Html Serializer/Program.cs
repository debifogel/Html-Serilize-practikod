
using Html_Serializer;
using System.Text.RegularExpressions;



static async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}

var html = await Load("https://learn.malkabruk.co.il/practicode/projects/pract-2/");
var cleanHtml = new Regex("\\s+").Replace(html," ");
var htmllines = new Regex("<(.*?)>").Split(cleanHtml).Where(s => s.Length > 1);
//build the tree
var  htmlElement =await  HtmlTree.BuildTree(htmllines);

string select= "div.language-text.highlight";//init selector

var result= SerilizedRequest.Query(select, htmlElement);//search the element

foreach (var item in result)
{
    Console.WriteLine( item);
}

//Console.ReadLine();   
