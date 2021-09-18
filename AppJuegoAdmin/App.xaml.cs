using AppJuegoAdmin.Views;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppJuegoAdmin
{
	public partial class App : Application
	{

		public App()
		{
			InitializeComponent();

			MainPage = new AppShell();
		}
		public static ObservableCollection<Models.Categoria> _listaCategorias = new ObservableCollection<Models.Categoria>();
		public static string _CategoriaElegida;
		public static int _idCategoriaElegida = 0;
		public static ObservableCollection<Models.SubCategoria> _listaSubCategorias = new ObservableCollection<Models.SubCategoria>();
		public static string _SubCategoriaElegida;
		public static int _idSubCategoriaElegida = 0;
		public static string _estadoElegido;
		public static string _nivelElegido;
		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
