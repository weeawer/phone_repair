using System;

namespace HomeWork
{
    class Client
    {
        public Client(
            int id,
            string name,
            string nomber)

        {
            Id = id;
            Name = name;
            Nomber = nomber;
        }

        public int Id { get;  }
        public string Name { get; }
        public string Nomber { get; }

        public override string ToString() =>
            $"{Id};{Name};{Nomber}";
    }
}
