using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Html_Serializer
{
    internal static class HtmlTree
    {
        static string IsTagit(string s)
        {
            string s2 = s.Split(" ")[0];
            foreach (var item in HtmlHelper.Instance.selectors)
            {
                if (s2 == item)
                    return item;
            }
            foreach (var item in HtmlHelper.Instance.Singleselectors)
            {
                if (s2 == item)
                    return item;
            }
            return null;
        }

        public static async Task<HtmlElement> BuildTree(IEnumerable<string> htmllines)
        {

            HtmlElement htmlRoot = new HtmlElement();
            HtmlElement htmlCurrent = htmlRoot;
            HtmlElement temp;
            bool isSign = false;
            //
            foreach (var line in htmllines)               
            {
                
                if (!isSign)
                {
                    string tagit = IsTagit(line);

                    if (line.StartsWith("!--"))
                    {
                        isSign = true;
                    }
                    
                    else if (line.StartsWith("/html"))
                        return htmlRoot.childrens[0];

                    else if (line[0]=='/')
                    {
                        htmlCurrent = htmlCurrent.Parent;
                    }
                    else if (tagit != null)
                    {
                        temp = new HtmlElement() { Name = tagit, Parent = htmlCurrent };
                        htmlCurrent.childrens.Add(temp);

                        //string word = line.Replace(tagit, "");

                        var attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(line).ToList();
                        foreach (var match in attributes)
                        {
                            string name = match.Groups[1].Value; // Accessing attribute name
                            string value = match.Groups[2].Value; // Accessing attribute value
                            if (name == "id")
                                temp.Id = value;
                            else if (name == "class")
                                temp.Classes.AddRange(value.Split(' '));
                            else
                                temp.Attributes.Add(name + "=" + value);
                        }
                        if (!(HtmlHelper.Instance.Singleselectors.Contains(temp.Name)
                            || line.EndsWith('/'))||
                            temp.Name=="style")
                        {
                            htmlCurrent = temp;
                        }
                       
                    }
                    else
                    {
                        htmlCurrent.InnerHtml += line;
                    }
                }
                else
                {
                    htmlCurrent.InnerHtml += line;
                    if (line.EndsWith('>'))
                        isSign = false;
                }
            }
            return htmlRoot.childrens[0];
        }
    }
}
