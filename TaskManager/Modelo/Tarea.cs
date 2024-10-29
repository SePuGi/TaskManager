using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Tarea
    {
        /*
     Tarea:
 	    id
        idUsuario
 	    descripción
 	    DataInicio
 	    DataFin
        */

        public int id;
        public int idUsuario;
        public string descripcion;
        public DateTime dataInicio;
        public DateTime dataFin;

        public Tarea(int id, int idUsuario, string descripcion, DateTime dataInicio, DateTime dataFin)
        {
            Id = id;
            IdUsuario = idUsuario;
            Descripcion = descripcion;
            if (ComprobarFechaFi(dataInicio, dataFin))
            {
                DataInicio = dataInicio;
                DataFin = dataFin;
            }
            else
                throw new Exception("Fecha de inicio mayor que la fecha de fin");
        }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
            }
        }

        public int IdUsuario
        {
            get { return idUsuario; }
            set
            {
                idUsuario = value;
            }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                descripcion = value;
            }
        }

        public DateTime DataInicio
        {
            get { return dataInicio; }
            set
            {
                dataInicio = value;
            }
        }

        public DateTime DataFin
        {
            get { return dataFin; }
            set
            {
                dataFin = value;
            }
        }

        /// <summary>
        /// Comprueba que la fecha de inicio sea menor que la fecha de fin
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public bool ComprobarFechaFi(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio < fechaFin)
                return true;
            else
                return false;
        }
    }
}
