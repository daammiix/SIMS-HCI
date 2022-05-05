using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Util;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model
{
    public class MedicalRecord : ObservableObject
    {
        public static int numCounter = 0;

        #region Fields

        private int _patientId;
        private int _number;
        private string _parentName;
        private MaritalStatus _maritalStatus;
        private string _healthCardNumber;
        private BloodType _bloodType;
        private List<string>? _allergens;
        private List<string>? _diseases;

        #endregion

        #region Properties

        public int PatientId
        {
            get
            {
                return _patientId;
            }
            set
            {
                if (_patientId != value)
                {
                    _patientId = value;
                    OnPropertyChanged("PatientId");
                }
            }
        }

        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                if (_number != value)
                {
                    _number = value;
                    OnPropertyChanged("Number");
                }
            }
        }

        public string ParentName
        {
            get
            {
                return _parentName;
            }
            set
            {
                if (_parentName != value)
                {
                    _parentName = value;
                    OnPropertyChanged("ParentName");
                }
            }
        }

        public MaritalStatus MaritalStatus
        {
            get
            {
                return _maritalStatus;
            }
            set
            {
                if (_maritalStatus != value)
                {
                    _maritalStatus = value;
                    OnPropertyChanged("MaritalStatus");
                }
            }
        }

        public string HealthCardNumber
        {
            get
            {
                return _healthCardNumber;
            }
            set
            {
                if (_healthCardNumber != value)
                {
                    _healthCardNumber = value;
                    OnPropertyChanged("HealthCardNumber");
                }
            }
        }

        public BloodType BloodType
        {
            get
            {
                return _bloodType;
            }
            set
            {
                if (_bloodType != value)
                {
                    _bloodType = value;
                    OnPropertyChanged("BloodType");
                }
            }
        }

        public List<string>? Allergens
        {
            get
            {
                return _allergens ?? new List<string>();
            }
            set
            {
                if (_allergens != value)
                {
                    _allergens = value;
                    OnPropertyChanged("Allergens");
                }
            }
        }

        public List<string>? Diseases
        {
            get
            {
                return _diseases ?? new List<string>();
            }
            set
            {
                if (_diseases != value)
                {
                    _diseases = value;
                    OnPropertyChanged("Diseases");
                }
            }
        }

        #endregion

        #region Constructor

        public MedicalRecord(int pId, string parName, MaritalStatus ms, string hcNum, BloodType bt
            , List<string>? allergens = null, List<string>? diseases = null)
        {
            PatientId = pId;
            Number = ++numCounter;
            ParentName = parName;
            MaritalStatus = ms;
            HealthCardNumber = hcNum;
            BloodType = bt;
            Allergens = allergens ?? new List<string>();
            Diseases = diseases ?? new List<string>();
        }

        // Zbog fileHandlera, mora da bude parameterless constructor
        public MedicalRecord()
        {
            // uvecamo numCounter za svaki koji ucitamo
            // procitati Person klasu
            // numCounter++;
        }

        #endregion
    }
}
