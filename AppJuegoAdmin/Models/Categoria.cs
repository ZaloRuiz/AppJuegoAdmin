using System;
using System.Collections.Generic;
using System.Text;

namespace AppJuegoAdmin.Models
{
	public class Categoria
	{
		public int id_categoria { get; set; }
		public string nombre { get; set; }
		public string descripcion { get; set; }
		public string estado { get; set; }
		public string comprado { get; set; }
	}
}
