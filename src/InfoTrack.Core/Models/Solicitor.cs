namespace InfoTrack.Core.Models;

public record Solicitor(
    string Name,
    string Location,
    string? PhoneNumber,
    string? Address,
    int? ReviewCount,
    string? Description,
    string? WebsiteUrl
);
