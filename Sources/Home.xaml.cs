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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();

            initializeProblemTray();

            viewHistoryButton.MouseDown += (s, e) =>
            {
                HistoryWindow historyWindow = new HistoryWindow();
                historyWindow.Show();
            };

            exit.MouseDown += (s, e) =>
            {
                Application.Current.Shutdown();
            };
        }

        public void submitCode(Problem problem)
        {
            SubmitCodeWindow submitCodeWindow = new SubmitCodeWindow(problem);
            submitCodeWindow.Show();
        }

        public void initializeProblemTray()
        {
            problem_tray.ItemsSource = ProblemData.GetProblems();
            problem_tray.SelectionMode = SelectionMode.Single;
            problem_tray.PreviewMouseLeftButtonDown += (s, e) =>
            {
                Problem problem = problem_tray.SelectedItem as Problem;
                if (problem != null)
                {
                    displayInfoPanel.Children.Clear();
                    initDisplayPanel(problem);

                    submitCodeBtn.Visibility = Visibility.Visible;
                }
            };

            submitCodeBtn.MouseDown += (s, e) =>
            {
                Problem problem = problem_tray.SelectedItem as Problem;
                submitCode(problem);
            };
        }

        private void initDisplayPanel(Problem problem)
        {
            TextBlock ID = new TextBlock();
            ID.Text = problem.ID;
            ID.Padding = new Thickness(0, 10, 0, 0);
            ID.Height = 25;
            ID.Width = 692;
            ID.TextAlignment = TextAlignment.Center;
            ID.FontSize = 14;

            TextBlock Name = new TextBlock { 
                Text = problem.Name,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                Padding = new Thickness(0,5,0,0),
                FontWeight = FontWeights.Bold,
                FontSize = 20,
            };

            TextBlock TimeLimit = new TextBlock
            {
                Text = "Thời gian chạy: " + problem.TimeLimit + " giây",
                Height = 25,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                FontStyle = FontStyles.Italic,
                FontSize = 14
            };

            TextBlock DescriptionHeader = new TextBlock
            {
                Text = "Mô tả",
                Padding = new Thickness(20, 10, 0, 0),
                FontWeight = FontWeights.SemiBold,
                FontSize = 16
            };

            string descriptionContent = "";
            foreach (string s in problem.Description)
                descriptionContent += s + "\n";
            TextBlock Description = new TextBlock {
                Text = descriptionContent,
                FontSize = 14,
                Padding = new Thickness(40, 0, 40, 0),
                TextWrapping = TextWrapping.Wrap
            };

            TextBlock InputRequirementHeader = new TextBlock
            {
                Text = "Input",
                Padding = new Thickness(20, 10, 0, 0),
                FontWeight = FontWeights.SemiBold,
                FontSize = 16
            };

            string inputRequirementContent = "";
            foreach (string s in problem.Input)
                inputRequirementContent += s + "\n";
            TextBlock InputRequirement = new TextBlock
            {
                Text = inputRequirementContent,
                FontSize = 14,
                Padding = new Thickness(40, 0, 40, 0),
                TextWrapping = TextWrapping.Wrap
            };

            TextBlock OutputRequirementHeader = new TextBlock
            {
                Text = "Output",
                Padding = new Thickness(20, 10, 0, 0),
                FontWeight = FontWeights.SemiBold,
                FontSize = 16
            };

            string outputRequirementContent = "";
            foreach(string s in problem.Output)
                outputRequirementContent += s + "\n";
            TextBlock OutputRequirement = new TextBlock
            {
                Text = outputRequirementContent,
                FontSize = 14,
                Padding = new Thickness(40, 0, 40, 0),
                TextWrapping = TextWrapping.Wrap
            };

            TextBlock ExampleHeader = new TextBlock
            {
                Text = "Test mẫu",
                Padding = new Thickness(20, 10, 0, 0),
                FontWeight = FontWeights.SemiBold,
                FontSize = 16
            };

            TextBlock ExampleInputHeader = new TextBlock
            {
                Text = "Input",
                FontSize = 14,
                Padding = new Thickness(40, 0, 40, 0),
                TextWrapping = TextWrapping.Wrap,
                FontWeight = FontWeights.SemiBold
            };

            TextBlock exampleInput = new TextBlock
            {
                Text = problem.ExampleTest.toStringInput(),
                FontFamily = new FontFamily("Consolas"),
                FontSize = 14,
                Padding = new Thickness(40, 5, 40, 0),
                Background = new SolidColorBrush(Color.FromRgb(224, 224, 224))
            };

            TextBlock ExampleOutputHeader = new TextBlock
            {
                Text = "Output",
                FontSize = 14,
                Padding = new Thickness(40, 0, 40, 0),
                TextWrapping = TextWrapping.Wrap,
                FontWeight = FontWeights.SemiBold
            };

            TextBlock exampleOutput = new TextBlock
            {
                Text = problem.ExampleTest.toStringOutput(),
                FontFamily = new FontFamily("Consolas"),
                FontSize = 14,
                Padding = new Thickness(40, 5, 40, 0),
                Background = new SolidColorBrush(Color.FromRgb(224,224,224)),
            };

            TextBlock NoteHeader = new TextBlock
            {
                Text = "Ghi chú",
                Padding = new Thickness(20, 10, 0, 0),
                FontWeight = FontWeights.SemiBold,
                FontSize = 16
            };

            string noteContent = "";
            foreach (string s in problem.Note)
                noteContent += s + "\n";
            TextBlock Note = new TextBlock
            {
                Text = noteContent,
                FontSize = 14,
                Padding = new Thickness(40, 0, 40, 0),
                TextWrapping = TextWrapping.Wrap
            };

            displayInfoPanel.Children.Add(ID);
            displayInfoPanel.Children.Add(Name);
            displayInfoPanel.Children.Add(TimeLimit);
            displayInfoPanel.Children.Add(DescriptionHeader);
            displayInfoPanel.Children.Add(Description);
            displayInfoPanel.Children.Add(InputRequirementHeader);
            displayInfoPanel.Children.Add(InputRequirement);
            displayInfoPanel.Children.Add(OutputRequirementHeader);
            displayInfoPanel.Children.Add(OutputRequirement);
            displayInfoPanel.Children.Add(ExampleHeader);
            displayInfoPanel.Children.Add(ExampleInputHeader);
            displayInfoPanel.Children.Add(exampleInput);
            displayInfoPanel.Children.Add(ExampleOutputHeader);
            displayInfoPanel.Children.Add(exampleOutput);
            displayInfoPanel.Children.Add(NoteHeader);
            displayInfoPanel.Children.Add(Note);
        }
    }
}
