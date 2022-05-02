using System.Net.Mime;
using System;
using Entidades;
using System.Collections.Generic;
using System.Linq;
using App.Systema;




namespace Consola
{
    class Controlador
    {
        private Vista _vista;

        private Gestor _sistema;
        private Dictionary<string, Action> _casosDeUso;

        private Dictionary<string, Action> _usoSocio;

        public Socio aux;

        private Dictionary<string, Action> _usoMascota;


        public Controlador(Vista vista, Gestor businessLogic)
        {
            _vista = vista;
            _sistema = businessLogic;
            _casosDeUso = new Dictionary<string, Action>(){
                {"Dar de alta a un Nuevo Socio", DarDeAltaS},
                {"Dar de baja a un Socio",DarDeBajaS},
                {"Ver lista de Socios",VerSocios},
                {"Dar de alta a una Nueva Mascota",AñadirMascota},
                {"Ver las Mascotas del Club",VerMascotasDelClub},
                {"Ver las Mascotas de cada Socio",VerMascotasSocio},
                {"Comprar Mascota",Comprar},
                {"Poner en venta Mascota",PonerEnVentaMascotaSocio},
                {"Salir",Salir}           

            };
        }
        public void Run()
        {
            _vista.LimpiarPantalla();
            var menu = _casosDeUso.Keys.ToList<String>();

            while (true)
                try
                {
                    _vista.LimpiarPantalla();
                    var key = _vista.TryObtenerElementoDeLista("Bienvenido a tu Club de Mascotas", menu, "Selecciona una opcion ");
                    _vista.Mostrar("");
                    _casosDeUso[key].Invoke();
                    _vista.MostrarYReturn("Pulsa <Return> para continuar");

                }
                catch { return; }
        }
        public void Salir()
        {
            var key = "fin";
             _vista.Mostrar("Gracias\n\nHasta la próxima!!\n\n");
           
           _casosDeUso[key].Invoke();
        }


        private void DarDeAltaS()
        {
            try
            {
                var nombre = _vista.TryObtenerDatoDeTipo<string>("Nombre del Socio");
                var dni = _vista.TryObtenerDatoDeTipo<string>("DNI del Socio");
                var sexo = _vista.TryObtenerDatoDeTipo<Sexo>("Sexo del Socio ( H-M-NB )");
                Socio nuevo = new Socio
                (
                    Nombre: nombre,
                    DNI : dni,
                    Sexo : sexo
                    
                );

                _sistema.NuevoSocio(nuevo);
            }catch (Exception e)
                {
                    _vista.Mostrar($"UC: {e.Message}");
                }
                finally{
                     _vista.Mostrar("Socio añadido!!!\nNo te olvides de añadir sus mascotas .");
                }


        }

        private void DarDeBajaS()
        {
           Socio p;

            p = _vista.TryObtenerElementoDeLista("Socios del Club de Mascotas       ", _sistema.misSocios, "Selecciona un Socio para dar de baja ");     
            if(p.Nombre!="Club"){           
            _sistema.BorrarSocio(p);

            aux=p;

            _usoSocio = new Dictionary<string, Action>(){
                {"Eliminar mascotas", EliminarMascota},
                {"Poner en venta mascotas",PonerEnVentaMascota}       

            };
             var menu2 = _usoSocio.Keys.ToList<String>();
              try
                {
                    _vista.LimpiarPantalla();
                    var key = _vista.TryObtenerElementoDeLista("Que quieres hacer con sus mascotas",menu2,"Selecciona una opcion");
                    _vista.Mostrar("");
                    _usoSocio[key].Invoke();

                }
                catch { return; }
            }else{
                _vista.Mostrar("Este socio no se puede borrar.\nEs la cuenta del Club.");
            }
        }       
        private void VerSocios() 
        {
            _vista.MostrarListaEnumerada<Socio>("Lista De Socios", _sistema.misSocios);

        }
        public void AñadirMascota()
        {
           try
            {
                var nombre = _vista.TryObtenerDatoDeTipo<string>("Nombre de la Mascota ");
                var especie = _vista.TryObtenerElementoDeLista<TipoEspecie>("Tipo de especie ", _vista.EnumToList<TipoEspecie>(),"Selecciona uno ");
                var edad = _vista.TryObtenerDatoDeTipo<string>("Edad de la Mascota ");
                var dni = _vista.TryObtenerDatoDeTipo<string>("DNI del dueño  ");
                var venta = _vista.TryObtenerDatoDeTipo<string>("Esta en venta : True/False ");

                Mascota nueva = new Mascota
                (
                    Nombre: nombre,
                    Especie : especie,
                    Edad : int.Parse(edad),
                    DNIPr : dni,
                    enVenta: venta
                );

                _sistema.NuevaMasc(nueva);
            }catch (Exception e)
                {
                    _vista.Mostrar($"UC: {e.Message}");
                }

        }

        private void VerMascotasSocio() 
        {
             Socio p;

            p = _vista.TryObtenerElementoDeLista("Socios del Club de Mascotas       ", _sistema.misSocios, "Selecciona un Socio para ver sus Mascotas ");   

            _vista.LimpiarPantalla(); 
            if(_sistema.EncontrarMascotasPorDNI(p).Count()==0)
            {
                 _vista.Mostrar("El socio "+p.Nombre+" no tiene Mascotas registradas\n\n");
                
            }else{

                _vista.MostrarListaEnumerada<Mascota>("Lista De Mascotas de "+p.Nombre.ToString(),  _sistema.EncontrarMascotasPorDNI(p));

            }


        }

        private void VerMascotasDelClub()
        {
            _usoMascota = new Dictionary<string, Action>(){
                {"Ver ordenadas por Edad ", OrdenarMascotaEdad},
                {"Ver ordenadas por Especie",OrdenarMascotaEspecie}       

            };
             var menu3 = _usoMascota.Keys.ToList<String>();
              try
                {
                    _vista.LimpiarPantalla();
                    var key = _vista.TryObtenerElementoDeLista("Como quieres ver ordenadas las Mascotas",menu3,"Selecciona una opcion");
                    _vista.Mostrar("");
                    _usoMascota[key].Invoke();


                }
                catch { return; }

        }
        public void OrdenarMascotaEdad()
        {         
            _vista.MostrarListaEnumerada<Mascota>("Mascotas ordenadas por edad ",_sistema.misMascotas.OrderByDescending(mascota => mascota.Edad).ToList());            
        }
        private void OrdenarMascotaEspecie()
        {
            _vista.MostrarListaEnumerada<Mascota>("Mascotas ordenadas por especie ",_sistema.misMascotas.OrderByDescending(mascota => mascota.Especie).ToList());
        }
        private void PonerEnVentaMascota(){
            _sistema.VenderMascota(aux);
            _vista.Mostrar("\nMascotas en venta");
        }
        private void PonerEnVentaMascotaSocio(){
             Socio p;
             Mascota m;

            p = _vista.TryObtenerElementoDeLista("Socios del Club de Mascotas       ", _sistema.misSocios, "Selecciona un Socio ");   
            _vista.LimpiarPantalla(); 
            if(_sistema.EncontrarMascotasPorDNI(p)==null)
            {
                _vista.Mostrar("Este Socio no tine Mascotas");
            }else{

            m =_vista.TryObtenerElementoDeLista("Lista De Mascotas de "+p.Nombre.ToString(),_sistema.EncontrarMascotasPorDNI(p),"Selecciona una mascota");
           
            m.enVenta="true";
            _sistema.ActualizarRepoMasc();
            _vista.Mostrar("\nMascotas en venta");
            }

        }
        private void EliminarMascota(){
            _sistema.BorrarMascotasDeSocio(aux);    
            _vista.Mostrar("Las Mascotas fueron borradas del sistema\nGracias . ");  
        }
        public void Comprar()
        {        
            Mascota masc;
            masc = _vista.TryObtenerElementoDeLista("Lista de Mascotas en venta ", _sistema.VerMascotasEnVenta(), "Selecciona una Mascota que quieras comprar");
            var dniNuevo = _vista.TryObtenerDatoDeTipo<string>("Introduce tu dni");
            if (_sistema.misSocios.Find(socio => dniNuevo.Equals(socio.DNI)) == null)
            {
                _vista.Mostrar("Si quieres comprar un Mascota primero debes hacerte Socio\nGracias");
            }else
            {
                masc.DNIPr = dniNuevo;
                masc.duenio.Nombre = (_sistema.misSocios.Find(socio => dniNuevo.Equals(socio.DNI))).Nombre;
                masc.enVenta = "false";
                _sistema.ActualizarRepoMasc();
            }  
            _vista.Mostrar("\nMascota comprada!!\n");         

        }   
        
        
    }
}