namespace Shared.DataTransferObject.People;

public record PersonDto
{
    public int id { get; init; }
    public string name { get; init; } = string.Empty;
    public string lastname { get; init; } = string.Empty;
    public string zipcode { get; init; } = string.Empty;
    public string city { get; init; } = string.Empty;
    public string color { get; init; } = string.Empty;
}