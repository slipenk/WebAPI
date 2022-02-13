using System.Collections.Generic;

namespace Lab_7_kpz
{
        public class EF_clients_repo : Clients_repo
        {
            private DataContext Context;

            public EF_clients_repo(DataContext context)
            {
                Context = context;
            }

            public IEnumerable<Clients> Get()
            {
                return Context.Clients;
            }

            public Clients Get(int ID)
            {
                return Context.Clients.Find(ID);
            }
            public void Create(Clients client)
            {
                Context.Clients.Add(client);
                Context.SaveChanges();
            }
            public void Update(Clients client, int ID)
            {
                Clients currentClient = Get(ID);
                currentClient.Name = client.Name;
                currentClient.Surname = client.Surname;
                currentClient.Middle_name = client.Middle_name;
                currentClient.Phone_number = client.Phone_number;
                currentClient.Email = client.Email;
                Context.Clients.Update(currentClient);
                Context.SaveChanges();
            }

            public Clients Delete(int ID)
            {
                Clients client = Get(ID);

                if (client != null)
                {
                    Context.Clients.Remove(client);
                    Context.SaveChanges();
                }

                return client;
            }
        }
    
}
