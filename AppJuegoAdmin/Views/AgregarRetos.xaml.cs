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
	public partial class AgregarRetos : ContentPage
	{
		private int _idCategoria = 0;
		private int _idSubCategoria = 0;
		public AgregarRetos()
		{
			InitializeComponent();
			GetCategorias();
			GetSubCategorias();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			MessagingCenter.Subscribe<PickerCategoria>(this, "CategoriaElegida", sender =>
			{
				entryCategoria.Text = App._CategoriaElegida;
				_idCategoria = App._idCategoriaElegida;
			});
			MessagingCenter.Subscribe<PickerSubCategoria>(this, "SubCategoriaElegida", sender =>
			{
				entrySubCategoria.Text = App._SubCategoriaElegida;
				_idSubCategoria = App._idSubCategoriaElegida;
			});
			MessagingCenter.Subscribe<PickerEstado>(this, "EstadoElegido", sender =>
			{
				entryEstado.Text = App._estadoElegido;
			});
		}
		private async void entryCategoria_Focused(object sender, FocusEventArgs e)
		{
			await PopupNavigation.Instance.PushAsync(new PickerCategoria());
		}
		private async void entrySubCategoria_Focused(object sender, FocusEventArgs e)
		{
			await PopupNavigation.Instance.PushAsync(new PickerSubCategoria());
		}
		private async void entryEstado_Focused(object sender, FocusEventArgs e)
		{
			await PopupNavigation.Instance.PushAsync(new PickerEstado());
		}
		public async void GetCategorias()
		{
			if (CrossConnectivity.Current.IsConnected)
			{
				try
				{
					HttpClient client = new HttpClient();
					var response = await client.GetStringAsync("https://dmrbolivia.com/api_appjuego/listaCategorias.php");
					var lista_c = JsonConvert.DeserializeObject<List<Categoria>>(response);
					App._listaCategorias.Clear();
					foreach (var item in lista_c)
					{
						App._listaCategorias.Add(item);
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
		public async void GetSubCategorias()
		{
			if (CrossConnectivity.Current.IsConnected)
			{
				try
				{
					HttpClient client = new HttpClient();
					var response = await client.GetStringAsync("https://dmrbolivia.com/api_appjuego/listaSubCategorias.php");
					var lista_sc = JsonConvert.DeserializeObject<List<SubCategoria>>(response);
					App._listaSubCategorias.Clear();
					foreach (var item in lista_sc)
					{
						App._listaSubCategorias.Add(item);
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
							if (!string.IsNullOrWhiteSpace(entryCategoria.Text) || (!string.IsNullOrEmpty(entryCategoria.Text)))
							{
								if (!string.IsNullOrWhiteSpace(entrySubCategoria.Text) || (!string.IsNullOrEmpty(entrySubCategoria.Text)))
								{
									if (!string.IsNullOrWhiteSpace(entryN1penitencia.Text) || (!string.IsNullOrEmpty(entryN1penitencia.Text)))
									{
										if (!string.IsNullOrWhiteSpace(entryN2penitencia.Text) || (!string.IsNullOrEmpty(entryN2penitencia.Text)))
										{
											if (!string.IsNullOrWhiteSpace(entryN3penitencia.Text) || (!string.IsNullOrEmpty(entryN3penitencia.Text)))
											{
												if (CrossConnectivity.Current.IsConnected)
												{
													try
													{
														Reto _reto = new Reto()
														{
															descripcion = entryDescripcion.Text,
															tiempo = Convert.ToInt32(entryTiempo.Text),
															nivel = Convert.ToInt32(entryNivel.Text),
															estado = entryEstado.Text.ToLower(),
															n1_penitencia = entryN1penitencia.Text,
															n2_penitencia = entryN2penitencia.Text,
															n3_penitencia = entryN3penitencia.Text,
															id_categoria = _idCategoria,
															id_sub_categoria = _idSubCategoria
														};

														var json = JsonConvert.SerializeObject(_reto);
														var content = new StringContent(json, Encoding.UTF8, "application/json");
														HttpClient client = new HttpClient();
														var result = await client.PostAsync("https://dmrbolivia.com/api_appjuego/agregarReto.php", content);
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
												await DisplayAlert("Campo vacio", "El campo de Penitencia nivel 3 esta vacio", "Ok");
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
									await DisplayAlert("Campo vacio", "Elija una SubCategoria para poder guardar el reto", "Ok");
								}
							}
							else
							{
								await DisplayAlert("Campo vacio", "Elija una Categoria para poder guardar el reto", "Ok");
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