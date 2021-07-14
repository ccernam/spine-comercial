
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Spine.Entities.Lgt
{
    public class GuiaRemision
    {
        // COLUMNAS
        [XmlAttribute]
        public long nIdGuiaRemision { get; set; }

        [XmlAttribute]
        public byte nIdEmpresa { get; set; }

        [XmlAttribute]
        public short nIdSucursal { get; set; }

        [XmlAttribute]
        public short nIdUsuario { get; set; }

        [XmlAttribute]
        public short nIdFolio { get; set; }

        [XmlAttribute]
        public string sGReSerie { get; set; }

        [XmlAttribute]
        public long nGReCorrelativo { get; set; }

        [XmlAttribute]
        public DateTime dGReEmision { get; set; }

        [XmlAttribute]
        public DateTime dGReTraslado { get; set; }

        [XmlAttribute]
        public long nIdAlmacenMov { get; set; }

        [XmlAttribute]
        public int nIdDestinatario { get; set; }

        [XmlAttribute]
        public int nGReTipoTransporte { get; set; }

        [XmlAttribute]
        public int nIdTransportista { get; set; }

        [XmlAttribute]
        public string sGReConductorDoc { get; set; }

        [XmlAttribute]
        public string sGReVehiculoMarca { get; set; }

        [XmlAttribute]
        public string sGReVehiculoPlaca { get; set; }

        [XmlAttribute]
        public short nIdUbigeoPartida { get; set; }

        [XmlAttribute]
        public string sGReDireccionPartida { get; set; }

        [XmlAttribute]
        public short nIdUbigeoLlegada { get; set; }

        [XmlAttribute]
        public string sGReDireccionLlegada { get; set; }

        [XmlAttribute]
        public byte nGReMotivo { get; set; }

        [XmlAttribute]
        public string sGReMotivoOtros { get; set; }

        [XmlAttribute]
        public string sGReObservacion { get; set; }

        [XmlAttribute]
        public byte nGReEstado { get; set; }

        // OTROS
        [XmlAttribute]
        public string sSucNombre { get; set; }
        [XmlAttribute]
        public string sPedCodigo { get; set; }
        [XmlAttribute]
        public string sUsuUsuario { get; set; }
        [XmlAttribute]
        public string sGReSerieCorrelativo { get; set; }
        [XmlAttribute]
        public string sAMoCodigo { get; set; }
        [XmlAttribute]
        public string sCVeSerieCorrelativo { get; set; }
        [XmlAttribute]
        public string sDesDoc { get; set; }
        [XmlAttribute]
        public string sDesNombre { get; set; }
        [XmlAttribute]
        public string sGReTipoTransporte { get; set; }
        [XmlAttribute]
        public string sTraDoc { get; set; }
        [XmlAttribute]
        public string sTraRazonSocial { get; set; }
        [XmlAttribute]
        public string sUbiRutaPartida { get; set; }
        [XmlAttribute]
        public string sUbiRutaLlegada { get; set; }
        [XmlAttribute]
        public string sGReMotivo { get; set; }
        [XmlAttribute]
        public string sGReEstado { get; set; }

        // RELACIONES
        public List<GuiaRemisionLinea> lstGuiaRemisionLinea { set; get; }
    }
}
