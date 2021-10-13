using OxyPlot;
using Rope.Cmds;
using Rope.Interfaces;
using Rope.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Microsoft.Win32;
using System.Windows;

namespace Rope.ViewModel
{
    class LW2_PendulumViewModel : BaseViewModel, ICloseWindows
    {
        private Random random = new Random();
        private double _c1 = 7;

        public double C1
        {
            get => _c1;
            set => SetProperty(ref _c1, value);
        }
        private double _c2 = 7;

        public double C2
        {
            get => _c2;
            set => SetProperty(ref _c2, value);
        }
        private double _m1 = 3;

        public double M1
        {
            get => _m1;
            set => SetProperty(ref _m1, value);
        }
        private double _m2 = 2;

        public double M2
        {
            get => _m2;
            set => SetProperty(ref _m2, value);
        }

        private double _M3 = 1;
        public double M3
        {
            get => _M3;
            set => SetProperty(ref _M3, value);
        }
        private double _x1;
        public double X1
        {
            get => _x1;
            set => SetProperty(ref _x1, value);
        }
        private double _u1;
        public double U1
        {
            get => _u1;
            set => SetProperty(ref _u1, value);
        }
        private double _x2;
        public double X2
        {
            get => _x2;
            set => SetProperty(ref _x2, value);
        }
        private double _u2;
        public double U2
        {
            get => _u2;
            set => SetProperty(ref _u2, value);
        }
        private double _x3;
        public double X3
        {
            get => _x3;
            set => SetProperty(ref _x3, value);
        }
        private double _u3;
        public double U3
        {
            get => _u3;
            set => SetProperty(ref _u3, value);
        }


        public event Action Closed;

        public void Close()
        {
            throw new NotImplementedException();
        }

        private readonly IDialogService dialogService;
        private readonly IFileService fileService;
        public LW2_PendulumViewModel(IDialogService dialogService = null, IFileService fileService = null)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;
            X1 =  40;
            X2 = 2*X1;
            X3 = 3*X1;
            U1 = 25;
            U2 = U3 = 5;
            Width1 = X1;
            Width2 = X2;
            Width3 = X3;
            M1 = 10;
            M2 = 5;
            M1 = 3;
            
        }
        private Visibility _v1 = Visibility.Visible; 

        public Visibility V1
        {
            get => _v1;
            set => SetProperty(ref _v1, value);
        }
        private Visibility _v2 = Visibility.Visible;

        public Visibility V2
        {
            get => _v2;
            set => SetProperty(ref _v2, value);
        }

        private double _xCurrent1;

        public double XCurrent1
        {
            get { return _xCurrent1; }
            set { SetProperty(ref _xCurrent1, value); }
        }
        private double _uCurrent1;

        public double UCurrent1
        {
            get { return _uCurrent1; }
            set { SetProperty(ref _uCurrent1, value); }
        }

        private double _result1 = 0;

        public double Result1
        {
            get { return _result1; }
            set { SetProperty(ref _result1, value); }
        }
        private double _width1;

        public double Width1
        {
            get { return _width1; }
            set { SetProperty(ref _width1, value); }
        }



        private double _xCurrent2;

        public double XCurrent2
        {
            get { return _xCurrent2; }
            set { SetProperty(ref _xCurrent2, value); }
        }
        private double _uCurrent2;

        public double UCurrent2
        {
            get { return _uCurrent2; }
            set { SetProperty(ref _uCurrent2, value); }
        }

        private double _result2 = 0;

        public double Result2
        {
            get { return _result2; }
            set { SetProperty(ref _result2, value); }
        }
        private double _width2;

        public double Width2
        {
            get { return _width2; }
            set { SetProperty(ref _width2, value); }
        }


        private double _xCurrent3;

        public double XCurrent3
        {
            get { return _xCurrent3; }
            set { SetProperty(ref _xCurrent3, value); }
        }
        private double _uCurrent3;

        public double UCurrent3
        {
            get { return _uCurrent3; }
            set { SetProperty(ref _uCurrent3, value); }
        }

        private double _result3 = 0;

        public double Result3
        {
            get { return _result3; }
            set { SetProperty(ref _result3, value); }
        }
        private double _width3;

        public double Width3
        {
            get { return _width3; }
            set { SetProperty(ref _width3, value); }
        }

        private RelayCommand _solution;
        public RelayCommand Solution
        {
            get
            {
                return _solution ?? (_solution = new RelayCommand(() =>
                {
                    XCurrent1 = X1;
                    UCurrent1 = U1;
                    XCurrent2 = X2;
                    UCurrent2 = U2;
                    XCurrent3 = X3;
                    UCurrent3 = U3;
                    MethodAsync();


                }));
            }
        }

        async void MethodAsync()
        {
            await Task.Run(() => Method());
        }

        void Method()
        {
            //SaveFileDialog saveFileDialog1 = new SaveFileDialog()
            //{
            //    FileName = "result.csv",
            //};
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Comma-Separated Values|*.csv";
            saveFileDialog1.Title = "Сохраняем csv файл";
            saveFileDialog1.FileName = "result.csv";


            if (true)
            {
                //using (var sw = new StreamWriter(saveFileDialog1.FileName))
                {
                  
                    // sw.WriteLine($"X = ;{XCurrent:f3}; U = ;{UCurrent:f3};");
                    double tau = 1e-5;
                    double tb = 0.0, te = 40.0;
                    bool sep = false;
                    double L = 80;

                    while (te > tb)
                    {
                        double[] Uxk = { UCurrent1, UCurrent2, UCurrent3 };
                        double[] Xk = { XCurrent1, XCurrent2, XCurrent3 };
                        UCurrent1 = UCurrent1 + tau * (-C1 * (L - (Xk[1] - Xk[0])))*1/M1;
                        XCurrent1 = XCurrent1 + tau * Uxk[0];
                        UCurrent2 = UCurrent2 + tau * (C1 * (L - (Xk[1] - Xk[0])) - C2 * (L - (Xk[2] - Xk[1]))) * 1 / M2;
                        XCurrent2 = XCurrent2 + tau * Uxk[1];
                        UCurrent3 = UCurrent3 + tau * (C2 * (L - (Xk[2] - Xk[1]))) * 1 / M3;
                        XCurrent3 = XCurrent3 + tau * Uxk[2];
                        Uxk[0] = UCurrent1;
                        Uxk[1] = UCurrent2;
                        Uxk[2] = UCurrent3;
                        Xk[0] = XCurrent1;
                        Xk[1] = XCurrent2;
                        Xk[2] = XCurrent3;
                        int K = 135;
                        if ((Xk[1] - Xk[0]) > K)
                        {
                            C1 = 0;
                           V1 =  Visibility.Collapsed;
                        }

                        if (Xk[2] - Xk[1] > K)
                        {
                            C2 = 0;
                            V2 = Visibility.Collapsed;
                        }
                        tb += tau;
                        Result1 = XCurrent1;
                        Width1 = 2 * Result1;
                        Result3 = XCurrent3;
                        Width3 = 2 * Result3;
                        Result2 =XCurrent2;
                        Width2 = 2 * Result2;
                    }
                }

               // dialogService.ShowMessageBoxDialog("Data in the file");
            }

        }
    }
}
