using Caliburn.Micro;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace DesktopInterface.ViewModels
{
    internal class ShellViewModel : Conductor<object>
    {
        private string _applicationVersion = string.Empty;
        private string _applicationVersionStage = string.Empty;

        public string ApplicationVersion
        {
            get { return _applicationVersion; }
            set
            {
                _applicationVersion = value;
                NotifyOfPropertyChange(() => ApplicationVersion);
            }
        }

        public string ApplicationVersionStage
        {
            get { return _applicationVersionStage; }
            set
            {
                _applicationVersionStage = value;
                NotifyOfPropertyChange(() => ApplicationVersionStage);
            }
        }

        public ShellViewModel(IConfiguration config)
        {
            _applicationVersion = config["app-version"] ?? string.Empty;

            if (Regex.Match(_applicationVersion, "^\\d+.\\d+.\\d+$").Success)
            {
                ApplicationVersionStage = "stable";
            }
            else
            {
                ApplicationVersionStage = "[PRE-RELEASE]";
            }

            ActivateItemAsync(IoC.Get<BookcaseViewModel>());
        }
    }
}
