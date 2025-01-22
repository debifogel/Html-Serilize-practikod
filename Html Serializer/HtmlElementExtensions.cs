using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Html_Serializer
{
    internal static class HtmlElementExtensions
    {
        
        public static bool Match(Selector select,HtmlElement element)
        {
            //check it
            if (!(select.TagName == element.Name || select.TagName == null))
                return false;
            if (!(select.Id == element.Id || select.Id == null))
                return false;
            if(select.Classes!=null)
            foreach (var item in select.Classes)
            {
                if (!element.Classes.Contains(item)) return false;
            }
            return true;

        }
        public static  void MatchElement(this HtmlElement html,Selector select, HashSet<HtmlElement> matches)
        {
            if (select == null) 
            { matches.Add(html);
                return; 
            }
            if (select.Children == null&& Match(select, html))
            {
                matches.Add(html);
                return;
            }
            if (html == null)
                return;
            foreach (var item in html.Descendants())
            {
                if (Match(select, item))
                    MatchElement(item, select.Children, matches);
            }
        }
    }
}
