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
        private readonly IDialogService dialogService;
        private readonly IFileService fileService;
        public MainWindowViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;
        }



        #region Commands
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
        private RelayCommand _LW2_PendulumCommand;
        public RelayCommand LW2_PendulumCommand
        { 
            get
            {
                return _LW2_PendulumCommand ??
                    (_LW2_PendulumCommand = new RelayCommand(() =>
                    {
                        LW2_PendulumViewModel viewModel = new LW2_PendulumViewModel();
                        dialogService.ShowDialog(viewModel);
                    }
                    ));
            }

        }

        private RelayCommand _LW1_PendulumCommand;
        public RelayCommand LW1_PendulumCommand
        {
            get
            {
                return _LW1_PendulumCommand ??
                       (_LW1_PendulumCommand = new RelayCommand(() =>
                           {
                               LW1_PendulumViewModel viewModel = new LW1_PendulumViewModel(dialogService);
                               dialogService.ShowDialog(viewModel);
                           }
                       ));
            }

        }
        #endregion

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
        private void Open()
        {
          
        }
        public bool Save()
        {
            return false;
        }
    }
}
