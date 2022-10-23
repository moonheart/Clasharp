using System;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using ClashGui.ViewModels;

namespace ClashGui
{
    public class ViewLocator : IDataTemplate
    {
        private static Regex _regex = new Regex(@"(?:DesignTime|ViewModels)\.(?:Design)?([^\.]+)ViewModel"); 
        public IControl Build(object data)
        {
            var name = _regex.Replace(data.GetType().FullName!, "Views.$1View");
            var type = Type.GetType(name);

            if (type != null)
            {
                return (Control) Activator.CreateInstance(type)!;
            }

            return new TextBlock {Text = "Not Found: " + name};
        }

        public bool Match(object data)
        {
            return data is ViewModelBase;
        }
    }
}