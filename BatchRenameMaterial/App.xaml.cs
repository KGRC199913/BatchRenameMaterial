using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BatchRenameMaterial
{
    public enum DuplicateResolveType
    {
        KeepOldName,
        AddNumber
    };
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Logger.Info("Application Exit--");
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Logger.Info("--Application Startup");
        }
    }
}
