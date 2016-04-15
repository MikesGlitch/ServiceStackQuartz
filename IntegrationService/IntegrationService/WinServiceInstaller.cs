using System.ComponentModel;
using System.Configuration.Install;

namespace IntegrationService
{
    [RunInstaller(true)]
    public partial class WinServiceInstaller : Installer
    {
        public WinServiceInstaller()
        {
            InitializeComponent();
        }
    }
}
