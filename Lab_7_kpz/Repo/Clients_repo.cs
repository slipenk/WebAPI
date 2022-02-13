
using System.Collections.Generic;

namespace Lab_7_kpz
{
    public interface Clients_repo
    {
        IEnumerable<Clients> Get();
        Clients Get(int ID);
        void Create(Clients client);
        void Update(Clients client, int ID);
        Clients Delete(int ID);
    }
}
