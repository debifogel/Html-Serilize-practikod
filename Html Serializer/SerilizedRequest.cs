using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Html_Serializer
{
    internal static class SerilizedRequest
    {
        public static HashSet<HtmlElement> Query(string select,HtmlElement element)
        {
            Selector selector = Selector.BuildSelector(select);

            HashSet<HtmlElement > result = new HashSet<HtmlElement>();
             element.MatchElement(selector, result);
            return result;
        }
    }
}
