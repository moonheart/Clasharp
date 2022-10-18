using System.Collections.ObjectModel;
using ClashGui.Clash.Models.Providers;
using ClashGui.Clash.Models.Rules;
using ClashGui.Interfaces;

namespace ClashGui.ViewModels;

public class ProxyRulesListViewModel : ViewModelBase, IProxyRulesListViewModel
{
    public ObservableCollection<RuleInfo> Rules { get; } = new();
    public ObservableCollection<RuleProvider> Providers { get; } = new();
}