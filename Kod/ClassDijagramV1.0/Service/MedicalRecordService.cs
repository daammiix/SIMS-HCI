using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Repository;
using Model;
using Service;
using System;
using System.Collections.ObjectModel;

namespace ClassDijagramV1._0.Service
{
    public class MedicalRecordService
    {
        #region Fields

        private MedicalRecordRepo _medicalRecordRepo;

        private PatientService _patientService;

        #endregion

        #region Constructor

        public MedicalRecordService(MedicalRecordRepo mrr, PatientService ps)
        {
            _medicalRecordRepo = mrr;
            _patientService = ps;
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
            return _medicalRecordRepo.GetMedicalRecords();
        }

        /// <summary>
        ///  Cuva sve zdravstvene kartone
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SaveMedicalRecords()
        {
            _medicalRecordRepo.SaveMedicalRecords();
        }

        /// <summary>
        /// Dodaje novi zdravstveni karton ako ne postoji vec jedan sa istim brojem, u suprotnom ce da pregazi stari
        /// takodje stavlja polje MedicalRecordNumber pacijenta na broj Medical Record-a
        /// </summary>
        /// <param name="newMedicalRecord"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddMedicalRecord(MedicalRecord newMedicalRecord)
        {
            // Proverimo da li postoji kartona sa istim brojem kao novi
            bool exists = false;
            MedicalRecord? toUpdate = null;
            foreach (var mr in _medicalRecordRepo.GetMedicalRecords())
            {
                if (mr.Number == newMedicalRecord.Number)
                {
                    exists = true;
                    toUpdate = mr;
                    break;
                }
            }

            if (exists)
            {
                toUpdate = newMedicalRecord;
                return;
            }
            else
            {
                // Dodamo medical Record
                _medicalRecordRepo.AddMedicalRecord(newMedicalRecord);
                // Povezemo pacijenta sa njegovim Medical Record-om
                Patient p = _patientService.GetPatientById(newMedicalRecord.PatientId);
                p.MedicalRecordNumber = newMedicalRecord.Number;
            }
        }

        /// <summary>
        /// Birse zdravstveni karton i vraca true, ako ne postoji vraca false, takodje stavlja pacijentu cije je karton
        /// izbrisan atribut MedicalRecordNumber na null
        /// </summary>
        /// <param name="medicalRecordNumber"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool RemoveMedicalRecord(int medicalRecordNumber)
        {
            MedicalRecord mrToRemove = null;
            foreach (var mr in _medicalRecordRepo.GetMedicalRecords())
            {
                if (mr.Number == medicalRecordNumber)
                {
                    mrToRemove = mr;
                }
            }

            if (mrToRemove != null)
            {
                // Izbrisemo medical Record
                _medicalRecordRepo.RemoveMedicalRecord(mrToRemove);
                // Stavimo MedicalRecordNumber pacijenta na null
                Patient p = _patientService.GetPatientById(mrToRemove.PatientId);
                p.MedicalRecordNumber = null;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Updatuje zdravstveni karton
        /// </summary>
        /// <param name="medicalRecord"></param>
        /// <returns></returns>
        public bool UpdateMedicalRecord(MedicalRecord medicalRecord)
        {
            return _medicalRecordRepo.UpdateMedicalRecord(medicalRecord);
        }

        /// <summary>
        /// Vraca zdravstveni karton za zadatim brojem u suprotnom vraca null
        /// </summary>
        /// <param name="medicalRecordNumber"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public MedicalRecord GetMedicalRecord(int medicalRecordNumber)
        {
            return _medicalRecordRepo.GetMedicalRecord(medicalRecordNumber);
        }

        /// <summary>
        /// Vraca zdravstveni karton od zadatok pacijenta
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public MedicalRecord GetPatientsMedicalRecord(int patientID)
        {
            return _medicalRecordRepo.GetPatientsMedicalRecord(patientID);
        }

        /// <summary>
        /// Proverava da li postoji medical Record sa zadatim brojem preskacuci zadati medicalRecord
        /// </summary>
        /// <param name="number"></param>
        /// <param name="mr">Medical Record koji ne gledamo</param>
        /// <returns></returns>
        public bool IsMedicalRecordNumberUnique(int number, MedicalRecord mr = null)
        {
            bool unique = true;
            foreach (MedicalRecord medicalRecord in _medicalRecordRepo.GetMedicalRecords())
            {
                // Proverimo da li smo prosledili medicalRecord koji preskacemo
                if (mr != null && medicalRecord == mr)
                {
                    continue;
                }
                if (medicalRecord.Number == number)
                {
                    unique = false;
                    break;
                }
            }

            return unique;
        }

        #endregion
    }
}
