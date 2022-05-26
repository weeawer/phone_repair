namespace HomeWork
{
    internal class Request
    {
        public Request(int id, Client client, string masterFIO, string timeAndDate, Telephone telephone, string repairCost)
        {
            Id = id;
            Client = client;
            MasterFIO = masterFIO;
            TimeAndDate = timeAndDate;
            Telephone = telephone;
            RepairCost = repairCost;
        }

        public int Id { get; }
        public Client Client { get; }
        public string MasterFIO { get; }
        public string TimeAndDate { get; }
        public Telephone Telephone { get; }
        public string RepairCost { get; }
        public override string ToString() => $"{Id};{Client.Id};{MasterFIO};{TimeAndDate};{Telephone.Id};{RepairCost}";
    }
}