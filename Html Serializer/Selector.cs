using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Html_Serializer
{
    internal class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public Selector Parent { get; set; }
        public Selector Children { get; set; }
        public Selector(Selector p) { Parent = p; Children = null; }
        Selector() { }
        private static string CutName(string s)
        {
            int t1 = s.IndexOf('.') != -1 ? s.IndexOf('.') : int.MaxValue;
            int t2 = s.IndexOf('#') != -1 ? s.IndexOf('#') : int.MaxValue;
            int t3=Math.Min(t1, t2);
            if(t3 == int.MaxValue)
                t3=s.Length;
           return  s.Substring(0, t3);
        }
        private static string CutId(string s)
        {
            int i1=s.IndexOf("#");
            int i2= s.IndexOf(".",i1);
            if(i2==-1)
                i2=s.Length;
            return s.Substring(i1,Math.Min(i2,s.Length));
        }
      
        public static Selector BuildSelector(string select)
        {
            string helper;
            string[] statements = select.Split(' ');
            Selector root= new Selector();
            Selector current = root;
            foreach (string statement in statements) {
                current=new Selector(current);
                current.Parent.Children = current;
                helper=statement;
                if (!helper.StartsWith('.')&&!helper.StartsWith('#'))
                {
                    string s=CutName(helper);
                    if (HtmlHelper.Instance.Singleselectors.Contains(s) || HtmlHelper.Instance.selectors.Contains(s))
                        current.TagName = s;
                    else
                        throw new Exception("not valid selsct");
                    helper= helper.Replace(s,""); 

                }   
                if(helper.Contains('#'))
                {
                    string s=CutId(helper);
                    current.Id = s;
                    helper=helper.Replace(s, "");
                }
                if (helper.Contains('.'))
                    current.Classes = new List<string>();
                while (helper.Contains('.'))
                {
                    int i2 = helper.IndexOf(".",1);
                    if (i2 == -1)
                        i2 = helper.Length-1;
                    string s= helper.Substring(1, i2-1);
                    current.Classes.Add (s);
                    helper = helper.Substring (i2);
                }

               
            }
            current = root;
            
            current = root.Children;           
            root = null;
            current.Parent = null;
            return current;
        }

    }
}