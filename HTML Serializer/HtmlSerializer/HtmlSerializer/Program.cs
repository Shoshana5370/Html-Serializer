using HtmlSerializer;
using System.Text.RegularExpressions;
//static List<string> func2(string input)
//{
//    string noOtherSpaces = Regex.Replace(input, @"[^\S ]+", "");
//    string cleanString = Regex.Replace(noOtherSpaces, @" {2,}", " ");
//    List<string> htmlLines = new Regex("<(.*?)>").Split(cleanString).Where(s => s.Length > 0).ToList();
//    List<string> listWithoutEmptyLines = new List<string>();
//    foreach (var line in htmlLines)
//        if (line != " ")
//            listWithoutEmptyLines.Add(line);
//    return listWithoutEmptyLines;
//}
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

        if ('/' == lines[i][0])//up to the parent back
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
var html = await Load("https://chani-k.co.il/sherlok-game/");
var cleanHtml = Regex.Replace(html, @"[\r\n]+", "");
var linesHtml = new Regex("<(.*?)>").Split(cleanHtml).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
linesHtml.RemoveAt(0);
HtmlElement tree=BuildTree(linesHtml);
Selector selector = Selector.CastQueryToSelector("body div#copyright div.copyR");
var resualt=tree.FindElementsBySelector(selector);




//Selector s = Selector.CastQueryToSelector("div p#p1 .class1");
//Selector s1 = Selector.CastQueryToSelector("div.claasss p.ckj#p1 .class1 p");
//Selector s2 = Selector.CastQueryToSelector("div#nydiv.c1");
//Selector s3 = Selector.CastQueryToSelector("div #nydiv .c1");
//Console.WriteLine();


//var cleanHtml = new Regex("\\t|\\r|\\n").Replace(html, " ");
//var lines = Regex.Matches(cleanHtml, @"<\/?([A-Za-z][A-Za-z0-9]*)\b[^>]*>|([^<]+)");
//List<string> htmlLines = new List<string>();
//foreach (var line in lines)
//{
//    string l = line.ToString();
//    l = l.Replace('<', ' ');
//    l = l.Replace('>', ' ');
//    l = l.Trim();
//    if (!string.IsNullOrWhiteSpace(l))
//        htmlLines.Add(l);
//}
