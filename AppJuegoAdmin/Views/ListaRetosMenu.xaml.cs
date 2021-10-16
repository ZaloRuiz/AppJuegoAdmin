using AppJuegoAdmin.Models;
using AppJuegoAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppJuegoAdmin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaRetosMenu : ContentPage
	{
		public ListaRetosMenu()
		{
			InitializeComponent();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			BindingContext = new ListaRetosMenuVM();
		}
		private async void listCategoria_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			var detalles = e.Item as CategoriaCont;
			await Shell.Current.Navigation.PushAsync(new ListaRetos(detalles.id_categoria), true);
		}
	}
}