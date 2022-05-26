using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HomeWork
{
    internal class ClientsSystem : IDisposable
    {
        private string StorageFileName { get; }

        public List<Client> Clients { get; private set; }

        public ClientsSystem(string storageFileName)
        {
            StorageFileName = storageFileName;
            ReadFromStorage();
        }

        public void Dispose() => WriteToStorage();

        public Client AddClient(
            int id,
            string name,
            string nomber)
        {
            var client = new Client(id, name, nomber);
            Clients.Add(client);
            return client;
        }



        public Client RemoveClient(int id)
        {
            var clientToRemove = Clients.Find(call => call.Id == id);
            if (clientToRemove == null)
                throw new ArgumentException($"Клиента с id={id} не существует");
            Clients.Remove(clientToRemove);
            return clientToRemove;
        }

        public Client UpdateClient(
            int id,
            string name = null,
            string nomber = null)
        {
            var clientIndexToUpdate = Clients.FindIndex(client => client.Id == id);
            if (clientIndexToUpdate == -1)
                throw new ArgumentException($"Клиента с id={id} не существует");
            var clientToUpdate = Clients[clientIndexToUpdate];
            Clients[clientIndexToUpdate] = new Client(id,
                name ?? clientToUpdate.Name,
                nomber ?? clientToUpdate.Nomber);
            return Clients[clientIndexToUpdate];
        }

        private void WriteToStorage()
        {
            using var writer = new StreamWriter(StorageFileName);
            foreach (var client in Clients) writer.WriteLine(client.ToString());
        }

        private void ReadFromStorage()
        {
            Clients = new List<Client>();
            using var reader = new StreamReader(StorageFileName);
            string currentLine;
            while ((currentLine = reader.ReadLine()) != null)
            {
                var infoData = currentLine.Split(Constants.DataSeparator);
                Clients.Add(new Client(
                    int.Parse(infoData[0]),
                    infoData[1],
                    infoData[2]));

            }
        }
    }
}
