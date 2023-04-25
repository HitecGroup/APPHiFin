using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPHiFin.Shared
{
    public class Modelo
    {
        public int IdModelo { get; set; }
        public int? IdMarca { get; set; }
        public string? Origen { get; set; }
        public string? TipoMaquina { get; set; }
        public string? ModeloMaquina { get; set; }
        public string? InternalSapcode { get; set; }
        public string? ModeloSap { get; set; }
        public bool? Extra { get; set; }
        public int Hq40 { get; set; }
        public int Hq20 { get; set; }
        public int Fr40 { get; set; }
        public int Fr20 { get; set; }
        public int Ot40 { get; set; }
        public int Ot20 { get; set; }
    }
}
