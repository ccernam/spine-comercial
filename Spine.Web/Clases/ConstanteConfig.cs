using Spine.Constants.Cfg;
using Spine.Entities.Cfg;
using Spine.Services.Cfg;
using Spine.Services.Interfaces.Cfg;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;

namespace Spine.Web.Clases
{
    public static class ConstanteConfig
    {
        public async static void RegistrarCatalogo()
        {
            try
            {
                ICatalogoService vobjCatalogoService = DependencyResolver.Current.GetService<ICatalogoService>();
                IEnumerable<Catalogo> vlstCatalogos = await vobjCatalogoService.Consultar(piCatEstado: 1);
                foreach (var vobjCatalogo in vlstCatalogos)
                {
                    if (!string.IsNullOrEmpty(vobjCatalogo.sCatConstante))
                    {
                        try
                        {
                            var vobjType = typeof(CatalogoConstant);
                            var vobjProperty = vobjType.GetProperty(vobjCatalogo.sCatConstante, BindingFlags.Public | BindingFlags.Static);
                            if (vobjProperty != null)
                                vobjProperty.SetValue(null, Convert.ChangeType(vobjCatalogo.iCatalogoId, vobjProperty.PropertyType));
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        public static void RegistrarCatalogoItem()
        {
            try
            {
                //IEnumerable<CatalogoItem> vlstCatalogoItem = await new CatalogoService(null).ConsultarItems(piCatIteEstado: 1);
                //foreach (var vobjItem in vlstCatalogoItem)
                //{
                //    if (!string.IsNullOrEmpty(vobjItem.sCatIteClase) && !string.IsNullOrEmpty(vobjItem.sCatIteCampo))
                //    {
                //        try
                //        {
                //            var vobjType = Type.GetType(vobjItem.sCatIteClase);
                //            var vobjProperty = vobjType.GetProperty(vobjItem.sCatIteCampo, BindingFlags.Public | BindingFlags.Static);
                //            if (vobjProperty != null)
                //                vobjProperty.SetValue(null, Convert.ChangeType(vobjItem.iCatIteNumero, vobjProperty.PropertyType));
                //        }
                //        catch (Exception)
                //        {
                //        }
                //    }
                //}
            }
            catch (Exception)
            {
            }
        }
        public async static void RegistrarEntidades()
        {
            try
            {
                IEnumerable<Entidad> vlstEntidad = await new EntidadService().Consultar(piEntEstado: 1);
                foreach (var vobjEntidad in vlstEntidad)
                {
                    if (!string.IsNullOrEmpty(vobjEntidad.sEntConstante))
                    {
                        try
                        {
                            var vobjType = typeof(EntidadConstant);
                            var vobjProperty = vobjType.GetProperty(vobjEntidad.sEntConstante, BindingFlags.Public | BindingFlags.Static);
                            if (vobjProperty != null)
                                vobjProperty.SetValue(null, Convert.ChangeType(vobjEntidad.iEntId, vobjProperty.PropertyType));
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        public static void RegistrarParametro()
        {
            try
            {
                //List<Parametro> vlstParametro = new NegParametro(null).Consultar(piParEstado: 1);
                //foreach (var vobjParametro in vlstParametro)
                //{
                //    if (!string.IsNullOrEmpty(vobjParametro.sParClase) && !string.IsNullOrEmpty(vobjParametro.sParCampo))
                //    {
                //        try
                //        {
                //            var vobjType = Type.GetType(vobjParametro.sParClase);
                //            var vobjProperty = vobjType.GetProperty(vobjParametro.sParCampo, BindingFlags.Public | BindingFlags.Static);
                //            if (vobjProperty != null)
                //                vobjProperty.SetValue(null, Convert.ChangeType(vobjParametro.sParCodigo, vobjProperty.PropertyType));
                //        }
                //        catch (Exception)
                //        {
                //        }
                //    }
                //}
            }
            catch (Exception)
            {
            }
        }


        public async static void RegistrarAcciones()
        {
            try
            {
                IEnumerable<Accion> vlstAcciones = await new AccionService().Consultar(piAcnEstado: 1);
                foreach (var vobjAccion in vlstAcciones)
                {
                    if (!string.IsNullOrEmpty(vobjAccion.sAcnConstante))
                    {
                        try
                        {
                            var vobjType = typeof(AccionConstant);
                            var vobjProperty = vobjType.GetProperty(vobjAccion.sAcnConstante, BindingFlags.Public | BindingFlags.Static);
                            if (vobjProperty != null)
                                vobjProperty.SetValue(null, Convert.ChangeType(vobjAccion.iAcnId, vobjProperty.PropertyType));
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}