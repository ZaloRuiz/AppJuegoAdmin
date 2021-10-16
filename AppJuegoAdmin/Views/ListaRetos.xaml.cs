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
		private int _id_categoria = 0;
		public ListaRetos(int id_categoria)
		{
			InitializeComponent();
			_id_categoria = id_categoria;
		}
		private async void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			await Shell.Current.Navigation.PushAsync(new AgregarRetos(), true);
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			BindingContext = new ListaRetosVM(_id_categoria);
		}
		private async void listReto_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			var detalles = e.Item as RetoNombre;
			await Shell.Current.Navigation.PushAsync(new EditarReto(detalles.id_reto, detalles.descripcion, detalles.tiempo, detalles.nivel, detalles.estado,
				detalles.n1_penitencia, detalles.n2_penitencia), true);
		}
	}
}