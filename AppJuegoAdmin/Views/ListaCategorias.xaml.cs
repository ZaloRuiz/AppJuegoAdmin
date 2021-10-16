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
	public partial class ListaCategorias : ContentPage
	{
		public ListaCategorias()
		{
			InitializeComponent();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			BindingContext = new ListaCategoriasVM();
		}
		private async void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			await Shell.Current.Navigation.PushAsync(new AgregarCategorias(), true);
		}
	}
}