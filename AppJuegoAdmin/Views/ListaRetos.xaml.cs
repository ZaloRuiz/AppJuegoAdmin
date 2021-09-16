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
			//ToolbarItem toolbar = new ToolbarItem
			//{
			//	IconImageSource = "icon_add.png",
			//	Order = ToolbarItemOrder.Primary,
			//	Priority = 1
			//};
			//this.ToolbarItems.Add(toolbar);
			//toolbar.Clicked += ToolbarItem_Clicked;
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
	}
}