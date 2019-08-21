using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class CostumerDatabase : ICostumerDatabase
    {
        private HashSet<Costumer> costumers;
        private readonly bool _debug = true;
        private readonly string conString = "User Id=system;Password=pass;Data Source=localhost:1521/XEPDB1;";
        private OracleConnection con;
        public CostumerDatabase()
        {
            con = new OracleConnection(conString);

            con.Open();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT \'Hello World!\' FROM dual";

            OracleDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Console.WriteLine(reader.GetString(0));

            costumers = new HashSet<Costumer>();
            this.Add(new Costumer {
            FirstName = "Peder",
            LastName = "Birkeland",
            Email = "Peder.Birkeland@Bouvet.no",
            Type=CostumerType.BASIC});
        }

        public void Add(Costumer c)
        {
            if (_debug)
            {
                Console.WriteLine($"Added user: {c.FirstName} {c.LastName} with email: {c.Email}");
            }
            costumers.Add(c);
        }

        public Costumer FindByEmail(string email)
        {
            return costumers.Where(x => x.Email == email).FirstOrDefault();
        }

        public IEnumerable<Costumer> FindByFirstName(string firstName)
        {
            return costumers.Where(x => x.FirstName == firstName);
        }

        public IEnumerable<Costumer> FindByLastName(string lastName)
        {
            return costumers.Where(x => x.LastName == lastName);
        }

        public IEnumerable<Costumer> FindByType(CostumerType type)
        {
            return costumers.Where(x => x.Type == type);
        }

        public void Remove(Costumer c)
        {
            costumers.Remove(c);
        }

        public void Remove(IEnumerable<Costumer> cs)
        {
            foreach (var c in cs)
            {
                Remove(c);
            }
        }

        public IEnumerable<Costumer> GetAll()
        {
            return costumers;
        }
    }
}