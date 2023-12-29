using System.Text.Json.Serialization;

namespace TechTask.AA.Core.Common;

/// <summary>
/// Represent whether flight InTime, Delayed or Cancelled
/// </summary>
/// 
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FlightStatus : byte
{
    InTime,
    Delayed,
    Cancelled
}
