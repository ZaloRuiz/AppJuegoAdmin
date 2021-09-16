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
	public class ListaCategoriasVM : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		private ObservableCollection<Categoria> _listaCategorias;
		public ObservableCollection<Categoria> ListaCategorias
		{
			get { return _listaCategorias; }
			set
			{
				if (_listaCategorias != value)
				{
					_listaCategorias = value;
					OnPropertyChanged("ListaCategorias");
				}
			}
		}
		public ListaCategoriasVM()
		{
			_listaCategorias = new ObservableCollection<Categoria>();
			GetCategorias();
		}
		public async void GetCategorias()
		{
			try
			{
				HttpClient client = new HttpClient();
				var response = await client.GetStringAsync("https://dmrbolivia.com/api_appjuego/listaCategorias.php");
				var lista_retos = JsonConvert.DeserializeObject<List<Categoria>>(response);
				foreach (var item in lista_retos)
				{
					_listaCategorias.Add(item);
				}
			}
			catch (Exception err)
			{
				Console.WriteLine("#######################################" + err.ToString() + "#################################################");
			}
		}
	}
}
