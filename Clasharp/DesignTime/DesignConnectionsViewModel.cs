using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.Collections;
using Clasharp.Clash.Models.Connections;
using Clasharp.Interfaces;
using Clasharp.Models.Connections;
using Clasharp.ViewModels;
using ReactiveUI;

namespace Clasharp.DesignTime;

public class DesignConnectionsViewModel : ViewModelBase, IConnectionsViewModel
{
    public override string Name => "Connections";
    public string DownloadTotal { get; set; } = "↓ 123124KB";

    public string UploadTotal { get; set; } = "↑ 123124KB";
    public string DownloadSpeed => "↓ 123124KB/s";
    public string UploadSpeed => "↑ 123124KB/s";

    public ReadOnlyObservableCollection<ConnectionExt> Connections { get; set; } = new(
        new ObservableCollection<ConnectionExt>(new[]
        {
            new ConnectionExt(new Connection
            {
                Chains = new List<string> {"A", "B"}, Download = 123123, Id = Guid.NewGuid().ToString(),
                Rule = "rule", RulePayload = "rulepayload", Start = DateTime.Now, Upload = 3245234,
                Metadata = new()
                {
                    DestinationIP = "1.2.3.4", DestinationPort = "123", SourceIP = "12.3.4.5", SourcePort = "3453",
                    Host = "asd.sdtf.ww", Network = "TCP", Type = "TUN", DnsMode = "dnsmoe", ProcessPath = "/sf/sfd.exe"
                }
            }),
            new ConnectionExt(new Connection
            {
                Chains = new List<string> {"A", "B"}, Download = 123123, Id = Guid.NewGuid().ToString(),
                Rule = "rule", RulePayload = "rulepayload", Start = DateTime.Now, Upload = 3245234,
                Metadata = new()
                {
                    DestinationIP = "1.2.3.4", DestinationPort = "123", SourceIP = "12.3.4.5", SourcePort = "3453",
                    Host = "asd.sdtf.ww", Network = "TCP", Type = "TUN", DnsMode = "dnsmoe", ProcessPath = "/sf/sfd.exe"
                }
            }),
        }));

    public ConnectionExt? SelectedItem { get; set; }
    public ReactiveCommand<string, Unit> CloseConnection { get; } = ReactiveCommand.Create((string _) => { });
    public ReactiveCommand<Unit, Unit> CloseAllConnection { get; } = ReactiveCommand.Create(() => { });
}