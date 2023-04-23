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
using System.Windows.Shapes;

namespace InterC
{
    /// <summary>
    /// Interaction logic for HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
        {
            InitializeComponent();

            history_tray.ItemsSource = SubmitHistoryManager.histories;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SubmitHistory submitHistory= history_tray.SelectedItem as SubmitHistory;  
            if (submitHistory != null)
            {
                MessageBox.Show(submitHistory.ProblemName + "\n" 
                    + submitHistory.StartDate + "\n"
                    + submitHistory.Status + "\n" +
                    submitHistory.Detail);
            }
        }
    }
}
