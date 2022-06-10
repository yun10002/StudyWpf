using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFSmartHomeMonitoringApp.ViewModels
{
    public class MainViewModel :    Conductor<object> //screen에는 Activateitem이라는 메서드 없음
    {
        public MainViewModel()
        {
            DisplayName = "SmartHomeMonitoring v2.0"; //윈도우 타이틀
        }

        public void LoadDataBaseView()
        {
            ActivateItemAsync(new DataBaseViewModel()); 
        }

        public void LoadRealTimeView()
        {
            ActivateItemAsync(new RealTimeViewModel());
        }

        public void LoadHistoryView()
        {
            ActivateItemAsync(new HistoryViewModel());
        }

        public void ExitProgram()
        {
            Environment.Exit(0); //오류없이 종료
        }

    }
}
