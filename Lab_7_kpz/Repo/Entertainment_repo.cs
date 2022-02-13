using System;
using System.Collections.Generic;

namespace Lab_7_kpz
{
    
        public interface Entertainment_repo
        {
            IEnumerable<Entertainment> Get();
            Entertainment Get(int ID);
            void Create(Entertainment client);
            void Update(Entertainment client, int ID);
            Entertainment Delete(int ID);
        }
    
}
