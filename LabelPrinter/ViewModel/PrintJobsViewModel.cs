using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LabelPrinter.Model;
using LabelPrinter.Storage;
using System;
using System.Collections.Generic;
using System.Windows;

namespace LabelPrinter.ViewModel
{
    public class PrintJobsViewModel : ViewModelBase
    {
        GodexPrinter _printer;
        public RelayCommand PrintJobsButtonCommand { get; }
        public RelayCommand<object> ExitButtonCommand { get; } 

        List<string> _labelSource;
        /// <summary>
        /// List of all LabelNames
        /// </summary>
        public List<string> LabelSource
        {
            get => _labelSource;
            set
            {
                _labelSource = value;
                RaisePropertyChanged(nameof(LabelSource));
            }
        }
         
        private PrintJobsLabel _printJobsLabel;
        /// <summary>
        /// Print jobs property
        /// </summary>
        public PrintJobsLabel PrintJobsLabel
        {
            get => _printJobsLabel;
            set
            {
                _printJobsLabel = value;
                RaisePropertyChanged(nameof(PrintJobsLabel));
            }
        }
        
        readonly StorageSelector _storageSelector;

        public PrintJobsViewModel()
        {
            _printer = new GodexPrinter();

            _storageSelector = new StorageSelector();
            LabelSource = _storageSelector.GetStorage().GetLabelNames();
            InitJobs();
            PrintJobsButtonCommand = new RelayCommand(PrintJobsCommand);
            ExitButtonCommand = new RelayCommand<object>(ExitCommand); 
        }

        private void InitJobs()
        {
            PrintJobsLabel = new PrintJobsLabel
            {
                PrintJobsRow = new List<PrintJobsRow>()  // As we 10 jobs in UI.
                {
                   new PrintJobsRow(),
                   new PrintJobsRow(),
                   new PrintJobsRow(),
                   new PrintJobsRow(),
                   new PrintJobsRow(),
                   new PrintJobsRow(),
                   new PrintJobsRow(),
                   new PrintJobsRow(),
                   new PrintJobsRow(),
                   new PrintJobsRow(),
                   new PrintJobsRow()
                }
            };
        }
         
        private void PrintJobsCommand()
        {
            var jobs = PrintJobsLabel;
            if(jobs != null && jobs.PrintJobsRow != null)
            {
                var storage = _storageSelector.GetStorage();

                foreach (var job in jobs.PrintJobsRow)
                {
                    if (!string.IsNullOrEmpty(job.LabelName) && job.NoOfCopy > 0)
                    {
                        Label label = storage.GetLabel(job.LabelName);
                        if(label != null)
                        {
                            PhysicalPrinter.Instance.Print(label, job.NoOfCopy);
                        }                        
                    }                    
                }
            }          
        }

        private void ExitCommand(object obj)
        {
            Window Win = obj as Window;
            Win.Close();
        }
        
    }
}
