using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Clasharp.Clash.Models.Proxies;
using Clasharp.Interfaces;
using Clasharp.ViewModels;

namespace Clasharp.DesignTime;

public class DesignProxyGroupListViewModel : ViewModelBase, IProxyGroupListViewModel
{
    public DesignProxyGroupListViewModel()
    {
        ProxyGroupViewModels = new ReadOnlyObservableCollection<ProxyGroupModel>(
            new ObservableCollection<ProxyGroupModel>(new[]
            {
                new ProxyGroupModel(new ProxyGroup()
                {
                    Name = "name", All = new List<string>() {"12", "345"}, Type = ProxyGroupType.LoadBalance, Udp = true
                }, (s, s1) => Task.CompletedTask)
            }));
    }

    public ReadOnlyObservableCollection<ProxyGroupModel> ProxyGroupViewModels { get; }
}