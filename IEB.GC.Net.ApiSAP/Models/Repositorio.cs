using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IEB.GC.Net.ApiSAP.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;
using IEB.GC.Net.ApiSAP.Models;
using SAP2000v19;
using System.IO;

namespace IEB.GC.Net.ApiSAP.Models
{
    public class Repositorio
    {
        List<Cuerpo> lista = new List<Cuerpo>();
        APISAPEntities1 entidadesBD = new APISAPEntities1();
                
        #region Operaciones BD

        public List<Perfile> Perfiles() //trae la lista de perfiles de la base de datos
        {
            List<Perfile> resultado;

            resultado = entidadesBD.Perfiles.ToList();
            return resultado;

        }


        public List<Perfile> Per(string tension)//trae una lista de perfiles por nivel de tension especificado
        {
            List<Perfile> resultado = entidadesBD.Perfiles.ToList();

            List<Perfile> per = resultado.FindAll(x=>x.Nivel_de_tensio.Equals(tension));


            return per;

        }


        public List<IGrouping<bool, Perfile>> Perfiles(string tension) //trae dos lista de perfiles la [0] con los valores que cumple la condicion y la [1] son diferentes
        {
            var resultado = entidadesBD.Perfiles.ToList();

            var perfiles = resultado.GroupBy(x => x.Nivel_de_tensio.Equals(tension)).ToList();


            return perfiles;

        }

        public Perfile Perfil(int IdPerfil)//trae el perfil con la Id ingresada 
        {
            var resultado = entidadesBD.Perfiles.ToList();

            var perfil = resultado.Find(x => x.ID_perfil == IdPerfil);

            return perfil;
        }

        public CuerposDeElemento Cuerpo(int Idcuerpo)//trae el cuerpol con la Id ingresada 
        {
            var resultado = entidadesBD.CuerposDeElementoes.ToList();

            var Cuerpo = resultado.Find(x => x.Id_Cuerpo == Idcuerpo);

            return Cuerpo;
        }
                
        public void agregarPerfil(Perfile perfil)//crea un nuevo perfil o modifica un perfil existente 
        {

            try
            {
                if (perfil.ID_perfil == 0)
                {
                    entidadesBD.Perfiles.Add(perfil);
                    entidadesBD.SaveChanges();
                }
                else
                {
                    var resultado = entidadesBD.Perfiles.Find(perfil.ID_perfil);
                    if (resultado != null)
                    {
                        resultado.Nombre_del_perfil = perfil.Nombre_del_perfil;
                        resultado.Material_del_perfil = perfil.Material_del_perfil;
                        resultado.Tipo_del_perfil = perfil.Tipo_del_perfil;
                        resultado.Uso_del_perfil = perfil.Uso_del_perfil;
                        resultado.Nivel_de_tensio = perfil.Nivel_de_tensio;
                        resultado.t3___mm_ = perfil.t3___mm_;
                        resultado.t2__mm_ = perfil.t2__mm_;
                        resultado.tf__mm_ = perfil.tf__mm_;
                        resultado.tw__mm_ = perfil.tw__mm_;
                        resultado.t2b__mm_ = perfil.t2b__mm_;
                        resultado.tfb__mm_ = perfil.tfb__mm_;
                        resultado.Area___mm2_ = perfil.Area___mm2_;
                        resultado.Constante_torsional__mm4_ = perfil.Constante_torsional__mm4_;
                        resultado.I33__mm4_ = perfil.I33__mm4_;
                        resultado.I22___mm4_ = perfil.I22___mm4_;
                        resultado.I23___mm4_ = perfil.I23___mm4_;
                        resultado.AS2__mm2_ = perfil.AS2__mm2_;
                        resultado.AS3__mm2_ = perfil.AS3__mm2_;
                        resultado.S33__mm3_ = perfil.S33__mm3_;
                        resultado.S22__mm3_ = perfil.S22__mm3_;
                        resultado.Z33__mm3_ = perfil.Z33__mm3_;
                        resultado.Z22__mm3_ = perfil.Z22__mm3_;
                        resultado.R33__mm_ = perfil.R33__mm_;
                        resultado.R22__mm_ = perfil.R22__mm_;
                        resultado.Lmax__mm_ = perfil.Lmax__mm_;
                        resultado.Lmin__mm_ = perfil.Lmin__mm_;
                        resultado.Rz__mm_ = perfil.Rz__mm_;
                        resultado.Ru__mm_ = perfil.Ru__mm_;

                        entidadesBD.SaveChanges();
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public List<CuerposDeElemento> CuerposElemento(int idElemento)//recupera la lista de cuerpos de columna
        {
            List<CuerposDeElemento> resultado;

            resultado = entidadesBD.CuerposDeElementoes.ToList();

            resultado = resultado.FindAll(x => x.Id_Elemento == idElemento);

            return resultado;
        }

        public void AgregarCuerpoElemento(CuerposDeElemento cuerpo) //crea un nuevo cuerpo o modifica un cuerpo existente 
        {
            try
            {
                if (cuerpo.Id_Cuerpo == 0)
                {
                    entidadesBD.CuerposDeElementoes.Add(cuerpo);
                    entidadesBD.SaveChanges();
                }
                else
                {
                    var resultado = entidadesBD.CuerposDeElementoes.Find(cuerpo.Id_Cuerpo);
                    if (resultado != null)
                    {
                        resultado.Altura = cuerpo.Altura;
                        resultado.Ancho_inferior = cuerpo.Ancho_inferior;
                        resultado.Ancho_superior = cuerpo.Ancho_superior;
                        resultado.Largo_Inferior = cuerpo.Largo_Inferior;
                        resultado.Largo_Superior = cuerpo.Largo_Superior;
                        resultado.Nivel_de_tension = cuerpo.Nivel_de_tension;
                        resultado.TipoDeElemento = cuerpo.TipoDeElemento;
                        resultado.PerfilCierre = cuerpo.PerfilCierre;
                        resultado.PerfilDiagonalCastillete = cuerpo.PerfilDiagonalCastillete;
                        resultado.PerfilDiagonalViga = cuerpo.PerfilDiagonalViga;
                        resultado.PerfilDiagonarl = cuerpo.PerfilDiagonarl;
                        resultado.PerfilMontante = cuerpo.PerfilMontante;
                        resultado.PerfilMontanteCastillete = cuerpo.PerfilMontanteCastillete;
                        resultado.PerfilMontanteViga = cuerpo.PerfilMontanteViga;
                        resultado.Tipo_de_diagonal = cuerpo.Tipo_de_diagonal;
                        resultado.Division_adicional = cuerpo.Division_adicional;
                        resultado.Id_Elemento = cuerpo.Id_Elemento;

                        entidadesBD.SaveChanges();
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }


        }

        public void BorrarCuerpoElemento(int idCuerpo)//borra el cuerpo de la id ingresada
        {
            try
            {
                if (idCuerpo != 0)
                {
                    var resultado = entidadesBD.CuerposDeElementoes.Find(idCuerpo);
                    entidadesBD.CuerposDeElementoes.Remove(resultado);
                    entidadesBD.SaveChanges();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public void agregar_punto(Punto Point)
        {
            try
            {
                string idpoint = Point.Name;
                var resultado = entidadesBD.Puntos.ToList();
                var buscar = resultado.Find(x => x.Name.Equals(idpoint));

                if(buscar==null)
                {
                    Punto nuevoPunto = new Punto();                    
                    nuevoPunto.IdCuerpo = Point.IdCuerpo;
                    nuevoPunto.MyName = Point.MyName;
                    nuevoPunto.Name = Point.Name;
                    nuevoPunto.X = Point.X;
                    nuevoPunto.Y = Point.Y;
                    nuevoPunto.Z = Point.Z;                    

                    entidadesBD.Puntos.Add(nuevoPunto);
                    entidadesBD.SaveChanges();
                }
                else
                {
                    //buscar.Name = Point.Name;
                    buscar.X = Point.X;
                    buscar.Y = Point.Y;
                    buscar.Z = Point.Z;
                    buscar.MyName = Point.MyName;
                    buscar.IdCuerpo = Point.IdCuerpo;
                    entidadesBD.SaveChanges();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }//agrega o modifica puntos en la base de datos 
        #endregion

        public ElemtosSAP abrir_sap2000()
        {
            // inicializacion de sap2000            
            string rutadelprograma;
            rutadelprograma = "C:\\Program Files\\Computers and Structures\\SAP2000 19\\SAP2000.exe";
            //declaracion de objeto como cOAPI
            cOAPI subestacion = null;
            //usar ret para chequear si la funcion retorna bien (ret=0) si falla (ret=no cero)
            int ret = 0;
            //Crear objeto auxiliar API
            cHelper Auxiliar = null;
            try
            {
                Auxiliar = new Helper();
            }

            catch (Exception ex)
            {
                Console.WriteLine("error"+"No se puede crear una instancia del objeto Helper");
                
                //Console.WriteLine("Cannot create an instance of the Helper object");

            }
            //'create an instance of the SapObject from the specified path
            try
            {
                //crear SapObject
                subestacion = Auxiliar.CreateObject(rutadelprograma);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error" + "No se puede iniciar una nueva instancia del programa desde" + rutadelprograma);
                //Console.WriteLine("Cannot start a new instance of the program from " + ProgramPath);

            }
            //iniciar aplicacion SAP2000 
            ret = subestacion.ApplicationStart();
            //crear un modelo objeto sap
            cSapModel subestacionmodelo;
            subestacionmodelo = subestacion.SapModel;
            //iniciar un modelo
            ret = subestacionmodelo.InitializeNewModel((eUnits.kgf_mm_C));
            //crear un modelo en blanco
            ret = subestacionmodelo.File.NewBlank();
            ElemtosSAP resultado=new ElemtosSAP();
            resultado.Objeto = subestacion;
            resultado.Modelo = subestacionmodelo;
            return resultado;
        }

        public void Material_default(cSapModel subestacionmodelo)
        {
            int ret = 0;
            List<Material> Materiales = entidadesBD.Materials.ToList();
            Material Material_Default = Materiales.Find(x => x.Id == 1);
            ret = subestacionmodelo.PropMaterial.SetMaterial(Material_Default.NombreMaterial, eMatType.Steel);
            //asignacion de propiedades isotropicas
            ret = subestacionmodelo.PropMaterial.SetMPIsotropic(Material_Default.NombreMaterial, Material_Default.E_Material, Material_Default.V_Material, Material_Default.T_Material);
            //asignacion de otras propiedades del acero
            ret = subestacionmodelo.PropMaterial.SetOSteel_1(Material_Default.NombreMaterial, Material_Default.Fy_Material, Material_Default.Fu_Material, Material_Default.eFy_Material, Material_Default.eFu_Material, 1, 2, 0.02, 0.14, 0.2, -0.1);
        }

        public void Nuevo_material(Material Material, cSapModel subestacion)
        {
            int ret = 0;
            ret = subestacion.PropMaterial.SetMaterial(Material.NombreMaterial, eMatType.Steel);
            //asignacion de propiedades isotropicas
            ret = subestacion.PropMaterial.SetMPIsotropic(Material.NombreMaterial, Material.E_Material, Material.V_Material,Material.T_Material);
            //asignacion de otras propiedades del acero
            ret = subestacion.PropMaterial.SetOSteel_1(Material.NombreMaterial, Material.Fy_Material, Material.Fu_Material, Material.eFy_Material, Material.eFu_Material, 1, 2, 0.02, 0.14, 0.2, -0.1);
            
        }


        public void Agregar_perfil(Perfile Nombres,cSapModel subestacion)
        {
            double Area = 0, As2 = 0, As3 = 0, Torsion = 0, I22 = 0, I33 = 0, I23 = 0, S22 = 0, S33 = 0, Z22 = 0, Z33 = 0, R22 = 0, R33 = 0, t3 = 0, t2 = 0, EccV2 = 0, EccV3 = 0;
            int ret=0;
            Perfile actualizacion_perfil = new Perfile();
            
            ret = subestacion.PropFrame.SetAngle(Nombres.Nombre_del_perfil.Trim(), Nombres.Material_del_perfil.Trim(), Nombres.t3___mm_, Nombres.t2__mm_, Nombres.tf__mm_, Nombres.tw__mm_); //Montante  //ret = subestacionmodelo.PropFrame.GetSectProps(Nombres.Nombre_del_perfil.Trim(), ref Area, ref As2, ref As3, ref Torsion, ref I22, ref I33, ref S22, ref S33, ref Z22, ref Z33, ref R22, ref R33);                
            ret = subestacion.PropFrame.GetSectProps_1(Nombres.Nombre_del_perfil.Trim(), ref Area, ref As2, ref As3, ref Torsion, ref I22, ref I33, ref I23, ref S22, ref S33, ref Z22, ref Z33, ref R22, ref R33, ref EccV2, ref EccV3);           
            actualizacion_perfil.ID_perfil = Nombres.ID_perfil;
            actualizacion_perfil.Nombre_del_perfil = Nombres.Nombre_del_perfil.Trim();
            actualizacion_perfil.Material_del_perfil = Nombres.Material_del_perfil.Trim();
            actualizacion_perfil.Tipo_del_perfil = "Angle";
            actualizacion_perfil.Uso_del_perfil = null;
            actualizacion_perfil.Nivel_de_tensio = null;
            actualizacion_perfil.t3___mm_ = Nombres.t3___mm_;
            actualizacion_perfil.t2__mm_ = Nombres.t2__mm_;
            actualizacion_perfil.tf__mm_ = Nombres.tf__mm_;
            actualizacion_perfil.tw__mm_ = Nombres.tw__mm_;
            actualizacion_perfil.t2b__mm_ = Nombres.t2b__mm_;
            actualizacion_perfil.tfb__mm_ = Nombres.tfb__mm_;
            actualizacion_perfil.Area___mm2_ = Area;
            actualizacion_perfil.Constante_torsional__mm4_ = Torsion;
            actualizacion_perfil.I33__mm4_ = I33;
            actualizacion_perfil.I22___mm4_ = I22;
            actualizacion_perfil.I23___mm4_ = I23;
            actualizacion_perfil.AS2__mm2_ = As2;
            actualizacion_perfil.AS3__mm2_ = As3;
            actualizacion_perfil.S33__mm3_ = S33;
            actualizacion_perfil.S22__mm3_ = S22;
            actualizacion_perfil.Z33__mm3_ = Z33;
            actualizacion_perfil.Z22__mm3_ = Z22;
            actualizacion_perfil.R33__mm_ = R33;
            actualizacion_perfil.R22__mm_ = R22;
            actualizacion_perfil.Ru__mm_ = Math.Pow((((I22 + I33) / 2) - ((I22 - I33) / 2) * Math.Cos(Math.PI / 2) + I23 * Math.Sin(Math.PI / 2)) / Area, 0.5);
            actualizacion_perfil.Rz__mm_ = Math.Pow((((I22 + I33) / 2) + ((I22 - I33) / 2) * Math.Cos(Math.PI / 2) - I23 * Math.Sin(Math.PI / 2)) / Area, 0.5);
            actualizacion_perfil.Lmax__mm_ = 1;
            actualizacion_perfil.Lmin__mm_ = 1;
            agregarPerfil(actualizacion_perfil);
        }

        public void Perfiles_default(string Nivel_tension, cSapModel subestacion)
        {
            int ret = 0;
            List<Perfile> perfileria = Per(Nivel_tension);
            foreach (Perfile Nombres in perfileria)
            {
                ret = subestacion.PropFrame.SetAngle(Nombres.Nombre_del_perfil.Trim(), Nombres.Material_del_perfil.Trim(), Nombres.t3___mm_, Nombres.t2__mm_, Nombres.tf__mm_, Nombres.tw__mm_);
            }
        }

        public void Borrar_puntos()
        {
            List<Punto> P_principales = new List<Punto>();
            P_principales = entidadesBD.Puntos.ToList();
            foreach (Punto P in P_principales)
            {
                entidadesBD.Puntos.Remove(P);
                entidadesBD.SaveChanges();
            }
        }

        public Ensamble_subestacion crear_cuerpo(CuerposDeElemento cuerpo, Ensamble_subestacion referencia, cSapModel subestacion, int n)
        {

            int ret = 0;
            #region grupos particulares
            ret = subestacion.GroupDef.SetGroup(referencia.Elemento);
            ret = subestacion.GroupDef.SetGroup(referencia.Elemento + referencia.Id);
            #endregion
            #region definicion de perfiles para el cuerpo
            double CcM, CcD, CcMV, CcDV, CcMC, CcDC;
            Perfile Montante, Diagonal, Montante_viga, Diagonal_viga, Montante_Castillete, Diagonal_castillete;
            if (string.IsNullOrWhiteSpace(cuerpo.PerfilMontante))
            {
                Montante = entidadesBD.Perfiles.ToList().Find(x => (x.Nivel_de_tensio.Equals(cuerpo.Nivel_de_tension) && x.Uso_del_perfil.Equals("Montante")));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Montante.Material_del_perfil));
                CcM = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
                Montante.Lmax__mm_ = 150 * Montante.R33__mm_ / 1.2;
                Montante.Lmin__mm_ = 150 * Montante.Rz__mm_ / 1.2;
                entidadesBD.Entry(Montante).State = EntityState.Modified;
                entidadesBD.SaveChanges();

            }
            else
            {
                Montante = entidadesBD.Perfiles.ToList().Find(x => (x.id_subestacion == referencia.id_Subestacion && x.Nombre_del_perfil.Equals(cuerpo.PerfilMontante)));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Montante.Material_del_perfil));
                CcM = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
                Montante.Lmax__mm_ = 150 * Montante.R33__mm_ / 1.2;
                Montante.Lmin__mm_ = 150 * Montante.Rz__mm_ / 1.2;
                entidadesBD.Entry(Montante).State = EntityState.Modified;
                entidadesBD.SaveChanges();
            }

            if (string.IsNullOrWhiteSpace(cuerpo.PerfilDiagonarl))
            {
                Diagonal = entidadesBD.Perfiles.ToList().Find(x => (x.Nivel_de_tensio.Equals(cuerpo.Nivel_de_tension) && x.Uso_del_perfil.Equals("Diagonal")));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Diagonal.Material_del_perfil));
                CcD = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
                Diagonal.Lmax__mm_ = 190 * Diagonal.R33__mm_ ;
                Diagonal.Lmin__mm_ = 190 * Diagonal.Rz__mm_ ;
                entidadesBD.Entry(Diagonal).State = EntityState.Modified;
                entidadesBD.SaveChanges();

            }
            else
            {
                Diagonal = entidadesBD.Perfiles.ToList().Find(x => (x.id_subestacion == referencia.id_Subestacion && x.Nombre_del_perfil.Equals(cuerpo.PerfilDiagonarl)));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Diagonal.Material_del_perfil));
                CcD = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
                Diagonal.Lmax__mm_ = 190 * Diagonal.R33__mm_;
                Diagonal.Lmin__mm_ = 190 * Diagonal.Rz__mm_;
                entidadesBD.Entry(Diagonal).State = EntityState.Modified;
                entidadesBD.SaveChanges();
            }

            if (string.IsNullOrWhiteSpace(cuerpo.PerfilMontanteViga))
            {
                Montante_viga = entidadesBD.Perfiles.ToList().Find(x => (x.Nivel_de_tensio.Equals(cuerpo.Nivel_de_tension) && x.Uso_del_perfil.Equals("Montante Viga")));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Montante_viga.Material_del_perfil));
                CcMV = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
                Montante_viga.Lmax__mm_ = 120 * Montante_viga.R33__mm_;
                Montante_viga.Lmin__mm_ = 120 * Montante_viga.Rz__mm_ ;
                entidadesBD.Entry(Montante_viga).State = EntityState.Modified;
                entidadesBD.SaveChanges();
            }
            else
            {
                Montante_viga = entidadesBD.Perfiles.ToList().Find(x => (x.id_subestacion == referencia.id_Subestacion && x.Nombre_del_perfil.Equals(cuerpo.PerfilMontanteViga)));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Montante_viga.Material_del_perfil));
                CcMV = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
                Montante_viga.Lmax__mm_ = 120 * Montante_viga.R33__mm_ ;
                Montante_viga.Lmin__mm_ = 120 * Montante_viga.Rz__mm_ ;
                entidadesBD.Entry(Montante_viga).State = EntityState.Modified;
                entidadesBD.SaveChanges();
            }

            if (string.IsNullOrWhiteSpace(cuerpo.PerfilDiagonalViga))
            {
                Diagonal_viga = entidadesBD.Perfiles.ToList().Find(x => (x.Nivel_de_tensio.Equals(cuerpo.Nivel_de_tension) && x.Uso_del_perfil.Equals("Diagonal viga")));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Diagonal_viga.Material_del_perfil));
                CcDV = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
                Diagonal_viga.Lmax__mm_ = 190 * Diagonal_viga.R33__mm_;
                Diagonal_viga.Lmin__mm_ = 190 * Diagonal_viga.Rz__mm_;
                entidadesBD.Entry(Diagonal_viga).State = EntityState.Modified;
                entidadesBD.SaveChanges();
            }
            else
            {
                Diagonal_viga = entidadesBD.Perfiles.ToList().Find(x => (x.id_subestacion == referencia.id_Subestacion && x.Nombre_del_perfil.Equals(cuerpo.PerfilDiagonalViga)));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Diagonal_viga.Material_del_perfil));
                CcDV = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
                Diagonal_viga.Lmax__mm_ = 190 * Diagonal_viga.R33__mm_;
                Diagonal_viga.Lmin__mm_ = 190 * Diagonal_viga.Rz__mm_;
                entidadesBD.Entry(Diagonal_viga).State = EntityState.Modified;
                entidadesBD.SaveChanges();
            }

            if (string.IsNullOrWhiteSpace(cuerpo.PerfilMontanteCastillete))
            {
                Montante_Castillete = entidadesBD.Perfiles.ToList().Find(x => (x.Nivel_de_tensio.Equals(cuerpo.Nivel_de_tension) && x.Uso_del_perfil.Equals("Montante Castillete")));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Montante_Castillete.Material_del_perfil));
                CcMC = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
                Montante_Castillete.Lmax__mm_ = 120 * Montante_Castillete.R33__mm_ ;
                Montante_Castillete.Lmin__mm_ = 120 * Montante_Castillete.Rz__mm_ ;
                entidadesBD.Entry(Montante_Castillete).State = EntityState.Modified;
                entidadesBD.SaveChanges();
            }
            else
            {
                Montante_Castillete = entidadesBD.Perfiles.ToList().Find(x => (x.id_subestacion == referencia.id_Subestacion && x.Nombre_del_perfil.Equals(cuerpo.PerfilMontanteCastillete)));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Montante_Castillete.Material_del_perfil));
                CcMC = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
                Montante_Castillete.Lmax__mm_ = 120 * Montante_Castillete.R33__mm_ ;
                Montante_Castillete.Lmin__mm_ = 120 * Montante_Castillete.Rz__mm_ ;
                entidadesBD.Entry(Montante_Castillete).State = EntityState.Modified;
                entidadesBD.SaveChanges();
            }

            if (string.IsNullOrWhiteSpace(cuerpo.PerfilDiagonalCastillete))
            {
                Diagonal_castillete = entidadesBD.Perfiles.ToList().Find(x => (x.Nivel_de_tensio.Equals(cuerpo.Nivel_de_tension) && x.Uso_del_perfil.Equals("Diagonal castillete")));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Diagonal_castillete.Material_del_perfil));
                CcDC = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
                Diagonal_castillete.Lmax__mm_ = 190 * Diagonal_castillete.R33__mm_;
                Diagonal_castillete.Lmin__mm_ = 190 * Diagonal_castillete.Rz__mm_;
                entidadesBD.Entry(Diagonal_castillete).State = EntityState.Modified;
                entidadesBD.SaveChanges();
            }
            else
            {
                Diagonal_castillete = entidadesBD.Perfiles.ToList().Find(x => (x.id_subestacion == referencia.id_Subestacion && x.Nombre_del_perfil.Equals(cuerpo.PerfilDiagonalCastillete)));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Diagonal_castillete.Material_del_perfil));
                CcDC = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
                Diagonal_castillete.Lmax__mm_ = 190 * Diagonal_castillete.R33__mm_;
                Diagonal_castillete.Lmin__mm_ = 190 * Diagonal_castillete.Rz__mm_;
                entidadesBD.Entry(Diagonal_castillete).State = EntityState.Modified;
                entidadesBD.SaveChanges();
            }
            #endregion

            #region PUNTOS PRINCIPALES DEL CUERPO A CREAR
            Punto Point = new Punto();

            Punto A1 = new Punto();
            Punto A2 = new Punto();
            Punto B1 = new Punto();
            Punto B2 = new Punto();
            Punto C1 = new Punto();
            Punto C2 = new Punto();
            Punto D1 = new Punto();
            Punto D2 = new Punto();


            A1.Name = "" + cuerpo.Id_Cuerpo + "A10";
            A1.X = referencia.X - cuerpo.Ancho_inferior * 0.5; ;
            A1.Y = referencia.Y - cuerpo.Largo_Inferior * 0.5;
            A1.Z = referencia.Z;
            A1.MyName = "" + cuerpo.Id_Cuerpo + "A10";
            A1.IdCuerpo = cuerpo.Id_Cuerpo;

            A2.Name = "" + cuerpo.Id_Cuerpo + "A2";
            A2.X = referencia.X - cuerpo.Ancho_superior * 0.5;
            A2.Y = referencia.Y - cuerpo.Largo_Superior * 0.5;
            A2.Z = referencia.Z + cuerpo.Altura;
            A2.MyName = "" + cuerpo.Id_Cuerpo + "A2";
            A2.IdCuerpo = cuerpo.Id_Cuerpo;

            B1.Name = "" + cuerpo.Id_Cuerpo + "B10";
            B1.X = referencia.X + cuerpo.Ancho_inferior * 0.5; ;
            B1.Y = referencia.Y - cuerpo.Largo_Inferior * 0.5;
            B1.Z = referencia.Z;
            B1.MyName = "" + cuerpo.Id_Cuerpo + "B10";
            B1.IdCuerpo = cuerpo.Id_Cuerpo;

            B2.Name = "" + cuerpo.Id_Cuerpo + "B2";
            B2.X = referencia.X + cuerpo.Ancho_superior * 0.5;
            B2.Y = referencia.Y - cuerpo.Largo_Superior * 0.5;
            B2.Z = referencia.Z + cuerpo.Altura;
            B2.MyName = "" + cuerpo.Id_Cuerpo + "B2";
            B2.IdCuerpo = cuerpo.Id_Cuerpo;

            C1.Name = "" + cuerpo.Id_Cuerpo + "C10";
            C1.X = referencia.X + cuerpo.Ancho_inferior * 0.5; ;
            C1.Y = referencia.Y + cuerpo.Largo_Inferior * 0.5;
            C1.Z = referencia.Z;
            C1.MyName = "" + cuerpo.Id_Cuerpo + "C10";
            C1.IdCuerpo = cuerpo.Id_Cuerpo;

            C2.Name = "" + cuerpo.Id_Cuerpo + "C2";
            C2.X = referencia.X + cuerpo.Ancho_superior * 0.5;
            C2.Y = referencia.Y + cuerpo.Largo_Superior * 0.5;
            C2.Z = referencia.Z + cuerpo.Altura;
            C2.MyName = "" + cuerpo.Id_Cuerpo + "C2";
            C2.IdCuerpo = cuerpo.Id_Cuerpo;

            D1.Name = "" + cuerpo.Id_Cuerpo + "D10";
            D1.X = referencia.X - cuerpo.Ancho_inferior * 0.5; ;
            D1.Y = referencia.Y + cuerpo.Largo_Inferior * 0.5;
            D1.Z = referencia.Z;
            D1.MyName = "" + cuerpo.Id_Cuerpo + "D10";
            D1.IdCuerpo = cuerpo.Id_Cuerpo;

            D2.Name = "" + cuerpo.Id_Cuerpo + "D2";
            D2.X = referencia.X - cuerpo.Ancho_superior * 0.5;
            D2.Y = referencia.Y + cuerpo.Largo_Superior * 0.5;
            D2.Z = referencia.Z + cuerpo.Altura;
            D2.MyName = "" + cuerpo.Id_Cuerpo + "D2";
            D2.IdCuerpo = cuerpo.Id_Cuerpo;
            #endregion

            double Lm, nd, ld, dx = 0, dy = 0, dz = 0, X, Y, Z, ld1, ld2,Lm1,Lm2,Lm3;
            Lm = Math.Sqrt(Math.Pow(A1.X - A2.X, 2) + Math.Pow(A1.Y - A2.Y, 2) + Math.Pow(A1.Z - A2.Z, 2));
            
            double crxrz, crxrzd;

            #region longitud de montantes y diagonales
            if (cuerpo.TipoDeElemento.Equals("Cuerpo de castillete"))
            {
                crxrz = Montante_Castillete.Rz__mm_.Value / Montante_Castillete.R33__mm_.Value;
                crxrzd = Diagonal_castillete.Rz__mm_.Value / Diagonal_castillete.R33__mm_.Value;
                if (cuerpo.Tipo_de_diagonal.Equals("X"))
                {
                    Lm3 = 250 * Montante_Castillete.Rz__mm_.Value;
                    ld1 = 190 * Diagonal_castillete.Rz__mm_.Value / crxrzd;
                    ld2 = 190 * Diagonal_castillete.Rz__mm_.Value ;
                    ld = Math.Min(ld1, ld2);
                    nd = Math.Round((Lm / Lm3) + 0.49);
                    double a, b, a1, b1, a2, b2, L11, L21, L31, L41, L12, L22, L32, L42, Dx1, Dx2, Dy1, Dy2, Dz1, Dz2, X1, X2, Y1, Y2, Z1, Z2, K11, K21, K12, K22;
                    a = 201;
                    while (a >= 200)
                    {
                        dx = Math.Abs(A1.X - A2.X) / (nd);
                        dy = Math.Abs(A1.Y - A2.Y) / (nd);
                        dz = Math.Abs(A1.Z - A2.Z) / (nd);
                        X = A1.X + dx;
                        Y = A1.Y + dy;
                        Z = A1.Z + dz;
                        Dx1 = (X - B1.X);
                        Dy1 = (Y - B1.Y);
                        Dz1 = (Z - B1.Z);
                        Dx2 = (X - D1.X);
                        Dy2 = (Y - D1.Y);
                        Dz2 = (Z - D1.Z);
                        X1 = referencia.X;
                        Y1 = ((X1 - X) / Dx1) * Dy1 + Y;
                        Z1 = ((X1 - X) / Dx1) * Dz1 + Z;
                        Y2 = referencia.Y;
                        X2 = ((Y2 - Y) / Dy2) * Dx2 + X;
                        Z2 = ((Y2 - Y) / Dy2) * Dz2 + Z;
                        L11 = Math.Sqrt(Math.Pow(B1.X - X, 2) + Math.Pow(B1.Y - Y, 2) + Math.Pow(B1.Z - Z, 2));
                        L12 = Math.Sqrt(Math.Pow(D1.X - X, 2) + Math.Pow(D1.Y - Y, 2) + Math.Pow(D1.Z - Z, 2));
                        L21 = Math.Sqrt(Math.Pow(B1.X - X1, 2) + Math.Pow(B1.Y - Y1, 2) + Math.Pow(B1.Z - Z1, 2));
                        L22 = Math.Sqrt(Math.Pow(D1.X - X2, 2) + Math.Pow(D1.Y - Y2, 2) + Math.Pow(D1.Z - Z2, 2));
                        L31 = Math.Sqrt(Math.Pow(X1 - X, 2) + Math.Pow(Y1 - Y, 2) + Math.Pow(Z1 - Z, 2));
                        L32 = Math.Sqrt(Math.Pow(X2 - X, 2) + Math.Pow(Y2 - Y, 2) + Math.Pow(Z2 - Z, 2));
                        L41 = L21 + L31;
                        L42 = L22 + L32;
                        K11 = L11 / L21;
                        K21 = Diagonal_castillete.Rz__mm_.Value / Diagonal_castillete.R33__mm_.Value;
                        K12 = L12 / L22;
                        K22 = Diagonal_castillete.Rz__mm_.Value / Diagonal_castillete.R33__mm_.Value;
                        a1 = (L11 / Diagonal_castillete.Rz__mm_.Value) * Math.Max(K21, (1 / K11));
                        a2 = (L12 / Diagonal_castillete.Rz__mm_.Value) * Math.Max(K22, (1 / K12));
                        b1 = (L11 / Diagonal_castillete.Rz__mm_.Value) * Math.Min(K21, (1 / K11));
                        b2 = (L12 / Diagonal_castillete.Rz__mm_.Value) * Math.Min(K22, (1 / K12));
                        b = Math.Min(b1, b2);
                        a = Math.Max(a1, a2);
                        if (a > 200)
                        {
                            nd = nd + 1;
                        }

                    }
                }
                else
                {
                    Lm1 = 250 * Montante_Castillete.Rz__mm_.Value/ crxrz;
                    Lm2 = 250 * Montante_Castillete.Rz__mm_.Value*2;
                    Lm3 = Math.Min(Lm1, Lm2);
                    ld = 190 * Diagonal_castillete.Rz__mm_.Value;
                    nd = Math.Round((Lm / Lm3) + 0.49);
                    double a, a1, a2, L11, L12;
                    a = 201;
                    while (a >= 200)
                    {
                        dx = Math.Abs(A1.X - A2.X) / (nd);
                        dy = Math.Abs(A1.Y - A2.Y) / (nd);
                        dz = Math.Abs(A1.Z - A2.Z) / (nd);
                        X = A1.X + dx;
                        Y = A1.Y + dy;
                        Z = A1.Z + dz;
                        L11 = Math.Sqrt(Math.Pow(B1.X - X, 2) + Math.Pow(B1.Y - Y, 2) + Math.Pow(B1.Z - Z, 2));
                        L12 = Math.Sqrt(Math.Pow(D1.X - X, 2) + Math.Pow(D1.Y - Y, 2) + Math.Pow(D1.Z - Z, 2));
                        a1 = (L11 / Diagonal_castillete.Rz__mm_.Value);
                        a2 = (L12 / Diagonal_castillete.Rz__mm_.Value);
                        a = Math.Max(a1, a2);
                        if (a > 200)
                        {
                            nd = nd + 1;
                        }

                    }



                }

                if (nd==1)
                {
                    nd = 2;
                }
            }
            else
            {
                crxrz = Montante.Rz__mm_.Value / Montante.R33__mm_.Value;
                crxrzd = Diagonal.Rz__mm_.Value / Diagonal.R33__mm_.Value;
                if (cuerpo.Tipo_de_diagonal.Equals("X"))
                {
                    Lm3 = 150 * Montante.Rz__mm_.Value/1.2;
                    ld1 = 190 * Diagonal.Rz__mm_.Value / crxrzd;
                    ld2 = 190 * Diagonal.Rz__mm_.Value;
                    ld = Math.Min(ld1, ld2);
                    nd = Math.Round((Lm / Lm3) + 0.49);
                    double a, b, a1, b1, a2, b2, L11, L21, L31, L41, L12, L22, L32, L42, Dx1, Dx2, Dy1, Dy2, Dz1, Dz2, X1, X2, Y1, Y2, Z1, Z2, K11, K21, K12, K22;
                    a = 201;
                    while (a >= 200)
                    {
                        dx = Math.Abs(A1.X - A2.X) / (nd);
                        dy = Math.Abs(A1.Y - A2.Y) / (nd);
                        dz = Math.Abs(A1.Z - A2.Z) / (nd);
                        X = A1.X + dx;
                        Y = A1.Y + dy;
                        Z = A1.Z + dz;
                        Dx1 = (X - B1.X);
                        Dy1 = (Y - B1.Y);
                        Dz1 = (Z - B1.Z);
                        Dx2 = (X - D1.X);
                        Dy2 = (Y - D1.Y);
                        Dz2 = (Z - D1.Z);
                        X1 = referencia.X;
                        Y1 = ((X1 - X) / Dx1) * Dy1 + Y;
                        Z1 = ((X1 - X) / Dx1) * Dz1 + Z;
                        Y2 = referencia.Y;
                        X2 = ((Y2 - Y) / Dy2) * Dx2 + X;
                        Z2 = ((Y2 - Y) / Dy2) * Dz2 + Z;
                        L11 = Math.Sqrt(Math.Pow(B1.X - X, 2) + Math.Pow(B1.Y - Y, 2) + Math.Pow(B1.Z - Z, 2));
                        L12 = Math.Sqrt(Math.Pow(D1.X - X, 2) + Math.Pow(D1.Y - Y, 2) + Math.Pow(D1.Z - Z, 2));
                        L21 = Math.Sqrt(Math.Pow(B1.X - X1, 2) + Math.Pow(B1.Y - Y1, 2) + Math.Pow(B1.Z - Z1, 2));
                        L22 = Math.Sqrt(Math.Pow(D1.X - X2, 2) + Math.Pow(D1.Y - Y2, 2) + Math.Pow(D1.Z - Z2, 2));
                        L31 = Math.Sqrt(Math.Pow(X1 - X, 2) + Math.Pow(Y1 - Y, 2) + Math.Pow(Z1 - Z, 2));
                        L32 = Math.Sqrt(Math.Pow(X2 - X, 2) + Math.Pow(Y2 - Y, 2) + Math.Pow(Z2 - Z, 2));
                        L41 = L21 + L31;
                        L42 = L22 + L32;
                        K11 = L11 / L21;
                        K21 = Diagonal.Rz__mm_.Value / Diagonal.R33__mm_.Value;
                        K12 = L12 / L22;
                        K22 = Diagonal.Rz__mm_.Value / Diagonal.R33__mm_.Value;
                        a1 = (L11 / Diagonal.Rz__mm_.Value) * Math.Max(K21, (1 / K11));
                        a2 = (L12 / Diagonal.Rz__mm_.Value) * Math.Max(K22, (1 / K12));
                        b1 = (L11 / Diagonal.Rz__mm_.Value) * Math.Min(K21, (1 / K11));
                        b2 = (L12 / Diagonal.Rz__mm_.Value) * Math.Min(K22, (1 / K12));
                        b = Math.Min(b1, b2);
                        a = Math.Max(a1, a2);
                        if (a > 200)
                        {
                            nd = nd + 1;
                        }

                    }

                    
                }
                else
                {
                    Lm1 = 150 * Montante.Rz__mm_.Value / (crxrz*1.2);
                    Lm2 = 150 * Montante.Rz__mm_.Value * 2/1.2;
                    Lm3 = Math.Min(Lm1, Lm2);
                    ld = 190 * Diagonal.Rz__mm_.Value;
                    nd = Math.Round((Lm / Lm3) + 0.49);

                    double a, a1, a2, L11, L12;
                    a = 201;
                    while (a >= 200)
                    {
                        dx = Math.Abs(A1.X - A2.X) / (nd);
                        dy = Math.Abs(A1.Y - A2.Y) / (nd);
                        dz = Math.Abs(A1.Z - A2.Z) / (nd);
                        X = A1.X + dx;
                        Y = A1.Y + dy;
                        Z = A1.Z + dz;                        
                        L11 = Math.Sqrt(Math.Pow(B1.X - X, 2) + Math.Pow(B1.Y - Y, 2) + Math.Pow(B1.Z - Z, 2));
                        L12 = Math.Sqrt(Math.Pow(D1.X - X, 2) + Math.Pow(D1.Y - Y, 2) + Math.Pow(D1.Z - Z, 2));     
                        a1 = (L11 / Diagonal.Rz__mm_.Value);
                        a2 = (L12 / Diagonal.Rz__mm_.Value);
                        a = Math.Max(a1,a2);
                        if(a>200)
                        {
                            nd = nd + 1;
                        }

                    }

                }
            }
             #endregion
            
           
            int i = 0;
            
            dx = Math.Abs(A1.X - A2.X) / (nd + cuerpo.Division_adicional);
            dy = Math.Abs(A1.Y - A2.Y) / (nd + cuerpo.Division_adicional);
            dz = Math.Abs(A1.Z - A2.Z) / (nd + cuerpo.Division_adicional);

            nd = nd + cuerpo.Division_adicional;
            while (i < nd  + 1)
            {
                Point.Name = "" + referencia.Id + "A1" + (i + referencia.Divisiones);
                Point.X = A1.X + dx * (i);
                Point.Y = A1.Y + dy * (i);
                Point.Z = A1.Z + dz * (i);
                Point.MyName = "" + referencia.Id + "A1" + (i + referencia.Divisiones);
                Point.IdCuerpo = cuerpo.Id_Cuerpo;
                agregar_punto(Point);

                Point.Name = "" + referencia.Id + "B1" + (i + referencia.Divisiones);
                Point.X = B1.X - dx * (i);
                Point.Y = B1.Y + dy * (i);
                Point.Z = B1.Z + dz * (i);
                Point.MyName = "" + referencia.Id + "B1" + (i + referencia.Divisiones);
                Point.IdCuerpo = cuerpo.Id_Cuerpo;
                agregar_punto(Point);

                Point.Name = "" + referencia.Id + "C1" + (i + referencia.Divisiones);
                Point.X = C1.X - dx * (i);
                Point.Y = C1.Y - dy * (i);
                Point.Z = C1.Z + dz * (i);
                Point.MyName = "" + referencia.Id + "C1" + (i + referencia.Divisiones);
                Point.IdCuerpo = cuerpo.Id_Cuerpo;
                agregar_punto(Point);

                Point.Name = "" + referencia.Id + "D1" + (i + referencia.Divisiones);
                Point.X = D1.X + dx * (i);
                Point.Y = D1.Y - dy * (i);
                Point.Z = D1.Z + dz * (i);
                Point.MyName = "" + referencia.Id + "D1" + (i + referencia.Divisiones);
                Point.IdCuerpo = cuerpo.Id_Cuerpo;
                agregar_punto(Point);
                i++;
            }//creacion de puntos 
            List<Punto> P_principales = new List<Punto>();
            P_principales = entidadesBD.Puntos.AsNoTracking().ToList();
            Borrar_puntos();
            foreach (Punto P in P_principales)
            {
                string name = P.Name.ToString();//debido al error no me dejaba pasar un parametro q era llave, 
                                                //entonces asigne el valor a una variable temporal
                ret = subestacion.PointObj.AddCartesian(P.X, P.Y, P.Z, ref name, P.MyName);//revisae error de ref P.Name 
                                                                                           //-> La variable name es la de arriba
            }
            i = 0;

            while (i < 4)
            {
                double ang = 0;

                string c1 = "";
                if (i == 0)
                {
                    c1 = "A";
                    ang = 90;

                }
                else if (i == 1)
                {
                    c1 = "B";
                    ang = 180;
                }
                else if (i == 2)
                {
                    c1 = "C";
                    ang = 270;
                }
                else if (i == 3)
                {
                    c1 = "D";
                    ang = 0;
                }
                int j = 0;

                while (j < nd)
                {
                    string P2, P1;

                    if (cuerpo.Tipo_de_diagonal.Equals("X"))
                    {
                        P1 = "" + referencia.Id + c1 + "1" + (referencia.Divisiones + j);
                        P2 = "" + referencia.Id + c1 + "1" + (referencia.Divisiones + j + 1);

                        if (cuerpo.TipoDeElemento.Equals("Cuerpo de castillete"))
                        {
                            if (j >= nd || j + 2 >= nd)
                            {
                                P2 = "" + referencia.Id + "D1" + (nd + referencia.Divisiones);
                            }
                            else
                            {
                                P2 = "" + referencia.Id + c1 + "1" + (referencia.Divisiones + j + 2);
                            }

                        }//definicion del punto final del castillete
                        string M = "" + referencia.Id + "M" + (i + 1 + referencia.Divisiones) + j;

                        if (cuerpo.TipoDeElemento.Equals("Cuerpo de castillete"))
                        {
                            ret = subestacion.FrameObj.AddByPoint(P1, P2, ref M, Montante_Castillete.Nombre_del_perfil, M);
                            #region asignacion de K
                            double L11, Kmajor, Kminor, Lr, KLr;
                            Punto Pun1 = P_principales.Find(x => x.Name.Equals(P1));
                            Punto Pun2 = P_principales.Find(x => x.Name.Equals(P2));                            
                            L11 = Math.Sqrt(Math.Pow(Pun1.X - Pun2.X, 2) + Math.Pow(Pun1.Y - Pun2.Y, 2) + Math.Pow(Pun1.Z - Pun2.Z, 2));

                            Lr = (L11 / Montante_Castillete.Rz__mm_.Value);
                            if (Lr <= 120)
                            {
                                Kmajor = 1;
                                Kminor = 1;

                            }
                            else if (Lr > 120 && Lr < 250)
                            {
                                KLr = 46.2 + 0.615 * Lr;
                                Kmajor = KLr / Lr;
                                Kminor = KLr / Lr;

                            }
                            else
                            {
                                Kmajor = 0;
                                Kminor = 0;
                            }
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 17, 0.99);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 18, 0.99);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 19, Kmajor);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 20, Kminor);

                            #endregion
                            ret = subestacion.FrameObj.SetLocalAxes(M, 225);
                            ret = subestacion.FrameObj.SetGroupAssign(M, "Montante Castillete");
                            ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento);
                            ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento + referencia.Id);
                        }
                        else
                        {
                            ret = subestacion.FrameObj.AddByPoint(P1, P2, ref M, Montante.Nombre_del_perfil, M);
                            #region asignacion de K
                            double  L11, Kmajor, Kminor, Lr, KLr;
                            Punto Pun1 = P_principales.Find(x => x.Name.Equals(P1));
                            Punto Pun2 = P_principales.Find(x => x.Name.Equals(P2));                           
                            L11 = Math.Sqrt(Math.Pow(Pun1.X - Pun2.X, 2) + Math.Pow(Pun1.Y - Pun2.Y, 2) + Math.Pow(Pun1.Z - Pun2.Z, 2));
                            Lr = ( L11 / Montante.Rz__mm_.Value);
                            if (Lr <= 150)
                            {
                                Kmajor = 1.2;
                                Kminor = 1.2;

                            }
                            else if (Lr > 150 && Lr < 250)
                            {
                                KLr = 46.2 + 0.615 * Lr;
                                Kmajor = KLr / Lr;
                                Kminor = KLr / Lr;

                            }
                            else
                            {
                                Kmajor = 0;
                                Kminor = 0;
                            }
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 17, 0.99);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 18, 0.99);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 19, Kmajor);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 20, Kminor);

                            #endregion
                            if (cuerpo.Ancho_inferior != cuerpo.Ancho_superior || cuerpo.Largo_Inferior != cuerpo.Largo_Superior)
                            {
                                ret = subestacion.FrameObj.SetLocalAxes(M, 225);
                            }
                            else
                            {
                                ret = subestacion.FrameObj.SetLocalAxes(M, ang);
                            }
                            ret = subestacion.FrameObj.SetGroupAssign(M, "Montante");
                            ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento);
                            ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento + referencia.Id);
                        }
                        j = j + 1;

                    }//montantes para diagonales en X
                    else
                    {
                        P1 = "" + referencia.Id + c1 + "1" + (referencia.Divisiones + j);

                        if (nd == 1 || j + 1 == nd || j + 2 >= nd)
                        {
                            P2 = "" + referencia.Id + c1 + "1" + (referencia.Divisiones + nd);
                        }//definicion de punto final de montante del ultimo tramo
                        else
                        {
                            P2 = "" + referencia.Id + c1 + "1" + (referencia.Divisiones + j + 2);
                        }//definicion de punto final de montante difernete a la del ultimo tramo


                        if (cuerpo.TipoDeElemento.Equals("Cuerpo de castillete"))
                        {
                            if (j >= nd || j + 2 >= nd)
                            {
                                P2 = "" + referencia.Id + "D1" + (nd + referencia.Divisiones);
                            }
                            else
                            {
                                P2 = "" + referencia.Id + c1 + "1" + (referencia.Divisiones + j + 2);
                            }

                        }//definicion del punto final del castillete
                        string M = "" + referencia.Id + "M" + (i + 1 + referencia.Divisiones) + j;


                        if (cuerpo.TipoDeElemento.Equals("Cuerpo de castillete"))//montantes de columnas del caestillete
                        {
                            ret = subestacion.FrameObj.AddByPoint(P1, P2, ref M, Montante_Castillete.Nombre_del_perfil, M);
                            #region asignacion de K
                            double a, b, a1, b1, a2, b2, L11, L21, L31, L41, L12, L22, L32, L42, Dx1, Dx2, Dy1, Dy2, Dz1, Dz2, X1, X2, Y1, Y2, Z1, Z2, K11, K21, K12, K22,Kmajor,Kminor,Lr,KLr;
                            Punto Pun1 = P_principales.Find(x => x.Name.Equals(P1));
                            Punto Pun2 = P_principales.Find(x => x.Name.Equals(P2));
                            K21 = Montante_Castillete.Rz__mm_.Value / Montante_Castillete.R33__mm_.Value;
                            L11 = Math.Sqrt(Math.Pow(Pun1.X - Pun2.X, 2) + Math.Pow(Pun1.Y - Pun2.Y, 2) + Math.Pow(Pun1.Z - Pun2.Z, 2));
                            L21 = L11 * 0.5;
                            if(K21>0.5)
                            {
                                Lr = (K21 * L11 / Montante_Castillete.Rz__mm_.Value);

                                if (Lr <= 120)
                                {
                                    Kmajor = 1* K21;
                                    Kminor = 1* K21;

                                }
                                else if(Lr > 120 && Lr<250)
                                {
                                    KLr = 46.2 + 0.615 * Lr;
                                    Kmajor = KLr* K21 / Lr;
                                    Kminor = KLr* K21 / Lr;

                                }
                                else
                                {
                                    Kmajor = 0;
                                    Kminor = 0;
                                }
                            }
                            else
                            {
                                Lr = (0.5 * L11 / Montante_Castillete.Rz__mm_.Value);
                                if (Lr <= 120)
                                {
                                    Kmajor = 1*0.5;
                                    Kminor = 1*0.5;

                                }
                                else if (Lr > 120 && Lr < 250)
                                {
                                    KLr = 46.2 + 0.615 * Lr;
                                    Kmajor = (KLr / Lr)*0.5;
                                    Kminor = (KLr / Lr)*0.5;

                                }
                                else
                                {
                                    Kmajor = 0;
                                    Kminor = 0;

                                }
                            }
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 17, 0.99);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 18, 0.99);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 19, Kmajor);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 20, Kminor);

                            #endregion
                            ret = subestacion.FrameObj.SetLocalAxes(M, 225);
                            ret = subestacion.FrameObj.SetGroupAssign(M, "Montante Castillete");
                            ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento);
                            ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento + referencia.Id);
                        }
                        else
                        {
                            ret = subestacion.FrameObj.AddByPoint(P1, P2, ref M, Montante.Nombre_del_perfil, M);
                            #region asignacion de K
                            double a, b, a1, b1, a2, b2, L11, L21, L31, L41, L12, L22, L32, L42, Dx1, Dx2, Dy1, Dy2, Dz1, Dz2, X1, X2, Y1, Y2, Z1, Z2, K11, K21, K12, K22, Kmajor, Kminor, Lr, KLr;
                            Punto Pun1 = P_principales.Find(x => x.Name.Equals(P1));
                            Punto Pun2 = P_principales.Find(x => x.Name.Equals(P2));
                            K21 = Montante.Rz__mm_.Value / Montante.R33__mm_.Value;
                            L11 = Math.Sqrt(Math.Pow(Pun1.X - Pun2.X, 2) + Math.Pow(Pun1.Y - Pun2.Y, 2) + Math.Pow(Pun1.Z - Pun2.Z, 2));
                            L21 = L11 * 0.5;
                            if (K21 > 0.5)
                            {
                                Lr = (K21 * L11 / Montante.Rz__mm_.Value);

                                if (Lr <= 150)
                                {
                                    Kmajor = 1.2* K21;
                                    Kminor = 1.2* K21;

                                }
                                else if (Lr > 150 && Lr < 250)
                                {
                                    KLr = 46.2 + 0.615 * Lr;
                                    Kmajor = KLr* K21 / Lr;
                                    Kminor = KLr * K21 / Lr;

                                }
                                else
                                {
                                    Kmajor = 0;
                                    Kminor = 0;
                                }
                            }
                            else
                            {
                                Lr = (0.5 * L11 / Montante.Rz__mm_.Value);
                                if (Lr <= 150)
                                {
                                    Kmajor = 1 * 0.5;
                                    Kminor = 1 * 0.5;

                                }
                                else if (Lr > 150 && Lr < 250)
                                {
                                    KLr = 46.2 + 0.615 * Lr;
                                    Kmajor = (KLr / Lr) * 0.5;
                                    Kminor = (KLr / Lr) * 0.5;

                                }
                                else
                                {
                                    Kmajor = 0;
                                    Kminor = 0;

                                }
                            }
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 17, 0.99);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 18, 0.99);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 19, Kmajor);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 20, Kminor);

                            #endregion
                            if (cuerpo.Ancho_inferior != cuerpo.Ancho_superior || cuerpo.Largo_Inferior != cuerpo.Largo_Superior)
                            {
                                ret = subestacion.FrameObj.SetLocalAxes(M, 225);
                            }//rotacion de ejes si es seccion variable
                            else
                            {
                                ret = subestacion.FrameObj.SetLocalAxes(M, ang);
                            }//rotacion de ejes si es seccion constante
                            ret = subestacion.FrameObj.SetGroupAssign(M, "Montante");
                            ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento);
                            ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento + referencia.Id);
                        }//montantes de columnas diferentes al castillete
                        j = j + 2;


                    }//montantes para diagonales en Z

                }



                i++;
            }//creacion de montantes
            i = 0;
            while (i < 4)
            {
                string c1 = "";
                string c2 = "";
                string P1 = "";
                string P2 = "";
                string P3 = "";
                string P4 = "";
                string D = "";
                string Dx = "";
                double angulo = 0;
                if (i == 0)
                {
                    c1 = "A";
                    c2 = "B";
                    if (cuerpo.Ancho_inferior != cuerpo.Ancho_superior || cuerpo.Largo_Inferior != cuerpo.Largo_Superior)
                    {
                        angulo = Math.Atan(cuerpo.Altura / (cuerpo.Largo_Inferior - cuerpo.Largo_Superior)) * 180 / Math.PI;
                    }


                }
                else if (i == 1)
                {
                    c1 = "B";
                    c2 = "C";
                    if (cuerpo.Ancho_inferior != cuerpo.Ancho_superior || cuerpo.Largo_Inferior != cuerpo.Largo_Superior)
                    {
                        angulo = Math.Atan(cuerpo.Altura / (cuerpo.Ancho_inferior - cuerpo.Ancho_superior)) * 180 / Math.PI;
                    }
                }
                else if (i == 2)
                {
                    c1 = "C";
                    c2 = "D";
                    if (cuerpo.Ancho_inferior != cuerpo.Ancho_superior || cuerpo.Largo_Inferior != cuerpo.Largo_Superior)
                    {
                        angulo = Math.Atan(cuerpo.Altura / (cuerpo.Largo_Inferior - cuerpo.Largo_Superior)) * 180 / Math.PI;
                    }
                }
                else if (i == 3)
                {
                    c1 = "D";
                    c2 = "A";
                    if (cuerpo.Ancho_inferior != cuerpo.Ancho_superior || cuerpo.Largo_Inferior != cuerpo.Largo_Superior)
                    {
                        angulo = Math.Atan(cuerpo.Altura / (cuerpo.Ancho_inferior - cuerpo.Ancho_superior)) * 180 / Math.PI;
                    }
                }
                double j = 0;
                while (j < nd)
                {

                    if ((j % 2) == 0)
                    {
                        P1 = "" + referencia.Id + c1 + "" + 1 + "" + (j + referencia.Divisiones);
                        P2 = "" + referencia.Id + c2 + "" + 1 + "" + (j + 1 + referencia.Divisiones);
                        if (cuerpo.TipoDeElemento.Equals("Cuerpo de castillete") && j == nd - 1)
                        {
                            P2 = "" + referencia.Id + c2 + "" + 1 + "" + (j + referencia.Divisiones);
                        }
                        D = "" + referencia.Id + "D" + i + (j + 1 + referencia.Divisiones);
                        if (cuerpo.Tipo_de_diagonal.Equals("X"))
                        {
                            P3 = "" + referencia.Id + c2 + "" + 1 + "" + (j + referencia.Divisiones);
                            P4 = "" + referencia.Id + c1 + "" + 1 + "" + (j + 1 + referencia.Divisiones);
                            if (cuerpo.TipoDeElemento.Equals("Cuerpo de castillete") && j == nd - 1)
                            {
                                P4 = "" + referencia.Id + c2 + "" + 1 + "" + (j + referencia.Divisiones);
                            }
                            Dx = "" + referencia.Id + "D" + i + (j + 1 + referencia.Divisiones) + 2;

                        }

                    }
                    else
                    {
                        P1 = "" + referencia.Id + c2 + "" + 1 + "" + (j + referencia.Divisiones);
                        P2 = "" + referencia.Id + c1 + "" + 1 + "" + (j + 1 + referencia.Divisiones);
                        if (cuerpo.TipoDeElemento.Equals("Cuerpo de castillete") && j == nd - 1)
                        {
                            P2 = "" + referencia.Id + c1 + "" + 1 + "" + (j + referencia.Divisiones);
                        }
                        D = "" + referencia.Id + "D" + i + (j + 1 + referencia.Divisiones);
                        if (cuerpo.Tipo_de_diagonal.Equals("X"))
                        {
                            P3 = "" + referencia.Id + c1 + "" + 1 + "" + (j + referencia.Divisiones);
                            P4 = "" + referencia.Id + c2 + "" + 1 + "" + (j + 1 + referencia.Divisiones);
                            if (cuerpo.TipoDeElemento.Equals("Cuerpo de castillete"))
                            {
                                P4 = "" + referencia.Id + c1 + "" + 1 + "" + (j + referencia.Divisiones);
                            }
                            Dx = "" + referencia.Id + "D" + i + (j + 1 + referencia.Divisiones) + 2;


                        }
                    }

                    if (cuerpo.TipoDeElemento.Equals("Cuerpo de castillete"))
                    {
                        bool[] II = new bool[6];
                        bool[] JJ = new bool[6];
                        double[] Valorinicio = new double[6];
                        double[] Valorfinal = new double[6];
                        eItemType tipo = eItemType.Objects;
                        II[3] = true;
                        II[4] = true;
                        II[5] = true;
                        JJ[4] = true;
                        JJ[5] = true;

                        ret = subestacion.FrameObj.AddByPoint(P1, P2, ref D, Diagonal_castillete.Nombre_del_perfil, D);
                        ret = subestacion.FrameObj.SetReleases(D, ref II, ref JJ, ref Valorinicio, ref Valorfinal, tipo);
                        
                        if (cuerpo.Ancho_inferior != cuerpo.Ancho_superior || cuerpo.Largo_Inferior != cuerpo.Largo_Superior)
                        {
                            if ((j % 2) == 0)
                            {
                                ret = subestacion.FrameObj.SetLocalAxes(D, angulo);
                            }
                            else
                            {
                                ret = subestacion.FrameObj.SetLocalAxes(D, angulo + 45);
                            }

                        }
                        ret = subestacion.FrameObj.SetGroupAssign(D, "Diagonal castillete");
                        ret = subestacion.FrameObj.SetGroupAssign(D, referencia.Elemento);
                        ret = subestacion.FrameObj.SetGroupAssign(D, referencia.Elemento + referencia.Id);
                        if (cuerpo.Tipo_de_diagonal.Equals("X"))
                        {
                            ret = subestacion.FrameObj.AddByPoint(P3, P4, ref Dx, Diagonal_castillete.Nombre_del_perfil, Dx);
                            if (cuerpo.Ancho_inferior != cuerpo.Ancho_superior || cuerpo.Largo_Inferior != cuerpo.Largo_Superior)
                            {
                                if ((j % 2) == 0)
                                {
                                    ret = subestacion.FrameObj.SetLocalAxes(Dx, (90 - angulo + 180));
                                }
                                else
                                {
                                    ret = subestacion.FrameObj.SetLocalAxes(Dx, (angulo));
                                }

                            }
                            ret = subestacion.FrameObj.SetGroupAssign(Dx, "Diagonal castillete");
                            ret = subestacion.FrameObj.SetGroupAssign(Dx, referencia.Elemento + referencia.Id);
                        }

                        if (cuerpo.Tipo_de_diagonal.Equals("X"))
                        {
                            #region asignacion de K
                            double a, b, a1, b1, a2, b2, L11, L21, L31, L41, L12, L22, L32, L42, Dx1, Dx2, Dy1, Dy2, Dz1, Dz2, X1=1, X2, Y1=1, Y2, Z1=1, Z2, K11, K21, K12, K22, Kmajor, Kminor, Lr, KLr;
                            Punto Pun1 = P_principales.Find(x => x.Name.Equals(P1));
                            Punto Pun2 = P_principales.Find(x => x.Name.Equals(P2));
                            K21 = Diagonal_castillete.Rz__mm_.Value / Diagonal_castillete.R33__mm_.Value;                            
                            Dx1 = (Pun1.X - Pun2.X);
                            Dy1 = (Pun1.Y - Pun2.Y);
                            Dz1 = (Pun1.Z - Pun2.Z);
                            if(Math.Abs(Dx1) > Math.Abs(Dy1))
                            {
                                X1 = referencia.X;
                                Y1 = ((X1 - Pun1.X) / Dx1) * Dy1 + Pun1.Y;
                                Z1 = ((X1 - Pun1.X) / Dx1) * Dz1 + Pun1.Z;
                            }
                            else
                            {
                                Y1 = referencia.Y;
                                X1= ((Y1 - Pun1.Y) / Dy1) * Dx1 + Pun1.X;
                                Z1 = ((Y1 - Pun1.Y) / Dy1) * Dz1 + Pun1.Z;
                            }
                            L11 = Math.Sqrt(Math.Pow(Pun1.X - Pun2.X, 2) + Math.Pow(Pun1.Y - Pun2.Y, 2) + Math.Pow(Pun1.Z - Pun2.Z, 2));
                            L21 =Math.Max( Math.Sqrt(Math.Pow(Pun1.X - X1, 2) + Math.Pow(Pun1.Y - Y1, 2) + Math.Pow(Pun1.Z - Z1, 2)), Math.Sqrt(Math.Pow(X1 - Pun2.X, 2) + Math.Pow(Y1 - Pun2.Y, 2) + Math.Pow(Z1 - Pun2.Z, 2)));
                            L31 = Math.Sqrt(Math.Pow(X1 - Pun2.X, 2) + Math.Pow(Y1 - Pun2.Y, 2) + Math.Pow(Z1 - Pun2.Z, 2));
                            K11 = L21 / L11;
                            if (K21 > K11)
                            {
                                Lr = (K21 * L11 / Diagonal_castillete.Rz__mm_.Value);

                                if (Lr <= 120)
                                {
                                    KLr = 30 + 0.75 * Lr;
                                    Kmajor = KLr* K21 / Lr;
                                    Kminor = KLr* K21 / Lr;

                                }
                                else if (Lr > 120 && Lr < 200)
                                {
                                    
                                    Kmajor = 1* K21;
                                    Kminor = 1* K21;

                                }
                                else
                                {
                                    Kmajor = 0;
                                    Kminor = 0;
                                }
                            }
                            else
                            {
                                Lr = (K11 * L11 / Diagonal_castillete.Rz__mm_.Value);
                                if (Lr <= 120)
                                {
                                    KLr = 30 + 0.75 * Lr;
                                    Kmajor = (KLr / Lr) * K11;
                                    Kminor = (KLr / Lr) * K11;

                                }
                                else if (Lr > 120 && Lr < 200)
                                {
                                    Kmajor = 1 * K11;
                                    Kminor = 1 * K11;

                                }
                                else
                                {
                                    Kmajor = 0;
                                    Kminor = 0;

                                }
                            }
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 17, 0.99);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 18, 0.99);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 19, Kmajor);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 20, Kminor);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(Dx, 17, 0.99);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(Dx, 18, 0.99);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(Dx, 19, Kmajor);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(Dx, 20, Kminor);

                            #endregion


                        }//definicion de K para diagonales en X
                        else
                        {
                            #region asignacion de K
                            double a, b, a1, b1, a2, b2, L11, L21, L31, L41, L12, L22, L32, L42, Dx1, Dx2, Dy1, Dy2, Dz1, Dz2, X1 = 1, X2, Y1 = 1, Y2, Z1 = 1, Z2, K11, K21, K12, K22, Kmajor, Kminor, Lr, KLr;
                            Punto Pun1 = P_principales.Find(x => x.Name.Equals(P1));
                            Punto Pun2 = P_principales.Find(x => x.Name.Equals(P2));
                            L11 = Math.Sqrt(Math.Pow(Pun1.X - Pun2.X, 2) + Math.Pow(Pun1.Y - Pun2.Y, 2) + Math.Pow(Pun1.Z - Pun2.Z, 2));
                            Lr = (L11 / Diagonal_castillete.Rz__mm_.Value);

                            if (Lr <= 120)
                            {
                                KLr = 60 + 0.5 * Lr;
                                Kmajor = KLr / Lr;
                                Kminor = KLr / Lr;

                            }
                            else if (Lr > 120 && Lr < 200)
                            {

                                Kmajor = 1;
                                Kminor = 1;

                            }
                            else
                            {
                                Kmajor = 0;
                                Kminor = 0;
                            }
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 17, 0.99);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 18, 0.99);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 19, Kmajor);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 20, Kminor);


                            #endregion

                        }//definicion de K para diagonales en Z
                        
                    }
                    else
                    {
                        bool[] II = new bool[6];
                        bool[] JJ = new bool[6];
                        double[] Valorinicio = new double[6];
                        double[] Valorfinal = new double[6];
                        eItemType tipo = eItemType.Objects;
                        II[3] = true;
                        II[4] = true;
                        II[5] = true;
                        JJ[4] = true;
                        JJ[5] = true;
                        ret = subestacion.FrameObj.AddByPoint(P1, P2, ref D, Diagonal.Nombre_del_perfil, D);
                        ret = subestacion.FrameObj.SetReleases(D, ref II, ref JJ, ref Valorinicio, ref Valorfinal, tipo);
                        

                        if (cuerpo.Ancho_inferior != cuerpo.Ancho_superior || cuerpo.Largo_Inferior != cuerpo.Largo_Superior)
                        {
                            if ((j % 2) == 0)
                            {
                                ret = subestacion.FrameObj.SetLocalAxes(D, angulo);
                            }
                            else
                            {
                                ret = subestacion.FrameObj.SetLocalAxes(D, (90 - angulo + 180));
                            }

                        }
                        ret = subestacion.FrameObj.SetGroupAssign(D, "Diagonal");
                        ret = subestacion.FrameObj.SetGroupAssign(D, referencia.Elemento);
                        ret = subestacion.FrameObj.SetGroupAssign(D, referencia.Elemento + referencia.Id);

                        if (cuerpo.Tipo_de_diagonal.Equals("X"))
                        {
                            ret = subestacion.FrameObj.AddByPoint(P3, P4, ref Dx, Diagonal.Nombre_del_perfil, Dx);
                            ret = subestacion.FrameObj.SetReleases(Dx, ref II, ref JJ, ref Valorinicio, ref Valorfinal, tipo);
                            if (cuerpo.Ancho_inferior != cuerpo.Ancho_superior || cuerpo.Largo_Inferior != cuerpo.Largo_Superior)
                            {
                                if ((j % 2) == 0)
                                {
                                    ret = subestacion.FrameObj.SetLocalAxes(Dx, (90 - angulo + 180));
                                }
                                else
                                {
                                    ret = subestacion.FrameObj.SetLocalAxes(Dx, (angulo));
                                }

                            }
                            ret = subestacion.FrameObj.SetGroupAssign(Dx, "Diagonal");
                            ret = subestacion.FrameObj.SetGroupAssign(Dx, referencia.Elemento);
                            ret = subestacion.FrameObj.SetGroupAssign(Dx, referencia.Elemento + referencia.Id);
                        }


                        if (cuerpo.Tipo_de_diagonal.Equals("X"))
                        {
                            #region asignacion de K
                            double a, b, a1, b1, a2, b2, L11, L21, L31, L41, L12, L22, L32, L42, Dx1, Dx2, Dy1, Dy2, Dz1, Dz2, X1 = 1, X2, Y1 = 1, Y2, Z1 = 1, Z2, K11, K21, K12, K22, Kmajor, Kminor, Lr, KLr;
                            Punto Pun1 = P_principales.Find(x => x.Name.Equals(P1));
                            Punto Pun2 = P_principales.Find(x => x.Name.Equals(P2));
                            K21 = Diagonal.Rz__mm_.Value / Diagonal.R33__mm_.Value;
                            Dx1 = (Pun1.X - Pun2.X);
                            Dy1 = (Pun1.Y - Pun2.Y);
                            Dz1 = (Pun1.Z - Pun2.Z);
                            if (Math.Abs(Dx1) > Math.Abs(Dy1))
                            {
                                X1 = referencia.X;
                                Y1 = ((X1 - Pun1.X) / Dx1) * Dy1 + Pun1.Y;
                                Z1 = ((X1 - Pun1.X) / Dx1) * Dz1 + Pun1.Z;
                            }
                            else
                            {
                                Y1 = referencia.Y;
                                X1 = ((Y1 - Pun1.Y) / Dy1) * Dx1 + Pun1.X;
                                Z1 = ((Y1 - Pun1.Y) / Dy1) * Dz1 + Pun1.Z;
                            }
                            L11 = Math.Sqrt(Math.Pow(Pun1.X - Pun2.X, 2) + Math.Pow(Pun1.Y - Pun2.Y, 2) + Math.Pow(Pun1.Z - Pun2.Z, 2));
                            L21 = Math.Max(Math.Sqrt(Math.Pow(Pun1.X - X1, 2) + Math.Pow(Pun1.Y - Y1, 2) + Math.Pow(Pun1.Z - Z1, 2)), Math.Sqrt(Math.Pow(X1 - Pun2.X, 2) + Math.Pow(Y1 - Pun2.Y, 2) + Math.Pow(Z1 - Pun2.Z, 2)));
                            L31 = Math.Sqrt(Math.Pow(X1 - Pun2.X, 2) + Math.Pow(Y1 - Pun2.Y, 2) + Math.Pow(Z1 - Pun2.Z, 2));
                            K11 = L21 / L11;
                            if (K21 > K11)
                            {
                                Lr = (K21 * L11 / Diagonal.Rz__mm_.Value);

                                if (Lr <= 120)
                                {
                                    KLr = 30 + 0.75 * Lr;
                                    Kmajor = KLr* K21 / Lr;
                                    Kminor = KLr* K21 / Lr;

                                }
                                else if (Lr > 120 && Lr < 200)
                                {

                                    Kmajor = 1* K21;
                                    Kminor = 1* K21;

                                }
                                else
                                {
                                    Kmajor = 0;
                                    Kminor = 0;
                                }
                            }
                            else
                            {
                                Lr = (K11 * L11 / Diagonal.Rz__mm_.Value);
                                if (Lr <= 120)
                                {
                                    KLr = 30 + 0.75 * Lr;
                                    Kmajor = (KLr / Lr) * K11;
                                    Kminor = (KLr / Lr) * K11;

                                }
                                else if (Lr > 120 && Lr < 200)
                                {
                                    Kmajor = 1 * K11;
                                    Kminor = 1 * K11;

                                }
                                else
                                {
                                    Kmajor = 0;
                                    Kminor = 0;

                                }
                            }
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 17, 0.99);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 18, 0.99);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 19, Kmajor);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 20, Kminor);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(Dx, 17, 0.99);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(Dx, 18, 0.99);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(Dx, 19, Kmajor);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(Dx, 20, Kminor);

                            #endregion


                        }//definicion de K para diagonales en X
                        else
                        {
                            #region asignacion de K
                            double a, b, a1, b1, a2, b2, L11, L21, L31, L41, L12, L22, L32, L42, Dx1, Dx2, Dy1, Dy2, Dz1, Dz2, X1 = 1, X2, Y1 = 1, Y2, Z1 = 1, Z2, K11, K21, K12, K22, Kmajor, Kminor, Lr, KLr;
                            Punto Pun1 = P_principales.Find(x => x.Name.Equals(P1));
                            Punto Pun2 = P_principales.Find(x => x.Name.Equals(P2));
                            L11 = Math.Sqrt(Math.Pow(Pun1.X - Pun2.X, 2) + Math.Pow(Pun1.Y - Pun2.Y, 2) + Math.Pow(Pun1.Z - Pun2.Z, 2));
                            Lr = (L11 / Diagonal.Rz__mm_.Value);

                            if (Lr <= 120)
                            {
                                KLr = 60 + 0.5 * Lr;
                                Kmajor = KLr / Lr;
                                Kminor = KLr / Lr;

                            }
                            else if (Lr > 120 && Lr < 200)
                            {

                                Kmajor = 1;
                                Kminor = 1;

                            }
                            else
                            {
                                Kmajor = 0;
                                Kminor = 0;
                            }
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 17, 0.99);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 18, 0.99);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 19, Kmajor);
                            ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 20, Kminor);


                            #endregion

                        }//definicion de K para diagonales en Z

                    }


                    j++;
                }
                i++;
            }//creacion de diagonales
            //perfiles de la conexion viga
            if (cuerpo.TipoDeElemento.Equals("Cuerpo de conexion viga"))
            {
                i = 0;
                while (i < 4)
                {
                    bool[] II = new bool[6];
                    bool[] JJ = new bool[6];
                    double[] Valorinicio = new double[6];
                    double[] Valorfinal = new double[6];
                    eItemType tipo = eItemType.Objects;
                    II[3] = true;
                    II[4] = true;
                    II[5] = true;
                    JJ[4] = true;
                    JJ[5] = true;
                    string c1 = "";
                    string c2 = "";
                    if (i == 0)
                    {
                        c1 = "A";
                        c2 = "B";
                    }
                    else if (i == 1)
                    {
                        c1 = "B";
                        c2 = "C";
                    }
                    else if (i == 2)
                    {
                        c1 = "C";
                        c2 = "D";
                    }
                    else if (i == 3)
                    {
                        c1 = "D";
                        c2 = "A";
                    }

                    string P1 = "" + referencia.Id + c1 + "1" + referencia.Divisiones;
                    string P2 = "" + referencia.Id + c2 + "1" + referencia.Divisiones;
                    string M = "" + referencia.Id + "MV" + (i + 1 + referencia.Divisiones);
                    ret = subestacion.FrameObj.AddByPoint(P1, P2, ref M, Montante_viga.Nombre_del_perfil, M);                    
                    #region asignacion de K
                    double L11, Kmajor, Kminor, Lr, KLr;
                    Punto Pun1 = P_principales.Find(x => x.Name.Equals(P1));
                    Punto Pun2 = P_principales.Find(x => x.Name.Equals(P2));
                    L11 = Math.Sqrt(Math.Pow(Pun1.X - Pun2.X, 2) + Math.Pow(Pun1.Y - Pun2.Y, 2) + Math.Pow(Pun1.Z - Pun2.Z, 2));

                    Lr = (L11 / Montante_viga.Rz__mm_.Value);
                    if (Lr <= 120)
                    {
                        Kmajor = 1;
                        Kminor = 1;

                    }
                    else if (Lr > 120 && Lr < 250)
                    {
                        KLr = 46.2 + 0.615 * Lr;
                        Kmajor = 1;
                        Kminor = 1;

                    }
                    else
                    {
                        Kmajor = 0;
                        Kminor = 0;
                    }
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 17, 0.99);
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 18, 0.99);
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 19, Kmajor);
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 20, Kminor);

                    #endregion
                    ret = subestacion.FrameObj.SetReleases(M, ref II, ref JJ, ref Valorinicio, ref Valorfinal, tipo);
                    ret = subestacion.FrameObj.SetGroupAssign(M, "Montante Viga");
                    ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento);
                    ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento + referencia.Id);

                    P1 = "" + referencia.Id + c1 + "1" + (nd + referencia.Divisiones);
                    P2 = "" + referencia.Id + c2 + "1" + (nd + referencia.Divisiones);
                    M = "" + referencia.Id + "MV" + (i + 1 + (nd + referencia.Divisiones));
                    ret = subestacion.FrameObj.AddByPoint(P1, P2, ref M, Montante_viga.Nombre_del_perfil, M);
                    #region asignacion de K
                    
                    Pun1 = P_principales.Find(x => x.Name.Equals(P1));
                    Pun2 = P_principales.Find(x => x.Name.Equals(P2));
                    L11 = Math.Sqrt(Math.Pow(Pun1.X - Pun2.X, 2) + Math.Pow(Pun1.Y - Pun2.Y, 2) + Math.Pow(Pun1.Z - Pun2.Z, 2));

                    Lr = (L11 / Montante_viga.Rz__mm_.Value);
                    if (Lr <= 120)
                    {
                        Kmajor = 1;
                        Kminor = 1;

                    }
                    else if (Lr > 120 && Lr < 250)
                    {
                       
                        Kmajor = 1;
                        Kminor = 1;

                    }
                    else
                    {
                        Kmajor = 0;
                        Kminor = 0;
                    }
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 17, 0.99);
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 18, 0.99);
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 19, Kmajor);
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 20, Kminor);

                    #endregion
                    ret = subestacion.FrameObj.SetReleases(M, ref II, ref JJ, ref Valorinicio, ref Valorfinal, tipo);
                    ret = subestacion.FrameObj.SetLocalAxes(M, 270);
                    ret = subestacion.FrameObj.SetGroupAssign(M, "Montante Viga");
                    ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento);
                    ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento + referencia.Id);
                    i++;
                }//Montantes de conexion viga

                i = 0;
                while (i < 2)
                {
                    bool[] II = new bool[6];
                    bool[] JJ = new bool[6];
                    double[] Valorinicio = new double[6];
                    double[] Valorfinal = new double[6];
                    eItemType tipo = eItemType.Objects;
                    II[3] = true;
                    II[4] = true;
                    II[5] = true;
                    JJ[4] = true;
                    JJ[5] = true;
                    string c1 = "";
                    string c2 = "";
                    if (i == 0)
                    {
                        c1 = "A";
                        c2 = "C";
                    }
                    else if (i == 1)
                    {
                        c1 = "B";
                        c2 = "D";
                    }

                    string P1 = "" + referencia.Id + c1 + "1" + referencia.Divisiones;
                    string P2 = "" + referencia.Id + c2 + "1" + referencia.Divisiones;
                    string M = "" + referencia.Id + 2 + "MV" + (i + 1 + referencia.Divisiones);
                    ret = subestacion.FrameObj.AddByPoint(P1, P2, ref M, Diagonal_viga.Nombre_del_perfil, M);
                    //*************************************
                    #region asignacion de K
                    double a, b, a1, b1, a2, b2, L11, L21, L31, L41, L12, L22, L32, L42, Dx1, Dx2, Dy1, Dy2, Dz1, Dz2, X1 = 1, X2, Y1 = 1, Y2, Z1 = 1, Z2, K11, K21, K12, K22, Kmajor, Kminor, Lr, KLr;
                    Punto Pun1 = P_principales.Find(x => x.Name.Equals(P1));
                    Punto Pun2 = P_principales.Find(x => x.Name.Equals(P2));
                    K21 = Diagonal_viga.Rz__mm_.Value / Diagonal_viga.R33__mm_.Value;
                    Dx1 = (Pun1.X - Pun2.X);
                    Dy1 = (Pun1.Y - Pun2.Y);
                    Dz1 = (Pun1.Z - Pun2.Z);
                    if (Math.Abs(Dx1) > Math.Abs(Dy1))
                    {
                        X1 = referencia.X;
                        Y1 = ((X1 - Pun1.X) / Dx1) * Dy1 + Pun1.Y;
                        Z1 = ((X1 - Pun1.X) / Dx1) * Dz1 + Pun1.Z;
                    }
                    else
                    {
                        Y1 = referencia.Y;
                        X1 = ((Y1 - Pun1.Y) / Dy1) * Dx1 + Pun1.X;
                        Z1 = ((Y1 - Pun1.Y) / Dy1) * Dz1 + Pun1.Z;
                    }
                    L11 = Math.Sqrt(Math.Pow(Pun1.X - Pun2.X, 2) + Math.Pow(Pun1.Y - Pun2.Y, 2) + Math.Pow(Pun1.Z - Pun2.Z, 2));
                    L21 = Math.Max(Math.Sqrt(Math.Pow(Pun1.X - X1, 2) + Math.Pow(Pun1.Y - Y1, 2) + Math.Pow(Pun1.Z - Z1, 2)), Math.Sqrt(Math.Pow(X1 - Pun2.X, 2) + Math.Pow(Y1 - Pun2.Y, 2) + Math.Pow(Z1 - Pun2.Z, 2)));
                    L31 = Math.Sqrt(Math.Pow(X1 - Pun2.X, 2) + Math.Pow(Y1 - Pun2.Y, 2) + Math.Pow(Z1 - Pun2.Z, 2));
                    K11 = L21 / L11;
                    if (K21 > K11)
                    {
                        Lr = (K21 * L11 / Diagonal_viga.Rz__mm_.Value);

                        if (Lr <= 120)
                        {
                            KLr = 30 + 0.75 * Lr;
                            Kmajor = KLr* K21 / Lr;
                            Kminor = KLr * K21 / Lr;

                        }
                        else if (Lr > 120 && Lr < 200)
                        {

                            Kmajor = 1* K21;
                            Kminor = 1* K21;

                        }
                        else
                        {
                            Kmajor = 0;
                            Kminor = 0;
                        }
                    }
                    else
                    {
                        Lr = (K11 * L11 / Diagonal_viga.Rz__mm_.Value);
                        if (Lr <= 120)
                        {
                            KLr = 30 + 0.75 * Lr;
                            Kmajor = (KLr / Lr) * K11;
                            Kminor = (KLr / Lr) * K11;

                        }
                        else if (Lr > 120 && Lr < 200)
                        {
                            Kmajor = 1 * K11;
                            Kminor = 1 * K11;

                        }
                        else
                        {
                            Kmajor = 0;
                            Kminor = 0;

                        }
                    }
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 17, 0.99);
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 18, 0.99);
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 19, Kmajor);
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 20, Kminor);                   
                    #endregion

                    ret = subestacion.FrameObj.SetReleases(M, ref II, ref JJ, ref Valorinicio, ref Valorfinal, tipo);
                    if (i == 1)
                    {
                        double ang = 180;
                        ret = subestacion.FrameObj.SetLocalAxes(M, ang);
                    }
                    ret = subestacion.FrameObj.SetGroupAssign(M, "Diagonal viga");
                    ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento);
                    ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento + referencia.Id);

                    P1 = "" + referencia.Id + c1 + "1" + (nd + referencia.Divisiones);
                    P2 = "" + referencia.Id + c2 + "1" + (nd + referencia.Divisiones);
                    M = "" + referencia.Id + "DV" + (i + 1 + (nd + referencia.Divisiones));
                    ret = subestacion.FrameObj.AddByPoint(P1, P2, ref M, Diagonal_viga.Nombre_del_perfil, M);
                    #region asignacion de K
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 17, 0.99);
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 18, 0.99);
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 19, Kmajor);
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 20, Kminor);
                    #endregion
                    ret = subestacion.FrameObj.SetReleases(M, ref II, ref JJ, ref Valorinicio, ref Valorfinal, tipo);
                    if (i == 1)
                    {
                        double ang = 180;
                        ret = subestacion.FrameObj.SetLocalAxes(M, ang);
                    }
                    ret = subestacion.FrameObj.SetGroupAssign(M, "Diagonal viga");
                    ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento);
                    ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento + referencia.Id);
                    i++;
                }//diagonales de conexion viga

            }//creacion de montantes y diagonales de la conexion de viga

            if (n == 1)
            {
                bool[] Value = new bool[6];

                for (i = 0; i <= 2; i++)
                {
                    Value[i] = true;
                }

                for (i = 3; i <= 5; i++)
                {
                    Value[i] = false;
                }
                i = 0;
                while (i < 4)
                {
                    string c1 = "";
                    if (i == 0)
                    {
                        c1 = "A";
                    }
                    else if (i == 1)
                    {
                        c1 = "B";
                    }
                    else if (i == 2)
                    {
                        c1 = "C";
                    }
                    else if (i == 3)
                    {
                        c1 = "D";
                    }
                    string P1;
                    P1 = "" + referencia.Id + c1 + "1" + (referencia.Divisiones);
                    ret = subestacion.PointObj.SetRestraint(P1, ref Value);
                    ret = subestacion.PointObj.SetGroupAssign(P1, "Apoyos");
                    i++;
                }


            }//agregar apoyos

            if (cuerpo.TipoDeElemento.Equals("Cuerpo de castillete"))
            {
                string P2;
                P2 = "" + referencia.Id + "D1" + (nd + referencia.Divisiones);

                //***************************************
                List<Carga> cargas_subensambles = entidadesBD.Cargas.ToList();
                List<Carga> carga_castillete = cargas_subensambles.FindAll(x => x.Id_Ensamble == referencia.Id);
                if (carga_castillete.Count != 0)
                {
                    double[] PP, CTV, VV, CTH, VH, CMM;
                    double CTX = 0, CTY = 0, VX = 0, VY = 0;
                    PP = new double[6];
                    CTV = new double[6];
                    CMM = new double[6];
                    VV = new double[6];
                    CTH = new double[6];
                    VH = new double[6];
                    #region carga de guardas
                    List<CARGAS_DE_CONEXIÓN> conexion = entidadesBD.CARGAS_DE_CONEXIÓN.ToList();
                    foreach (Carga car in carga_castillete)
                    {
                        CARGAS_DE_CONEXIÓN tendido1 = conexion.Find(x => x.Id == car.Id_Carga_de_conexion);
                        PP[2] = PP[2] - tendido1.Peso___kg_;
                        CMM[2] = -150;
                        if ((car.Orientacion >= 45 && car.Orientacion < 135) || (car.Orientacion >= 225 && car.Orientacion < 315))
                        {

                            CTX = tendido1.Tiro__kg_ * Math.Cos(car.Orientacion * Math.PI / 180);
                            CTY = tendido1.Tiro__kg_ * Math.Sin(car.Orientacion * Math.PI / 180);
                            VX = tendido1.Viento_Transversal__kg_ * Math.Sin(car.Orientacion * Math.PI / 180) + tendido1.Viento_Longitudinal__kg_ * Math.Cos(car.Orientacion * Math.PI / 180);
                            VY = tendido1.Viento_Transversal__kg_ * Math.Cos(car.Orientacion * Math.PI / 180) + tendido1.Viento_Longitudinal__kg_ * Math.Sin(car.Orientacion * Math.PI / 180);

                            if (Math.Abs(CTV[1] * 1.1 + VV[1] * 1.2) < Math.Abs(CTY * 1.1 + VY * 1.2))
                            {
                                CTV[1] = CTY;
                                VV[1] = VY;
                            }

                            CTV[0] = CTV[0] + Math.Abs(CTX);
                            VV[0] = VV[0] + Math.Abs(VX);


                        }//orientacion en dirrecion y
                        else
                        {
                            CTX = tendido1.Tiro__kg_ * Math.Cos(car.Orientacion * Math.PI / 180);
                            CTY = tendido1.Tiro__kg_ * Math.Sin(car.Orientacion * Math.PI / 180);
                            VX = tendido1.Viento_Transversal__kg_ * Math.Sin(car.Orientacion * Math.PI / 180) + tendido1.Viento_Longitudinal__kg_ * Math.Cos(car.Orientacion * Math.PI / 180);
                            VY = tendido1.Viento_Transversal__kg_ * Math.Cos(car.Orientacion * Math.PI / 180) + tendido1.Viento_Longitudinal__kg_ * Math.Sin(car.Orientacion * Math.PI / 180);

                            if (Math.Abs(CTH[1] * 1.1 + VH[1] * 1.2) < Math.Abs(CTX * 1.1 + VX * 1.2))
                            {
                                CTH[0] = CTX;
                                VH[0] = VX;
                            }

                            CTH[1] = CTH[1] + Math.Abs(CTY);
                            VH[1] = VH[1] + Math.Abs(VY);

                        }//orientacion en dirrecion X

                    }
                    ret = subestacion.PointObj.SetLoadForce(P2, "PP", ref PP);
                    ret = subestacion.PointObj.SetLoadForce(P2, "CMM", ref CMM);
                    ret = subestacion.PointObj.SetLoadForce(P2, "CT", ref CTV);
                    ret = subestacion.PointObj.SetLoadForce(P2, "CVcx", ref VV);
                    ret = subestacion.PointObj.SetLoadForce(P2, "CT", ref CTH);
                    ret = subestacion.PointObj.SetLoadForce(P2, "CVcy", ref VH);

                    #endregion
                }




            }//CARGA DE CASTILLETE


            referencia.Z = referencia.Z + cuerpo.Altura;
            referencia.Divisiones = referencia.Divisiones + int.Parse(nd.ToString());
            return referencia;

        }
        
        public void crear_columna( Ensamble_subestacion Referencia, cSapModel subestacion)
        {
            List<CuerposDeElemento> cuerpo = new List<CuerposDeElemento>();

           cuerpo = entidadesBD.CuerposDeElementoes.AsNoTracking().ToList().FindAll(x => x.Id_Elemento == Referencia.Id_Elemento);

            int n = 1;
            foreach (CuerposDeElemento cue in cuerpo)
            {
                CuerposDeElemento cuerpo1 = cuerpo.Find(x => x.Id_Cuerpo == cue.Id_Cuerpo);
                //Punto referencia = new Punto();
                Referencia = crear_cuerpo(cuerpo1, Referencia, subestacion,n);
                n = n + 1;
            }


        }

        public void Default_T_Cargas(cSapModel subestacion,Default_T_Carga T)
        {
            int ret = 0;           
            ret = subestacion.LoadPatterns.Add("DEAD", eLoadPatternType.Dead,1.15,true);
            ret = subestacion.LoadPatterns.Add("PP", eLoadPatternType.SuperDead, 0, true);
            ret = subestacion.LoadPatterns.Add("CT", eLoadPatternType.Other, 0, true);
            ret = subestacion.LoadPatterns.Add("CC", eLoadPatternType.Other, 0, true);
            ret = subestacion.LoadPatterns.Add("CMM", eLoadPatternType.Live, 0, true);
            ret = subestacion.LoadPatterns.Add("CVsx", eLoadPatternType.Wind, 0, true);
            ret = subestacion.LoadPatterns.Add("CVsy", eLoadPatternType.Wind, 0, true);
            ret = subestacion.LoadPatterns.Add("CVcx", eLoadPatternType.Wind, 0, true);
            ret = subestacion.LoadPatterns.Add("CVcy", eLoadPatternType.Wind, 0, true);
            ret = subestacion.LoadPatterns.Add("CSsx", eLoadPatternType.Quake, 0, true);
            ret = subestacion.LoadPatterns.Add("CSsy", eLoadPatternType.Quake, 0, true);
            ret = subestacion.LoadPatterns.Add("CSsz", eLoadPatternType.Quake, 0, true);            
            ret = subestacion.LoadPatterns.AutoSeismic.SetUserCoefficient("CSsx", 1, 0.05, false, 0, 0, T.Cortante_basal, T.K_exponente);
            ret = subestacion.LoadPatterns.AutoSeismic.SetUserCoefficient("CSsy", 2, 0.05, false, 0, 0, T.Cortante_basal, T.K_exponente);           
            ret = subestacion.LoadPatterns.AutoWind.SetASCE710("CVsx", 3, 0, 0, 0, 0, 0, 0, false, 0, 0, T.Velocidad_viento, T.Tipo_exposicion, T.Factor_importancia, T.Factor_topografia, T.Factor_rafaga, T.Factor_direccion, 0.2, false);
            ret = subestacion.LoadPatterns.AutoWind.SetASCE710("CVsy", 3, 90, 0, 0, 0, 0, 0, false, 0, 0, T.Velocidad_viento, T.Tipo_exposicion, T.Factor_importancia, T.Factor_topografia, T.Factor_rafaga, T.Factor_direccion, 0.2, false);
           
        }

        public void Default_combinaciones(cSapModel subestacion,Default_T_Carga T)
        {
            int ret = 0;
            eCNameType caso = eCNameType.LoadCase;
            eCNameType caso1 = eCNameType.LoadCombo;
            double av =  T.Cortante_basal*0.6666666667;
            ret = subestacion.RespCombo.Add("CSsz1", 0);
            ret = subestacion.RespCombo.SetCaseList("CSsz1", ref caso, "DEAD",av);
            ret = subestacion.RespCombo.SetCaseList("CSsz1", ref caso, "PP", av);
            ret = subestacion.RespCombo.Add("COMB1", 0);            
            ret = subestacion.RespCombo.SetCaseList("COMB1",ref caso, "DEAD", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB1", ref caso, "PP", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB1", ref caso, "CT", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB1", ref caso, "CC",0.75);
            ret = subestacion.RespCombo.SetCaseList("COMB1", ref caso, "CMM", 1);
            ret = subestacion.RespCombo.Add("COMB2", 0);
            ret = subestacion.RespCombo.SetCaseList("COMB2", ref caso, "DEAD", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB2", ref caso, "PP", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB2", ref caso, "CT", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB2", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMB2", ref caso, "CVsx", 1.2);
            ret = subestacion.RespCombo.SetCaseList("COMB2", ref caso, "CVcx", 1.2);
            ret = subestacion.RespCombo.Add("COMB3", 0);
            ret = subestacion.RespCombo.SetCaseList("COMB3", ref caso, "DEAD", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB3", ref caso, "PP", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB3", ref caso, "CT", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB3", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMB3", ref caso, "CVsx", -1.2);
            ret = subestacion.RespCombo.SetCaseList("COMB3", ref caso, "CVcx", -1.2);
            ret = subestacion.RespCombo.Add("COMB4", 0);
            ret = subestacion.RespCombo.SetCaseList("COMB4", ref caso, "DEAD", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB4", ref caso, "PP", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB4", ref caso, "CT", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB4", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMB4", ref caso, "CVsy", 1.2);
            ret = subestacion.RespCombo.SetCaseList("COMB4", ref caso, "CVcy", 1.2);
            ret = subestacion.RespCombo.Add("COMB5", 0);
            ret = subestacion.RespCombo.SetCaseList("COMB5", ref caso, "DEAD", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB5", ref caso, "PP", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB5", ref caso, "CT", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB5", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMB5", ref caso, "CVsy", -1.2);
            ret = subestacion.RespCombo.SetCaseList("COMB5", ref caso, "CVcy", -1.2);
            ret = subestacion.RespCombo.Add("COMB6", 0);
            ret = subestacion.RespCombo.SetCaseList("COMB6", ref caso, "DEAD", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB6", ref caso, "PP", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB6", ref caso, "CT", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB6", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMB6", ref caso, "CSsx", 1);
            ret = subestacion.RespCombo.SetCaseList("COMB6", ref caso, "CSsy", 0.3);
            ret = subestacion.RespCombo.SetCaseList("COMB6", ref caso1, "CSsz1", 1);
            ret = subestacion.RespCombo.Add("COMB7", 0);
            ret = subestacion.RespCombo.SetCaseList("COMB7", ref caso, "DEAD", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB7", ref caso, "PP", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB7", ref caso, "CT", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB7", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMB7", ref caso, "CSsx", -1);
            ret = subestacion.RespCombo.SetCaseList("COMB7", ref caso, "CSsy", 0.3);
            ret = subestacion.RespCombo.SetCaseList("COMB7", ref caso1, "CSsz1", 1);
            ret = subestacion.RespCombo.Add("COMB8", 0);
            ret = subestacion.RespCombo.SetCaseList("COMB8", ref caso, "DEAD", 0.9);
            ret = subestacion.RespCombo.SetCaseList("COMB8", ref caso, "PP", 0.9);
            ret = subestacion.RespCombo.SetCaseList("COMB8", ref caso, "CT", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB8", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMB8", ref caso, "CSsx", 1);
            ret = subestacion.RespCombo.SetCaseList("COMB8", ref caso, "CSsy", 0.3);
            ret = subestacion.RespCombo.SetCaseList("COMB8", ref caso1, "CSsz1", -1);
            ret = subestacion.RespCombo.Add("COMB9", 0);
            ret = subestacion.RespCombo.SetCaseList("COMB9", ref caso, "DEAD", 0.9);
            ret = subestacion.RespCombo.SetCaseList("COMB9", ref caso, "PP", 0.9);
            ret = subestacion.RespCombo.SetCaseList("COMB9", ref caso, "CT", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB9", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMB9", ref caso, "CSsx", -1);
            ret = subestacion.RespCombo.SetCaseList("COMB9", ref caso, "CSsy", 0.3);
            ret = subestacion.RespCombo.SetCaseList("COMB9", ref caso1, "CSsz1", -1);
            ret = subestacion.RespCombo.Add("COMB10", 0);
            ret = subestacion.RespCombo.SetCaseList("COMB10", ref caso, "DEAD", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB10", ref caso, "PP", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB10", ref caso, "CT", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB10", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMB10", ref caso, "CSsx", 0.3);
            ret = subestacion.RespCombo.SetCaseList("COMB10", ref caso, "CSsy", 1);
            ret = subestacion.RespCombo.SetCaseList("COMB10", ref caso1, "CSsz1", 1);
            ret = subestacion.RespCombo.Add("COMB11", 0);
            ret = subestacion.RespCombo.SetCaseList("COMB11", ref caso, "DEAD", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB11", ref caso, "PP", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB11", ref caso, "CT", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB11", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMB11", ref caso, "CSsx", 0.3);
            ret = subestacion.RespCombo.SetCaseList("COMB11", ref caso, "CSsy", -1);
            ret = subestacion.RespCombo.SetCaseList("COMB11", ref caso1, "CSsz1", 1);
            ret = subestacion.RespCombo.Add("COMB12", 0);
            ret = subestacion.RespCombo.SetCaseList("COMB12", ref caso, "DEAD", 0.9);
            ret = subestacion.RespCombo.SetCaseList("COMB12", ref caso, "PP", 0.9);
            ret = subestacion.RespCombo.SetCaseList("COMB12", ref caso, "CT", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB12", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMB12", ref caso, "CSsx", 0.3);
            ret = subestacion.RespCombo.SetCaseList("COMB12", ref caso, "CSsy", 1);
            ret = subestacion.RespCombo.SetCaseList("COMB12", ref caso1, "CSsz1", -1);
            ret = subestacion.RespCombo.Add("COMB13", 0);
            ret = subestacion.RespCombo.SetCaseList("COMB13", ref caso, "DEAD", 0.9);
            ret = subestacion.RespCombo.SetCaseList("COMB13", ref caso, "PP", 0.9);
            ret = subestacion.RespCombo.SetCaseList("COMB13", ref caso, "CT", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB13", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMB13", ref caso, "CSsx", 0.3);
            ret = subestacion.RespCombo.SetCaseList("COMB13", ref caso, "CSsy", -1);
            ret = subestacion.RespCombo.SetCaseList("COMB13", ref caso1, "CSsz1", -1);
            ret = subestacion.RespCombo.Add("COMB14", 0);
            ret = subestacion.RespCombo.SetCaseList("COMB14", ref caso, "DEAD", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB14", ref caso, "PP", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB14", ref caso, "CT", 1.1);
            ret = subestacion.RespCombo.SetCaseList("COMB14", ref caso, "CC", 1);
            //envolvente
            ret = subestacion.RespCombo.Add("ENVD", 1);
            ret = subestacion.RespCombo.SetCaseList("ENVD", ref caso1, "COMB1", 1);
            ret = subestacion.RespCombo.SetCaseList("ENVD", ref caso1, "COMB2", 1);
            ret = subestacion.RespCombo.SetCaseList("ENVD", ref caso1, "COMB3", 1);
            ret = subestacion.RespCombo.SetCaseList("ENVD", ref caso1, "COMB4", 1);
            ret = subestacion.RespCombo.SetCaseList("ENVD", ref caso1, "COMB5", 1);
            ret = subestacion.RespCombo.SetCaseList("ENVD", ref caso1, "COMB6", 1);
            ret = subestacion.RespCombo.SetCaseList("ENVD", ref caso1, "COMB7", 1);
            ret = subestacion.RespCombo.SetCaseList("ENVD", ref caso1, "COMB8", 1);
            ret = subestacion.RespCombo.SetCaseList("ENVD", ref caso1, "COMB9", 1);
            ret = subestacion.RespCombo.SetCaseList("ENVD", ref caso1, "COMB10", 1);
            ret = subestacion.RespCombo.SetCaseList("ENVD", ref caso1, "COMB11", 1);
            ret = subestacion.RespCombo.SetCaseList("ENVD", ref caso1, "COMB12", 1);
            ret = subestacion.RespCombo.SetCaseList("ENVD", ref caso1, "COMB13", 1);
            ret = subestacion.RespCombo.SetCaseList("ENVD", ref caso1, "COMB14", 1);            
            //servisio

            ret = subestacion.RespCombo.Add("COMBS1", 0);
            ret = subestacion.RespCombo.SetCaseList("COMBS1", ref caso, "DEAD", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS1", ref caso, "PP", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS1", ref caso, "CT", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS1", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMBS1", ref caso, "CMM", 1);
            ret = subestacion.RespCombo.Add("COMBS2", 0);
            ret = subestacion.RespCombo.SetCaseList("COMBS2", ref caso, "DEAD", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS2", ref caso, "PP", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS2", ref caso, "CT", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS2", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMBS2", ref caso, "CVsx", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS2", ref caso, "CVcx", 1);
            ret = subestacion.RespCombo.Add("COMBS3", 0);
            ret = subestacion.RespCombo.SetCaseList("COMBS3", ref caso, "DEAD", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS3", ref caso, "PP", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS3", ref caso, "CT", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS3", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMBS3", ref caso, "CVsx", -1);
            ret = subestacion.RespCombo.SetCaseList("COMBS3", ref caso, "CVcx", -1);
            ret = subestacion.RespCombo.Add("COMBS4", 0);
            ret = subestacion.RespCombo.SetCaseList("COMBS4", ref caso, "DEAD", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS4", ref caso, "PP", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS4", ref caso, "CT", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS4", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMBS4", ref caso, "CVsy", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS4", ref caso, "CVcy", 1);
            ret = subestacion.RespCombo.Add("COMBS5", 0);
            ret = subestacion.RespCombo.SetCaseList("COMBS5", ref caso, "DEAD", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS5", ref caso, "PP", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS5", ref caso, "CT", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS5", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMBS5", ref caso, "CVsy", -1);
            ret = subestacion.RespCombo.SetCaseList("COMBS5", ref caso, "CVcy", -1);
            ret = subestacion.RespCombo.Add("COMBS6", 0);
            ret = subestacion.RespCombo.SetCaseList("COMBS6", ref caso, "DEAD", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS6", ref caso, "PP", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS6", ref caso, "CT", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS6", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMBS6", ref caso, "CSsx", 0.7);
            ret = subestacion.RespCombo.SetCaseList("COMBS6", ref caso, "CSsy", 0.27);
            ret = subestacion.RespCombo.SetCaseList("COMBS6", ref caso1, "CSsz1", 0.7);
            ret = subestacion.RespCombo.Add("COMBS7", 0);
            ret = subestacion.RespCombo.SetCaseList("COMBS7", ref caso, "DEAD", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS7", ref caso, "PP", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS7", ref caso, "CT", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS7", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMBS7", ref caso, "CSsx", -0.7);
            ret = subestacion.RespCombo.SetCaseList("COMBS7", ref caso, "CSsy", 0.21);
            ret = subestacion.RespCombo.SetCaseList("COMBS7", ref caso1, "CSsz1", 0.7);
            ret = subestacion.RespCombo.Add("COMBS8", 0);
            ret = subestacion.RespCombo.SetCaseList("COMBS8", ref caso, "DEAD", 0.6);
            ret = subestacion.RespCombo.SetCaseList("COMBS8", ref caso, "PP", 0.6);
            ret = subestacion.RespCombo.SetCaseList("COMBS8", ref caso, "CT", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS8", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMBS8", ref caso, "CSsx", 0.7);
            ret = subestacion.RespCombo.SetCaseList("COMBS8", ref caso, "CSsy", 0.21);
            ret = subestacion.RespCombo.SetCaseList("COMBS8", ref caso1, "CSsz1", -0.7);
            ret = subestacion.RespCombo.Add("COMBS9", 0);
            ret = subestacion.RespCombo.SetCaseList("COMBS9", ref caso, "DEAD", 0.6);
            ret = subestacion.RespCombo.SetCaseList("COMBS9", ref caso, "PP", 0.6);
            ret = subestacion.RespCombo.SetCaseList("COMBS9", ref caso, "CT", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS9", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMBS9", ref caso, "CSsx", -0.7);
            ret = subestacion.RespCombo.SetCaseList("COMBS9", ref caso, "CSsy", 0.21);
            ret = subestacion.RespCombo.SetCaseList("COMBS9", ref caso1, "CSsz1", -0.7);
            ret = subestacion.RespCombo.Add("COMBS10", 0);
            ret = subestacion.RespCombo.SetCaseList("COMBS10", ref caso, "DEAD", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS10", ref caso, "PP", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS10", ref caso, "CT", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS10", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMBS10", ref caso, "CSsx", 0.21);
            ret = subestacion.RespCombo.SetCaseList("COMBS10", ref caso, "CSsy", 0.7);
            ret = subestacion.RespCombo.SetCaseList("COMBS10", ref caso1, "CSsz1", 0.7);
            ret = subestacion.RespCombo.Add("COMBS11", 0);
            ret = subestacion.RespCombo.SetCaseList("COMBS11", ref caso, "DEAD", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS11", ref caso, "PP", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS11", ref caso, "CT", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS11", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMBS11", ref caso, "CSsx", 0.21);
            ret = subestacion.RespCombo.SetCaseList("COMBS11", ref caso, "CSsy", -0.7);
            ret = subestacion.RespCombo.SetCaseList("COMBS11", ref caso1, "CSsz1", 0.7);
            ret = subestacion.RespCombo.Add("COMBS12", 0);
            ret = subestacion.RespCombo.SetCaseList("COMBS12", ref caso, "DEAD", 0.6);
            ret = subestacion.RespCombo.SetCaseList("COMBS12", ref caso, "PP", 0.6);
            ret = subestacion.RespCombo.SetCaseList("COMBS12", ref caso, "CT", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS12", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMBS12", ref caso, "CSsx", 0.21);
            ret = subestacion.RespCombo.SetCaseList("COMBS12", ref caso, "CSsy", 0.7);
            ret = subestacion.RespCombo.SetCaseList("COMBS12", ref caso1, "CSsz1", -0.7);
            ret = subestacion.RespCombo.Add("COMBS13", 0);
            ret = subestacion.RespCombo.SetCaseList("COMBS13", ref caso, "DEAD", 0.6);
            ret = subestacion.RespCombo.SetCaseList("COMBS13", ref caso, "PP", 0.6);
            ret = subestacion.RespCombo.SetCaseList("COMBS13", ref caso, "CT", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS13", ref caso, "CC", 0.75);
            ret = subestacion.RespCombo.SetCaseList("COMBS13", ref caso, "CSsx", 0.21);
            ret = subestacion.RespCombo.SetCaseList("COMBS13", ref caso, "CSsy", -0.7);
            ret = subestacion.RespCombo.SetCaseList("COMBS13", ref caso1, "CSsz1", -0.7);
            ret = subestacion.RespCombo.Add("COMBS14", 0);
            ret = subestacion.RespCombo.SetCaseList("COMBS14", ref caso, "DEAD", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS14", ref caso, "PP", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS14", ref caso, "CT", 1);
            ret = subestacion.RespCombo.SetCaseList("COMBS14", ref caso, "CC", 1);

        }//combinaciones por defecto para porticos
        
        public Ensamble_subestacion crear_cuerpo_vigas(CuerposDeElemento cuerpo, Ensamble_subestacion referencia, cSapModel subestacion,int con,int numCuerpos)
        {
            int ret = 0;
            //List<Perfile> perfileria = Per(cuerpo.Nivel_de_tension);
            Perfile Montante_viga, Diagonal_viga, Cierres;
            double CcMV, CcDV;
            #region grupos particulares
            ret = subestacion.GroupDef.SetGroup(referencia.Elemento);
            ret = subestacion.GroupDef.SetGroup(referencia.Elemento + referencia.Id);
            #endregion
            #region definicion de perfiles para el cuerpo
            if (string.IsNullOrWhiteSpace(cuerpo.PerfilMontanteViga))
            {
                Montante_viga = entidadesBD.Perfiles.ToList().Find(x => (x.Nivel_de_tensio.Equals(cuerpo.Nivel_de_tension) && x.Uso_del_perfil.Equals("Montante Viga")));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Montante_viga.Material_del_perfil));
                CcMV = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
                Montante_viga.Lmax__mm_ = 120 * Montante_viga.R33__mm_ ;
                Montante_viga.Lmin__mm_ = 120 * Montante_viga.Rz__mm_ ;
                entidadesBD.Entry(Montante_viga).State = EntityState.Modified;
                entidadesBD.SaveChanges();
            }
            else
            {
                Montante_viga = entidadesBD.Perfiles.ToList().Find(x => (x.id_subestacion == referencia.id_Subestacion && x.Nombre_del_perfil.Equals(cuerpo.PerfilMontanteViga)));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Montante_viga.Material_del_perfil));
                CcMV = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
                Montante_viga.Lmax__mm_ = 120 * Montante_viga.R33__mm_ ;
                Montante_viga.Lmin__mm_ = 120 * Montante_viga.Rz__mm_ ;
                entidadesBD.Entry(Montante_viga).State = EntityState.Modified;
                entidadesBD.SaveChanges();
            }

            if (string.IsNullOrWhiteSpace(cuerpo.PerfilDiagonalViga))
            {
                Diagonal_viga = entidadesBD.Perfiles.ToList().Find(x => (x.Nivel_de_tensio.Equals(cuerpo.Nivel_de_tension) && x.Uso_del_perfil.Equals("Diagonal viga")));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Diagonal_viga.Material_del_perfil));
                CcDV = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
                Diagonal_viga.Lmax__mm_ = 190 * Diagonal_viga.R33__mm_;
                Diagonal_viga.Lmin__mm_ = 190 * Diagonal_viga.Rz__mm_;
                entidadesBD.Entry(Diagonal_viga).State = EntityState.Modified;
                entidadesBD.SaveChanges();
            }
            else
            {
                Diagonal_viga = entidadesBD.Perfiles.ToList().Find(x => (x.id_subestacion == referencia.id_Subestacion && x.Nombre_del_perfil.Equals(cuerpo.PerfilDiagonalViga)));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Diagonal_viga.Material_del_perfil));
                CcDV = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
                Diagonal_viga.Lmax__mm_ = 190 * Diagonal_viga.R33__mm_;
                Diagonal_viga.Lmin__mm_ = 190 * Diagonal_viga.Rz__mm_;
                entidadesBD.Entry(Diagonal_viga).State = EntityState.Modified;
                entidadesBD.SaveChanges();
            }
            if (string.IsNullOrWhiteSpace(cuerpo.PerfilCierre))
            {
                Cierres = entidadesBD.Perfiles.ToList().Find(x => (x.Nivel_de_tensio.Equals(cuerpo.Nivel_de_tension) && x.Uso_del_perfil.Equals("Cierres")));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Cierres.Material_del_perfil));
                CcDV = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
                Cierres.Lmax__mm_ = 200 * Cierres.R33__mm_;
                Cierres.Lmin__mm_ = 200 * Cierres.Rz__mm_;
                entidadesBD.Entry(Cierres).State = EntityState.Modified;
                entidadesBD.SaveChanges();
            }
            else
            {
                Cierres = entidadesBD.Perfiles.ToList().Find(x => (x.id_subestacion == referencia.id_Subestacion && x.Nombre_del_perfil.Equals(cuerpo.PerfilCierre)));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Cierres.Material_del_perfil));
                CcDV = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
                Cierres.Lmax__mm_ = 200 * Cierres.R33__mm_;
                Cierres.Lmin__mm_ = 200 * Cierres.Rz__mm_;
                entidadesBD.Entry(Cierres).State = EntityState.Modified;
                entidadesBD.SaveChanges();
            }
            #endregion
            #region PUNTOS PRINCIPALES DEL CUERPO A CREAR
            Punto Point = new Punto();

            Punto A1 = new Punto();
            Punto A2 = new Punto();
            Punto B1 = new Punto();
            Punto B2 = new Punto();
            Punto C1 = new Punto();
            Punto C2 = new Punto();
            Punto D1 = new Punto();
            Punto D2 = new Punto();

            if (referencia.Orientacion_viga.Equals("X"))
            {
                A1.Name = "" + cuerpo.Id_Cuerpo + "A10";
                A1.X = referencia.X + cuerpo.Ancho_inferior * 0.5; 
                A1.Y = referencia.Y - cuerpo.Largo_Inferior * 0.5;
                A1.Z = referencia.Z - cuerpo.Ancho_inferior * 0.5;
                A1.MyName = "" + cuerpo.Id_Cuerpo + "A10";
                A1.IdCuerpo = cuerpo.Id_Cuerpo;

                A2.Name = "" + cuerpo.Id_Cuerpo + "A2";
                A2.X = referencia.X + cuerpo.Ancho_inferior * 0.5+cuerpo.Altura;
                A2.Y = referencia.Y - cuerpo.Largo_Inferior * 0.5;
                A2.Z = referencia.Z - cuerpo.Ancho_inferior * 0.5;
                A2.MyName = "" + cuerpo.Id_Cuerpo + "A2";
                A2.IdCuerpo = cuerpo.Id_Cuerpo;

                B1.Name = "" + cuerpo.Id_Cuerpo + "B10";
                B1.X = referencia.X + cuerpo.Ancho_inferior * 0.5; ;
                B1.Y = referencia.Y - cuerpo.Largo_Inferior * 0.5;
                B1.Z = referencia.Z + cuerpo.Ancho_inferior * 0.5;
                B1.MyName = "" + cuerpo.Id_Cuerpo + "B10";
                B1.IdCuerpo = cuerpo.Id_Cuerpo;

                B2.Name = "" + cuerpo.Id_Cuerpo + "B2";
                B2.X = referencia.X + cuerpo.Ancho_superior * 0.5 + cuerpo.Altura;
                B2.Y = referencia.Y - cuerpo.Largo_Superior * 0.5;
                B2.Z = referencia.Z + cuerpo.Ancho_inferior * 0.5;
                B2.MyName = "" + cuerpo.Id_Cuerpo + "B2";
                B2.IdCuerpo = cuerpo.Id_Cuerpo;

                C1.Name = "" + cuerpo.Id_Cuerpo + "C10";
                C1.X = referencia.X + cuerpo.Ancho_inferior * 0.5; ;
                C1.Y = referencia.Y + cuerpo.Largo_Inferior * 0.5;
                C1.Z = referencia.Z + cuerpo.Ancho_inferior * 0.5;
                C1.MyName = "" + cuerpo.Id_Cuerpo + "C10";
                C1.IdCuerpo = cuerpo.Id_Cuerpo;

                C2.Name = "" + cuerpo.Id_Cuerpo + "C2";
                C2.X = referencia.X + cuerpo.Ancho_superior * 0.5 + cuerpo.Altura;
                C2.Y = referencia.Y + cuerpo.Largo_Superior * 0.5;
                C2.Z = referencia.Z + cuerpo.Ancho_inferior * 0.5;
                C2.MyName = "" + cuerpo.Id_Cuerpo + "C2";
                C2.IdCuerpo = cuerpo.Id_Cuerpo;

                D1.Name = "" + cuerpo.Id_Cuerpo + "D10";
                D1.X = referencia.X + cuerpo.Ancho_inferior * 0.5; ;
                D1.Y = referencia.Y + cuerpo.Largo_Inferior * 0.5;
                D1.Z = referencia.Z - cuerpo.Ancho_inferior * 0.5;
                D1.MyName = "" + cuerpo.Id_Cuerpo + "D10";
                D1.IdCuerpo = cuerpo.Id_Cuerpo;

                D2.Name = "" + cuerpo.Id_Cuerpo + "D2";
                D2.X = referencia.X + cuerpo.Ancho_superior * 0.5 + cuerpo.Altura;
                D2.Y = referencia.Y + cuerpo.Largo_Superior * 0.5;
                D2.Z = referencia.Z - cuerpo.Ancho_inferior * 0.5;
                D2.MyName = "" + cuerpo.Id_Cuerpo + "D2";
                D2.IdCuerpo = cuerpo.Id_Cuerpo;                
            }
            else
            {
                A1.Name = "" + cuerpo.Id_Cuerpo + "A10";
                A1.X = referencia.X + cuerpo.Ancho_inferior * 0.5;
                A1.Y = referencia.Y + cuerpo.Largo_Inferior * 0.5;
                A1.Z = referencia.Z - cuerpo.Ancho_inferior * 0.5;
                A1.MyName = "" + cuerpo.Id_Cuerpo + "A10";
                A1.IdCuerpo = cuerpo.Id_Cuerpo;

                A2.Name = "" + cuerpo.Id_Cuerpo + "A2";
                A2.X = referencia.X + cuerpo.Ancho_inferior * 0.5;
                A2.Y = referencia.Y + cuerpo.Largo_Inferior * 0.5 + cuerpo.Altura;
                A2.Z = referencia.Z - cuerpo.Ancho_inferior * 0.5;
                A2.MyName = "" + cuerpo.Id_Cuerpo + "A2";
                A2.IdCuerpo = cuerpo.Id_Cuerpo;

                B1.Name = "" + cuerpo.Id_Cuerpo + "B10";
                B1.X = referencia.X + cuerpo.Ancho_inferior * 0.5; ;
                B1.Y = referencia.Y + cuerpo.Largo_Inferior * 0.5;
                B1.Z = referencia.Z + cuerpo.Ancho_inferior * 0.5;
                B1.MyName = "" + cuerpo.Id_Cuerpo + "B10";
                B1.IdCuerpo = cuerpo.Id_Cuerpo;

                B2.Name = "" + cuerpo.Id_Cuerpo + "B2";
                B2.X = referencia.X + cuerpo.Ancho_superior * 0.5;
                B2.Y = referencia.Y + cuerpo.Largo_Superior * 0.5 + cuerpo.Altura;
                B2.Z = referencia.Z + cuerpo.Ancho_inferior * 0.5;
                B2.MyName = "" + cuerpo.Id_Cuerpo + "B2";
                B2.IdCuerpo = cuerpo.Id_Cuerpo;

                C1.Name = "" + cuerpo.Id_Cuerpo + "C10";
                C1.X = referencia.X - cuerpo.Ancho_inferior * 0.5; ;
                C1.Y = referencia.Y + cuerpo.Largo_Inferior * 0.5;
                C1.Z = referencia.Z + cuerpo.Ancho_inferior * 0.5;
                C1.MyName = "" + cuerpo.Id_Cuerpo + "C10";
                C1.IdCuerpo = cuerpo.Id_Cuerpo;

                C2.Name = "" + cuerpo.Id_Cuerpo + "C2";
                C2.X = referencia.X - cuerpo.Ancho_superior * 0.5 ;
                C2.Y = referencia.Y + cuerpo.Largo_Superior * 0.5 + cuerpo.Altura;
                C2.Z = referencia.Z + cuerpo.Ancho_inferior * 0.5;
                C2.MyName = "" + cuerpo.Id_Cuerpo + "C2";
                C2.IdCuerpo = cuerpo.Id_Cuerpo;

                D1.Name = "" + cuerpo.Id_Cuerpo + "D10";
                D1.X = referencia.X - cuerpo.Ancho_inferior * 0.5; ;
                D1.Y = referencia.Y + cuerpo.Largo_Inferior * 0.5;
                D1.Z = referencia.Z - cuerpo.Ancho_inferior * 0.5;
                D1.MyName = "" + cuerpo.Id_Cuerpo + "D10";
                D1.IdCuerpo = cuerpo.Id_Cuerpo;

                D2.Name = "" + cuerpo.Id_Cuerpo + "D2";
                D2.X = referencia.X - cuerpo.Ancho_superior * 0.5;
                D2.Y = referencia.Y + cuerpo.Largo_Superior * 0.5 + cuerpo.Altura;
                D2.Z = referencia.Z - cuerpo.Ancho_inferior * 0.5;
                D2.MyName = "" + cuerpo.Id_Cuerpo + "D2";
                D2.IdCuerpo = cuerpo.Id_Cuerpo;
            }

            #endregion
            double Lm, nd, ld, dx = 0, dy = 0, dz = 0, X, Y, Z, ld1, ld2, Lm1, Lm2, Lm3;            
            Lm = Math.Sqrt(Math.Pow(A1.X - A2.X, 2) + Math.Pow(A1.Y - A2.Y, 2) + Math.Pow(A1.Z - A2.Z, 2));            
            double crxrz, crxrzd;
            #region longitud de montantes y diagonales
            crxrz = Montante_viga.Rz__mm_.Value / Montante_viga.R33__mm_.Value;
            crxrzd = Diagonal_viga.Rz__mm_.Value / Diagonal_viga.R33__mm_.Value;
            if (cuerpo.Tipo_de_diagonal.Equals("X"))
            {
                Lm3 = 150 * Montante_viga.Rz__mm_.Value / 1.2;
                ld1 = 190 * Diagonal_viga.Rz__mm_.Value / crxrzd;
                ld2 = 190 * Diagonal_viga.Rz__mm_.Value;
                ld = Math.Min(ld1, ld2);
                nd = Math.Round((Lm / Lm3) + 0.49);
                double a, b, a1, b1, a2, b2, L11, L21, L31, L41, L12, L22, L32, L42, Dx1, Dx2, Dy1, Dy2, Dz1, Dz2, X1, X2, Y1, Y2, Z1, Z2, K11, K21, K12, K22;
                a = 201;
                while (a >= 200)
                {
                    dx = Math.Abs(A1.X - A2.X) / (nd);
                    dy = Math.Abs(A1.Y - A2.Y) / (nd);
                    dz = Math.Abs(A1.Z - A2.Z) / (nd);
                    X = A1.X + dx;
                    Y = A1.Y + dy;
                    Z = A1.Z + dz;
                   

                    L11 = Math.Sqrt(Math.Pow(B1.X - X, 2) + Math.Pow(B1.Y - Y, 2) + Math.Pow(B1.Z - Z, 2));
                    L12 = Math.Sqrt(Math.Pow(D1.X - X, 2) + Math.Pow(D1.Y - Y, 2) + Math.Pow(D1.Z - Z, 2));
                    L21 =L11*0.5;
                    L22 =L12*0.5;                    
                    K11 = L11 / L21;
                    K21 = Diagonal_viga.Rz__mm_.Value / Diagonal_viga.R33__mm_.Value;
                    K12 = L12 / L22;
                    K22 = Diagonal_viga.Rz__mm_.Value / Diagonal_viga.R33__mm_.Value;
                    a1 = (L11 / Diagonal_viga.Rz__mm_.Value) * Math.Max(K21, (1 / K11));
                    a2 = (L12 / Diagonal_viga.Rz__mm_.Value) * Math.Max(K22, (1 / K12));
                    b1 = (L11 / Diagonal_viga.Rz__mm_.Value) * Math.Min(K21, (1 / K11));
                    b2 = (L12 / Diagonal_viga.Rz__mm_.Value) * Math.Min(K22, (1 / K12));
                    b = Math.Min(b1, b2);
                    a = Math.Max(a1, a2);
                    if (a > 200)
                    {
                        nd = nd + 1;
                    }

                }
                
            }
            else
            {
                Lm1 = 150 * Montante_viga.Rz__mm_.Value / (crxrz * 1.2);
                Lm2 = 150 * Montante_viga.Rz__mm_.Value * 2 / 1.2;
                Lm3 = Math.Min(Lm1, Lm2);
                ld = 190 * Diagonal_viga.Rz__mm_.Value;
                nd = Math.Round((Lm / Lm3) + 0.49);
                double a, a1, a2, L11, L12;
                a = 201;
                while (a >= 200)
                {
                    dx = Math.Abs(A1.X - A2.X) / (nd);
                    dy = Math.Abs(A1.Y - A2.Y) / (nd);
                    dz = Math.Abs(A1.Z - A2.Z) / (nd);
                    X = A1.X + dx;
                    Y = A1.Y + dy;
                    Z = A1.Z + dz;
                    L11 = Math.Sqrt(Math.Pow(B1.X - X, 2) + Math.Pow(B1.Y - Y, 2) + Math.Pow(B1.Z - Z, 2));
                    L12 = Math.Sqrt(Math.Pow(D1.X - X, 2) + Math.Pow(D1.Y - Y, 2) + Math.Pow(D1.Z - Z, 2));
                    a1 = (L11 / Diagonal_viga.Rz__mm_.Value);
                    a2 = (L12 / Diagonal_viga.Rz__mm_.Value);
                    a = Math.Max(a1, a2);
                    if (a > 200)
                    {
                        nd = nd + 1;
                    }

                }

            }
            #endregion
            
            int i = 0;
            
            dx = Math.Abs(A1.X - A2.X) / (nd + cuerpo.Division_adicional);
            dy = Math.Abs(A1.Y - A2.Y) / (nd + cuerpo.Division_adicional);
            dz = Math.Abs(A1.Z - A2.Z) / (nd + cuerpo.Division_adicional);
            nd = nd + cuerpo.Division_adicional;
            while (i < nd + 1)
            {
                Point.Name = "" + referencia.Id + "A1" + (i + referencia.Divisiones);
                Point.X = A1.X + dx * (i);
                Point.Y = A1.Y + dy * (i);
                Point.Z = A1.Z + dz * (i);
                Point.MyName = "" + referencia.Id + "A1" + (i + referencia.Divisiones);
                Point.IdCuerpo = cuerpo.Id_Cuerpo;
                agregar_punto(Point);

                Point.Name = "" + referencia.Id + "B1" + (i + referencia.Divisiones);
                Point.X = B1.X + dx * (i);
                Point.Y = B1.Y + dy * (i);
                Point.Z = B1.Z + dz * (i);
                Point.MyName = "" + referencia.Id + "B1" + (i + referencia.Divisiones);
                Point.IdCuerpo = cuerpo.Id_Cuerpo;
                agregar_punto(Point);

                Point.Name = "" + referencia.Id + "C1" + (i + referencia.Divisiones);
                Point.X = C1.X + dx * (i);
                Point.Y = C1.Y + dy * (i);
                Point.Z = C1.Z + dz * (i);
                Point.MyName = "" + referencia.Id + "C1" + (i + referencia.Divisiones);
                Point.IdCuerpo = cuerpo.Id_Cuerpo;
                agregar_punto(Point);

                Point.Name = "" + referencia.Id + "D1" + (i + referencia.Divisiones);
                Point.X = D1.X + dx * (i);
                Point.Y = D1.Y + dy * (i);
                Point.Z = D1.Z + dz * (i);
                Point.MyName = "" + referencia.Id + "D1" + (i + referencia.Divisiones);
                Point.IdCuerpo = cuerpo.Id_Cuerpo;
                agregar_punto(Point);
                i++;
            }//creacion de puntos 
            List<Punto> P_principales = new List<Punto>();
            P_principales = entidadesBD.Puntos.ToList();
            Borrar_puntos();
            foreach (Punto P in P_principales)
            {

                string name = P.Name.ToString();//debido al error no me dejaba pasar un parametro q era llave, 
                                                //entonces asigne el valor a una variable temporal
                ret = subestacion.PointObj.AddCartesian(P.X, P.Y, P.Z, ref name, P.MyName);//revisae error de ref P.Name 
                                                                                           //-> La variable name es la de arriba


            }
            i = 0;
            while (i < 4)
            {
                double ang = 0;
                string c1 = "";
                if (i == 0)
                {
                    c1 = "A";
                    ang = 0;
                }
                else if (i == 1)
                {
                    c1 = "B";
                    ang = 270;
                }
                else if (i == 2)
                {
                    c1 = "C";
                    ang = 180;
                }
                else if (i == 3)
                {
                    c1 = "D";
                    ang = 90;
                }
                int j = 0;
                                
                while (j < nd)
                {
                    string P2, P1;

                    if (cuerpo.Tipo_de_diagonal.Equals("X"))
                    {
                        P1 = "" + referencia.Id + c1 + "1" + (referencia.Divisiones + j);
                        P2 = "" + referencia.Id + c1 + "1" + (referencia.Divisiones + j + 1);
                        string M = "" + referencia.Id + "M" + (i + 1 + referencia.Divisiones) + j;
                        
                            ret = subestacion.FrameObj.AddByPoint(P1, P2, ref M, Montante_viga.Nombre_del_perfil, M);
                        #region asignacion de K
                        double L11, Kmajor, Kminor, Lr, KLr;
                        Punto Pun1 = P_principales.Find(x => x.Name.Equals(P1));
                        Punto Pun2 = P_principales.Find(x => x.Name.Equals(P2));
                        L11 = Math.Sqrt(Math.Pow(Pun1.X - Pun2.X, 2) + Math.Pow(Pun1.Y - Pun2.Y, 2) + Math.Pow(Pun1.Z - Pun2.Z, 2));
                        Lr = (L11 / Montante_viga.Rz__mm_.Value);
                        if (Lr <= 120)
                        {
                            Kmajor = 1;
                            Kminor = 1;

                        }
                        else if (Lr > 120 && Lr < 250)
                        {
                            KLr = 46.2 + 0.615 * Lr;
                            Kmajor = KLr / Lr;
                            Kminor = KLr / Lr;

                        }
                        else
                        {
                            Kmajor = 0;
                            Kminor = 0;
                        }
                        ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 17, 0.99);
                        ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 18, 0.99);
                        ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 19, Kmajor);
                        ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 20, Kminor);

                        #endregion
                            ret = subestacion.FrameObj.SetLocalAxes(M, ang);
                            ret = subestacion.FrameObj.SetGroupAssign(M, "Montante Viga");
                            ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento);
                            ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento+referencia.Id);
                        j = j + 1;

                    }
                    else
                    {
                        P1 = "" + referencia.Id + c1 + "1" + (referencia.Divisiones + j);
                        if (nd == 1 || j + 1 == nd || j + 2 == nd)
                        {
                            P2 = "" + referencia.Id + c1 + "1" + (referencia.Divisiones + nd);
                        }
                        else
                        {
                            P2 = "" + referencia.Id + c1 + "1" + (referencia.Divisiones + j + 2);
                        }
                        
                        string M = "" + referencia.Id + "M" + (i + 1 + referencia.Divisiones) + j;                        
                            ret = subestacion.FrameObj.AddByPoint(P1, P2, ref M, Montante_viga.Nombre_del_perfil, M);
                        #region asignacion de K
                        double a, b, a1, b1, a2, b2, L11, L21, L31, L41, L12, L22, L32, L42, Dx1, Dx2, Dy1, Dy2, Dz1, Dz2, X1, X2, Y1, Y2, Z1, Z2, K11, K21, K12, K22, Kmajor, Kminor, Lr, KLr;
                        Punto Pun1 = P_principales.Find(x => x.Name.Equals(P1));
                        Punto Pun2 = P_principales.Find(x => x.Name.Equals(P2));
                        K21 = Montante_viga.Rz__mm_.Value / Montante_viga.R33__mm_.Value;
                        L11 = Math.Sqrt(Math.Pow(Pun1.X - Pun2.X, 2) + Math.Pow(Pun1.Y - Pun2.Y, 2) + Math.Pow(Pun1.Z - Pun2.Z, 2));
                        L21 = L11 * 0.5;
                        if (K21 > 0.5)
                        {
                            Lr = (K21 * L11 / Montante_viga.Rz__mm_.Value);

                            if (Lr <= 120)
                            {
                                Kmajor = 1* K21;
                                Kminor = 1* K21;

                            }
                            else if (Lr > 120 && Lr < 250)
                            {
                                KLr = 46.2 + 0.615 * Lr;
                                Kmajor = KLr* K21 / Lr;
                                Kminor = KLr* K21 / Lr;

                            }
                            else
                            {
                                Kmajor = 0;
                                Kminor = 0;
                            }
                        }
                        else
                        {
                            Lr = (0.5 * L11 / Montante_viga.Rz__mm_.Value);
                            if (Lr <= 120)
                            {
                                Kmajor = 1 * 0.5;
                                Kminor = 1 * 0.5;

                            }
                            else if (Lr > 120 && Lr < 250)
                            {
                                KLr = 46.2 + 0.615 * Lr;
                                Kmajor = (KLr / Lr) * 0.5;
                                Kminor = (KLr / Lr) * 0.5;

                            }
                            else
                            {
                                Kmajor = 0;
                                Kminor = 0;

                            }
                        }
                        ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 17, 0.99);
                        ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 18, 0.99);
                        ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 19, Kmajor);
                        ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 20, Kminor);

                        #endregion
                        ret = subestacion.FrameObj.SetLocalAxes(M, ang);
                            ret = subestacion.FrameObj.SetGroupAssign(M, "Montante Viga");
                            ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento);
                            ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento + referencia.Id);
                        
                        j = j + 2;


                    }

                }



                i++;
            }//creacion de montantes            
           
            i = 0;
            while (i < 4)
            {
                string c1 = "";
                string c2 = "";
                string P1 = "";
                string P2 = "";
                string P3 = "";
                string P4 = "";
                string D = "";
                string Dx = "";
                if (i == 0)
                {
                    c1 = "A";
                    c2 = "B";
                }
                else if (i == 1)
                {
                    c1 = "B";
                    c2 = "C";
                }
                else if (i == 2)
                {
                    c1 = "C";
                    c2 = "D";
                }
                else if (i == 3)
                {
                    c1 = "D";
                    c2 = "A";
                }
                double j = 0;
                while (j < nd)
                {

                    if ((j % 2) == 0)
                    {
                        P1 = "" + referencia.Id + c1 + "" + 1 + "" + (j + referencia.Divisiones);
                        P2 = "" + referencia.Id + c2 + "" + 1 + "" + (j + 1 + referencia.Divisiones);                        
                        D = "" + referencia.Id + "D" + i + (j + 1 + referencia.Divisiones);
                        if (cuerpo.Tipo_de_diagonal.Equals("X"))
                        {
                            P3 = "" + referencia.Id + c2 + "" + 1 + "" + (j + referencia.Divisiones);
                            P4 = "" + referencia.Id + c1 + "" + 1 + "" + (j + 1 + referencia.Divisiones);                            
                            Dx = "" + referencia.Id + "D" + i + (j + 1 + referencia.Divisiones) + 2;

                        }

                    }
                    else
                    {
                        P1 = "" + referencia.Id + c2 + "" + 1 + "" + (j + referencia.Divisiones);
                        P2 = "" + referencia.Id + c1 + "" + 1 + "" + (j + 1 + referencia.Divisiones);                        
                        D = "" + referencia.Id + "D" + i + (j + 1 + referencia.Divisiones);
                        if (cuerpo.Tipo_de_diagonal.Equals("X"))
                        {
                            P3 = "" + referencia.Id + c1 + "" + 1 + "" + (j + referencia.Divisiones);
                            P4 = "" + referencia.Id + c2 + "" + 1 + "" + (j + 1 + referencia.Divisiones);                            
                            Dx = "" + referencia.Id + "D" + i + (j + 1 + referencia.Divisiones) + 2;
                        }
                    }

                    bool[] II = new bool[6];
                    bool[] JJ = new bool[6];
                    double[] Valorinicio = new double[6];
                    double[] Valorfinal = new double[6];
                    eItemType tipo = eItemType.Objects;
                    II[3] = true;
                    II[4] = true;
                    II[5] = true;
                    JJ[4] = true;
                    JJ[5] = true;
                    ret = subestacion.FrameObj.AddByPoint(P1, P2, ref D, Diagonal_viga.Nombre_del_perfil, D);
                    ret = subestacion.FrameObj.SetReleases(D, ref II, ref JJ, ref Valorinicio, ref Valorfinal, tipo);
                    ret = subestacion.FrameObj.SetGroupAssign(D, "Diagonal viga");
                    if ((j % 2) == 0)
                    {
                        double ang = 180;
                        ret = subestacion.FrameObj.SetLocalAxes(D, ang);
                    }
                    ret = subestacion.FrameObj.SetGroupAssign(D, referencia.Elemento);
                    ret = subestacion.FrameObj.SetGroupAssign(D, referencia.Elemento + referencia.Id);
                    if (cuerpo.Tipo_de_diagonal.Equals("X"))
                    {
                        ret = subestacion.FrameObj.AddByPoint(P3, P4, ref Dx, Diagonal_viga.Nombre_del_perfil, Dx);
                        ret = subestacion.FrameObj.SetReleases(Dx, ref II, ref JJ, ref Valorinicio, ref Valorfinal, tipo);
                        if ((j % 2) != 0)
                        {
                            double ang = 180;
                            ret = subestacion.FrameObj.SetLocalAxes(Dx, ang);
                        }
                        ret = subestacion.FrameObj.SetGroupAssign(Dx, "Diagonal viga");
                        ret = subestacion.FrameObj.SetGroupAssign(Dx, referencia.Elemento);
                        ret = subestacion.FrameObj.SetGroupAssign(D, referencia.Elemento + referencia.Id);
                    }


                    //***************************************************
                    if (cuerpo.Tipo_de_diagonal.Equals("X"))
                    {
                        #region asignacion de K
                        double a, b, a1, b1, a2, b2, L11, L21, L31, L41, L12, L22, L32, L42, Dx1, Dx2, Dy1, Dy2, Dz1, Dz2, X1 = 1, X2, Y1 = 1, Y2, Z1 = 1, Z2, K11, K21, K12, K22, Kmajor, Kminor, Lr, KLr;
                        Punto Pun1 = P_principales.Find(x => x.Name.Equals(P1));
                        Punto Pun2 = P_principales.Find(x => x.Name.Equals(P2));
                        K21 = Diagonal_viga.Rz__mm_.Value / Diagonal_viga.R33__mm_.Value;
                        
                        L11 = Math.Sqrt(Math.Pow(Pun1.X - Pun2.X, 2) + Math.Pow(Pun1.Y - Pun2.Y, 2) + Math.Pow(Pun1.Z - Pun2.Z, 2));
                        L21 = L11*0.5;
                        
                        K11 = L21 / L11;
                        if (K21 > K11)
                        {
                            Lr = (K21 * L11 / Diagonal_viga.Rz__mm_.Value);

                            if (Lr <= 120)
                            {
                                KLr = 30 + 0.75 * Lr;
                                Kmajor = KLr* K21 / Lr;
                                Kminor = KLr* K21 / Lr;

                            }
                            else if (Lr > 120 && Lr < 200)
                            {

                                Kmajor = 1* K21;
                                Kminor = 1* K21;

                            }
                            else
                            {
                                Kmajor = 0;
                                Kminor = 0;
                            }
                        }
                        else
                        {
                            Lr = (K11 * L11 / Diagonal_viga.Rz__mm_.Value);
                            if (Lr <= 120)
                            {
                                KLr = 30 + 0.75 * Lr;
                                Kmajor = (KLr / Lr) * K11;
                                Kminor = (KLr / Lr) * K11;

                            }
                            else if (Lr > 120 && Lr < 200)
                            {
                                Kmajor = 1 * K11;
                                Kminor = 1 * K11;

                            }
                            else
                            {
                                Kmajor = 0;
                                Kminor = 0;

                            }
                        }
                        ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 17, 0.99);
                        ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 18, 0.99);
                        ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 19, Kmajor);
                        ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 20, Kminor);
                        ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(Dx, 17, 0.99);
                        ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(Dx, 18, 0.99);
                        ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(Dx, 19, Kmajor);
                        ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(Dx, 20, Kminor);

                        #endregion


                    }//definicion de K para diagonales en X
                    else
                    {
                        #region asignacion de K
                        double a, b, a1, b1, a2, b2, L11, L21, L31, L41, L12, L22, L32, L42, Dx1, Dx2, Dy1, Dy2, Dz1, Dz2, X1 = 1, X2, Y1 = 1, Y2, Z1 = 1, Z2, K11, K21, K12, K22, Kmajor, Kminor, Lr, KLr;
                        Punto Pun1 = P_principales.Find(x => x.Name.Equals(P1));
                        Punto Pun2 = P_principales.Find(x => x.Name.Equals(P2));
                        L11 = Math.Sqrt(Math.Pow(Pun1.X - Pun2.X, 2) + Math.Pow(Pun1.Y - Pun2.Y, 2) + Math.Pow(Pun1.Z - Pun2.Z, 2));
                        Lr = (L11 / Diagonal_viga.Rz__mm_.Value);

                        if (Lr <= 120)
                        {
                            KLr = 60 + 0.5 * Lr;
                            Kmajor = KLr / Lr;
                            Kminor = KLr / Lr;

                        }
                        else if (Lr > 120 && Lr < 200)
                        {

                            Kmajor = 1;
                            Kminor = 1;

                        }
                        else
                        {
                            Kmajor = 0;
                            Kminor = 0;
                        }
                        ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 17, 0.99);
                        ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 18, 0.99);
                        ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 19, Kmajor);
                        ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(D, 20, Kminor);


                        #endregion

                    }//definicion de K para diagonales en Z



                    j++;
                }


                i++;
            }//creacion de diagonales

            //*******************************************************
            if (con< numCuerpos)
            {
                i = 0;
                while (i < 4)
                {
                    bool[] II = new bool[6];
                    bool[] JJ = new bool[6];
                    double[] Valorinicio = new double[6];
                    double[] Valorfinal = new double[6];
                    eItemType tipo = eItemType.Objects;
                    II[3] = true;
                    II[4] = true;
                    II[5] = true;
                    JJ[4] = true;
                    JJ[5] = true;
                    string c1 = "";
                    string c2 = "";
                    if (i == 0)
                    {
                        c1 = "A";
                        c2 = "B";
                    }
                    else if (i == 1)
                    {
                        c1 = "B";
                        c2 = "C";
                    }
                    else if (i == 2)
                    {
                        c1 = "C";
                        c2 = "D";
                    }
                    else if (i == 3)
                    {
                        c1 = "D";
                        c2 = "A";
                    }

                    string P1 = "" + referencia.Id + c1 + "1" + (nd + referencia.Divisiones);
                    string P2 = "" + referencia.Id + c2 + "1" + (nd + referencia.Divisiones);
                    string M = "" + referencia.Id + "C" + (i + 1 + (nd + referencia.Divisiones));
                    ret = subestacion.FrameObj.AddByPoint(P1, P2, ref M, Cierres.Nombre_del_perfil, M);
                    #region asignacion de K
                    double L11, Kmajor, Kminor, Lr, KLr;
                    Punto Pun1 = P_principales.Find(x => x.Name.Equals(P1));
                    Punto Pun2 = P_principales.Find(x => x.Name.Equals(P2));
                    L11 = Math.Sqrt(Math.Pow(Pun1.X - Pun2.X, 2) + Math.Pow(Pun1.Y - Pun2.Y, 2) + Math.Pow(Pun1.Z - Pun2.Z, 2));

                    Lr = (L11 / Cierres.Rz__mm_.Value);
                    if (Lr <= 120)
                    {
                        Kmajor = 1;
                        Kminor = 1;

                    }
                    else if (Lr > 120 && Lr < 250)
                    {
                        KLr = 46.2 + 0.615 * Lr;
                        Kmajor = 1;
                        Kminor = 1;

                    }
                    else
                    {
                        Kmajor = 0;
                        Kminor = 0;
                    }
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 17, 0.99);
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 18, 0.99);
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 19, Kmajor);
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 20, Kminor);

                    #endregion
                    ret = subestacion.FrameObj.SetReleases(M, ref II, ref JJ, ref Valorinicio, ref Valorfinal, tipo);
                    ret = subestacion.FrameObj.SetGroupAssign(M, "Cierres");
                    ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento);
                    ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento + referencia.Id);

                    
                    i++;
                }//cierre de viga

                i = 0;
                while (i < 2)
                {
                    bool[] II = new bool[6];
                    bool[] JJ = new bool[6];
                    double[] Valorinicio = new double[6];
                    double[] Valorfinal = new double[6];
                    eItemType tipo = eItemType.Objects;
                    II[3] = true;
                    II[4] = true;
                    II[5] = true;
                    JJ[4] = true;
                    JJ[5] = true;

                    string c1 = "";
                    string c2 = "";
                    if (i == 0)
                    {
                        c1 = "A";
                        c2 = "C";
                    }
                    else if (i == 1)
                    {
                        c1 = "B";
                        c2 = "D";
                    }
                    string P1 = "" + referencia.Id + c1 + "1" + (nd + referencia.Divisiones);
                    string  P2 = "" + referencia.Id + c2 + "1" + (nd + referencia.Divisiones);
                    string  M = "" + referencia.Id + "DV" + (i + 1 + (nd + referencia.Divisiones));
                    ret = subestacion.FrameObj.AddByPoint(P1, P2, ref M, Diagonal_viga.Nombre_del_perfil, M);
                    #region asignacion de K
                    double a, b, a1, b1, a2, b2, L11, L21, L31, L41, L12, L22, L32, L42, Dx1, Dx2, Dy1, Dy2, Dz1, Dz2, X1 = 1, X2, Y1 = 1, Y2, Z1 = 1, Z2, K11, K21, K12, K22, Kmajor, Kminor, Lr, KLr;
                    Punto Pun1 = P_principales.Find(x => x.Name.Equals(P1));
                    Punto Pun2 = P_principales.Find(x => x.Name.Equals(P2));
                    K21 = Diagonal_viga.Rz__mm_.Value / Diagonal_viga.R33__mm_.Value;
                    
                    L11 = Math.Sqrt(Math.Pow(Pun1.X - Pun2.X, 2) + Math.Pow(Pun1.Y - Pun2.Y, 2) + Math.Pow(Pun1.Z - Pun2.Z, 2));
                    L21 = L11*0.5;
                   
                    K11 = L21 / L11;
                    if (K21 > K11)
                    {
                        Lr = (K21 * L11 / Diagonal_viga.Rz__mm_.Value);

                        if (Lr <= 120)
                        {
                            KLr = 30 + 0.75 * Lr;
                            Kmajor = KLr* K21 / Lr;
                            Kminor = KLr* K21 / Lr;

                        }
                        else if (Lr > 120 && Lr < 200)
                        {

                            Kmajor = 1*K21;
                            Kminor = 1* K21;

                        }
                        else
                        {
                            Kmajor = 0;
                            Kminor = 0;
                        }
                    }
                    else
                    {
                        Lr = (K11 * L11 / Diagonal_viga.Rz__mm_.Value);
                        if (Lr <= 120)
                        {
                            KLr = 30 + 0.75 * Lr;
                            Kmajor = (KLr / Lr) * K11;
                            Kminor = (KLr / Lr) * K11;

                        }
                        else if (Lr > 120 && Lr < 200)
                        {
                            Kmajor = 1 *K11;
                            Kminor = 1 * K11;

                        }
                        else
                        {
                            Kmajor = 0;
                            Kminor = 0;

                        }
                    }
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 17, 0.99);
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 18, 0.99);
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 19, Kmajor);
                    ret = subestacion.DesignSteel.ASCE_10_97.SetOverwrite(M, 20, Kminor);
                    #endregion

                    ret = subestacion.FrameObj.SetReleases(M, ref II, ref JJ, ref Valorinicio, ref Valorfinal, tipo);
                    if (i == 1)
                    {
                        double ang = 180;
                        ret = subestacion.FrameObj.SetLocalAxes(M, ang);
                    }
                    ret = subestacion.FrameObj.SetGroupAssign(M, "Diagonal viga");
                    ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento);
                    ret = subestacion.FrameObj.SetGroupAssign(M, referencia.Elemento + referencia.Id);
                    i++;
                }//diagonales de cierre de viga

                List<Carga> cargas_subensambles = entidadesBD.Cargas.ToList();
                List<Carga> carga_viga = cargas_subensambles.FindAll(x => x.Id_Ensamble == referencia.Id);
                if(carga_viga.Count!=0)
                {
                    #region carga en viga                
                    List<CARGAS_DE_CONEXIÓN> conexion = entidadesBD.CARGAS_DE_CONEXIÓN.ToList();
                    string c1 = "";
                    string P1 = "";
                    string P2 = "";
                    Carga car1 = new Carga();
                    Carga car2 = new Carga();
                    Carga car = new Carga();
                    double signo;
                    double[] PP1, CC1, V1, CT1,CMM1, PP2, CC2, V2, CT2,CMM2, PP, CC, V, CT;
                    P2 = "" + referencia.Id + "A1" + (nd + referencia.Divisiones);
                    List<Elemento> eleme = entidadesBD.Elementos.ToList();
                    Elemento elemento = eleme.Find(x => x.Id == referencia.Id_Elemento);
                    if (carga_viga.Count == 1)
                    {
                        #region car1
                        car1 = carga_viga[0];
                        PP1 = new double[6];
                        CC1 = new double[6];
                        V1 = new double[6];
                        CT1 = new double[6];
                        CMM1 = new double[6];
                        CARGAS_DE_CONEXIÓN tendido1 = conexion.Find(x => x.Id == car1.Id_Carga_de_conexion);
                        if (referencia.Orientacion_viga.ToUpper().Equals("X"))
                        {

                            if (car1.Orientacion >= 0 && car1.Orientacion <= 180)
                            {
                                signo = 1;
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            else
                            {
                                signo = -1;
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            PP1[2] = -tendido1.Peso___kg_;
                            CMM1[2] = -250;
                            CC1[0] = tendido1.Corto_Transversal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180)) + tendido1.Corto_Longitudinal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180));
                            CC1[1] = (tendido1.Corto_Transversal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180)) + tendido1.Corto_Longitudinal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180))) * signo;
                            CT1[0] = tendido1.Tiro__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180));
                            CT1[1] = signo * tendido1.Tiro__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180));
                            V1[0] = tendido1.Viento_Transversal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180)) + tendido1.Viento_Longitudinal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180));
                            V1[1] = signo * (tendido1.Viento_Transversal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180)) + tendido1.Viento_Longitudinal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180)));


                        }//definicion de cargas segun la orientacion de la viga (dirreccion x)
                        else
                        {
                            if (car1.Orientacion >= 90 && car1.Orientacion <= 270)
                            {
                                signo = -1;
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            else
                            {
                                signo = 1;
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            PP1[2] = -tendido1.Peso___kg_;
                            CMM1[2] = -250;
                            CC1[1] = tendido1.Corto_Transversal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180)) + tendido1.Corto_Longitudinal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180));
                            CC1[0] = (tendido1.Corto_Transversal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180)) + tendido1.Corto_Longitudinal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180))) * signo;
                            CT1[1] = tendido1.Tiro__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180));
                            CT1[0] = signo * tendido1.Tiro__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180));
                            V1[1] = tendido1.Viento_Transversal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180)) + tendido1.Viento_Longitudinal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180));
                            V1[0] = signo * (tendido1.Viento_Transversal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180)) + tendido1.Viento_Longitudinal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180)));

                        }//definicion de cargas segun la orientacion de la viga (dirreccion y)
                        #endregion
                        #region Aisgnacion de car1
                        if (referencia.Orientacion_viga.ToUpper().Equals("X"))
                        {
                            if (car1.Orientacion >= 0 && car1.Orientacion <= 180)
                            {
                                if (car1.Posicion.Equals("Superior"))
                                {
                                    c1 = "C";
                                }//definicion de punto superior o inferior a cargar
                                else
                                {
                                    c1 = "D";
                                }//definicion de punto superior o inferior a cargar
                                P1 = "" + referencia.Id + c1 + "1" + (nd + referencia.Divisiones);
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            else
                            {
                                if (car1.Posicion.Equals("Superior"))
                                {
                                    c1 = "B";
                                }//definicion de punto superior o inferior a cargar
                                else
                                {
                                    c1 = "A";
                                }//definicion de punto superior o inferior a cargar
                                P1 = "" + referencia.Id + c1 + "1" + (nd + referencia.Divisiones);
                            }//definicion de direccion positiva o negativa para las cargas longitudinales

                            ret = subestacion.PointObj.SetLoadForce(P1, "PP", ref PP1);
                            ret = subestacion.PointObj.SetLoadForce(P1, "CC", ref CC1);
                            ret = subestacion.PointObj.SetLoadForce(P1, "CT", ref CT1);
                            ret = subestacion.PointObj.SetLoadForce(P1, "CVcx", ref V1);
                            ret = subestacion.PointObj.SetLoadForce(P1, "CMM", ref CMM1);
                        }//asignacion de cargas en el modelo
                        else
                        {
                            if (car1.Orientacion >= 90 && car1.Orientacion <= 270)
                            {
                                if (car1.Posicion.Equals("Superior"))
                                {
                                    c1 = "C";
                                }//definicion de punto superior o inferior a cargar
                                else
                                {
                                    c1 = "D";
                                }//definicion de punto superior o inferior a cargar
                                P1 = "" + referencia.Id + c1 + "1" + (nd + referencia.Divisiones);
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            else
                            {
                                if (car1.Posicion.Equals("Superior"))
                                {
                                    c1 = "B";
                                }//definicion de punto superior o inferior a cargar
                                else
                                {
                                    c1 = "A";
                                }//definicion de punto superior o inferior a cargar
                                P1 = "" + referencia.Id + c1 + "1" + (nd + referencia.Divisiones);
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            ret = subestacion.PointObj.SetLoadForce(P1, "PP", ref PP1);
                            ret = subestacion.PointObj.SetLoadForce(P1, "CC", ref CC1);
                            ret = subestacion.PointObj.SetLoadForce(P1, "CT", ref CT1);
                            ret = subestacion.PointObj.SetLoadForce(P1, "CVcy", ref V1);
                            ret = subestacion.PointObj.SetLoadForce(P1, "CMM", ref CMM1);
                        }
                        #endregion
                    }//una sola templa
                    else if (carga_viga[0].Separacion_diferentes.Equals("Si") || carga_viga[0].Separacion_diferentes.Equals("Si"))
                    {
                        #region car1
                        car1 = carga_viga[0];
                        PP1 = new double[6];
                        CC1 = new double[6];
                        V1 = new double[6];
                        CT1 = new double[6];
                        CARGAS_DE_CONEXIÓN tendido1 = conexion.Find(x => x.Id == car1.Id_Carga_de_conexion);
                        if (referencia.Orientacion_viga.ToUpper().Equals("X"))
                        {

                            if (car1.Orientacion >= 0 && car1.Orientacion <= 180)
                            {
                                signo = 1;
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            else
                            {
                                signo = -1;
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            PP1[2] = -tendido1.Peso___kg_;
                            CC1[0] = tendido1.Corto_Transversal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180)) + tendido1.Corto_Longitudinal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180));
                            CC1[1] = (tendido1.Corto_Transversal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180)) + tendido1.Corto_Longitudinal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180))) * signo;
                            CT1[0] = tendido1.Tiro__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180));
                            CT1[1] = signo * tendido1.Tiro__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180));
                            V1[0] = tendido1.Viento_Transversal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180)) + tendido1.Viento_Longitudinal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180));
                            V1[1] = signo * (tendido1.Viento_Transversal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180)) + tendido1.Viento_Longitudinal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180)));


                        }//definicion de cargas segun la orientacion de la viga (dirreccion x)
                        else
                        {
                            if (car1.Orientacion >= 90 && car1.Orientacion <= 270)
                            {
                                signo = -1;
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            else
                            {
                                signo = 1;
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            PP1[2] = -tendido1.Peso___kg_;
                            CC1[1] = tendido1.Corto_Transversal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180)) + tendido1.Corto_Longitudinal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180));
                            CC1[0] = (tendido1.Corto_Transversal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180)) + tendido1.Corto_Longitudinal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180))) * signo;
                            CT1[1] = tendido1.Tiro__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180));
                            CT1[0] = signo * tendido1.Tiro__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180));
                            V1[1] = tendido1.Viento_Transversal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180)) + tendido1.Viento_Longitudinal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180));
                            V1[0] = signo * (tendido1.Viento_Transversal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180)) + tendido1.Viento_Longitudinal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180)));

                        }//definicion de cargas segun la orientacion de la viga (dirreccion y)
                        #endregion
                        #region car2
                        PP2 = new double[6];
                        CC2 = new double[6];
                        V2 = new double[6];
                        CT2 = new double[6];
                        car2 = carga_viga[1];
                        CARGAS_DE_CONEXIÓN tendido2 = conexion.Find(x => x.Id == car2.Id_Carga_de_conexion);
                        if (referencia.Orientacion_viga.ToUpper().Equals("X"))
                        {

                            if (car2.Orientacion >= 0 && car2.Orientacion <= 180)
                            {
                                signo = 1;
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            else
                            {
                                signo = -1;
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            PP2[2] = -tendido2.Peso___kg_;
                            CC2[0] = tendido2.Corto_Transversal__kg_ * Math.Abs(Math.Sin(car2.Orientacion * Math.PI / 180)) + tendido2.Corto_Longitudinal__kg_ * Math.Abs(Math.Cos(car2.Orientacion * Math.PI / 180));
                            CC2[1] = (tendido2.Corto_Transversal__kg_ * Math.Abs(Math.Cos(car2.Orientacion * Math.PI / 180)) + tendido2.Corto_Longitudinal__kg_ * Math.Abs(Math.Sin(car2.Orientacion * Math.PI / 180))) * signo;
                            CT2[0] = tendido2.Tiro__kg_ * Math.Abs(Math.Cos(car2.Orientacion * Math.PI / 180));
                            CT2[1] = signo * tendido2.Tiro__kg_ * Math.Abs(Math.Sin(car2.Orientacion * Math.PI / 180));
                            V2[0] = tendido2.Viento_Transversal__kg_ * Math.Abs(Math.Sin(car2.Orientacion * Math.PI / 180)) + tendido2.Viento_Longitudinal__kg_ * Math.Abs(Math.Cos(car2.Orientacion * Math.PI / 180));
                            V2[1] = signo * (tendido2.Viento_Transversal__kg_ * Math.Abs(Math.Cos(car2.Orientacion * Math.PI / 180)) + tendido2.Viento_Longitudinal__kg_ * Math.Abs(Math.Sin(car2.Orientacion * Math.PI / 180)));

                        }
                        else
                        {
                            if (car2.Orientacion >= 90 && car1.Orientacion <= 270)
                            {
                                signo = -1;
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            else
                            {
                                signo = 1;
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            PP1[2] = -tendido2.Peso___kg_;
                            CC1[1] = tendido2.Corto_Transversal__kg_ * Math.Abs(Math.Cos(car2.Orientacion * Math.PI / 180)) + tendido2.Corto_Longitudinal__kg_ * Math.Abs(Math.Sin(car2.Orientacion * Math.PI / 180));
                            CC1[0] = (tendido2.Corto_Transversal__kg_ * Math.Abs(Math.Sin(car2.Orientacion * Math.PI / 180)) + tendido2.Corto_Longitudinal__kg_ * Math.Abs(Math.Cos(car2.Orientacion * Math.PI / 180))) * signo;
                            CT1[1] = tendido2.Tiro__kg_ * Math.Abs(Math.Sin(car2.Orientacion * Math.PI / 180));
                            CT1[0] = signo * tendido2.Tiro__kg_ * Math.Abs(Math.Cos(car2.Orientacion * Math.PI / 180));
                            V1[1] = tendido2.Viento_Transversal__kg_ * Math.Abs(Math.Cos(car2.Orientacion * Math.PI / 180)) + tendido2.Viento_Longitudinal__kg_ * Math.Abs(Math.Sin(car2.Orientacion * Math.PI / 180));
                            V1[0] = signo * (tendido2.Viento_Transversal__kg_ * Math.Abs(Math.Sin(car2.Orientacion * Math.PI / 180)) + tendido2.Viento_Longitudinal__kg_ * Math.Abs(Math.Cos(car2.Orientacion * Math.PI / 180)));

                        }
                        #endregion
                        #region definicion de tiro unilateral en vigas
                        if (referencia.Orientacion_viga.ToUpper().Equals("X"))
                        {
                            if (Math.Abs(CT1[1] * 1.1 + CC1[1] * 0.75 + V1[1] * 1.2) < Math.Abs(CT2[1] * 1.1 + CC2[1] * 0.75 + V2[1] * 1.2))
                            {
                                CT1[1] = 0;
                                CC1[1] = 0;
                                V1[1] = 0;
                            }
                            else
                            {
                                CT2[1] = 0;
                                CC2[1] = 0;
                                V2[1] = 0;
                            }
                        }
                        else
                        {
                            if (Math.Abs(CT1[0] * 1.1 + CC1[0] * 0.75 + V1[0] * 1.2) < Math.Abs(CT2[0] * 1.1 + CC2[0] * 0.75 + V2[0] * 1.2))
                            {
                                CT1[0] = 0;
                                CC1[0] = 0;
                                V1[0] = 0;
                            }
                            else
                            {
                                CT2[0] = 0;
                                CC2[0] = 0;
                                V2[0] = 0;
                            }

                        }
                        #endregion


                    }//dos templas de espaciamientos diferentes o desfasadas
                    else
                    {
                        #region car1
                        car1 = carga_viga[0];
                        PP1 = new double[6];
                        CC1 = new double[6];
                        V1 = new double[6];
                        CT1 = new double[6];
                        CMM1 = new double[6];
                        CARGAS_DE_CONEXIÓN tendido1 = conexion.Find(x => x.Id == car1.Id_Carga_de_conexion);
                        if (referencia.Orientacion_viga.ToUpper().Equals("X"))
                        {

                            if (car1.Orientacion >= 0 && car1.Orientacion <= 180)
                            {
                                signo = 1;
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            else
                            {
                                signo = -1;
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            PP1[2] = -tendido1.Peso___kg_;
                            CMM1[2] = -250;
                            CC1[0] = tendido1.Corto_Transversal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180)) + tendido1.Corto_Longitudinal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180));
                            CC1[1] = (tendido1.Corto_Transversal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180)) + tendido1.Corto_Longitudinal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180))) * signo;
                            CT1[0] = tendido1.Tiro__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180));
                            CT1[1] = signo * tendido1.Tiro__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180));
                            V1[0] = tendido1.Viento_Transversal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180)) + tendido1.Viento_Longitudinal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180));
                            V1[1] = signo * (tendido1.Viento_Transversal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180)) + tendido1.Viento_Longitudinal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180)));


                        }//definicion de cargas segun la orientacion de la viga (dirreccion x)
                        else
                        {
                            if (car1.Orientacion >= 90 && car1.Orientacion <= 270)
                            {
                                signo = -1;
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            else
                            {
                                signo = 1;
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            PP1[2] = -tendido1.Peso___kg_;
                            CMM1[2] = -250;
                            CC1[1] = tendido1.Corto_Transversal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180)) + tendido1.Corto_Longitudinal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180));
                            CC1[0] = (tendido1.Corto_Transversal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180)) + tendido1.Corto_Longitudinal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180))) * signo;
                            CT1[1] = tendido1.Tiro__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180));
                            CT1[0] = signo * tendido1.Tiro__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180));
                            V1[1] = tendido1.Viento_Transversal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180)) + tendido1.Viento_Longitudinal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180));
                            V1[0] = signo * (tendido1.Viento_Transversal__kg_ * Math.Abs(Math.Sin(car1.Orientacion * Math.PI / 180)) + tendido1.Viento_Longitudinal__kg_ * Math.Abs(Math.Cos(car1.Orientacion * Math.PI / 180)));

                        }//definicion de cargas segun la orientacion de la viga (dirreccion y)
                        #endregion
                        #region car2
                        PP2 = new double[6];
                        CC2 = new double[6];
                        V2 = new double[6];
                        CT2 = new double[6];
                        CMM2 = new double[6];
                        car2 = carga_viga[1];

                        CARGAS_DE_CONEXIÓN tendido2 = conexion.Find(x => x.Id == car2.Id_Carga_de_conexion);
                        if (referencia.Orientacion_viga.ToUpper().Equals("X"))
                        {

                            if (car2.Orientacion >= 0 && car2.Orientacion <= 180)
                            {
                                signo = 1;
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            else
                            {
                                signo = -1;
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            PP2[2] = -tendido2.Peso___kg_;
                            CMM2[2] = -250;
                            CC2[0] = tendido2.Corto_Transversal__kg_ * Math.Abs(Math.Sin(car2.Orientacion * Math.PI / 180)) + tendido2.Corto_Longitudinal__kg_ * Math.Abs(Math.Cos(car2.Orientacion * Math.PI / 180));
                            CC2[1] = (tendido2.Corto_Transversal__kg_ * Math.Abs(Math.Cos(car2.Orientacion * Math.PI / 180)) + tendido2.Corto_Longitudinal__kg_ * Math.Abs(Math.Sin(car2.Orientacion * Math.PI / 180))) * signo;
                            CT2[0] = tendido2.Tiro__kg_ * Math.Abs(Math.Cos(car2.Orientacion * Math.PI / 180));
                            CT2[1] = signo * tendido2.Tiro__kg_ * Math.Abs(Math.Sin(car2.Orientacion * Math.PI / 180));
                            V2[0] = tendido2.Viento_Transversal__kg_ * Math.Abs(Math.Sin(car2.Orientacion * Math.PI / 180)) + tendido2.Viento_Longitudinal__kg_ * Math.Abs(Math.Cos(car2.Orientacion * Math.PI / 180));
                            V2[1] = signo * (tendido2.Viento_Transversal__kg_ * Math.Abs(Math.Cos(car2.Orientacion * Math.PI / 180)) + tendido2.Viento_Longitudinal__kg_ * Math.Abs(Math.Sin(car2.Orientacion * Math.PI / 180)));

                        }
                        else
                        {
                            if (car2.Orientacion >= 90 && car1.Orientacion <= 270)
                            {
                                signo = -1;
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            else
                            {
                                signo = 1;
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            PP2[2] = -tendido2.Peso___kg_;
                            CMM2[2] = -250;
                            CC2[1] = tendido2.Corto_Transversal__kg_ * Math.Abs(Math.Cos(car2.Orientacion * Math.PI / 180)) + tendido2.Corto_Longitudinal__kg_ * Math.Abs(Math.Sin(car2.Orientacion * Math.PI / 180));
                            CC2[0] = (tendido2.Corto_Transversal__kg_ * Math.Abs(Math.Sin(car2.Orientacion * Math.PI / 180)) + tendido2.Corto_Longitudinal__kg_ * Math.Abs(Math.Cos(car2.Orientacion * Math.PI / 180))) * signo;
                            CT2[1] = tendido2.Tiro__kg_ * Math.Abs(Math.Sin(car2.Orientacion * Math.PI / 180));
                            CT2[0] = signo * tendido2.Tiro__kg_ * Math.Abs(Math.Cos(car2.Orientacion * Math.PI / 180));
                            V2[1] = tendido2.Viento_Transversal__kg_ * Math.Abs(Math.Cos(car2.Orientacion * Math.PI / 180)) + tendido2.Viento_Longitudinal__kg_ * Math.Abs(Math.Sin(car2.Orientacion * Math.PI / 180));
                            V2[0] = signo * (tendido2.Viento_Transversal__kg_ * Math.Abs(Math.Sin(car2.Orientacion * Math.PI / 180)) + tendido2.Viento_Longitudinal__kg_ * Math.Abs(Math.Cos(car2.Orientacion * Math.PI / 180)));

                        }
                        #endregion

                        #region definicion de tiro unilateral en vigas
                        if (referencia.Orientacion_viga.ToUpper().Equals("X"))
                        {
                            if (Math.Abs(CT1[1] * 1.1 + CC1[1] * 0.75 + V1[1] * 1.2) < Math.Abs(CT2[1] * 1.1 + CC2[1] * 0.75 + V2[1] * 1.2))
                            {
                                CT1[1] = 0;
                                CC1[1] = 0;
                                V1[1] = 0;
                            }
                            else
                            {
                                CT2[1] = 0;
                                CC2[1] = 0;
                                V2[1] = 0;
                            }
                        }
                        else
                        {
                            if (Math.Abs(CT1[0] * 1.1 + CC1[0] * 0.75 + V1[0] * 1.2) < Math.Abs(CT2[0] * 1.1 + CC2[0] * 0.75 + V2[0] * 1.2))
                            {
                                CT1[0] = 0;
                                CC1[0] = 0;
                                V1[0] = 0;
                            }
                            else
                            {
                                CT2[0] = 0;
                                CC2[0] = 0;
                                V2[0] = 0;
                            }

                        }
                        #endregion

                        #region Asignacion de car1
                        if (referencia.Orientacion_viga.ToUpper().Equals("X"))
                        {
                            if (car1.Orientacion >= 0 && car1.Orientacion <= 180)
                            {
                                if (car1.Posicion.Equals("Superior"))
                                {
                                    c1 = "C";
                                }//definicion de punto superior o inferior a cargar
                                else
                                {
                                    c1 = "D";
                                }//definicion de punto superior o inferior a cargar
                                P1 = "" + referencia.Id + c1 + "1" + (nd + referencia.Divisiones);
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            else
                            {
                                if (car1.Posicion.Equals("Superior"))
                                {
                                    c1 = "B";
                                }//definicion de punto superior o inferior a cargar
                                else
                                {
                                    c1 = "A";
                                }//definicion de punto superior o inferior a cargar
                                P1 = "" + referencia.Id + c1 + "1" + (nd + referencia.Divisiones);
                            }//definicion de direccion positiva o negativa para las cargas longitudinales

                            ret = subestacion.PointObj.SetLoadForce(P1, "PP", ref PP1);
                            ret = subestacion.PointObj.SetLoadForce(P1, "CC", ref CC1);
                            ret = subestacion.PointObj.SetLoadForce(P1, "CT", ref CT1);
                            ret = subestacion.PointObj.SetLoadForce(P1, "CVcx", ref V1);
                            ret = subestacion.PointObj.SetLoadForce(P1, "CMM", ref CMM1);
                        }//asignacion de cargas en el modelo
                        else
                        {
                            if (car1.Orientacion >= 90 && car1.Orientacion <= 270)
                            {
                                if (car1.Posicion.Equals("Superior"))
                                {
                                    c1 = "C";
                                }//definicion de punto superior o inferior a cargar
                                else
                                {
                                    c1 = "D";
                                }//definicion de punto superior o inferior a cargar
                                P1 = "" + referencia.Id + c1 + "1" + (nd + referencia.Divisiones);
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            else
                            {
                                if (car1.Posicion.Equals("Superior"))
                                {
                                    c1 = "B";
                                }//definicion de punto superior o inferior a cargar
                                else
                                {
                                    c1 = "A";
                                }//definicion de punto superior o inferior a cargar
                                P1 = "" + referencia.Id + c1 + "1" + (nd + referencia.Divisiones);
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            ret = subestacion.PointObj.SetLoadForce(P1, "PP", ref PP1);
                            ret = subestacion.PointObj.SetLoadForce(P1, "CC", ref CC1);
                            ret = subestacion.PointObj.SetLoadForce(P1, "CT", ref CT1);
                            ret = subestacion.PointObj.SetLoadForce(P1, "CVcy", ref V1);
                            ret = subestacion.PointObj.SetLoadForce(P1, "CMM", ref CMM1);
                        }
                        #endregion
                        #region Asignacion de car2
                        if (referencia.Orientacion_viga.ToUpper().Equals("X"))
                        {
                            if (car2.Orientacion >= 0 && car2.Orientacion <= 180)
                            {
                                if (car2.Posicion.Equals("Superior"))
                                {
                                    c1 = "C";
                                }//definicion de punto superior o inferior a cargar
                                else
                                {
                                    c1 = "D";
                                }//definicion de punto superior o inferior a cargar
                                P2 = "" + referencia.Id + c1 + "1" + (nd + referencia.Divisiones);
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            else
                            {
                                if (car2.Posicion.Equals("Superior"))
                                {
                                    c1 = "B";
                                }//definicion de punto superior o inferior a cargar
                                else
                                {
                                    c1 = "A";
                                }//definicion de punto superior o inferior a cargar
                                P2 = "" + referencia.Id + c1 + "1" + (nd + referencia.Divisiones);
                            }//definicion de direccion positiva o negativa para las cargas longitudinales

                            ret = subestacion.PointObj.SetLoadForce(P2, "PP", ref PP2);
                            ret = subestacion.PointObj.SetLoadForce(P2, "CC", ref CC2);
                            ret = subestacion.PointObj.SetLoadForce(P2, "CT", ref CT2);
                            ret = subestacion.PointObj.SetLoadForce(P2, "CVcx", ref V2);
                            ret = subestacion.PointObj.SetLoadForce(P2, "CMM", ref CMM2);
                        }//asignacion de cargas en el modelo
                        else
                        {
                            if (car2.Orientacion >= 90 && car2.Orientacion <= 270)
                            {
                                if (car2.Posicion.Equals("Superior"))
                                {
                                    c1 = "C";
                                }//definicion de punto superior o inferior a cargar
                                else
                                {
                                    c1 = "D";
                                }//definicion de punto superior o inferior a cargar
                                P2 = "" + referencia.Id + c1 + "1" + (nd + referencia.Divisiones);
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            else
                            {
                                if (car2.Posicion.Equals("Superior"))
                                {
                                    c1 = "B";
                                }//definicion de punto superior o inferior a cargar
                                else
                                {
                                    c1 = "A";
                                }//definicion de punto superior o inferior a cargar
                                P2 = "" + referencia.Id + c1 + "1" + (nd + referencia.Divisiones);
                            }//definicion de direccion positiva o negativa para las cargas longitudinales
                            ret = subestacion.PointObj.SetLoadForce(P2, "PP", ref PP2);
                            ret = subestacion.PointObj.SetLoadForce(P2, "CC", ref CC2);
                            ret = subestacion.PointObj.SetLoadForce(P2, "CT", ref CT2);
                            ret = subestacion.PointObj.SetLoadForce(P2, "CVcy", ref V2);
                            ret = subestacion.PointObj.SetLoadForce(P2, "CMM", ref CMM2);
                        }
                        #endregion

                    }//igual posicion y espaciamiento de las dos templas


                    #endregion
                }                
            }//creacion de cierre
            //*********************************************************


            if (referencia.Orientacion_viga.Equals("X"))
            {
                referencia.X = referencia.X + cuerpo.Altura;
            }
            else
            {
                referencia.Y = referencia.Y + cuerpo.Altura;
            }               
            referencia.Divisiones = referencia.Divisiones + int.Parse(nd.ToString());
            return referencia;

        }

        public void crear_viga(Ensamble_subestacion Referencia, cSapModel subestacion)
        {


            List<CuerposDeElemento> todos_cuerpo = entidadesBD.CuerposDeElementoes.ToList();
            List<CuerposDeElemento> cuerpo = todos_cuerpo.FindAll(x => x.Id_Elemento == Referencia.Id_Elemento);
            int con=1;
            foreach (CuerposDeElemento cue in cuerpo)
            {
                int numCuerpos = cuerpo.Count;

                CuerposDeElemento cuerpo1 = cuerpo.Find(x => x.Id_Cuerpo == cue.Id_Cuerpo);
                //Punto referencia = new Punto();
                Referencia = crear_cuerpo_vigas(cuerpo1, Referencia, subestacion,con, numCuerpos);
                con = con + 1;
            }


        }
        
        public void Mass_Source(cSapModel subestacion,int id)
        {
            int ret = 0;
            string[] LoadPat;
            double[] SF;

            List<Tipos_de_carga> lista = entidadesBD.Tipos_de_cargas.ToList().FindAll(x=>(x.id_subestacion==0 || x.id_subestacion==id));
            List<Tipos_de_carga> lista2 = lista.FindAll(x=>(x.Clase.Equals("Muerta")|| x.Clase.Equals("Super Muerta")));

            var a = lista2.Count;
            LoadPat = new string[a];
            SF = new double[a];
            int j = 0;
            foreach(Tipos_de_carga item in lista2)
            {
                LoadPat[j] = item.Convencion;
                SF[j] = 1;
                j = j + 1;
            }
            
            bool v1 = new bool();
            bool v2 = new bool();
            bool v3 = new bool();
            bool v4 = new bool();
            v3 = true;
            string name = "MSSSRC1";
            ret = subestacion.SourceMass.SetMassSource(name, v1, v2, v3, v4, 2,ref LoadPat, ref SF);
        }

        public void Guardar(cSapModel subestacion, int id_caso)
        {
            Caso modelo = entidadesBD.Casos.Find(id_caso);
            SubestacionSAP item = entidadesBD.SubestacionSAPs.ToList().Find(x => x.Id == modelo.id_subestacion);
            int ret = 0;
            string name = "F:" + Path.DirectorySeparatorChar + "Proyectos Aplicacion SAP2000" + Path.DirectorySeparatorChar + item.NombreSubestacion + Path.DirectorySeparatorChar + modelo.nombre_caso + Path.DirectorySeparatorChar + modelo.nombre_caso;
            ret = subestacion.File.Save(name);
        }
        public void salir(ElemtosSAP subestacion)
        {                    
            subestacion.Objeto.ApplicationExit(false);
            subestacion.Modelo = null;
            subestacion.Objeto = null;
        }
        
        public void Materiales(cSapModel subestacion)
        {
            List<Material> lista = entidadesBD.Materials.ToList();
            int ret = 0;
            foreach(Material item in lista)
            {
                ret = subestacion.PropMaterial.SetMaterial(item.NombreMaterial, eMatType.Steel);
                //asignacion de propiedades isotropicas
                ret = subestacion.PropMaterial.SetMPIsotropic(item.NombreMaterial, item.E_Material, item.V_Material, item.T_Material);
                //asignacion de otras propiedades del acero
                ret = subestacion.PropMaterial.SetOSteel_1(item.NombreMaterial, item.Fy_Material, item.Fu_Material, item.eFy_Material, item.eFu_Material, 1, 2, 0.02, 0.14, 0.2, -0.1);
            }

        }
        public void tiposdecarga(cSapModel subestacion,int id)
        {
            List<Tipos_de_carga> lista = entidadesBD.Tipos_de_cargas.ToList().FindAll(x => (x.id_subestacion == 0 || x.id_subestacion == id));
            Cargas_Automatica auto = entidadesBD.Cargas_Automaticas.ToList().Find(x => x.Id_subestacion == id);
            foreach (Tipos_de_carga item in lista)
            {
                int ret = 0;

                if(item.Clase.Equals("Muerta"))
                {
                    ret = subestacion.LoadPatterns.Add(item.Convencion, eLoadPatternType.Dead, item.multiplicadorPP, true);
                }
                else if(item.Clase.Equals("Super Muerta"))
                {
                    ret = subestacion.LoadPatterns.Add(item.Convencion, eLoadPatternType.SuperDead, item.multiplicadorPP, true);
                }
                else if (item.Clase.Equals("Otras"))
                {
                    ret = subestacion.LoadPatterns.Add(item.Convencion, eLoadPatternType.Other, item.multiplicadorPP, true);
                }
                else if (item.Clase.Equals("Viento"))
                {
                    ret = subestacion.LoadPatterns.Add(item.Convencion, eLoadPatternType.Wind, item.multiplicadorPP, true);

                    if (item.Automatico.Equals("Si"))
                    {
                        if (auto.Tipo_exposicion.ToUpper().Equals("B"))
                        {
                            if (item.Convencion.ToUpper().Contains("X"))
                            {
                                ret = subestacion.LoadPatterns.AutoWind.SetASCE710(item.Convencion, 3, 0, 0, 0, 0, 0, 0, false, 0, 0, auto.Velocidad_viento, 1, auto.Factor_importancia, auto.Factor_topografia, auto.Factor_rafaga, auto.Factor_direccion, 0.31, false);
                            }
                            else
                            {
                                ret = subestacion.LoadPatterns.AutoWind.SetASCE710(item.Convencion, 1, 90, 0.8, 0.5, 1, 0, 0, false, 0, 0, auto.Velocidad_viento, 1, auto.Factor_importancia, auto.Factor_topografia, auto.Factor_rafaga, auto.Factor_direccion, 0, false);
                                ret = subestacion.LoadPatterns.AutoWind.SetASCE710(item.Convencion, 3, 90, 0, 0, 0, 0, 0, false, 0, 0, auto.Velocidad_viento, 1, auto.Factor_importancia, auto.Factor_topografia, auto.Factor_rafaga, auto.Factor_direccion, 0.31, false);
                            }

                        }
                        else if (auto.Tipo_exposicion.ToUpper().Equals("C"))
                        {
                            if (item.Convencion.ToUpper().Contains("X"))
                            {
                                ret = subestacion.LoadPatterns.AutoWind.SetASCE710(item.Convencion, 3, 0, 0, 0, 0, 0, 0, false, 0, 0, auto.Velocidad_viento, 2, auto.Factor_importancia, auto.Factor_topografia, auto.Factor_rafaga, auto.Factor_direccion, 0.31, false);
                            }
                            else
                            {
                                ret = subestacion.LoadPatterns.AutoWind.SetASCE710(item.Convencion, 1, 90, 0.8, 0.5, 1, 0, 0, false, 0, 0, auto.Velocidad_viento, 2, auto.Factor_importancia, auto.Factor_topografia, auto.Factor_rafaga, auto.Factor_direccion, 0, false);
                                ret = subestacion.LoadPatterns.AutoWind.SetASCE710(item.Convencion, 3, 90, 0, 0, 0, 0, 0, false, 0, 0, auto.Velocidad_viento, 2, auto.Factor_importancia, auto.Factor_topografia, auto.Factor_rafaga, auto.Factor_direccion, 0.31, false);
                            }
                        }
                        else
                        {
                            if (item.Convencion.ToUpper().Contains("X"))
                            {
                                ret = subestacion.LoadPatterns.AutoWind.SetASCE710(item.Convencion, 3, 0, 0, 0, 0, 0, 0, false, 0, 0, auto.Velocidad_viento, 3, auto.Factor_importancia, auto.Factor_topografia, auto.Factor_rafaga, auto.Factor_direccion, 0.31, false);
                            }
                            else
                            {
                               ret = subestacion.LoadPatterns.AutoWind.SetASCE710(item.Convencion, 1, 90, 0.8, 0.5, 1, 0, 0, false, 0, 0, auto.Velocidad_viento, 3, auto.Factor_importancia, auto.Factor_topografia, auto.Factor_rafaga, auto.Factor_direccion, 0, false);
                                ret = subestacion.LoadPatterns.AutoWind.SetASCE710(item.Convencion, 3, 90, 0, 0, 0, 0, 0, false, 0, 0, auto.Velocidad_viento, 3, auto.Factor_importancia, auto.Factor_topografia, auto.Factor_rafaga, auto.Factor_direccion, 0.31, false);
                            }
                        }
                    }
                }
                else if(item.Clase.Equals("Sismo"))
                {
                    ret = subestacion.LoadPatterns.Add(item.Convencion, eLoadPatternType.Quake, 0, true);
                    if(item.Automatico.Equals("Si"))
                    {
                        if(item.Convencion.ToUpper().Contains("X"))
                        {                            
                            if (item.Convencion.ToUpper().Contains("E"))
                            {
                                ret = subestacion.LoadPatterns.AutoSeismic.SetUserCoefficient(item.Convencion, 1, 0.05, false, 0, 0, (auto.Cortante_basal / auto.R_Equipos), auto.K_exponente);
                            }
                            else
                            {
                                ret = subestacion.LoadPatterns.AutoSeismic.SetUserCoefficient(item.Convencion, 1, 0.05, false, 0, 0, (auto.Cortante_basal / auto.R_Porticos), auto.K_exponente);
                            }                                
                        }
                        else if (item.Convencion.ToUpper().Contains("Y"))
                        {                           
                            if (item.Convencion.ToUpper().Contains("E"))
                            {
                                ret = subestacion.LoadPatterns.AutoSeismic.SetUserCoefficient(item.Convencion, 2, 0.05, false, 0, 0, (auto.Cortante_basal / auto.R_Equipos), auto.K_exponente);
                            }
                            else
                            {
                                ret = subestacion.LoadPatterns.AutoSeismic.SetUserCoefficient(item.Convencion, 2, 0.05, false, 0, 0, (auto.Cortante_basal / auto.R_Porticos), auto.K_exponente);
                            }
                        }                        
                    }
                }
            }            
        }
        public void combinaciones(cSapModel subestacion, int id)
        {
            int ret = 0;
            Cargas_Automatica auto = entidadesBD.Cargas_Automaticas.ToList().Find(x => x.Id_subestacion == id);
            List<Combinacione> lista = entidadesBD.Combinaciones.ToList().FindAll(x => (x.id_subestacion == 0 || x.id_subestacion == id));
            foreach(Combinacione item in lista)
            {
                if(item.Nombre.ToUpper().Contains("ENV"))
                {
                    ret = subestacion.RespCombo.Add(item.Nombre, 1);
                    List<Casos_de_Combinacione> lista2 = entidadesBD.Casos_de_Combinaciones.ToList().FindAll(x => x.Combinacion.Equals(item.Nombre));
                    foreach (Casos_de_Combinacione model in lista2)
                    {
                        eCNameType caso = eCNameType.LoadCombo;
                        ret = subestacion.RespCombo.SetCaseList(item.Nombre, ref caso, model.Tipo, model.Factor_de_modificacion);

                    }
                }
                else
                {
                    ret = subestacion.RespCombo.Add(item.Nombre, 0);
                    List<Casos_de_Combinacione> lista2 = entidadesBD.Casos_de_Combinaciones.ToList().FindAll(x => x.Combinacion.Equals(item.Nombre));
                    
                    foreach (Casos_de_Combinacione model in lista2)
                    {
                        if(model.Combinacion.ToUpper().Contains("SZ"))
                        {
                            eCNameType caso = eCNameType.LoadCase;
                            ret = subestacion.RespCombo.SetCaseList(item.Nombre, ref caso, model.Tipo, (model.Factor_de_modificacion * auto.Cortante_basal / auto.R_Porticos));

                        }
                        else if(model.Combinacion.ToUpper().Contains("EZ"))
                        {
                            eCNameType caso = eCNameType.LoadCase;
                            ret = subestacion.RespCombo.SetCaseList(item.Nombre, ref caso, model.Tipo, (model.Factor_de_modificacion* auto.Cortante_basal/ auto.R_Equipos));

                        }
                        else
                        {
                            if (model.Caso.Equals("Caso"))
                            {
                                eCNameType caso = eCNameType.LoadCase;
                                ret = subestacion.RespCombo.SetCaseList(item.Nombre, ref caso, model.Tipo, model.Factor_de_modificacion);
                            }
                            else
                            {
                                eCNameType caso = eCNameType.LoadCombo;
                                ret = subestacion.RespCombo.SetCaseList(item.Nombre, ref caso, model.Tipo, model.Factor_de_modificacion);
                            }
                        }                        
                    }

                }
            }
        }
        public void perfilesdefault(cSapModel subestacion, int id_caso)
        {
            int ret = 0;
            List<Perfile> perfil = entidadesBD.Perfiles.ToList().FindAll(x => x.id_subestacion == 0);
            List<Elemento> lista = entidadesBD.Elementos.ToList().FindAll(x => x.Caso == id_caso);            
            int a1 = lista.FindAll(x => x.NivelTension.Equals("34.5 kV")).Count;
            int a2 = lista.FindAll(x => x.NivelTension.Equals("60 kV")).Count;
            int a3 = lista.FindAll(x => x.NivelTension.Equals("110 kV")).Count;
            int a4 = lista.FindAll(x => x.NivelTension.Equals("115 kV")).Count;
            int a5 = lista.FindAll(x => x.NivelTension.Equals("220 kV")).Count;
            int a6 = lista.FindAll(x => x.NivelTension.Equals("230 kV")).Count;
            int a7 = lista.FindAll(x => x.NivelTension.Equals("500 kV")).Count;
            if(a1>0)
            {
                List<Perfile> perfil1 = perfil.ToList().FindAll(x => x.Nivel_de_tensio.Equals("34.5 kV"));
                //revisar 
                foreach(Perfile item in perfil1)
                {
                    ret = subestacion.PropFrame.SetAngle(item.Nombre_del_perfil.Trim(), item.Material_del_perfil.Trim(), item.t3___mm_, item.t2__mm_, item.tf__mm_, item.tw__mm_);
                }
            }
            if (a2 > 0)
            {
                List<Perfile> perfil1 = perfil.ToList().FindAll(x => x.Nivel_de_tensio.Equals("60 kV"));
                foreach (Perfile item in perfil1)
                {
                    ret = subestacion.PropFrame.SetAngle(item.Nombre_del_perfil.Trim(), item.Material_del_perfil.Trim(), item.t3___mm_, item.t2__mm_, item.tf__mm_, item.tw__mm_);
                }
            }
            if (a3 > 0)
            {
                List<Perfile> perfil1 = perfil.ToList().FindAll(x => x.Nivel_de_tensio.Equals("110 kV"));
                foreach (Perfile item in perfil1)
                {
                    ret = subestacion.PropFrame.SetAngle(item.Nombre_del_perfil.Trim(), item.Material_del_perfil.Trim(), item.t3___mm_, item.t2__mm_, item.tf__mm_, item.tw__mm_);
                }
            }
            if (a4 > 0)
            {
                List<Perfile> perfil1 = perfil.ToList().FindAll(x => x.Nivel_de_tensio.Equals("115 kV"));
                foreach (Perfile item in perfil1)
                {
                    ret = subestacion.PropFrame.SetAngle(item.Nombre_del_perfil.Trim(), item.Material_del_perfil.Trim(), item.t3___mm_, item.t2__mm_, item.tf__mm_, item.tw__mm_);
                }
            }
            if (a5 > 0)
            {
                List<Perfile> perfil1 = perfil.ToList().FindAll(x => x.Nivel_de_tensio.Equals("220 kV"));
                foreach (Perfile item in perfil1)
                {
                    ret = subestacion.PropFrame.SetAngle(item.Nombre_del_perfil.Trim(), item.Material_del_perfil.Trim(), item.t3___mm_, item.t2__mm_, item.tf__mm_, item.tw__mm_);
                }
            }
            if (a6 > 0)
            {
                List<Perfile> perfil1 = perfil.ToList().FindAll(x => x.Nivel_de_tensio.Equals("230 kV"));
                foreach (Perfile item in perfil1)
                {
                    ret = subestacion.PropFrame.SetAngle(item.Nombre_del_perfil.Trim(), item.Material_del_perfil.Trim(), item.t3___mm_, item.t2__mm_, item.tf__mm_, item.tw__mm_);
                }
            }
            if (a7 > 0)
            {
                List<Perfile> perfil1 = perfil.ToList().FindAll(x => x.Nivel_de_tensio.Equals("500 kV"));
                foreach (Perfile item in perfil1)
                {
                    ret = subestacion.PropFrame.SetAngle(item.Nombre_del_perfil.Trim(), item.Material_del_perfil.Trim(), item.t3___mm_, item.t2__mm_, item.tf__mm_, item.tw__mm_);
                }
            }

        }
        public void perfilesagregados(cSapModel subestacion, int id)
        {
            List<Perfile> lista = entidadesBD.Perfiles.ToList().FindAll(x => x.id_subestacion == id);
            foreach(Perfile item in lista)
            {
                Agregar_perfil(item, subestacion);
            }
        }
        public void diseño(cSapModel subestacion)
        {
            int ret = 0;
            ret = subestacion.DesignSteel.SetCode("ASCE 10-97");
            ret = subestacion.DesignSteel.ASCE_10_97.SetPreference(1, 2);
            ret = subestacion.DesignSteel.ASCE_10_97.SetPreference(9, 0.95);
            ret = subestacion.DesignSteel.ASCE_10_97.SetPreference(10, 1);
            ret = subestacion.DesignSteel.SetComboAutoGenerate(false);
            //string[] Nombrescombo=new string[];
            //ret = subestacion.RespCombo.GetNameList(ref numerocombos,ref Nombrescombo);
            for(int i=1;i<=14;i++)
            {
                string combinacion = "COMB" + i;
                ret = subestacion.DesignSteel.SetComboStrength(combinacion, true);                
            }
            ret = subestacion.DesignSteel.StartDesign();            
        }
        public void borrarcaso(int id)
        {
            #region borrar ensambles y cargas de ensamble
            List<Ensamble_subestacion> ensambles = entidadesBD.Ensamble_subestacions.ToList().FindAll(x => x.id_Caso == id);
            foreach (Ensamble_subestacion item in ensambles)
            {
                List<Carga> cargas1 = entidadesBD.Cargas.ToList().FindAll(x => x.Id_Ensamble == item.Id);
                foreach (Carga item1 in cargas1)
                {
                    entidadesBD.Cargas.Remove(item1);
                    entidadesBD.SaveChanges();
                }

                entidadesBD.Ensamble_subestacions.Remove(item);
                entidadesBD.SaveChanges();
            }
            #endregion
            #region borrar cargas de conexion
            List<CARGAS_DE_CONEXIÓN> cargas = entidadesBD.CARGAS_DE_CONEXIÓN.ToList().FindAll(x => x.Id_Caso == id);
            foreach(CARGAS_DE_CONEXIÓN item in cargas)
            {
                entidadesBD.CARGAS_DE_CONEXIÓN.Remove(item);
                entidadesBD.SaveChanges();
            }
            #endregion
            #region borrar elementos
            List<Elemento> elem = entidadesBD.Elementos.ToList().FindAll(x => x.Caso == id);
            foreach(Elemento item3 in elem)
            {
                List<CuerposDeElemento> List = entidadesBD.CuerposDeElementoes.ToList().FindAll(x => x.Id_Elemento == item3.Id);
                foreach (CuerposDeElemento i in List)
                {
                    entidadesBD.CuerposDeElementoes.Remove(i);
                    entidadesBD.SaveChanges();
                }
                entidadesBD.Elementos.Remove(item3);
                entidadesBD.SaveChanges();

            }
            #endregion
            #region borrar resultados 

            List<resultadosdiseño> lista = entidadesBD.resultadosdiseño.ToList().FindAll(x => x.caso == id);
            foreach(resultadosdiseño item4 in lista)
            {
                entidadesBD.resultadosdiseño.Remove(item4);
                entidadesBD.SaveChanges();

            }
            List<Combinacionesservicio> lista2 = entidadesBD.Combinacionesservicios.ToList().FindAll(x => x.Caso == id);
            foreach (Combinacionesservicio item5 in lista2)
            {
                entidadesBD.Combinacionesservicios.Remove(item5);
                entidadesBD.SaveChanges();

            }

            List<Combinacionespernosoequipos> lista3 = entidadesBD.Combinacionespernosoequipos.ToList().FindAll(x => x.Caso == id);
            foreach (Combinacionespernosoequipos item6 in lista3)
            {
                entidadesBD.Combinacionespernosoequipos.Remove(item6);
                entidadesBD.SaveChanges();

            }

            List<Combinacionesultima> lista4 = entidadesBD.Combinacionesultimas.ToList().FindAll(x => x.Caso == id);
            foreach (Combinacionesultima item7 in lista4)
            {
                entidadesBD.Combinacionesultimas.Remove(item7);
                entidadesBD.SaveChanges();

            }

            #endregion

            Caso cas = entidadesBD.Casos.Find(id);
            entidadesBD.Casos.Remove(cas);
            entidadesBD.SaveChanges();

        }
        public void borrarsubestacion(int id)
        {
            List<Caso> lista= entidadesBD.Casos.ToList().FindAll(x => x.id_subestacion == id);
            foreach(Caso item in lista)
            {
                borrarcaso(item.Id);
            }
            
            SubestacionSAP subestacion = entidadesBD.SubestacionSAPs.Find(id);

            

            List<Perfile> per = entidadesBD.Perfiles.ToList().FindAll(x => x.id_subestacion == id);
            foreach(Perfile item in per)
            {
                entidadesBD.Perfiles.Remove(item);
                entidadesBD.SaveChanges();
            }
            List<Casos_de_Combinacione> cas = entidadesBD.Casos_de_Combinaciones.ToList().FindAll(x => x.id_subestacion == id);
            foreach(Casos_de_Combinacione item in cas)
            {
                entidadesBD.Casos_de_Combinaciones.Remove(item);
                entidadesBD.SaveChanges();
            }
            List<Combinacione> comb = entidadesBD.Combinaciones.ToList().FindAll(x => x.id_subestacion == id);
            foreach (Combinacione item in comb)
            {
                entidadesBD.Combinaciones.Remove(item);
                entidadesBD.SaveChanges();
            }

            List<Tipos_de_carga> tcargas = entidadesBD.Tipos_de_cargas.ToList().FindAll(x => x.id_subestacion == id);
            foreach (Tipos_de_carga item in tcargas)
            {
                entidadesBD.Tipos_de_cargas.Remove(item);
                entidadesBD.SaveChanges();
            }

            Cargas_Automatica Acargas = entidadesBD.Cargas_Automaticas.ToList().Find(x => x.Id_subestacion == id);
            entidadesBD.Cargas_Automaticas.Remove(Acargas);
            entidadesBD.SaveChanges();


            entidadesBD.SubestacionSAPs.Remove(subestacion);
            entidadesBD.SaveChanges();

        }
        public void copiarcaso(int id)
        {
            #region borrar elementos



            #endregion
            #region borrar ensambles y cargas de ensamble

            #endregion
            #region borrar cargas de conexion

            #endregion
            #region borrar elementos

            #endregion



        }
        public void diseño_de_elemetos(Perfile perfil, string Tipodemiembro)
        {
            List<Material> materiales = entidadesBD.Materials.ToList();
            Material Material = materiales.Find(x => x.NombreMaterial.Equals(perfil.Material_del_perfil));
            double w = new double();
            double t = new double();
            double Fcr = new double();
            double Fa = new double();
            double Cc = new double();
            double Fy = new double();
            double C1 = new double();

            w = perfil.t2__mm_ - perfil.tf__mm_ * 2;
            t = perfil.tf__mm_;
            //valoracion para restringir el pandeo loca
            if ((w/t)<=25)
            {
                Fy = Material.Fy_Material;
            }
            else
            {
                if(((w / t) > (80*2.62/Math.Sqrt(Material.Fy_Material*9.81)))   && ((w / t) <= (144 * 2.62 / Math.Sqrt(Material.Fy_Material * 9.81))))
                {
                    Fcr = (1.677 - (0.677 * (w / t) / (80 * 2.62 / Math.Sqrt(Material.Fy_Material * 9.81)))) * Material.Fy_Material;// unidad kf/mm2
                    Fy = Fcr;
                }
                else
                {
                    Fcr = 0.0332 * (Math.Pow(Math.PI, 2)*Material.E_Material/Math.Pow((w / t), 2));
                    Fy = Fcr;
                }
            }
            //longitud efectiva
            C1 = 108.1081081;//KL/r


            Cc = Math.PI * Math.Sqrt(2 * Material.E_Material / Fy);
            //Esfuerzo de compresion de un miembro
            if(C1<=Cc)
            {
                Fa = (1-0.5*Math.Pow((C1/Cc),2)) * Fy;
            }
            else
            {
                Fa = Math.Pow(Math.PI, 2) * Material.E_Material / Math.Pow(C1, 2);
            }



        }

    }

    
}