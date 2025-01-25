using System.Runtime.CompilerServices;

namespace HtmlSerializer
{
    public class HtmlElement
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public Dictionary<string,string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string? InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }

        public HtmlElement()
        {
            Classes = new();
            Children = new();
            Attributes = new();
        }
        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                HtmlElement current = queue.Dequeue(); 
                yield return current;
                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }
        
        public IEnumerable<HtmlElement> Ancestors(HtmlElement e)
        {
            while(e.Parent!=null)
            {
                yield return e.Parent;
            }
        }

        public override string ToString()
        {
            // Building a string representation of the HtmlElement
            var classString = Classes.Count > 0 ? string.Join(" ", Classes) : "No Classes";
            var attributesString = Attributes.Count > 0
                ? string.Join(", ", Attributes.Select(a => $"{a.Key}=\"{a.Value}\""))
                : "No Attributes";

            return $"Element: {Name}, Id: {Id}, Classes: [{classString}], Attributes: {{{attributesString}}}, InnerHtml: {InnerHtml}";
        }

    }
}
