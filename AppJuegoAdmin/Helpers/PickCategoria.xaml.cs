using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppJuegoAdmin.Helpers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PickCategoria : Popup
	{
		public PickCategoria()
		{
			InitializeComponent();
			listCategoria.ItemsSource = App._listaCategorias;
		}
	}
}