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
	public class ListaRetosMenuVM : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		private ObservableCollection<CategoriaCont> _listaCategoriasCont;
		public ObservableCollection<CategoriaCont> ListaCategoriasCont
		{
			get { return _listaCategoriasCont; }
			set
			{
				if (_listaCategoriasCont != value)
				{
					_listaCategoriasCont = value;
					OnPropertyChanged("ListaCategoriasCont");
				}
			}
		}
		public ListaRetosMenuVM()
		{
			_listaCategoriasCont = new ObservableCollection<CategoriaCont>();
			GetCategorias();
		}
		public async void GetCategorias()
		{
			if (CrossConnectivity.Current.IsConnected)
			{
				try
				{
					HttpClient client = new HttpClient();
					var response = await client.GetStringAsync("https://dmrbolivia.com/api_appjuego/listaCategoriasCont.php");
					var cantc_lista = JsonConvert.DeserializeObject<List<CategoriaCont>>(response);

					foreach (var item in cantc_lista)
					{
						_listaCategoriasCont.Add(item);
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
