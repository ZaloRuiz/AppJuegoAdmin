using AppJuegoAdmin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;

namespace AppJuegoAdmin.ViewModels
{
	public class ListaRetosVM : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		private ObservableCollection<RetoNombre> _listaRetos;
		public ObservableCollection<RetoNombre> ListaRetos
		{
			get { return _listaRetos; }
			set
			{
				if (_listaRetos != value)
				{
					_listaRetos = value;
					OnPropertyChanged("ListaRetos");
				}
			}
		}
		public ListaRetosVM()
		{
			_listaRetos = new ObservableCollection<RetoNombre>();
			GetRetos();
		}
		public async void GetRetos()
		{
			try
			{
				HttpClient client = new HttpClient();
				var response = await client.GetStringAsync("https://dmrbolivia.com/api_appjuego/listaRetosNombre.php");
				var lista_retos = JsonConvert.DeserializeObject<List<RetoNombre>>(response);
				foreach (var item in lista_retos)
				{
					_listaRetos.Add(item);
				}
			}
			catch (Exception err)
			{
				Console.WriteLine("#######################################" + err.ToString() + "#################################################");
			}
		}
	}
}
