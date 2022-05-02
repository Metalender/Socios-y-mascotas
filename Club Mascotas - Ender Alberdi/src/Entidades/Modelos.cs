using System.IO;
using System.Security.AccessControl;
using System;
using Entidades;


namespace Entidades

{
    
    public enum TipoEspecie
    {
        Perro,
        Gato,
        Pajaro,
        Reptil,
        Roedor,
        Pez,
        Indefinido
    }
    public enum Sexo
    {
        H,
        M,
        NB
    }


    public class Socio 
    {
        public string Nombre { get; set; }
        public string DNI { get; set; }
        public Sexo Sexo { get; set; } 
        public Socio(string Nombre, string DNI, Sexo Sexo )
        {
            this.Nombre = Nombre;
            this.DNI = DNI;
            this.Sexo = Sexo;
        }
        public override string ToString() =>
        $"Nombre: {Nombre} - DNI: {DNI} - Sexo: {Sexo} " ;

    }

    public class Mascota
    {

        public string Nombre { get; set; } 
        public TipoEspecie Especie { get; set; } = TipoEspecie.Indefinido;
        public int Edad { get; set; }
        public string DNIPr { get; set; }
        public Socio duenio { get; set; }
        public string enVenta  { get; set; }


        public Mascota(string Nombre, TipoEspecie Especie, int Edad, string DNIPr, string enVenta)
        {
            this.Nombre = Nombre;
            this.Especie = Especie;
            this.Edad = Edad;
            this.DNIPr = DNIPr;
            this.enVenta=enVenta;           
        }
        public override string ToString() =>
        $"{Nombre} el {Especie} con {Edad} años - Dueño/a: ({duenio.Nombre}) - EnVenta : {enVenta} ";
    }
}
