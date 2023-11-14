using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace VDJPlaylistExport;

public class ExtVDJ
{
    public int Filesize { get; set; }
    public string Artist { get; set; }
    public string Title { get; set; }
    public string Remix { get; set; }
    public double SongLength { get; set; }

    public ExtVDJ(string xmlString)
    {
        xmlString = $"<root>{xmlString.Replace("&", "&amp;")}</root>";

        XElement element = XElement.Parse(xmlString);
        
        Filesize = int.Parse(element.Element("filesize").Value);
        Artist = WebUtility.HtmlDecode(element.Element("artist").Value);
        Title = WebUtility.HtmlDecode(element.Element("title").Value);
        Remix = WebUtility.HtmlDecode(element.Element("remix")?.Value ?? string.Empty);
        SongLength = double.Parse(element.Element("songlength").Value);
    }

    public string GetSafeFilename()
    {
        string fname = ToString();

        var invalidCharsPattern = $"[{Path.GetInvalidFileNameChars()}]";
        var safeFileName = Regex.Replace(fname, invalidCharsPattern, "_");

        return safeFileName;
    }

    public override string ToString()
    {
        string fname = null;
        if (Title.ToLower().Contains(Remix.ToLower()))
            fname = $"{Artist}-{Title}";
        else
            fname = $"{Artist}-{Title}({Remix})";

        return fname;
    }
}