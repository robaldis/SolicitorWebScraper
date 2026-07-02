using InfoTrack.Models;

namespace InfoTrack.Parsing;

public class SolicitorParser : ISolicitorParser
{
    public List<Solicitor> Parse(string html, string location)
    {
        var items = new List<Solicitor>();
        var blocks = ExtractResultItemBlocks(html);

        foreach (var block in blocks)
        {
            var solicitor = ParseItem(block, location);
            if (solicitor != null)
                items.Add(solicitor);
        }

        return items;
    }

    private static List<string> ExtractResultItemBlocks(string html)
    {
        var blocks = new List<string>();
        var searchFrom = 0;

        while (searchFrom < html.Length)
        {
            var start = html.IndexOf("<div class=\"result-item", searchFrom, StringComparison.OrdinalIgnoreCase);
            if (start < 0)
                break;

            var tagEnd = html.IndexOf('>', start);
            if (tagEnd < 0)
                break;

            var depth = 1;
            var pos = tagEnd + 1;

            while (depth > 0 && pos < html.Length)
            {
                var nextOpen = html.IndexOf("<div", pos, StringComparison.OrdinalIgnoreCase);
                var nextClose = html.IndexOf("</div>", pos, StringComparison.OrdinalIgnoreCase);

                if (nextClose < 0)
                    break;

                if (nextOpen >= 0 && nextOpen < nextClose)
                {
                    depth++;
                    pos = nextOpen + 4;
                }
                else
                {
                    depth--;
                    pos = nextClose + 6;
                }
            }

            blocks.Add(html.Substring(start, pos - start));
            searchFrom = pos;
        }

        return blocks;
    }

    private static Solicitor? ParseItem(string block, string location)
    {
        var name = ExtractName(block);
        if (string.IsNullOrWhiteSpace(name))
            return null;

        return new Solicitor(
            Name: name,
            Location: location,
            PhoneNumber: ExtractPhone(block),
            Address: ExtractAddress(block),
            ReviewCount: ExtractReviewCount(block),
            Description: ExtractDescription(block),
            WebsiteUrl: ExtractWebsite(block)
        );
    }

    private static string? ExtractName(string block)
    {
        var marker = "<span class=\"h2\">";
        var start = block.IndexOf(marker);
        if (start < 0)
            return null;

        start += marker.Length;
        var end = block.IndexOf('<', start);

        return end > start
            ? DecodeHtml(block[start..end].Trim())
            : null;
    }

    private static string? ExtractPhone(string block)
    {
        var marker = "href=\"tel:";
        var start = block.IndexOf(marker);
        if (start < 0)
            return null;

        start += marker.Length;
        var end = block.IndexOf('"', start);

        return end > start
            ? DecodeHtml(block[start..end].Trim())
            : null;
    }

    private static string? ExtractAddress(string block)
    {
        var marker = "<address>";
        var start = block.IndexOf(marker);
        if (start < 0)
            return null;

        start += marker.Length;
        var end = block.IndexOf("</address>", start);
        if (end < 0)
            return null;

        var raw = block[start..end].Trim();
        return raw.Length > 0 ? DecodeHtml(raw) : null;
    }

    private static string? ExtractDescription(string block)
    {
        var linkMapEnd = block.IndexOf("class=\"link-map\"");
        if (linkMapEnd < 0)
            return null;

        var pStart = block.IndexOf("<p>", linkMapEnd);
        if (pStart < 0)
            return null;

        pStart += 3;
        var pEnd = block.IndexOf("</p>", pStart);
        if (pEnd < 0)
            return null;

        var raw = block[pStart..pEnd].Trim();
        return raw.Length > 0 ? DecodeHtml(raw) : null;
    }

    private static int? ExtractReviewCount(string block)
    {
        var marker = "class=\"rev-results\"";
        var revStart = block.IndexOf(marker);
        if (revStart < 0)
            return null;

        var parenStart = block.IndexOf('(', revStart);
        if (parenStart < 0)
            return null;

        var parenEnd = block.IndexOf(')', parenStart);
        if (parenEnd < 0)
            return null;

        var numStr = block[(parenStart + 1)..parenEnd].Trim();
        if (int.TryParse(numStr,
                System.Globalization.NumberStyles.None,
                System.Globalization.CultureInfo.InvariantCulture,
                out var count))
            return count;

        return null;
    }

    private static string? ExtractWebsite(string block)
    {
        var listMarker = "<ul class=\"list-item\">";
        var ulStart = block.IndexOf(listMarker);
        if (ulStart < 0)
            return null;

        var ulEnd = block.IndexOf("</ul>", ulStart);
        if (ulEnd < 0)
            return null;

        var listContent = block[ulStart..ulEnd];
        var hrefMarker = "<a target=\"_blank\" href=\"";
        var pos = 0;

        while (pos < listContent.Length)
        {
            var aStart = listContent.IndexOf(hrefMarker, pos, StringComparison.OrdinalIgnoreCase);
            if (aStart < 0)
                break;

            aStart += hrefMarker.Length;
            var aEnd = listContent.IndexOf('"', aStart);
            if (aEnd < 0)
                break;

            var url = listContent[aStart..aEnd].Trim();
            if (url.StartsWith("http"))
                return DecodeHtml(url);

            pos = aEnd + 1;
        }

        return null;
    }

    private static string DecodeHtml(string text)
    {
        return text
            .Replace("&amp;", "&")
            .Replace("&lt;", "<")
            .Replace("&gt;", ">")
            .Replace("&quot;", "\"")
            .Replace("&nbsp;", " ")
            .Replace("&#39;", "'")
            .Replace("&mdash;", "\u2014");
    }
}
