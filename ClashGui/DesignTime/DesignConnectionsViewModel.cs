using System;
using System.Collections.Generic;
using Avalonia.Collections;
using ClashGui.Clash.Models.Connections;
using ClashGui.Interfaces;
using ClashGui.Models.Connections;
using ClashGui.ViewModels;

namespace ClashGui.DesignTime;

public class DesignConnectionsViewModel : ViewModelBase, IConnectionsViewModel
{
    public long DownloadTotal { get; set; } = 123124;

    public long UploadTotal { get; set; } = 4563456;

    public AvaloniaList<ConnectionExt> Connections { get; set; } = new(new[]
    {
        new ConnectionExt
        {
            Connection = new Connection
            {
                Chains = new List<string> {"A", "B"}, Download = 123123, Id = Guid.NewGuid().ToString(),
                Rule = "rule", RulePayload = "rulepayload", Start = DateTime.Now, Upload = 3245234,
                Metadata = new()
                {
                    DestinationIP = "1.2.3.4", DestinationPort = "123", SourceIP = "12.3.4.5", SourcePort = "3453",
                    Host = "asd.sdtf.ww", Network = "TCP", Type = "TUN", DnsMode = "dnsmoe", ProcessPath = "/sf/sfd.exe"
                }
            },
            Download = 45345435,
            Upload = 234534534
        },
        new ConnectionExt
        {
            Connection = new Connection
            {
                Chains = new List<string> {"A", "B"}, Download = 123123, Id = Guid.NewGuid().ToString(),
                Rule = "rule", RulePayload = "rulepayload", Start = DateTime.Now, Upload = 3245234,
                Metadata = new()
                {
                    DestinationIP = "1.2.3.4", DestinationPort = "123", SourceIP = "12.3.4.5", SourcePort = "3453",
                    Host = "asd.sdtf.ww", Network = "TCP", Type = "TUN", DnsMode = "dnsmoe", ProcessPath = "/sf/sfd.exe"
                }
            },
            Download = 45345435,
            Upload = 234534534
        },
    });
}