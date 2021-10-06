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

namespace Rope.ViewModel
{
    class LW2_PendulumViewModel : BaseViewModel, ICloseWindows
    {
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
            
          
        }


        /// <summary>Значение X_0</summary>

        private double _X = 0;
        public double X
        {
            get => _X;
            set => SetProperty(ref _X, value);
        }

        /// <summary>Значение M</summary>

        private double _M;
        public double M
        {
            get => _M;
            set => SetProperty(ref _M, value);

        }


        /// <summary>Значение N</summary>

        private double _N;
        public double N
        {
            get => _N;
            set => SetProperty(ref _N, value);

        }

        /// <summary>Значение U</summary>

        private double _U;
        public double U
        {
            get => _U;
            set => SetProperty(ref _U, value);

        }

        /// <summary>X current</summary>

        private double _XCurrent;
        public double XCurrent
        {
            get => _XCurrent;
            set => SetProperty(ref _XCurrent, value);

        }


        /// <summary>U current</summary>

        private double _UCurrent;
        public double UCurrent
        {
            get => _UCurrent;
            set => SetProperty(ref _UCurrent, value);

        }

        private double _actualWidth;
        public double ActWidth
        {
            get { return _actualWidth; }
            set { SetProperty(ref _actualWidth, value); }
        }

       
        private RelayCommand _solution;
        public RelayCommand Solution
        {
            get
            {
                return _solution ?? (_solution = new RelayCommand(() =>
                {

                  
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
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != string.Empty)
            {
                using (var sw = new StreamWriter(saveFileDialog1.FileName))
                {
                    XCurrent = X;
                    UCurrent = U;
                    sw.WriteLine($"X = ;{XCurrent:f3}; U = ;{UCurrent:f3};");
                    double tau = 1e-5;
                    double tb = 0.0, te = 10.0;
                    bool sep = false;

                    while (te > tb)
                    {

                        UCurrent += tau * (1.0/Math.Pow((1+XCurrent), N) - 1.0 / Math.Pow((1 - XCurrent), N))*1.0/M;
                        XCurrent += tau * UCurrent;
                        if (sep)
                        {
                            //sw.Write(";");
                        }
                        sep = true;
                        sw.WriteLine($"X = ;{XCurrent:f6}; U = ;{UCurrent:f6};");
                        tb += tau;
                        /*Result = XCurrent;
                        Width = 200 + Result;*/
                    }
                }
                dialogService.ShowMessageBoxDialog("Success");
            }


        }
    }
}
