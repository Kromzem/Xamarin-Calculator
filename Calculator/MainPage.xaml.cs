using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculator
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage
	{
		private string Result { get => result.Text; set => result.Text = value; }
		private string Input { get => input.Text; set => input.Text = value; }

		public MainPage()
		{
			InitializeComponent();
		}

		public void OnNumberSelected(object sender, EventArgs args)
		{
			Input += ((Button)sender).Text;
		}

		public void OnOperatorSelected(object sender, EventArgs args)
		{
			Result += Input + ((Button)sender).Text;
			Input = "";
		}

		public void OnClearComplete(object sender, EventArgs args)
		{
			Result = "";
			Input = "";
		}

		public void OnClearInput(object sender, EventArgs args)
		{
			Input = "";
		}

		public void OnDelete(object sender, EventArgs args)
		{
			if (!string.IsNullOrWhiteSpace(Input))
			{
				Input = Input.Remove(Input.Length - 1);
			}
		}
	}
}
