using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Html_Serializer
{
    internal class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> childrens { get; set; }
        public HtmlElement()
        {
            Attributes=new List<string>();
            Classes=new List<string>();
            childrens=new List<HtmlElement>();
            Parent = null;
        }
        public override string ToString()
        {
            return "name: " + Name+" Id: "+Id;
        }

        public IEnumerable<HtmlElement> Descendants()
        {
            HtmlElement html = new HtmlElement();
            Queue<HtmlElement>q=new Queue<HtmlElement>();
            q.Enqueue(this);
            while(q.Count > 0)
            {
                html=q.Dequeue();
                if(html.childrens!=null)
                foreach (var child in html.childrens)
                {
                    q.Enqueue(child);
                }
                  yield return html;
            }
        }
        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement html = this;
            while (html!=null)
            {
                yield return html;
                html = html.Parent;
            }
        }
    }
}
