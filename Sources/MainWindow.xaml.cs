using System;
using System.Collections.Generic;
using System.IO;
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

namespace InterC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ProblemData.load();

            setupLoginScreen();

            exit.MouseDown += (s, e) =>
            {
                Application.Current.Shutdown();
            };
        }

        private void setupLoginScreen()
        {
            // Setting up login screen media element
            MediaElement loginScreenElement = (MediaElement)FindName("login_screen");
            var videoPath = Directory.GetCurrentDirectory();
            Uri uri = new Uri("https://raw.githubusercontent.com/P-ro-VL/InterC/main/config/login_screen.mp4");
            loginScreenElement.Source = uri;
            loginScreenElement.MediaEnded += (s, e) =>
            {
                TimeSpan timeSpan = new TimeSpan(0, 0, 1);
                loginScreenElement.Position = timeSpan;
                loginScreenElement.Play();
            };
            loginScreenElement.Play();
            //

            TextBlock enterButton = (TextBlock) FindName("enter_button");
            enterButton.MouseDown += (s, e) =>
            {
                Home home = new Home();
                home.Show();
                this.Hide();
            };
        }
    }
}
