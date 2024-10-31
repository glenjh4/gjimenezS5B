using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gjimenezS5B.Utils
{
    public class PersonRepository
    {
        string _dbPath; //Ruta
        private SQLiteConnection conn;
        //Mensaje a mostrar
        public string StatusMessage { get; set; }

        //TODO: Add variable for the SQLite connection
        private void Init()
        {
            if (conn is not null)
                return;
            conn = new(_dbPath);
            conn.CreateTable<Persona>();
        }

        public PersonRepository(string dbPath)
            { 
                _dbPath = dbPath; 
            }


    }
}
