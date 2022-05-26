using System;

namespace HomeWork
{
    internal class Telephone
    {
        public Telephone(
            int id,
            string model,
            string damage,
            string details)
            
        {
            Id = id;
            Model = model;
            Damage = damage;
            Details = details;
        }

        public int Id { get; }
        
        public string Model { get; }
        public string Damage { get; }
        public string Details { get; }

        public override string ToString() =>
            $"{Id};{Model};{Damage};{Details}";
    }
}