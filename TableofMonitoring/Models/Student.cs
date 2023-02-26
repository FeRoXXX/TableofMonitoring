using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableofMonitoring.Models
{
    public class Student
    {
        public string FIO = string.Empty;
        public ushort MarkMathem;
        public ushort MarkOOP;
        public ushort Visual;
        public ushort MarkTRPO;
        public double Average;
        public string mainColor = string.Empty;
        public string MathemColor = string.Empty;
        public string OOPColor = string.Empty;
        public string VisualColor = string.Empty;
        public string TRPOColor = string.Empty;
        public string fio
        {
            get => FIO;
            set => FIO = value;
        }
        public ushort PmarkMathem
        {
            get => MarkMathem;
            set => MarkMathem = value;
        }
        public ushort PmarkOOP
        {
            get => MarkOOP;
            set => MarkOOP = value;
        }
        public ushort PVisual
        {
            get => Visual;
            set => Visual = value;
        }
        public ushort PmarkTRPO
        {
            get => MarkTRPO;
            set => MarkTRPO = value;
        }
        public double GetAverage
        {
            get => Average;
            set => Average = (MarkMathem + MarkOOP + Visual + MarkTRPO) / 4.0;
        }
        public string GetmainColor
        {
            get => mainColor;
            set
            {
                if (GetAverage < 1)
                {
                    mainColor = "Red";
                }
                if (GetAverage <= 1.5 && GetAverage >= 1)
                {
                    mainColor = "Yellow";
                }
                if (GetAverage > 1.5)
                {
                    mainColor = "Green";
                }
            }
        }
        public string GetMathemColor
        {
            get => MathemColor;
            set
            {
                if (MarkMathem == 0)
                {
                    MathemColor = "Red";
                }
                if (MarkMathem == 1)
                {
                    MathemColor = "Yellow";
                }
                if (MarkMathem == 2)
                {
                    MathemColor = "Green";
                }
            }
        }
        public string GetOOPColor
        {
            get => OOPColor;
            set
            {
                if (MarkOOP == 0)
                {
                    OOPColor = "Red";
                }
                if (MarkOOP == 1)
                {
                    OOPColor = "Yellow";
                }
                if (MarkOOP == 2)
                {
                    OOPColor = "Green";
                }
            }
        }
        public string GetVisualColor
        {
            get => VisualColor;
            set
            {
                if (Visual == 0)
                {
                    VisualColor = "Red";
                }
                if (Visual == 1)
                {
                    VisualColor = "Yellow";
                }
                if (Visual == 2)
                {
                    VisualColor = "Green";
                }
            }
        }
        public string GetTRPOColor
        {
            get => TRPOColor;
            set
            {
                if (MarkTRPO == 0)
                {
                    TRPOColor = "Red";
                }
                if (MarkTRPO == 1)
                {
                    TRPOColor = "Yellow";
                }
                if (MarkTRPO == 2)
                {
                    TRPOColor = "Green";
                }
            }
        }
    }
}
