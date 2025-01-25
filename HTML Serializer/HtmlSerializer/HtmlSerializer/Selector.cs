using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace HtmlSerializer;
public class Selector
{
    public string? TagName { get; set; }
    public string? Id { get; set; }
    public List<string> Classes { get; set; } = new List<string>();
    public Selector? Parent { get; set; }
    public Selector Child { get; set; }

    public static Selector CastQueryToSelector(string query)
    {
        Selector root = new();
        Selector current = new Selector()
        {
            Parent = root
        };
        root.Child = current;
        string[] levels = query.Split(' ');
        for (int i=0;i< levels.Length; i++)
        {
            string level = levels[i];
            int dot = level.IndexOf('.');
            int hash = level.IndexOf('#');
            if (hash >= 0 || dot >= 0)
            {
                if(hash < 0)
                {
                    current.TagName = level.Substring(0, dot);
                }
                else if(dot<0)
                {
                    current.TagName = level.Substring(0, hash);
                }
                else
                {
                    current.TagName = level.Substring(0, Math.Min(hash ,dot));
                }
                int j = 0;
                while (j < level.Length)
                {
                    char currentChar = level[j];

                    if (currentChar == '#')
                    {
                        j++;
                        while (j < level.Length && level[j] != '.' && level[j] != ' ')
                        {
                            current.Id += level[j];
                            j++;
                        }
                    }
                    else if (currentChar == '.')
                    {
                        j++;
                        string className = string.Empty;
                        while (j < level.Length && level[j] != '.' && level[j] != '#' && level[j] != ' ')
                        {
                            className += level[j];
                            j++;
                        }
                        current.Classes.Add(className);
                    }
                    else
                    {
                        j++; // Move to the next character
                    }
                }
            }
        
            else
            {
                current.TagName = level;
            }
            if(i!=levels.Length-1)

        {
                current.Child = new Selector
                {
                    Parent = current
                };
            }
            current = current.Child;
        }
        
        current = root.Child;
        current.Parent = null;
        return current;
    }
}


