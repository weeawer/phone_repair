using System;
using System.Linq;

namespace HomeWork
{
    internal abstract class Command
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string[] Arguments { get; }

        protected RequestsSystem RequestsSystem { get; }
        protected TelephonesSystem TelephonesSystem { get; }
        protected ClientsSystem ClientsSystem { get; }

        protected Command(RequestsSystem requestsSystem, ClientsSystem clientsSystem, TelephonesSystem telephonesSystem )
        {
            RequestsSystem = requestsSystem;
            TelephonesSystem = telephonesSystem;
            ClientsSystem = clientsSystem;
        }

        public abstract string Execute(string[] args);

        public override string ToString()
        {
            var argumentsString = Arguments.Any() ? " " + string.Join(";", Arguments) : "";
            return $"{Name}{argumentsString} ({Description})";
        }
    }

    internal class AllRequestsCommand : Command
    {
        public override string Name => "allRequests";
        public override string Description => "Получить информацию о заявках";
        public override string[] Arguments => new string[0];

        public AllRequestsCommand(RequestsSystem requestsSystem, ClientsSystem clientsSystem, TelephonesSystem telephonesSystem)
            : base(requestsSystem, clientsSystem, telephonesSystem)
        {
        }

        public override string Execute(string[] args) =>
            string.Join("\n", RequestsSystem.Requests);
    }

    internal class AllTelephonesCommand : Command
    {
        public override string Name => "allTelephones";
        public override string Description => "Получить информацию обо всех телефонах";
        public override string[] Arguments => new string[0];

        public AllTelephonesCommand(RequestsSystem requestsSystem, ClientsSystem clientsSystem, TelephonesSystem telephonesSystem)
            : base(requestsSystem, clientsSystem, telephonesSystem)
        {
        }

        public override string Execute(string[] args) =>
            string.Join("\n", TelephonesSystem.Telephones);
    }


    internal class AllClientsCommand : Command
    {
        public override string Name => "allClients";
        public override string Description => "Получить информацию обо всех клиентах";
        public override string[] Arguments => new string[0];

        public AllClientsCommand(RequestsSystem requestsSystem, ClientsSystem clientsSystem, TelephonesSystem telephonesSystem)
            : base(requestsSystem, clientsSystem, telephonesSystem)
        {
        }

        public override string Execute(string[] args) =>
            string.Join("\n", ClientsSystem.Clients);
    }








    internal class AddRequestCommand : Command
    {
        public override string Name => "addRequest";
        public override string Description => "Добавить заявку с данной информаией";
        public override string[] Arguments => new[] { "<clientnumber>", "<masterfio>", "<timeanddate>", "<telephoneid>", "<repaircost>" };

        public AddRequestCommand(RequestsSystem requestsSystem, ClientsSystem clientsSystem, TelephonesSystem telephonesSystem)
            : base(requestsSystem, clientsSystem, telephonesSystem)
        {
        }

        public override string Execute(string[] args)
        {
            var clientnumber = args[0];
            var masterfio = args[1];
            var timeanddate = args[2];
            var telephoneid = args[3];
            var repaircost = args[4];
            var request = RequestsSystem.AddRequest(clientnumber, masterfio, timeanddate, telephoneid, repaircost);
            return $"Успешно добавлена заявка {request}";
        }
    }

    internal class RemoveRequestCommand : Command
    {
        public override string Name => "removeRequest";
        public override string Description => "Удалить заявку с заданным id";
        public override string[] Arguments => new[] {"<id>"};

        public RemoveRequestCommand(RequestsSystem requestsSystem, ClientsSystem clientsSystem, TelephonesSystem telephonesSystem)
            : base(requestsSystem, clientsSystem, telephonesSystem)
        {
        }

        public override string Execute(string[] args)
        {
            var id = int.Parse(args[0]);
            var request = RequestsSystem.RemoveRequest(id);
            return $"Успешно удалена заявка {request}";
        }
    }

    internal class UpdateRequestCommand : Command
    {
        public override string Name => "updateRequest";
        public override string Description => "Обновить информаию о заявке с id, используйте '-', чтобы оставить текущее значение";
        public override string[] Arguments => new[] {"<id>", "<clientnumber>", "<masterfio>", "<timeanddate>", "<telephoneid>", "<repaircost>" };

        public UpdateRequestCommand(RequestsSystem requestsSystem, ClientsSystem clientsSystem, TelephonesSystem telephonesSystem)
            : base(requestsSystem, clientsSystem, telephonesSystem)
        {
        }

        public override string Execute(string[] args)
        {
            var id = int.Parse(args[0]);
            var clientnumber = args[1];
            var masterfio = args[2];
            var timeanddate = args[3];
            var telephoneid = args[4];
            var repaircost = args[5];
            var request = RequestsSystem.UpdateRequest(id,
                clientnumber == "-" ? null : clientnumber,
                masterfio == "-" ? null : masterfio,
                timeanddate == "-" ? null : timeanddate,
                telephoneid == "-" ? null : telephoneid,
                repaircost == "-" ? null : repaircost);
            return $"Успешно обновлена заявка {request}";
        }
    }

    internal class AddTelephoneCommand : Command
    {
        public override string Name => "addTelephone";
        public override string Description => "Добавить телефон с данной информаией";
        public override string[] Arguments => new[] { "<id>", "<model>", "<damage>", "<details>" };

        public AddTelephoneCommand(RequestsSystem requestsSystem, ClientsSystem clientsSystem, TelephonesSystem telephonesSystem)
            : base(requestsSystem, clientsSystem, telephonesSystem)
        {
        }

        public override string Execute(string[] args)
        {
            var id = int.Parse(args[0]);
            var model = args[1];
            var damage = args[2];
            var details = args[3];
            
            var telephone = TelephonesSystem.AddTelephone(id, model, damage, details);
            return $"Успешно добавлен звонок {telephone}";
        }
    }

    internal class RemoveTelephoneCommand : Command
    {
        public override string Name => "removeTelephone";
        public override string Description => "Удалить телефон с заданным id";

        public override string[] Arguments => new[] {"<id>"};

        public RemoveTelephoneCommand(RequestsSystem requestsSystem, ClientsSystem clientsSystem, TelephonesSystem telephonesSystem)
            : base(requestsSystem, clientsSystem, telephonesSystem)
        {
        }

        public override string Execute(string[] args)
        {
            var id = int.Parse(args[0]);
            var telephone = TelephonesSystem.RemoveTelephone(id);
            return $"Успешно удален телефон {telephone}";
        }
    }

    internal class UpdateTelephoneCommand : Command
    {
        public override string Name => "updateTelephone";
        public override string Description => "Обновить информаию о телефоне с id, используйте '-', чтобы оставить текущее значение";

        public override string[] Arguments => new[]
            {"<id>", "<model>", "<damage>", "<details>"};

        public UpdateTelephoneCommand(RequestsSystem requestsSystem, ClientsSystem clientsSystem, TelephonesSystem telephonesSystem)
            : base(requestsSystem, clientsSystem, telephonesSystem)
        {
        }

        public override string Execute(string[] args)
        {
            var id = int.Parse(args[0]);
            
            var model = args[1];
            var damage = args[2];
            var details = args[3];
            
            var telephone = TelephonesSystem.UpdateTelephone(id,
                model,
                damage,
                details);
            return $"Успешно обвнолена информация о телефоне {telephone}";
        }
    }








    internal class AddClientCommand : Command
    {
        public override string Name => "addClient";
        public override string Description => "Добавить клиента с данной информаией";
        public override string[] Arguments => new[] { "<id>", "<name>", "<nomber>" };

        public AddClientCommand(RequestsSystem requestsSystem, ClientsSystem clientsSystem, TelephonesSystem telephonesSystem)
            : base(requestsSystem, clientsSystem, telephonesSystem)
        {
        }

        public override string Execute(string[] args)
        {
            var id = int.Parse(args[0]);
            var name = args[1];
            var nomber = args[2];

            var client = ClientsSystem.AddClient(id, name, nomber);
            return $"Успешно добавлен звонок {client}";
        }
    }





   

    internal class RemoveClientCommand : Command
    {
        public override string Name => "removeClient";
        public override string Description => "Удалить клиента с заданным id";

        public override string[] Arguments => new[] { "<id>" };

        public RemoveClientCommand(RequestsSystem requestsSystem, ClientsSystem clientsSystem, TelephonesSystem telephonesSystem)
            : base(requestsSystem, clientsSystem, telephonesSystem)
        {
        }

        public override string Execute(string[] args)
        {
            var id = int.Parse(args[0]);
            var client = ClientsSystem.RemoveClient(id);
            return $"Успешно удален клиент {client}";
        }
    }

    internal class UpdateClientCommand : Command
    {
        public override string Name => "updateClient";
        public override string Description => "Обновить информаию о клиенте с id, используйте '-', чтобы оставить текущее значение";

        public override string[] Arguments => new[]
            {"<id>", "<name>", "<nomber>"};

        public UpdateClientCommand(RequestsSystem requestsSystem, ClientsSystem clientsSystem, TelephonesSystem telephonesSystem)
            : base(requestsSystem, clientsSystem, telephonesSystem)
        {
        }

        public override string Execute(string[] args)
        {
            var id = int.Parse(args[0]);

            var name = args[1];
            var nomber = args[2];

            var client = ClientsSystem.UpdateClient(id,
                name,
                nomber);
            return $"Успешно обвнолена информация о клиенте {client}";
        }
    }












    internal class ExitCommand : Command
    {
        public override string Name => "exit";
        public override string Description => "Завершить работу программы";
        public override string[] Arguments => new string[0];

        public ExitCommand(RequestsSystem requestsSystem, ClientsSystem clientsSystem, TelephonesSystem telephonesSystem)
            : base(requestsSystem, clientsSystem, telephonesSystem)
        {
        }

        public override string Execute(string[] args) => "Программа завершена.";
    }
}