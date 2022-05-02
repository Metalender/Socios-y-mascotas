using System;
using Entidades;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Datos
{
    public class MascotasCSV : IData<Mascota>
    {

        string _file1 = "../../RepositoriosCSV/Mascotas.csv";



        public void Guardar(List<Mascota> misMascotas)
        {
            List<string> data = new() { };
            misMascotas.ForEach(Mascota =>
            {
                var str = $"{Mascota.Nombre},{Mascota.Especie},{Mascota.Edad},{Mascota.DNIPr},{Mascota.enVenta}";
                data.Add(str);

            });
            File.WriteAllLines(_file1, data);
        }
        public List<Mascota> Leer()
        {
            List<Mascota> misMascotas = new();
            var data = File.ReadAllLines(_file1).Where(row => row.Length > 0).ToList();
            data.ForEach(row =>
            {
                var campos = row.Split(",");
                Mascota mascota = new Mascota(
                    Nombre: campos[0],
                    Especie: (TipoEspecie)Enum.Parse((typeof(TipoEspecie)), campos[1]),
                    Edad: int.Parse(campos[2]),
                    DNIPr: campos[3],
                    enVenta: campos[4]
                );
                misMascotas.Add(mascota);
            });

            return misMascotas;
        }


    }
    public class SociosCSV : IData<Socio>
    {
        string _file2 = "../../RepositoriosCSV/Socios.csv";

        public void Guardar(List<Socio> misSocios)
        {
            List<string> data = new() { };
            misSocios.ForEach(Socio =>
            {
                var str = $"{Socio.Nombre},{Socio.DNI},{Socio.Sexo}";
                data.Add(str);
            });
            File.WriteAllLines(_file2, data);
        }
        public List<Socio> Leer()
        {
            List<Socio> misSocios = new();
            var data = File.ReadAllLines(_file2).Where(row => row.Length > 0).ToList();
            data.ForEach(row =>
            {
                var campos = row.Split(",");
                var socio = new Socio(
                    Nombre: campos[0],
                    DNI: campos[1],
                    Sexo: (Sexo)Enum.Parse((typeof(Sexo)), campos[2])
                );
                misSocios.Add(socio);
            });
            return misSocios;
        }

    }


}