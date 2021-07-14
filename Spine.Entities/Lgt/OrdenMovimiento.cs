
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Spine.Entities.Lgt
{
    public class OrdenMovimiento
    {
        //COLUMNAS
        [XmlAttribute]
        public long nIdAlmacenMov { get; set; }

        [XmlAttribute]
        public byte nIdEmpresa { get; set; }

        [XmlAttribute]
        public short nIdSucursal { get; set; }

        [XmlAttribute]
        public short nIdSucursalRef { get; set; }

        [XmlAttribute]
        public short nIdUsuario { get; set; }

        [XmlAttribute]
        public int nIdCliente { get; set; }

        [XmlAttribute]
        public int nIdProveedor { get; set; }

        [XmlAttribute]
        public string sAMoCodigo { get; set; }

        [XmlAttribute]
        public byte nAMoTipo { get; set; }

        [XmlAttribute]
        public short nIdAlmacen { get; set; }

        [XmlAttribute]
        public short nIdAlmacenRef { get; set; }

        [XmlAttribute]
        public DateTime dAMoEmision { get; set; }

        [XmlAttribute]
        public byte nAMoMotivo { get; set; }

        [XmlAttribute]
        public string sAMoObservacion { get; set; }

        [XmlAttribute]
        public long nIdPedido { get; set; }

        [XmlAttribute]
        public long nIdComprobanteCompra { get; set; }

        [XmlAttribute]
        public long nIdComprobanteVenta { get; set; }

        [XmlAttribute]
        public long nIdGuiaRemision { get; set; }

        [XmlAttribute]
        public long nGReEstado { get; set; }

        [XmlAttribute]
        public string sAMoGuiaRef { get; set; }

        [XmlAttribute]
        public byte nAMoEstado { get; set; }

        //OTROS
        [XmlAttribute]
        public string sSucNombre { get; set; }

        [XmlAttribute]
        public string sSucDireccion { get; set; }

        [XmlAttribute]
        public string sUbiRuta { get; set; }

        [XmlAttribute]
        public string sUsuUsuario { get; set; }

        [XmlAttribute]
        public string sAlmNombre { get; set; }

        [XmlAttribute]
        public string sSucNombreRef { get; set; }

        [XmlAttribute]
        public string sAlmNombreRef { get; set; }

        [XmlAttribute]
        public int nIdUbigeo { get; set; }

        [XmlAttribute]
        public string sPedCodigo { get; set; }

        [XmlAttribute]
        public string sComSerieCorrelativo { get; set; }

        [XmlAttribute]
        public string sGReSerieCorrelativo { get; set; }

        [XmlAttribute]
        public string sCliDoc { get; set; }

        [XmlAttribute]
        public string sCliNombre { get; set; }

        [XmlAttribute]
        public string sCCoSerieCorrelativo { get; set; }

        [XmlAttribute]
        public string sCVeSerieCorrelativo { get; set; }

        [XmlAttribute]
        public string sProvDoc { get; set; }

        [XmlAttribute]
        public string sProvNombre { get; set; }

        [XmlAttribute]
        public byte nPerTipo { get; set; }

        [XmlAttribute]
        public string sAMoEstado { get; set; }

        [XmlAttribute]
        public string sAMoTipo { get; set; }

        [XmlAttribute]
        public string sAMoMotivo { get; set; }

        [XmlAttribute]
        public string sAMoFacturado { get; set; }

        //RELACIONES
        public List<OrdenMovimientoItem> lstAlmacenMovLinea { get; set; }
    }
}
