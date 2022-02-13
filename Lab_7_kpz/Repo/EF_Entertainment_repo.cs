using System;
using System.Collections.Generic;

namespace Lab_7_kpz
{
    public class EF_Entertainment_repo : Entertainment_repo
    {
        
            private DataContext Context;

            public EF_Entertainment_repo(DataContext context)
            {
                Context = context;
            }

            public IEnumerable<Entertainment> Get()
            {
                return Context.Entertainments;
            }

            public Entertainment Get(int ID)
            {
                return Context.Entertainments.Find(ID);
            }
            public void Create(Entertainment entertainment)
            {
                Context.Entertainments.Add(entertainment);
                Context.SaveChanges();
            }
            public void Update(Entertainment entertainment, int ID)
            {
                Entertainment currentEntertainment = Get(ID);
                currentEntertainment.Name = entertainment.Name;
                currentEntertainment.Description = entertainment.Description;
                currentEntertainment.Cost = entertainment.Cost;
                Context.Entertainments.Update(currentEntertainment);
                Context.SaveChanges();
            }

            public Entertainment Delete(int ID)
            {
                Entertainment entertainment = Get(ID);

                if (entertainment != null)
                {
                    Context.Entertainments.Remove(entertainment);
                    Context.SaveChanges();
                }

                return entertainment;
            }
        
    }
}
