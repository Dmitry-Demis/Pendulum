using Microsoft.Win32;
using Rope.BindingEnums;
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
using System.Windows;

namespace Rope.ViewModel
{
    public class MainWindowViewModel : BaseViewModel, ICloseWindows
    {
        private double _n = 3;

        public double N
        {
            get { return _n; }
            set { SetProperty(ref _n, value); }
        }
        private double _m =1;

        public double M
        {
            get { return _m; }
            set { SetProperty(ref _m, value); }
        }
        private double _b2 = 1;

        public double B2
        {
            get { return _b2; }
            set { SetProperty(ref _b2, value); }
        }
        private double _a2 = 1;

        public double A2
        {
            get { return _a2; }
            set { SetProperty(ref _a2, value); }
        }
        private double _x = 10;

        public double X
        {
            get { return _x; }
            set { SetProperty(ref _x, value); }
        }
        private double _u = 0;

        public double U
        {
            get { return _u; }
            set { SetProperty(ref _u, value); }
        }
        private double _xCurrent;

        public double XCurrent
        {
            get { return _xCurrent; }
            set { SetProperty(ref _xCurrent, value); }
        }
        private double _uCurrent;

        public double UCurrent
        {
            get { return _uCurrent; }
            set { SetProperty(ref _uCurrent, value); }
        }

        

        /// <summary>
        /// Services
        /// </summary>
        private readonly IDialogService dialogService;
        private readonly IFileService fileService;
       

        public event Action Closed;
        public void Close()
        {
            if (dialogService.ShowMessageBoxDialog("Сохранение изменений", $"Хотите ли вы сохранить изменения перед выходом?") == MessageBoxResult.Yes)
            {
                if (Save())
                {
                    Closed?.Invoke();
                }
            }
            else
            {
                Closed?.Invoke();
            }

        }

        /// <summary>
        /// A save method for the SaveFileDialogCommand
        /// </summary>
        public bool Save()
        {
            return false;
        }
        public MainWindowViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;
        }
        private double _result = 0;

        public double Result
        {
            get { return _result; }
            set { SetProperty(ref _result, value); }
        }
        private double _width = 200;

        public double Width
        {
            get { return _width; }
            set { SetProperty(ref _width, value); }
        }
        /// <summary>
        /// A command, which allows to close a window
        /// </summary>
        private RelayCommand _closeWindowCommand;
        public RelayCommand CloseWindowCommand
        {
            get
            {
                return _closeWindowCommand ??
                    (
                    _closeWindowCommand = new RelayCommand(() =>
                    {
                        Close();
                    }));
            }
        }        

        /// <summary>
        /// A command, which allows to save a datatable
        /// </summary>
        private RelayCommand _saveFileDialogCommand;
        public RelayCommand SaveFileDialogCommand
        {
            get
            {
                return _saveFileDialogCommand ??
                       (_saveFileDialogCommand = new RelayCommand(() =>
                       {
                           Save();
                       }));
            }
        }
        private RelayCommand _solution;
        public RelayCommand Solution
        {
            get
            {
                return _solution ?? (_solution = new RelayCommand(() =>
                {
                    MethodAsync();
                   
                 
                    /*XCurrent = X;
                    UCurrent = U;                    
                    double tau = 1e-1;
                    double tb = 0.0, te = 5.0;
                    bool sep = false;
                    while (te > tb)
                    {

                        UCurrent += tau * (-B2 * XCurrent + A2 / Math.Pow(XCurrent, 3));
                        XCurrent += tau * UCurrent;
                        if (sep)
                        {
                            //sw.Write(";");
                        }
                        sep = true;
                       
                        tb += tau;
                        Result+=0.05;
                        OnPropertyChanged(nameof(Result));
                        
                        //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                        //saveFileDialog1.Filter = "Comma-Separated Values|*.csv";
                        //saveFileDialog1.Title = "Сохраняем csv файл";
                        //saveFileDialog1.ShowDialog();


                        //if (saveFileDialog1.FileName != "")
                        //{
                            using (var sw = new StreamWriter(saveFileDialog1.FileName))
                            {
                                XCurrent = X;
                                UCurrent = U;
                                sw.WriteLine($"X = ;{XCurrent:f3}; U = ;{UCurrent:f3};");
                                double tau = 1e-1;
                                double tb = 0.0, te = 5.0;
                                bool sep = false;
                                while (te > tb)
                                {

                                    UCurrent +=  tau * (-B2 * XCurrent + A2 / Math.Pow(XCurrent, 3));
                                    XCurrent +=  tau * UCurrent;
                                    if (sep)
                                    {
                                        //sw.Write(";");
                                    }
                                    sep = true;
                                    sw.WriteLine($"X = ;{XCurrent:f3}; U = ;{UCurrent:f3};");
                                    tb += tau;
                                    Result++;
                                }
                    }*/
                        //dialogService.ShowMessageBoxDialog("Success");                   
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
            if (saveFileDialog1.FileName!=string.Empty)
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

                        UCurrent += tau * (-B2 * XCurrent + A2 / Math.Pow(XCurrent, 3));
                        XCurrent += tau * UCurrent;
                        if (sep)
                        {
                            //sw.Write(";");
                        }
                        sep = true;
                        sw.WriteLine($"X = ;{XCurrent:f6}; U = ;{UCurrent:f6};");
                        tb += tau;
                        Result = XCurrent;
                        Width = 200 + Result;
                    }
                }
                dialogService.ShowMessageBoxDialog("Success");
            }
            

        }
        /// <summary>
        /// A command, which allows to open a file dialogue
        /// </summary>
        private RelayCommand<Parameter> _openFileDialogCommand;
        public RelayCommand<Parameter> OpenFileDialogCommand
        {
            get
            {
                return _openFileDialogCommand ??
                  (_openFileDialogCommand = new RelayCommand<Parameter>((param) =>
                  {
                      Open();
                     // OnPropertyChanged(nameof(IsTableEmpty));
                      //dialogService.ShowMessageBoxDialog("Файл открыт");
                  }));
            }
        }

        /// <summary>
        /// An open method for the OpenFileDialogCommand
        /// </summary>
        private void Open()
        {
          
        }
    }
}
