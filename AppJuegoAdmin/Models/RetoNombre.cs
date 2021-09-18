using System;
using System.Collections.Generic;
using System.Text;

namespace AppJuegoAdmin.Models
{
	public class RetoNombre
	{
		public int id_reto { get; set; }
		public string descripcion { get; set; }
		public int tiempo { get; set; }
		public string nivel { get; set; }
		public string estado { get; set; }
		public string n1_penitencia { get; set; }
		public string n2_penitencia { get; set; }
		public string categoria { get; set; }
		public string sub_categoria { get; set; }
	}
}
