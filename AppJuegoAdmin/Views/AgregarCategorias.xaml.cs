using AppJuegoAdmin.Helpers;
using AppJuegoAdmin.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppJuegoAdmin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AgregarCategorias : ContentPage
	{
		public AgregarCategorias()
		{
			InitializeComponent();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			MessagingCenter.Subscribe<PickerEstado>(this, "EstadoElegido", sender =>
			{
				entryEstado.Text = App._estadoElegido;
			});
		}
		private async void entryEstado_Focused(object sender, FocusEventArgs e)
		{
			await PopupNavigation.Instance.PushAsync(new PickerEstado());
		}
		private async void btnGuardar_Clicked(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(entryNombre.Text) || (!string.IsNullOrEmpty(entryNombre.Text)))
			{
				if (!string.IsNullOrWhiteSpace(entryDescripcion.Text) || (!string.IsNullOrEmpty(entryDescripcion.Text)))
				{
					if (!string.IsNullOrWhiteSpace(entryEstado.Text) || (!string.IsNullOrEmpty(entryEstado.Text)))
					{
						if (CrossConnectivity.Current.IsConnected)
						{
							try
							{
								Categoria _categoria = new Categoria()
								{
									nombre = entryNombre.Text,
									descripcion = entryDescripcion.Text,
									estado = entryEstado.Text.ToLower(),
									comprado = "si"
								};

								var json = JsonConvert.SerializeObject(_categoria);
								var content = new StringContent(json, Encoding.UTF8, "application/json");
								HttpClient client = new HttpClient();
								var result = await client.PostAsync("https://dmrbolivia.com/api_appjuego/agregarCategoria.php", content);
								if (result.StatusCode == HttpStatusCode.OK)
								{
									await DisplayAlert("OK", "Se agrego correctamente", "OK");
									await Shell.Current.Navigation.PopAsync();
								}
								else
								{
									await DisplayAlert("Error", "Algo salio mal, intentelo de nuevo", "OK");
									await Shell.Current.Navigation.PopAsync();
								}
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
					else
					{
						await DisplayAlert("Campo vacio", "El campo de Estado esta vacio", "Ok");
					}
				}
				else
				{
					await DisplayAlert("Campo vacio", "El campo de Descripcion esta vacio", "Ok");
				}
			}
			else
			{
				await DisplayAlert("Campo vacio", "El campo de Nombre esta vacio", "Ok");
			}
		}
	}
}