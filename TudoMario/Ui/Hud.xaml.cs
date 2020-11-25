using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TudoMario.Ui
{
    public sealed partial class Hud : UserControl
    {
        private bool DialogShown = false;

        public void SetStressLevel(float val)
        {
            StressMeter.Value = val;
        }
        public Hud()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Prints the text into the Hud dialogbox. Returns false if it was not possible.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool ShowDialog(string text)
        {
            if (!DialogShown)
            {
                DialogShown = true;
                DialogTextBox.Visibility = Visibility.Visible;
                DialogTextBox.Text = text;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void RemoveDialog()
        {
            DialogTextBox.Visibility = Visibility.Collapsed;
            DialogShown = false;
        }
    }
}
