using System;
using System.Collections.Generic;
using Entidades;
using App.Systema;
using Datos;
using Consola;


var repoMasc = new MascotasCSV();
var repoSocio = new SociosCSV();
var view = new Vista();
var sistema = new Gestor(repoSocio,repoMasc);
var controlador = new Controlador(view,sistema);
controlador.Run();

