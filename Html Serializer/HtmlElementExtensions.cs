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
            if (select.TagName != null&&(select.TagName != element.Name ))
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
            Console.WriteLine(select.TagName);
            if( html == null)
                return;
            if (select == null) 
            {
               matches.Add(html);
                return;
            }
            var elementes= html.Descendants().Where(e=>Match(select,e));
            foreach (var elem in elementes)
            {
                if(select.Children==null)
                {
                    matches.Add(elem);
                }
                else
                {
                    MatchElement(elem, select.Children, matches);
                }
            }
            //foreach (var item in html.Descendants())
            //{
            //    if (Match(select, item))
            //        foreach (var item1 in item.Descendants())
            //        {
            //            MatchElement(item1, select.Children, matches);

            //        }

            //}
        }
    }
}
