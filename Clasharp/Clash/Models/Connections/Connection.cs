using System;
using System.Collections.Generic;

namespace Clasharp.Clash.Models.Connections;

public class Connection
{
    public string Id { get; set; } = "";


    public Metadata Metadata { get; set; } = new();


    public long Upload { get; set; }


    public long Download { get; set; }


    public DateTime Start { get; set; }


    public List<string> Chains { get; set; } = new();


    public string Rule { get; set; } = "";


    public string RulePayload { get; set; } = "";
}