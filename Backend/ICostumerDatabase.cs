using System.Collections.Generic;

namespace Model
{
    public interface ICostumerDatabase
    {
        void Add(Costumer c);

        void Remove(Costumer c);
        void Remove(IEnumerable<Costumer> cs);

        IEnumerable<Costumer> GetAll();

        IEnumerable<Costumer> FindByFirstName(string firstName);
        IEnumerable<Costumer> FindByLastName(string lastName);
        IEnumerable<Costumer> FindByType(CostumerType type);
        Costumer FindByEmail(string email);
    }
}