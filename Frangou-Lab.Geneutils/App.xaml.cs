using System.Windows;
using Prism;

namespace FrangouLab.Geneutils
{
    public partial class App
    {
        private readonly Bootstrapper _bootstrapper = new AppBootstrapper(); 

        protected override void OnStartup(StartupEventArgs eventArguments)
        {
            _bootstrapper.Run();
        }
    }
}
