using Caliburn.Micro;
using WPFSmartHomeMonitoringApp.Helpers;

namespace WPFSmartHomeMonitoringApp.ViewModels
{
    public class CustomPopupViewModel : Conductor<object>
    {
        private string brokerIp;
        private string topic;

        public string BrokerIp 
        { 
            get => brokerIp;
            set
            {
                brokerIp = value;
                NotifyOfPropertyChange(() => BrokerIp);
            }
            
        }
        public string Topic 
        { 
            get => topic;
            set
            {
                topic = value;
                NotifyOfPropertyChange(() => Topic);
            }
        }

        public CustomPopupViewModel(string title)
        {
            this.DisplayName = title;

            BrokerIp = "127.0.0.1";
            Topic = "home/device/fakedata";
        }


        public void AcceptClose()
        {
            Commons.BROKERHOST = BrokerIp;
            Commons.PUB_TOPIC = Topic;
            //창닫기
            TryCloseAsync(true);
        }
    }
}
