using InfoTrack.Models;

namespace InfoTrack.Parsing;

public interface ISolicitorParser
{
    List<Solicitor> Parse(string html, string location);
}
