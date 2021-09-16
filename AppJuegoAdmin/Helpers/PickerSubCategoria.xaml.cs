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
	public partial class PickerSubCategoria : PopupPage
	{
		public PickerSubCategoria()
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
					listSubCategoria.ItemsSource = App._listaSubCategorias;
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
			MessagingCenter.Send(this, "SubCategoriaElegida");
		}

		private void listSubCategoria_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			var detalles = e.Item as Models.SubCategoria;
			App._SubCategoriaElegida = string.Empty;
			App._SubCategoriaElegida = detalles.nombre;
			App._idSubCategoriaElegida = 0;
			App._idSubCategoriaElegida = detalles.id_sub_categoria;
		}
	}
}