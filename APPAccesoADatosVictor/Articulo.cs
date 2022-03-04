using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos
{
    public class Articulo
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("La descripción está vacía");
                }
                descripcion = value; 
            }
        }

    }
}
