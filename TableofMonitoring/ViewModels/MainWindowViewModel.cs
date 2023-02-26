using TableofMonitoring.Models;
using TableofMonitoring.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Net.Http.Headers;
using System.Reactive;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TableofMonitoring.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, Unit> SaveStudents { get; }
        public ReactiveCommand<Unit, Unit> AddStudent { get; }
        public ReactiveCommand<Unit, Unit> DeleteStudent { get; }
        public ReactiveCommand<Unit, Unit> LoadStudents { get; }
        public bool flag = true;
        public int count = 0;
        double q = 0;
        string FIO = string.Empty;
        ushort MarkMathem = 0;
        public ushort MarkOOP = 0;
        public ushort Visual = 0;
        public ushort MarkTRPO = 0;
        public double[] AverageScore = { 0, 0, 0, 0, 0 };
        public string[] mainColor = { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };

        public MainWindowViewModel()
        {
            AddStudent = ReactiveCommand.Create(() =>
            {
                Student[] temp = students;
                count++;
                Array.Resize(ref temp, count);

                temp[temp.Length - 1] = new Student { fio = FIO, PmarkMathem = MarkMathem, PVisual = Visual, PmarkOOP = MarkOOP, PmarkTRPO = MarkTRPO, GetAverage = 0, GetMathemColor = string.Empty, GetmainColor = string.Empty, GetTRPOColor = string.Empty, GetVisualColor = string.Empty, GetOOPColor = string.Empty};
                Students = temp;
                AllAverage(students);
                flag = false;
            });
            DeleteStudent = ReactiveCommand.Create(() =>
            {
                Student[] temp = students;
                int how = -1;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i].FIO == FIO)
                    {
                        how = i;
                        break;
                    }
                }
                if (how != -1)
                {
                    for (int i = how; i < temp.Length - 1; i++)
                    {
                        temp[i] = temp[i + 1];
                    }
                    count--;
                    Array.Resize(ref temp, count);
                    Students = temp;
                    AllAverage(students);
                    if (count == 0)
                    {
                        flag = true;
                    }
                }
            });
            SaveStudents = ReactiveCommand.Create(() =>
            {
                StreamWriter file = new StreamWriter("dat.txt");
                if (count != 0)
                {
                    for (int i = 0; i < students.Length; i++)
                    {
                        file.WriteLine(students[i].fio.ToString() + " " + students[i].PmarkMathem.ToString() + " " + students[i].PmarkOOP.ToString() + " " + students[i].PVisual.ToString() + " " + students[i].PmarkTRPO.ToString());
                    }
                }
                file.Close();
            });
            LoadStudents = ReactiveCommand.Create(() =>
            {
                if (flag)
                {
                    if (System.IO.File.Exists("dat.txt"))
                    {
                        StreamReader file = new StreamReader("dat.txt");
                        while (!file.EndOfStream)
                        {
                            int g = 0;
                            string line = file.ReadLine();
                            char[] ch = line.ToCharArray();
                            string[] res_array = { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
                            ushort[] int_array = { 0, 0, 0, 0 };
                            for (int i = 0; i < ch.Length; i++)
                            {
                                if (ch[i] == ' ')
                                {
                                    if (ch[i + 1] >= '0' && ch[i + 1] <= '9')
                                    {
                                        g++;
                                        i++;
                                    }
                                }
                                res_array[g] += ch[i].ToString();
                            }
                            for (int i = 1; i < res_array.Length; i++)
                            {
                                int_array[i - 1] = ushort.Parse(res_array[i]);
                            }
                            Student[] temp = students;
                            count++;
                            Array.Resize(ref temp, count);
                            temp[temp.Length - 1] = new Student { fio = res_array[0], PmarkMathem = int_array[0], PVisual = int_array[2], PmarkOOP = int_array[1], PmarkTRPO = int_array[3], GetAverage = 0, GetMathemColor = string.Empty, GetmainColor = string.Empty, GetTRPOColor = string.Empty, GetVisualColor = string.Empty, GetOOPColor = string.Empty };
                            Students = temp;
                            AllAverage(students);
                        }
                        file.Close();
                        flag = false;
                    }
                }
            });

        }
        public void AllAverage(Student[] students)
        {
            if (students.Length != 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    AverageScore[i] = 0;
                }
                for (int j = 0; j < students.Length; j++)
                {
                    GetAverageMathem += students[j].MarkMathem;
                    GetAverageOOP += students[j].MarkOOP;
                    GetAverageVisual += students[j].Visual;
                    GetAverageTRPO += students[j].MarkTRPO;
                }
                GetAverageMathem /= students.Length;
                GetmainColorMathem = setColor(GetAverageMathem);
                GetAverageOOP /= students.Length;
                GetmainColorOOP = setColor(GetAverageOOP);
                GetAverageVisual /= students.Length;
                GetmainColorVisual = setColor(GetAverageVisual);
                GetAverageTRPO /= students.Length;
                GetmainColorTRPO = setColor(GetAverageTRPO);
                GetQ = (GetAverageTRPO + GetAverageVisual + GetAverageOOP + GetAverageMathem) / 4.0;
                GetmainColorI = setColor(GetQ);
            }
            else
            {
                GetAverageMathem = 0;
                GetAverageOOP = 0;
                GetAverageVisual = 0;
                GetAverageTRPO = 0;
                GetQ = 0;
                GetmainColorMathem = setColor(GetAverageMathem);
                GetmainColorOOP = setColor(GetAverageOOP);
                GetmainColorVisual = setColor(GetAverageVisual);
                GetmainColorTRPO = setColor(GetAverageTRPO);
                GetQ = (GetAverageTRPO + GetAverageVisual + GetAverageOOP + GetAverageMathem) / 4.0;
                GetmainColorI = setColor(GetQ);
            }
        }
        public string setColor(double Score)
        {
            string result = string.Empty;
            if (Score < 1)
            {
                result = "Red";
            }
            if (Score >= 1 && Score <= 1.5)
            {
                result = "Yellow";
            }
            if (Score > 1.5)
            {
                result = "Green";
            }
            return result;
        }
        public Student[] Students { get => students; set => this.RaiseAndSetIfChanged(ref students, value); }
        private Student[] students;
        public string GetName { get => FIO; set { this.RaiseAndSetIfChanged(ref FIO, value); } }
        public ushort GetMarkMathem { get => MarkMathem; set { this.RaiseAndSetIfChanged(ref MarkMathem, value); } }
        public ushort GetMarkOOP { get => MarkOOP; set { this.RaiseAndSetIfChanged(ref MarkOOP, value); } }
        public ushort GetVisual { get => Visual; set { this.RaiseAndSetIfChanged(ref Visual, value); } }
        public ushort GetMarkTRPO { get => MarkTRPO; set { this.RaiseAndSetIfChanged(ref MarkTRPO, value); } }
        public double GetAverageMathem { get => AverageScore[0]; set { this.RaiseAndSetIfChanged(ref AverageScore[0], value); } }
        public double GetAverageOOP { get => AverageScore[1]; set { this.RaiseAndSetIfChanged(ref AverageScore[1], value); } }
        public double GetAverageVisual { get => AverageScore[2]; set { this.RaiseAndSetIfChanged(ref AverageScore[2], value); } }
        public double GetAverageTRPO { get => AverageScore[3]; set { this.RaiseAndSetIfChanged(ref AverageScore[3], value); } }
        public string GetmainColorMathem { get => mainColor[0]; set { this.RaiseAndSetIfChanged(ref mainColor[0], value); } }
        public string GetmainColorOOP { get => mainColor[1]; set { this.RaiseAndSetIfChanged(ref mainColor[1], value); } }
        public string GetmainColorVisual { get => mainColor[2]; set { this.RaiseAndSetIfChanged(ref mainColor[2], value); } }
        public string GetmainColorTRPO { get => mainColor[3]; set { this.RaiseAndSetIfChanged(ref mainColor[3], value); } }
        public string GetmainColorI { get => mainColor[4]; set { this.RaiseAndSetIfChanged(ref mainColor[4], value); } }
        public double GetQ { get => q; set { this.RaiseAndSetIfChanged(ref q, value); } }
        public string GetmainColor { get => mainColor[4]; set { this.RaiseAndSetIfChanged(ref mainColor[4], value); } }
    }
}