using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfBikeShop
{
    /// <summary>
    /// Bidings.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Bindings : Page
    {
        public Bindings()
        {
            InitializeComponent();

            Car c = new Car {
                Speed = 100,
                Color = Colors.Crimson,
                Driver = new Human {
                    Firstname = "Nick",
                    HasDrivingLicense = true
                }
            };

            this.DataContext = c;

            var carlist = new List<Car>();
            for (int i = 0; i < 10; i++) {
                carlist.Add(new Car
                {
                    Speed = i * 10,
                    Color = Colors.Purple
                }) ;
            }

            IbxCars.DataContext = carlist;

            //stxPanel.DataContext = c;
            //txtSpeed.DataContext = c; //필수
        }

    }
}
