using System.Text.Json.Serialization;
using Soot.Domain.ValueObjects;

namespace Soot.Application.Base;

public interface IMobileNumberModel
{
    public string MobileNumber { get; set; }
    [JsonIgnore]
    public MobileNumber Mobile { get; }
}