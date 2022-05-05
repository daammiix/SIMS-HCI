using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.MedicalRecordsViewModels
{
    public class MedicalRecordViewModel : ObservableObject
    {
        #region Fields

        private MedicalRecord _medicalRecord;

        // Pacijent za kog je zdravstveni karton vezan
        private Patient _patient;

        // Da bi povezali pacijenta
        private PatientController _patientController;

        #endregion

        #region Properties

        public MedicalRecord MedicalRecord
        {
            get { return _medicalRecord; }
            set { _medicalRecord = value; }
        }

        public Patient Patient
        {
            get { return _patient; }
            set { _patient = value; }
        }

        // Text iz searchBoxa, ne treba property change jer ce samo view da menja(oneWayToSource)
        public string SearchText { get; set; }

        #endregion

        #region Constructor

        public MedicalRecordViewModel(MedicalRecord mr)
        {
            _medicalRecord = mr;

            App app = Application.Current as App;

            _patientController = app.PatientController;

            foreach (Patient p in _patientController.GetPatients())
            {
                if (mr.PatientId == p.Id)
                {
                    _patient = p;
                }
            }
        }

        #endregion
    }
}
