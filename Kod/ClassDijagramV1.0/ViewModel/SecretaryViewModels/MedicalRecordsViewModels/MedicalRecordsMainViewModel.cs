using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.SecretaryView.MedicalRecordsView;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.MedicalRecordsViewModels
{
    public class MedicalRecordsMainViewModel
    {

        #region Fields

        private MedicalRecordController _medicalRecordController;

        private RelayCommand _searchRecord;

        private RelayCommand _changeRecord;

        private RelayCommand _addRecord;

        private RelayCommand _deleteRecord;

        // Da bi imali pacijente koji nemaju karton, kada obrisemo karton dodamo pacijenta u ovu listu
        private ObservableCollection<Patient> _patients;

        #endregion

        #region Properties

        public ObservableCollection<MedicalRecordViewModel> MedicalRecords { get; private set; }

        public MedicalRecordViewModel SelectedMedicalRecord { get; set; }

        #endregion

        #region Constructor

        public MedicalRecordsMainViewModel()
        {
            App app = Application.Current as App;

            _medicalRecordController = app.MedicalRecordController;

            MedicalRecords = new ObservableCollection<MedicalRecordViewModel>();

            // Popunimo listu medicalRecordViewModel-a
            foreach (MedicalRecord mr in _medicalRecordController.GetMedicalRecords())
            {
                MedicalRecords.Add(new MedicalRecordViewModel(mr));
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Prikazuje dijalog za dodavanje novog medical record-a
        /// </summary>
        public RelayCommand AddRecord
        {
            get
            {
                if (_addRecord == null)
                {
                    _addRecord = new RelayCommand(o =>
                    {
                        ShowAddRecordDialog();
                    });
                }

                return _addRecord;
            }
        }

        /// <summary>
        /// Brise selektovani medical record
        /// </summary>
        public RelayCommand DeleteRecord
        {
            get
            {
                if (_deleteRecord == null)
                {
                    _deleteRecord = new RelayCommand(o => DeleteRecordExecute(), ModifyRecordCanExecute);
                }

                return _deleteRecord;
            }
        }

        /// <summary>
        /// Prikazuje dijalog za izmenu selektovanog medical record-a
        /// </summary>
        public RelayCommand ChangeRecord
        {
            get
            {
                if (_changeRecord == null)
                {
                    _changeRecord = new RelayCommand(o => ShowChangeRecordDialog(SelectedMedicalRecord),
                        ModifyRecordCanExecute);
                }

                return _changeRecord;
            }
        }

        #endregion

        #region Private Helpers

        private void ShowAddRecordDialog()
        {
            AddMedicalRecordDialog addMedicalRecordDialog = new AddMedicalRecordDialog(MedicalRecords);
            addMedicalRecordDialog.Owner = Application.Current.MainWindow;
            addMedicalRecordDialog.ShowDialog();
        }

        private void DeleteRecordExecute()
        {
            // Izvrisemo medicalRecord iz baze prvo pa onda iz tabele da nam kad brisemo iz baze ne bi bio null
            _medicalRecordController.RemoveMedicalRecord(SelectedMedicalRecord.MedicalRecord.Number);
            MedicalRecords.Remove(SelectedMedicalRecord);

        }

        private bool ModifyRecordCanExecute()
        {
            // Ako nismo nista selektovali ne mozemo da izvrsimo komandu delete
            return SelectedMedicalRecord == null ? false : true;
        }

        private void ShowChangeRecordDialog(MedicalRecordViewModel medicalRecordViewModel)
        {
            ChangeMedicalRecordDialog dialog = new ChangeMedicalRecordDialog(
                new ChangeMedicalRecordDialogViewModel(medicalRecordViewModel));
            dialog.Owner = Application.Current.MainWindow;

            dialog.ShowDialog();
        }

        #endregion 
    }
}
