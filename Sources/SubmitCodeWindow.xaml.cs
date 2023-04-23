using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;

namespace InterC
{
    /// <summary>
    /// Interaction logic for SubmitCodeWindow.xaml
    /// </summary>
    public partial class SubmitCodeWindow : Window
    {
        private Problem problem;
        private string selectedFileInstance;

        public SubmitCodeWindow(Problem problem)
        {
            InitializeComponent();

            this.problem = problem;
            problemName.Text = problem.Name;

            selectFile.Click += (s, e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "C files (*.c)|*.c|C++ files (*.cpp)|*.cpp|All files|*.*";

                if (openFileDialog.ShowDialog() == true)
                {
                    selectedFile.Text = openFileDialog.FileName;
                    selectedFileInstance = openFileDialog.FileName;
                }
            };

            submitButton.Click += (s, e) =>
            {
                execute();
            };

            exit.MouseDown += (s, e) =>
            {
                Application.Current.Shutdown();
            };
        }

        private void execute()
        {
            string directory = Directory.GetParent(selectedFileInstance).FullName;
            File.Delete(directory + "\\compiled.exe");

            string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            statusBox.Text = "Đang biên dịch ...";
            if (!compile())
            {
                statusBox.Text = statusBox.Text + "\n" + "Biên dịch lỗi!";
                return;
            }
            Thread.Sleep(2000);
            statusBox.Text = statusBox.Text + "\n" + "Biên dịch thành công!";

            statusBox.Text = statusBox.Text + "\n" + "Đang chạy chương trình ...";

            List<Test> testCases = problem.Tests.Values.ToList<Test>();

            int success = 0;
            for (int i = 0; i < testCases.Count; i++)
            {
                statusBox.Text = statusBox.Text + "\n" + "Đang chạy test " + (i + 1) + "...";

                Test test = testCases[i];

                Process p = new Process();
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.FileName = directory + "\\compiled.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardError = true;

                // Đăng ký sự kiện OutputDataReceived để xử lý đầu ra từ quá trình
                p.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        string result = e.Data;
                        string output = test.toStringOutput();
                        if (!result.Equals(output))
                        {
                            MessageBox.Show("Làm sai (WR)");
                            SubmitHistoryManager.histories.Add(new SubmitHistory
                            {
                                StartDate = date,
                                ProblemName = problem.ID + "-" + problem.Name,
                                Detail = "Kết quả không đúng! Kết quả của bạn là: " + e.Data + ", nhưng kết quả đúng phải là " + output,
                                Status = "WR",
                            });
                            i = 99999999;
                        }
                        else
                        {
                            success++;
                            if (success == problem.Tests.Count)
                            {
                                MessageBox.Show("HOÀN THÀNH BÀI LÀM (AC)");
                                SubmitHistoryManager.histories.Add(new SubmitHistory
                                {
                                    StartDate = date,
                                    ProblemName = problem.ID + "-" + problem.Name,
                                    Detail = "Bài làm được chấp nhận",
                                    Status = "AC",
                                });
                            }
                        }
                    }
                };

                p.ErrorDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        MessageBox.Show("Xảy ra lỗi (ERR)");
                        SubmitHistoryManager.histories.Add(new SubmitHistory
                        {
                            StartDate = date,
                            ProblemName = problem.ID + "-" + problem.Name,
                            Detail = "Có lỗi phát sinh trong quá trình chạy! Cụ thể:\n" + e.Data,
                            Status = "ERR",
                        });
                        i = 99999999;
                    }
                };

                p.Start();

                foreach(string s in test.Input)
                    p.StandardInput.WriteLine(s);

                // Bắt đầu đọc đầu ra từ quá trình
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();

                // Đợi quá trình hoàn thành
                while (!p.HasExited)
                {
                    if (p.TotalProcessorTime.Seconds > problem.TimeLimit)
                    {
                        p.Kill();
                        MessageBox.Show("Quá thời gian (TLE)");
                        SubmitHistoryManager.histories.Add(new SubmitHistory
                        {
                            StartDate = date,
                            ProblemName = problem.ID + "-" + problem.Name,
                            Detail = "Bài làm cần nhiều hơn thời gian quy định là " + problem.TimeLimit + " giây để chạy",
                            Status = "TLE",
                        });
                        i = 99999999;
                        break;
                    }
                }
            }

            statusBox.Text = statusBox.Text + "\n" + "Hoàn tất đánh giá.";

        }

        private bool compile()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.RedirectStandardInput = true;
            startInfo.UseShellExecute = false;
            string cmd = "(cd /d \"" + Directory.GetParent(selectedFileInstance) + "\")&(gcc " + System.IO.Path.GetFileName(selectedFileInstance) + " -o compiled)";
            startInfo.Arguments = "/C \"" + cmd + "\"";
            process.StartInfo = startInfo;
            process.Start();

            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                MessageBox.Show("Xảy ra lỗi (ERR)");

                return false;
            }

            return true;
        }
    }
}
