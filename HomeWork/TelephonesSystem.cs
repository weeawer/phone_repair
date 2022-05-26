using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HomeWork
{
    internal class TelephonesSystem : IDisposable
    {
        private string StorageFileName { get; }

        public List<Telephone> Telephones { get; private set; }

        public TelephonesSystem(string storageFileName)
        {
            StorageFileName = storageFileName;
            ReadFromStorage();
        }

        public void Dispose() => WriteToStorage();

        public Telephone AddTelephone(
            int id,
            string model,
            string damage,
            string details)
        {
            var telephone = new Telephone(id, model, damage, details);
            Telephones.Add(telephone);
            return telephone;
        }

        

        public Telephone RemoveTelephone(int id)
        {
            var telephoneToRemove = Telephones.Find(call => call.Id == id);
            if (telephoneToRemove == null)
                throw new ArgumentException($"Телефона с id={id} не существует");
            Telephones.Remove(telephoneToRemove);
            return telephoneToRemove;
        }

        public Telephone UpdateTelephone(
            int id,
            string model = null,
            string damage = null,
            string details = null)
        {
            var telephoneIndexToUpdate = Telephones.FindIndex(telephone => telephone.Id == id);
            if (telephoneIndexToUpdate == -1)
                throw new ArgumentException($"Телефона с id={id} не существует");
            var telephoneToUpdate = Telephones[telephoneIndexToUpdate];
            Telephones[telephoneIndexToUpdate] = new Telephone(id,
                model ?? telephoneToUpdate.Model,
                damage ?? telephoneToUpdate.Damage,
                details ?? telephoneToUpdate.Details);
            return Telephones[telephoneIndexToUpdate];
        }

        private void WriteToStorage()
        {
            using var writer = new StreamWriter(StorageFileName);
            foreach (var telephone in Telephones) writer.WriteLine(telephone.ToString());
        }

        private void ReadFromStorage()
        {
            Telephones = new List<Telephone>();
            using var reader = new StreamReader(StorageFileName);
            string currentLine;
            while ((currentLine = reader.ReadLine()) != null)
            {
                var clientData = currentLine.Split(Constants.DataSeparator);
                Telephones.Add(new Telephone(
                    int.Parse(clientData[0]),
                    clientData[1],
                    clientData[2],
                    clientData[3]));
                    
            }
        }
    }
}