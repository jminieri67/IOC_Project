using IOC.Interfaces;
using IOC.WpfApp.Interfaces;
using System.Windows;

namespace IOC.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Container _myContainer;

        public MainWindow()
        {
            InitializeComponent();

            _myContainer = new Container("MyContainer");

            _myContainer.Register<ILogger, Logger>(LifecycleType.Singleton);
            _myContainer.Register<ICalculator, Calculator>();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Calculator calculator = (Calculator)_myContainer.Resolve<ICalculator>();
            calculator.Add(1, 2);
        }
    }
}
