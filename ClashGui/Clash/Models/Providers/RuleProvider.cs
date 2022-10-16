using System;

namespace ClashGui.Clash.Models.Providers;

public class RuleProvider
{
    public string Behavior { get; set; }
    public string Name { get; set; }
    public int RuleCount { get; set; }
    public string Type { get; set; }
    public DateTime UpdatedAt { get; set; }
    public VehicleType VehicleType { get; set; }
}