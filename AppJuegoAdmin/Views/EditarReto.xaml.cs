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
	public partial class EditarReto : ContentPage
	{
		private int _idReto = 0;
		public EditarReto (int id_reto, string descripcion, int tiempo, string nivel, string estado, string n1_penitencia, string n2_penitencia)
		{
			InitializeComponent ();
			entryDescripcion.Text = descripcion;
			entryTiempo.Text = tiempo.ToString();
			entryNivel.Text = nivel.ToString();
			entryEstado.Text = estado;
			entryN1penitencia.Text = n1_penitencia;
			entryN2penitencia.Text = n2_penitencia;
			_idReto = id_reto;
			entryDescripcion.Keyboard = Keyboard.Create(KeyboardFlags.Suggestions | KeyboardFlags.CapitalizeSentence);
			entryN1penitencia.Keyboard = Keyboard.Create(KeyboardFlags.Suggestions | KeyboardFlags.CapitalizeSentence);
			entryN2penitencia.Keyboard = Keyboard.Create(KeyboardFlags.Suggestions | KeyboardFlags.CapitalizeSentence);
		}
		private void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			if(btnGuardar.IsVisible == false)
			{
				entryDescripcion.IsReadOnly = false;
				entryTiempo.IsReadOnly = false;
				entryNivel.IsReadOnly = false;
				entryEstado.IsReadOnly = false;
				entryN1penitencia.IsReadOnly = false;
				entryN2penitencia.IsReadOnly = false;
				btnGuardar.IsVisible = true;
			}
			else
			{
				entryDescripcion.IsReadOnly = true;
				entryTiempo.IsReadOnly = true;
				entryNivel.IsReadOnly = true;
				entryEstado.IsReadOnly = true;
				entryN1penitencia.IsReadOnly = true;
				entryN2penitencia.IsReadOnly = true;
				btnGuardar.IsVisible = false;
			}
		}
		private async void entryEstado_Focused(object sender, FocusEventArgs e)
		{
			await PopupNavigation.Instance.PushAsync(new PickerEstado());
		}
		private async void entryNivel_Focused(object sender, FocusEventArgs e)
		{
			await PopupNavigation.Instance.PushAsync(new PickerNivel());
		}
		private async void btnGuardar_Clicked(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(entryDescripcion.Text) || (!string.IsNullOrEmpty(entryDescripcion.Text)))
			{
				if (!string.IsNullOrWhiteSpace(entryTiempo.Text) || (!string.IsNullOrEmpty(entryTiempo.Text)))
				{
					if (!string.IsNullOrWhiteSpace(entryNivel.Text) || (!string.IsNullOrEmpty(entryNivel.Text)))
					{
						if (!string.IsNullOrWhiteSpace(entryEstado.Text) || (!string.IsNullOrEmpty(entryEstado.Text)))
						{
							if (!string.IsNullOrWhiteSpace(entryN1penitencia.Text) || (!string.IsNullOrEmpty(entryN1penitencia.Text)))
							{
								if (!string.IsNullOrWhiteSpace(entryN2penitencia.Text) || (!string.IsNullOrEmpty(entryN2penitencia.Text)))
								{
									if(_idReto != 0)
									{
										if (CrossConnectivity.Current.IsConnected)
										{
											try
											{
												Reto _reto = new Reto()
												{
													id_reto = _idReto,
													descripcion = entryDescripcion.Text,
													tiempo = Convert.ToInt32(entryTiempo.Text),
													nivel = entryNivel.Text,
													estado = entryEstado.Text.ToLower(),
													n1_penitencia = entryN1penitencia.Text,
													n2_penitencia = entryN2penitencia.Text,
												};

												var json = JsonConvert.SerializeObject(_reto);
												var content = new StringContent(json, Encoding.UTF8, "application/json");
												HttpClient client = new HttpClient();
												var result = await client.PostAsync("https://dmrbolivia.com/api_appjuego/editarReto.php", content);
												if (result.StatusCode == HttpStatusCode.OK)
												{
													await DisplayAlert("OK", "Se edito correctamente", "OK");
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
										await DisplayAlert("Error", "Algo salio mal, por favor vuelva a intentarlo", "OK");
									}
								}
								else
								{
									await DisplayAlert("Campo vacio", "El campo de Penitencia nivel 2 esta vacio", "Ok");
								}
							}
							else
							{
								await DisplayAlert("Campo vacio", "El campo de Penitencia nivel 1 esta vacio", "Ok");
							}
						}
						else
						{
							await DisplayAlert("Campo vacio", "El campo de Estado esta vacio", "Ok");
						}
					}
					else
					{
						await DisplayAlert("Campo vacio", "El campo de Nivel esta vacio", "Ok");
					}
				}
				else
				{
					await DisplayAlert("Campo vacio", "El campo de Tiempo esta vacio", "Ok");
				}
			}
			else
			{
				await DisplayAlert("Campo vacio", "El campo de Descripcion esta vacio", "Ok");
			}
		}

		
	}
}