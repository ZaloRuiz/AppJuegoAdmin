using Plugin.Connectivity;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppJuegoAdmin.Helpers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PickerCategoria : PopupPage
	{
		public PickerCategoria()
		{
			InitializeComponent();
		}
		protected async override void OnAppearing()
		{
			base.OnAppearing();
			if (CrossConnectivity.Current.IsConnected)
			{
				try
				{
					listCategoria.ItemsSource = App._listaCategorias;
				}
				catch (Exception)
				{
					await DisplayAlert("Error", "Algo salio mal, intentelo de nuevo", "OK");
				}
			}
			else
			{
				await DisplayAlert("Error", "Necesitas estar conectado a internet", "OK");
			}
		}
		protected override void OnDisappearing()
		{

		}
		protected override bool OnBackButtonPressed()
		{
			return true;
		}
		protected override bool OnBackgroundClicked()
		{
			return false;
		}

		private async void btnCerrar_Clicked(object sender, EventArgs e)
		{
			await PopupNavigation.Instance.PopAllAsync();
			MessagingCenter.Send(this, "CategoriaElegida");
		}

		private void listCategoria_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			var detalles = e.Item as Models.Categoria;
			App._CategoriaElegida = string.Empty;
			App._CategoriaElegida = detalles.nombre;
			App._idCategoriaElegida = 0;
			App._idCategoriaElegida = detalles.id_categoria;
		}
	}
}