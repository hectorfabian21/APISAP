using SAP2000v19;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IEB.GC.Net.ApiSAP.Models
{
    public class Cuerpos
    {
        public static Repositorio Repositori = new Repositorio();
        APISAPEntities1 entidadesBD = new APISAPEntities1();
        public Ensamble_subestacion crear_cuerpo1(CuerposDeElemento cuerpo, Ensamble_subestacion referencia, cSapModel subestacion, int n)
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
            }
            else
            {
                Montante = entidadesBD.Perfiles.ToList().Find(x => (x.id_subestacion == referencia.id_Subestacion && x.Nombre_del_perfil.Equals(cuerpo.PerfilMontante)));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Montante.Material_del_perfil));
                CcM = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
            }

            if (string.IsNullOrWhiteSpace(cuerpo.PerfilDiagonarl))
            {
                Diagonal = entidadesBD.Perfiles.ToList().Find(x => (x.Nivel_de_tensio.Equals(cuerpo.Nivel_de_tension) && x.Uso_del_perfil.Equals("Diagonal")));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Diagonal.Material_del_perfil));
                CcD = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
            }
            else
            {
                Diagonal = entidadesBD.Perfiles.ToList().Find(x => (x.id_subestacion == referencia.id_Subestacion && x.Nombre_del_perfil.Equals(cuerpo.PerfilDiagonarl)));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Diagonal.Material_del_perfil));
                CcD = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
            }

            if (string.IsNullOrWhiteSpace(cuerpo.PerfilMontanteViga))
            {
                Montante_viga = entidadesBD.Perfiles.ToList().Find(x => (x.Nivel_de_tensio.Equals(cuerpo.Nivel_de_tension) && x.Uso_del_perfil.Equals("Montante Viga")));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Montante_viga.Material_del_perfil));
                CcMV = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
            }
            else
            {
                Montante_viga = entidadesBD.Perfiles.ToList().Find(x => (x.id_subestacion == referencia.id_Subestacion && x.Nombre_del_perfil.Equals(cuerpo.PerfilMontanteViga)));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Montante_viga.Material_del_perfil));
                CcMV = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
            }

            if (string.IsNullOrWhiteSpace(cuerpo.PerfilDiagonalViga))
            {
                Diagonal_viga = entidadesBD.Perfiles.ToList().Find(x => (x.Nivel_de_tensio.Equals(cuerpo.Nivel_de_tension) && x.Uso_del_perfil.Equals("Diagonal viga")));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Diagonal_viga.Material_del_perfil));
                CcDV = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
            }
            else
            {
                Diagonal_viga = entidadesBD.Perfiles.ToList().Find(x => (x.id_subestacion == referencia.id_Subestacion && x.Nombre_del_perfil.Equals(cuerpo.PerfilDiagonalViga)));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Diagonal_viga.Material_del_perfil));
                CcDV = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
            }

            if (string.IsNullOrWhiteSpace(cuerpo.PerfilMontanteCastillete))
            {
                Montante_Castillete = entidadesBD.Perfiles.ToList().Find(x => (x.Nivel_de_tensio.Equals(cuerpo.Nivel_de_tension) && x.Uso_del_perfil.Equals("Montante Castillete")));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Montante_Castillete.Material_del_perfil));
                CcMC = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
            }
            else
            {
                Montante_Castillete = entidadesBD.Perfiles.ToList().Find(x => (x.id_subestacion == referencia.id_Subestacion && x.Nombre_del_perfil.Equals(cuerpo.PerfilMontanteCastillete)));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Montante_Castillete.Material_del_perfil));
                CcMC = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
            }

            if (string.IsNullOrWhiteSpace(cuerpo.PerfilDiagonalCastillete))
            {
                Diagonal_castillete = entidadesBD.Perfiles.ToList().Find(x => (x.Nivel_de_tensio.Equals(cuerpo.Nivel_de_tension) && x.Uso_del_perfil.Equals("Diagonal castillete")));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Diagonal_castillete.Material_del_perfil));
                CcDC = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
            }
            else
            {
                Diagonal_castillete = entidadesBD.Perfiles.ToList().Find(x => (x.id_subestacion == referencia.id_Subestacion && x.Nombre_del_perfil.Equals(cuerpo.PerfilDiagonalCastillete)));
                Material acero = entidadesBD.Materials.ToList().Find(x => x.NombreMaterial.Equals(Diagonal_castillete.Material_del_perfil));
                CcDC = Math.PI * Math.Sqrt((2 * acero.E_Material) / (acero.Fy_Material));
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
            #region longitud de montante
            double Lrz1, Lrz2, Lm1, nd, Lm, Lm2, Lm3,Cc;
            Perfile m;
            Lm = Math.Sqrt(Math.Pow(A1.X - A2.X, 2) + Math.Pow(A1.Y - A2.Y, 2) + Math.Pow(A1.Z - A2.Z, 2));
            if (cuerpo.TipoDeElemento.Equals("Cuerpo de Castillete"))
            {
                m = Montante_Castillete;
                Cc = CcMC;
            }
            else
            {
                m = Montante;
                Cc = CcM;
            }
            if (cuerpo.Tipo_de_diagonal.Equals("X"))
            {
                Lrz1 = Cc;
                Lrz2 = (Cc - 46.2) / 0.615;
                if (Lrz1 <= 120)
                {
                    Lm1 = Lrz1 * m.Rz__mm_.Value;
                }
                else
                {
                    Lm1 = Lrz2 * m.Rz__mm_.Value;
                }
                nd = Math.Round((Lm / Lm1) + 0.499);
            }
            else
            {
                double c1 = m.Rz__mm_.Value / m.R22__mm_.Value;
                Lrz1 = Cc;
                Lrz2 = (Cc - 46.2) / 0.615;
                if (Lrz1 <= 120)
                {
                    Lm1 = Lrz1 * m.Rz__mm_.Value * 2;
                    Lm2 = Lrz1 * m.Rz__mm_.Value * c1;
                    Lm3 = Math.Min(Lm1, Lm2);
                }
                else
                {
                    Lm1 = Lrz2 * m.Rz__mm_.Value * 2;
                    Lm2 = Lrz2 * m.Rz__mm_.Value * c1;
                    Lm3 = Math.Min(Lm1, Lm2);
                }
                nd = Math.Round((Lm / Lm3) + 0.499);
            }
            #endregion




            return referencia;
        }





    }
}