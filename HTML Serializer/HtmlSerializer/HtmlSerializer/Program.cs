
using HtmlSerializer;
using System.Text.RegularExpressions;
var html = await Load("http://hebrewbooks.org/beis");
var cleanHtml = Regex.Replace(html, @"[\r\n]+", "");
var HtmlLInes = new Regex("<(.*?)>").Split(cleanHtml).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
HtmlLInes.RemoveAt(0);
HtmlElement current=new();
for (int i = 0; i < HtmlLInes.Count; i++)
{
    string tag = HtmlLInes[i];
    if ('/' == HtmlLInes[i][0])//up to the parent back
    {
        current = current.Parent;
    }

    if (HtmlHelper.Instance.TagsHtml.Contains(tag))
    {  

        HtmlElement childElement = new();
        childElement.Name = tag;
        current.Parent.Children.Add(childElement);
        if(HtmlHelper.Instance.TagsHtmlUnclosing.Contains(tag))
        {

        }
        else
        {

        }
    }
    else
    {
        current.InnerHtml = HtmlLInes[i];
    }
}


//for(int i = 1; i < htmlLines.Length; i++)
//    {
//    string s = htmlLines[i].Substring(0, htmlLines[i].IndexOf(' '));
//    if (s[0] == '/')
//    {
//        //htmlTag מלא בכל הנתונים
//        //currentElement.Parent.Children.Add(htmlTag);//הוספה בתור ילד לאבא
//        currentElement = currentElement.Parent;//עלייה לאבא
//    }
//    else if (HtmlHelper.Instance.AllHtmlTags.Contains(s))
//    {
//        htmlTag = new HtmlTag();
//        currentElement.Parent.Children.Add(htmlTag);//הוספה בתור ילד לאבא

//        htmlTag.Attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(htmlLines[i]);

//        if (HtmlHelper.Instance.HtmlVoidTags.Contains(s))//תגית יחידה
//        {
//            //img src="learn/assets/images/quote.png" alt=""


//            currentElement.Parent.Children.Add(htmlTag);//הוספה בתור ילד לאבא
//            currentElement = currentElement.Parent;//עלייה לאבא
//        }
//        else
//        {

//        }
//    }
//    else
//    {
//        // אני לא תגית - חייב להיות תוכן
//        htmlTag.InnerHtml += htmlLines[i];
//    }
//var Atrribute=new Regex("([^\\s]*?)=\"(.*?)\"").Matches(html);////htmlElement
Console.WriteLine( "jnubjvgf");
    async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}
