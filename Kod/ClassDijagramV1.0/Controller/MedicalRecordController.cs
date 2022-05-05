using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Controller
{
    public class MedicalRecordController
    {
        #region Fields

        private MedicalRecordService _medicalRecordService;

        #endregion

        #region Constructor

        public MedicalRecordController(MedicalRecordService mrs)
        {
            _medicalRecordService = mrs;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Vraca sve zdravstvene kartone
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ObservableCollection<MedicalRecord> GetMedicalRecords()
        {
            return _medicalRecordService.GetMedicalRecords();
        }

        /// <summary>
        ///  Cuva sve zdravstvene kartone
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SaveMedicalRecords()
        {
            _medicalRecordService.SaveMedicalRecords();
        }

        /// <summary>
        /// Dodaje novi zdravstveni karton ako ne postoji vec jedan sa istim brojem, u suprotnom ce da pregazi stari
        /// </summary>
        /// <param name="newMedicalRecord"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddMedicalRecord(MedicalRecord newMedicalRecord)
        {
            _medicalRecordService.AddMedicalRecord(newMedicalRecord);
        }

        /// <summary>
        /// Birse zdravstveni karton i vraca true, ako ne postoji vraca false
        /// </summary>
        /// <param name="medicalRecordNumber"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool RemoveMedicalRecord(int medicalRecordNumber)
        {
            return _medicalRecordService.RemoveMedicalRecord(medicalRecordNumber);
        }

        /// <summary>
        /// Updatuje zdravstveni karton
        /// </summary>
        /// <param name="medicalRecord"></param>
        /// <returns></returns>
        public bool UpdateMedicalRecord(MedicalRecord medicalRecord)
        {
            return _medicalRecordService.UpdateMedicalRecord(medicalRecord);
        }

        /// <summary>
        /// Vraca zdravstveni karton za zadatim brojem u suprotnom vraca null
        /// </summary>
        /// <param name="medicalRecordNumber"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public MedicalRecord GetMedicalRecord(int medicalRecordNumber)
        {
            return _medicalRecordService.GetMedicalRecord(medicalRecordNumber);
        }

        /// <summary>
        /// Proverava da li postoji medical Record sa zadatim brojem preskacuci zadati medicalRecord
        /// </summary>
        /// <param name="number"></param>
        /// <param name="mr">Medical Record koji ne gledamo</param>
        /// <returns></returns>
        public bool IsMedicalRecordNumberUnique(int number, MedicalRecord mr = null)
        {
            return _medicalRecordService.IsMedicalRecordNumberUnique(number, mr);
        }

        #endregion
    }
}