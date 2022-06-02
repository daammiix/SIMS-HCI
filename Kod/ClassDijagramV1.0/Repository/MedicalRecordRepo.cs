using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ClassDijagramV1._0.Repository
{
    public class MedicalRecordRepo
    {
        #region Fields

        private FileHandler<MedicalRecord> _fileHandler;

        #endregion

        #region Properties

        public ObservableCollection<MedicalRecord> MedicalRecords { get; set; }

        #endregion

        #region Constructor

        public MedicalRecordRepo(FileHandler<MedicalRecord> fileHandler)
        {
            _fileHandler = fileHandler;

            MedicalRecords = new ObservableCollection<MedicalRecord>(_fileHandler.GetItems());
        }

        #endregion

        #region Methods

        /// <summary>
        /// Vraca sve zdravstvene kartone
        /// </summary>
        public void SaveMedicalRecords()
        {
            _fileHandler.SaveItems(MedicalRecords.ToList());
        }

        /// <summary>
        /// Cuva sve zdravstvene kartone u bazu(fajl)
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<MedicalRecord> GetMedicalRecords()
        {
            return MedicalRecords;
        }

        /// <summary>
        /// Dodaje novi zdravstveni karton
        /// </summary>
        /// <param name="newMedicalRecord"></param>
        public void AddMedicalRecord(MedicalRecord newMedicalRecord)
        {
            MedicalRecords?.Add(newMedicalRecord);
        }

        /// <summary>
        /// Brise zdravstveni karton ako postoji i vraca true, u suprotnom vraca false
        /// </summary>
        /// <param name="medicalRecord"></param>
        /// <returns></returns>
        public bool RemoveMedicalRecord(MedicalRecord medicalRecord)
        {
            return MedicalRecords.Remove(medicalRecord);
        }

        /// <summary>
        /// Updatuje postojeci zdravstveni karton i braca true, u suprtonom vraca false
        /// </summary>
        /// <param name="medicalRecord"></param>
        /// <returns></returns>
        public bool UpdateMedicalRecord(MedicalRecord medicalRecord)
        {
            // Pronadjemo medicalRecord koji treba da updatujemo
            MedicalRecord? mrToUpdate = null;
            foreach (var mr in MedicalRecords)
            {
                if (mr.Number == medicalRecord.Number)
                {
                    mrToUpdate = mr;
                }
            }

            // Ako smo pronasli updatujemo ga i vratimo true, u suprotnom vratimo false
            if (mrToUpdate != null)
            {
                mrToUpdate = medicalRecord;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Vraca medical record sa zadatim brojem ako postoji, u suprotnom vraca null
        /// </summary>
        /// <param name="medicalRecordNumber"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public MedicalRecord GetMedicalRecord(int medicalRecordNumber)
        {
            MedicalRecord? ret = null;
            foreach (var mr in MedicalRecords)
            {
                if (mr.Number == medicalRecordNumber)
                {
                    ret = mr;
                    break;
                }
            }

            return ret;
        }

        #endregion
    }
}
