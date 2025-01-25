using HtmlSerializer;
using System.Text.RegularExpressions;

static HtmlElement BuildTree(List<string> lines)
{
    HtmlElement root = new();
    HtmlElement current = new()
    {
        Parent = root
    };
    for (int i = 0; i < lines.Count && lines[i] != "/html"; i++)//build tree;
    {
        string tag = lines[i];
        if (lines[i].Contains(' '))
        {
            tag = tag[..tag.IndexOf(' ')];
        }

        if ('/' == lines[i][0])
        {
            current = current.Parent;
        }

        else if (HtmlHelper.Helper.TagsHtml.Contains(tag))
        {

            HtmlElement childElement = new();
            childElement.Name = tag;
            childElement.Parent = current;
            childElement.Attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(lines[i]).Cast<Match>()
                .ToDictionary(m => m.Groups[1].Value, m => m.Groups[2].Value);
            if (childElement.Attributes.ContainsKey("id"))
                childElement.Id = childElement.Attributes["id"];
            if (childElement.Attributes.ContainsKey("class"))
            {
                childElement.Classes = childElement.Attributes["class"].Split(" ").ToList();
            }
            current.Children.Add(childElement);
            if (!HtmlHelper.Helper.TagsHtmlUnclosing.Contains(tag))
            {
                current = childElement;
            }
        }
        else
        {
            current.InnerHtml = lines[i];
        }
    }
    return current;
}
static async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}
var html = await Load("https://hebrewbooks.org");
var cleanHtml = Regex.Replace(html, @"[\r\n]+", "");
var linesHtml = new Regex("<(.*?)>").Split(cleanHtml).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
linesHtml.RemoveAt(0);
HtmlElement tree=BuildTree(linesHtml);
Selector selector = Selector.CastQueryToSelector("div img");
var resualt = tree.FindElementsBySelector(selector);
resualt.ToList().ForEach(i=> Console.WriteLine(i));
Console.WriteLine("--------------");
