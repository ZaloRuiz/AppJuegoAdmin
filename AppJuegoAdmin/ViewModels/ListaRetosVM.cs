using AppJuegoAdmin.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
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
		private int _idCategoria = 0;
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
		public ListaRetosVM(int _id_categoria)
		{
			_idCategoria = _id_categoria;
			_listaRetos = new ObservableCollection<RetoNombre>();
			GetRetos();
		}
		public async void GetRetos()
		{
			if (CrossConnectivity.Current.IsConnected)
			{
				try
				{
					Reto _retoBuscar = new Reto()
					{
						id_categoria = _idCategoria
					};
					var json = JsonConvert.SerializeObject(_retoBuscar);
					var content = new StringContent(json, Encoding.UTF8, "application/json");
					HttpClient client = new HttpClient();
					var result = await client.PostAsync("https://dmrbolivia.com/api_appjuego/listaRetosNombreBuscar.php", content);

					var jsonR = await result.Content.ReadAsStringAsync();
					var lista_retos = JsonConvert.DeserializeObject<List<RetoNombre>>(jsonR);

					foreach (var item in lista_retos)
					{
						_listaRetos.Add(item);
					}
				}
				catch (Exception)
				{
					await App.Current.MainPage.DisplayAlert("Error", "Algo salio mal, intentelo de nuevo", "OK");
				}
			}
			else
			{
				await App.Current.MainPage.DisplayAlert("Error", "Necesitas estar conectado a internet", "OK");
			}
		}
	}
}
