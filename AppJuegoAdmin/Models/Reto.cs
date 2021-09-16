using System;
using System.Collections.Generic;
using System.Text;

namespace AppJuegoAdmin.Models
{
	public class Reto
	{
		public int id_reto { get; set; }
		public string descripcion { get; set; }
		public int tiempo { get; set; }
		public int nivel { get; set; }
		public string estado { get; set; }
		public string n1_penitencia { get; set; }
		public string n2_penitencia { get; set; }
		public string n3_penitencia { get; set; }
		public int id_categoria { get; set; }
		public int id_sub_categoria { get; set; }
	}
}
