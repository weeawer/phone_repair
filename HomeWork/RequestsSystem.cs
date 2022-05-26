using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HomeWork
{
    internal class RequestsSystem : IDisposable
    {
        private string StorageFileName { get; }

        public List<Request> Requests { get; private set; }

        public RequestsSystem(string storageFileName)
        {
            StorageFileName = storageFileName;
            ReadFromStorage();
        }

        public void Dispose() => WriteToStorage();

        public Request AddRequest(string clientID, string masterFIO, string timeAndDate, string telephoneID, string repairCost)
        {
            var telephonesSystem = new TelephonesSystem("../../../telephones.txt");
            List<Telephone> telephones = telephonesSystem.Telephones;
            var telephone = telephones.Where(t => t.Id == Convert.ToInt32(telephoneID)).FirstOrDefault();

            var clientsSystem = new ClientsSystem("../../../clients.txt");
            List<Client> clients = clientsSystem.Clients;
            var client = clients.Where(t => t.Id == Convert.ToInt32(clientID)).FirstOrDefault();

            if (telephone == null)
                throw new ArgumentException($"Телефона с номером ={telephoneID} не существует");
            var request = new Request(Requests.Last().Id + 1, client, masterFIO, timeAndDate, telephone, repairCost);
            Requests.Add(request);
            return request;
        }

        public Request RemoveRequest(int id)
        {
            var requestToRemove = Requests.Find(request => request.Id == id);
            if (requestToRemove == null)
                throw new ArgumentException($"Заявки с номером ={id} не существует");
            Requests.Remove(requestToRemove);
            return requestToRemove;
        }

        public Request UpdateRequest(int id, string clientID = null, string masterFIO = null, string timeAndDate = null, string telephoneID = null, string repairCost = null)
        {
            var requestIndexToUpdate = Requests.FindIndex(request => request.Id == id);

            var telephonesSystem = new TelephonesSystem("../../../telephones.txt");
            List<Telephone> telephones = telephonesSystem.Telephones;
            var telephone = telephones.Where(t => t.Id == Convert.ToInt32(telephoneID)).FirstOrDefault();

            var clientsSystem = new ClientsSystem("../../../clients.txt");
            List<Client> clients = clientsSystem.Clients;
            var client = clients.Where(t => t.Id == Convert.ToInt32(clientID)).FirstOrDefault();

            if (requestIndexToUpdate == -1)
                throw new ArgumentException($"Заявки с номером ={id} не существует");
            if (telephone == null)
                throw new ArgumentException($"Телефона с номером ={telephoneID} не существует");
            if (client == null)
                throw new ArgumentException($"Телефона с номером ={clientID} не существует");

            var requestToUpdate = Requests[requestIndexToUpdate];
            Requests[requestIndexToUpdate] = new Request(id,
                client ?? requestToUpdate.Client,
                masterFIO ?? requestToUpdate.MasterFIO,
                timeAndDate ?? requestToUpdate.TimeAndDate,
                telephone ?? requestToUpdate.Telephone,
                repairCost ?? requestToUpdate.RepairCost);
                
            return Requests[requestIndexToUpdate];
        }

        private void WriteToStorage()
        {
            using var writer = new StreamWriter(StorageFileName);
            foreach (var request in Requests)
            {
                writer.WriteLine(request.ToString());
            }
        }

        private void ReadFromStorage()
        {
            Requests = new List<Request>();
            var telephonesSystem = new TelephonesSystem("../../../telephones.txt");
            List<Telephone> telephones = telephonesSystem.Telephones;

            var clientsSystem = new ClientsSystem("../../../clients.txt");
            List<Client> clients = clientsSystem.Clients;

            using var reader = new StreamReader(StorageFileName);
            string currentLine;
            while ((currentLine = reader.ReadLine()) != null)
            {
                var requestData = currentLine.Split(Constants.DataSeparator);

                var infoData = currentLine.Split(Constants.DataSeparator);

                Telephone telephone = telephones.Where(t => t.Id == int.Parse(requestData[4])).FirstOrDefault();

                

                Client client = clients.Where(z => z.Id == int.Parse(infoData[0])).FirstOrDefault();

                Requests.Add(new Request(
                    int.Parse(requestData[0]),
                    client,
                    requestData[2],
                    requestData[3],
                    telephone,
                    requestData[5])); 
            }
        }
    }
}