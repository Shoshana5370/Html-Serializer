namespace HtmlSerializer
{
    public static class Extension
    {
        public static HashSet<HtmlElement> FindElementsBySelector(this HtmlElement element, Selector selector)
        {
            HashSet<HtmlElement> result = new HashSet<HtmlElement>();
            FindElementsBySelectorRecursive(element, selector, result);
            return result;
        }

        private static void FindElementsBySelectorRecursive(HtmlElement element, Selector selector, HashSet<HtmlElement> result)
        {
            var descendants = element.Descendants();
            var matchingElements = descendants.Where(e => MatchesSelector(e, selector)).ToList();
            foreach (var matchingElement in matchingElements)
            {
                result.Add(matchingElement);
            }
            if (selector.Child != null)
            {
                foreach (var matchingElement in matchingElements)
                {
                    FindElementsBySelectorRecursive(matchingElement, selector.Child, result);
                }
            }
        }

        private static bool MatchesSelector(HtmlElement element, Selector selector)
        {
            // בדוק אם האלמנט תואם לסלקטור הנוכחי
            if (!string.IsNullOrEmpty(selector.TagName) && element.Name != selector.TagName)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(selector.Id) && element.Id != selector.Id)
            {
                return false;
            }

            if (selector.Classes.Any() && !selector.Classes.All(c => element.Classes.Contains(c)))
            {
                return false;
            }

            return true;
        }
    }
}

