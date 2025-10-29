namespace smartcity.org.apis.arch.Models;

public record HealthResponse(string Status, DateTimeOffset Timestamp, string? Details = null);
