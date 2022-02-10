using IOC.Controls;
using IOC.Controls.Interfaces;
using System;
using System.Windows;

namespace IOC.WpfApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
    {
        private Container _container;

        public MainWindow()
        {
            InitializeComponent();

            _container = new Container("MyContainer");

            _container.Register<ILogger, Logger>(LifecycleType.Singleton);
            _container.Register<ICalculator, Calculator>();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Calculator calculator = (Calculator)_container.Resolve<ICalculator>();
            int num1 = 2;
            int num2 = 13;
            int result = calculator.Add(num1, num2);

            tbResult.Text = $"{num1} + {num2} = {result}";
        }

		private void btnException_Click(object sender, RoutedEventArgs e)
		{
			try
			{
                EmailService emailService = (EmailService)_container.Resolve<IEmailService>();
            }
            catch(Exception ex)
			{
                tbResult.Text = ex.Message;
			}
		}
	}
}
