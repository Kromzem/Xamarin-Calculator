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

		private readonly List<string> _calculation = new List<string>();

		private bool _inputIsSolution;

		public MainPage()
		{
			InitializeComponent();
		}

		public void OnNumberSelected(object sender, EventArgs args)
		{
			if (Input.Equals("0") || _inputIsSolution)
			{
				Input = "";
			}

			Input += ((Button)sender).Text;

			_inputIsSolution = false;
		}

		public void OnOperatorSelected(object sender, EventArgs args)
		{
			var selectedOperator = ((Button)sender).Text;

			_calculation.Add(Input);
			_calculation.Add(selectedOperator);

			Result += Input + selectedOperator;
			Input = "0";

			_inputIsSolution = false;
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
			if (!Input.Equals("0"))
			{
				Input = Input.Remove(Input.Length - 1);

				if (Input.Length <= 0)
				{
					Input = "0";
				}
			}
			else if (_calculation.Count > 0)
			{
				RemoveLastCalculationElement();
				Input = RemoveLastCalculationElement();
			}
		}

		public void OnCalculateSolution(object sender, EventArgs args)
		{
			_calculation.Add(Input);

			for (var i = 0; i < _calculation.Count; i++)
			{
				var element = _calculation[i];

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

			for (var i = 0; i < _calculation.Count; i++)
			{
				var element = _calculation[i];

				if (element.Equals("+"))
				{
					CalculateAtIndex(i, (first, second) => first + second);
					i--;
				}
				else if (element.Equals("-"))
				{
					CalculateAtIndex(i, (first, second) => first - second);
					i--;
				}
			}

			Input = _calculation[0];
			Result = "";

			_calculation.Clear();

			_inputIsSolution = true;
		}

		private void CalculateAtIndex(int operatorIndex, Func<double, double, double> calculationLogic)
		{
			var first = Convert.ToDouble(_calculation[operatorIndex - 1]);
			var second = Convert.ToDouble(_calculation[operatorIndex + 1]);

			_calculation.RemoveAt(operatorIndex - 1);
			_calculation.RemoveAt(operatorIndex - 1);
			_calculation.RemoveAt(operatorIndex - 1);

			_calculation.Insert(operatorIndex - 1, Convert.ToString(calculationLogic(first, second)));
		}

		private string RemoveLastCalculationElement()
		{
			var element = _calculation.RemoveLast();

			Result = Result.Remove(Result.Length - element.Length);

			return element;
		}
	}
}
