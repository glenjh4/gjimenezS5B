using gjimenezS5B.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gjimenezS5B.Utils
{
    //Acción Crear Registro
    public class PersonRepository 
    {
        string dbPath;
        private SQLiteConnection conn;
        public string status { get; set; }
        public PersonRepository(string path)
        {
            dbPath = path;
        }

        public void Init()
        {
            if (conn is not null)
                return;
            conn = new(dbPath);
            conn.CreateTable<Persona>();
        }

        public void AddNewPerson(string nombre)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(nombre))
                {
                    throw new Exception("El nombre es requerido");
                }
                Persona person = new() { Name = nombre };
                result = conn.Insert(person);
                status = string.Format("Dato ingresado");
            }
            catch (Exception ex)
            {
                status = string.Format("Error al ingresar: " + ex.Message);
            }
        }
        
        //Acción Listar Registro
        public List<Persona> GetAllPeople()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                status = string.Format("Error al listar: " + ex.Message);
            }

            return new List<Persona>();
        }

        //Acción Modificar Registro
        public bool ModificarRegistro(int id, string nombre)
        {
            try
            {
                Init();
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    status = "El nombre es requerido";
                    return false;
                }

                Persona personaExistente = conn.Find<Persona>(id);
                if (personaExistente == null)
                {
                    status = "No se encontró ningún registro";
                    return false;
                }
                personaExistente.Name = nombre;
                int result = conn.Update(personaExistente);

                if (result > 0)
                {
                    status = "Registro modificado correctamente";
                    return true;
                }
                else
                {
                    status = "No se realizó la modificación";
                    return false;
                }
            }
            catch (Exception ex)
            {
                status = string.Format("Error al modificar: " + ex.Message);
                return false;
            }
        }

        //Acción Eliminar Registro
        public void EliminarRegistro(int id)
        {
            int result = 0;
            try
            {
                Init();
                if (id == 0)
                {
                    throw new Exception("Seleccione el registro que desea eliminar");
                }
                Persona person = new() { Id = id };
                result = conn.Delete(person);
                status = string.Format("Registro eliminado exitosamente");
            }
            catch (Exception ex)
            {
                status = string.Format("Error al eliminar: " + ex.Message);
            }
        }
    }
}
