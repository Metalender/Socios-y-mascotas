using System;
using Entidades;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Datos
{
    public interface IData<T>
    {

        public void Guardar(List<T> datos);
        public List<T> Leer();
    }

}