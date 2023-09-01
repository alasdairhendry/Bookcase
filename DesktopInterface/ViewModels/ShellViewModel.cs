using Caliburn.Micro;
using Microsoft.Extensions.Configuration;

namespace DesktopInterface.ViewModels
{
    internal class ShellViewModel : Conductor<object>
    {
        private string _applicationVersion;

        public string ApplicationVersion
        {
            get { return $"v{_applicationVersion}"; }
            set
            {
                _applicationVersion = value;
                NotifyOfPropertyChange(() => ApplicationVersion);
            }
        }

        public ShellViewModel(IConfiguration config)
        {
            _applicationVersion = config["app-version"] ?? string.Empty;
        }
    }
}
