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

		private List<string> calculation = new List<string>();

		private bool inputIsSolution;

		public MainPage()
		{
			InitializeComponent();
		}

		public void OnNumberSelected(object sender, EventArgs args)
		{
			if(Input.Equals("0") || inputIsSolution)
			{
				Input = "";
			}

			Input += ((Button)sender).Text;

			inputIsSolution = false;
		}

		public void OnOperatorSelected(object sender, EventArgs args)
		{
			var selectedOperator = ((Button)sender).Text;

			calculation.Add(Input);
			calculation.Add(selectedOperator);

			Result += Input + selectedOperator;
			Input = "0";

			inputIsSolution = false;
		}

		public void OnClearComplete(object sender, EventArgs args)
		{
			Result = "";
			Input = "0";
		}

		public void OnClearInput(object sender, EventArgs args)
		{
			Input = "0";
		}

		public void OnDelete(object sender, EventArgs args)
		{
			if(!Input.Equals("0"))
			{
				Input = Input.Remove(Input.Length - 1);

				if (Input.Length <= 0)
				{
					Input = "0";
				}
			}
			else if(calculation.Count > 0)
			{
				RemoveLastCalculationElement();
				Input = RemoveLastCalculationElement();
			}
		}

		public void OnCalculateSolution(object sender, EventArgs args)
		{
			calculation.Add(Input);

			for (var i = 0; i < calculation.Count; i++)
			{
				var element = calculation[i];

				if (element.Equals("*"))
				{
					CalculateAtIndex(i, (first, second) => first * second);
					i--;
				}
				else if (element.Equals("/"))
				{
					CalculateAtIndex(i, (first, second) => first / second);
					i--;
				}
			}

			for (var i = 0; i < calculation.Count; i++)
			{
				var element = calculation[i];

				if(element.Equals("+"))
				{
					CalculateAtIndex(i, (first, second) => first + second);
					i--;
				}
				else if(element.Equals("-"))
				{
					CalculateAtIndex(i, (first, second) => first - second);
					i--;
				}
			}

			Input = calculation[0];
			Result = "";

			calculation.Clear();

			inputIsSolution = true;
		}

		private void CalculateAtIndex(int operatorIndex, Func<double, double, double> calculationLogic)
		{
			var first = Convert.ToDouble(calculation[operatorIndex - 1]);
			var second = Convert.ToDouble(calculation[operatorIndex + 1]);

			calculation.RemoveAt(operatorIndex - 1);
			calculation.RemoveAt(operatorIndex - 1);
			calculation.RemoveAt(operatorIndex - 1);

			calculation.Insert(operatorIndex - 1, Convert.ToString(calculationLogic(first, second)));
		}

		private string RemoveLastCalculationElement()
		{
			var element = calculation.RemoveLast();

			Result = Result.Remove(Result.Length - element.Length);

			return element;
		}
	}
}
