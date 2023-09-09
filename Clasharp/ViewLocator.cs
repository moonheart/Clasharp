using System;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using Clasharp.ViewModels;

namespace Clasharp
{
    public class ViewLocator : IDataTemplate
    {
        private static Regex _regex = new Regex(@"(?:DesignTime|ViewModels)\.(?:Design)?([^\.]+)ViewModel"); 
        public Control Build(object data)
        {
            var name = _regex.Replace(data.GetType().FullName!, "Views.$1View");
            var type = Type.GetType(name);

            if (type != null)
            {
                try
                {
                    return (Control) Activator.CreateInstance(type)!;
                }
                catch (Exception e)
                {
                    ExceptionHandler.HandleException(e);
                    return new TextBlock {Text = "Error: " + e, TextWrapping = TextWrapping.Wrap};
                }
            }

            return new TextBlock {Text = "Not Found: " + name};
        }

        public bool Match(object data)
        {
            return data is ViewModelBase;
        }
    }
}