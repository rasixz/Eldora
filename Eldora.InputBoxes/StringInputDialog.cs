﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Eldora.InputBoxes;
public partial class StringInputDialog : Form
{
	public StringInputDialog()
	{
		InitializeComponent();
	}

	public static DialogResult Show(string title, string prompt, out string result, string defaultInput = "", Func<string, bool>? validateInput = null, string validationText = "")
	{
		var dialog = new StringInputDialog
		{
			Text = title,
		};
		dialog.lblQuestion.Text = prompt;
		dialog.txbInput.Text = defaultInput;
		dialog.txbInput.Focus();
		dialog.txbInput.SelectAll();

		DialogResult dialogResult;

		void showAndValidate()
		{
			dialogResult = dialog.ShowDialog();
			if (dialogResult == DialogResult.Cancel) return;
			if (validateInput == null) return;

			if (!validateInput(dialog.txbInput.Text))
			{
				MessageBox.Show("Input Invavlid!\n" + validationText, "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				showAndValidate();
			}
		}

		showAndValidate();

		result = dialog.txbInput.Text;
		return dialogResult;
	}

	private void TxbInput_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
		else if (e.KeyCode == Keys.Return)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
