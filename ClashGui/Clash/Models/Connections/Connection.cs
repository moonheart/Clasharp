using System;
using System.Collections.Generic;

namespace ClashGui.Models.Connections;

public class Connection
{
    public string Id { get; set; }


    public Metadata Metadata { get; set; }


    public int Upload { get; set; }


    public int Download { get; set; }


    public DateTime Start { get; set; }


    public List<string> Chains { get; set; }


    public string Rule { get; set; }


    public string RulePayload { get; set; }
}