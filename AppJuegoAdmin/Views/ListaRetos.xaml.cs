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
	public partial class ListaRetos : ContentPage
	{
		public ListaRetos()
		{
			InitializeComponent();
		}
		private async void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			await Shell.Current.Navigation.PushAsync(new AgregarRetos(), true);
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			BindingContext = new ListaRetosVM();
		}
		private async void listReto_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			var detalles = e.Item as RetoNombre;
			await Shell.Current.Navigation.PushAsync(new EditarReto(detalles.id_reto, detalles.descripcion, detalles.tiempo, detalles.nivel, detalles.estado,
				detalles.n1_penitencia, detalles.n2_penitencia), true);
		}
	}
}