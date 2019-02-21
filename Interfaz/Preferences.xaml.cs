using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Interfaz
{
    /// <summary>
    /// Lógica de interacción para Preferences.xaml
    /// </summary>
    public partial class Preferences : Window
    {
        public ViewModel vm;
        public Preferences()
        {
            InitializeComponent();
        }
        public Preferences(ViewModel vModel)
        {
            InitializeComponent();
            vm = vModel;
            graficas.ItemsSource = vm.Model.listFunciones;
            ejeXMax.Text = vm.Real.xMax.ToString();
            ejeXMin.Text = vm.Real.xMin.ToString();
            ejeYMax.Text = vm.Real.yMax.ToString();
            ejeYMin.Text = vm.Real.yMin.ToString();
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Bean funcion;
            if (funciones.SelectedIndex == 0 || funciones.SelectedIndex == 1 ||
               funciones.SelectedIndex == 3 || funciones.SelectedIndex == 5)
            {
                funcion = new Bean(cajaNombre.Text, funciones.SelectedIndex, float.Parse(cajaParA.Text), float.Parse(cajaParB.Text), (Color)cajaColor.SelectedColor);
                vm.AddFunction(funcion);
            }
            if(funciones.SelectedIndex == 2)
            {
                funcion = new Bean(cajaNombre.Text, funciones.SelectedIndex, float.Parse(cajaParA.Text), (Color)cajaColor.SelectedColor, float.Parse(cajaExponente.Text));
                vm.AddFunction(funcion);
            }

            if (funciones.SelectedIndex == 4)
            {
                funcion = new Bean(cajaNombre.Text, funciones.SelectedIndex, float.Parse(cajaParA.Text), float.Parse(cajaParB.Text), float.Parse(cajaParC.Text), (Color)cajaColor.SelectedColor);
                vm.AddFunction(funcion);
            }
            ViewModel.limites newLimits;
            newLimits.xMax = double.Parse(ejeXMax.Text);
            newLimits.xMin = double.Parse(ejeXMin.Text);
            newLimits.yMax = double.Parse(ejeYMax.Text);
            newLimits.yMin = double.Parse(ejeYMin.Text);
            vm.Real = newLimits;

        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            Bean funcV = (Bean)graficas.SelectedItem;
            Bean funcN = null;

            if (funciones.SelectedIndex == 0 || funciones.SelectedIndex == 1 ||
               funciones.SelectedIndex == 3 || funciones.SelectedIndex == 5)
            {
                funcN = new Bean(cajaNombre.Text, funciones.SelectedIndex, float.Parse(cajaParA.Text), float.Parse(cajaParB.Text), (Color)cajaColor.SelectedColor);
                vm.ModifyFunction(funcV, funcN, graficas.SelectedIndex);
            }
            if (funciones.SelectedIndex == 2)
            {
                funcN = new Bean(cajaNombre.Text, funciones.SelectedIndex, float.Parse(cajaParA.Text), (Color)cajaColor.SelectedColor, float.Parse(cajaExponente.Text));
                vm.ModifyFunction(funcV, funcN, graficas.SelectedIndex);
            }

            if (funciones.SelectedIndex == 4)
            {
                funcN = new Bean(cajaNombre.Text, funciones.SelectedIndex, float.Parse(cajaParA.Text), float.Parse(cajaParB.Text), float.Parse(cajaParC.Text), (Color)cajaColor.SelectedColor);
                vm.ModifyFunction(funcV,funcN, graficas.SelectedIndex);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Bean func = (Bean)graficas.SelectedItem;
            vm.DeleteFunction(func, graficas.SelectedIndex);
        }

        private void Formula1_Selected(object sender, RoutedEventArgs e)
        {
            cajaParB.IsEnabled = true;
            cajaParC.IsEnabled = false;
            cajaExponente.IsEnabled = false;
        }

        private void Formula2_Selected(object sender, RoutedEventArgs e)
        {
            cajaParB.IsEnabled = true;
            cajaParC.IsEnabled = false;
            cajaExponente.IsEnabled = false;
        }

        private void Formula3_Selected(object sender, RoutedEventArgs e)
        {
            cajaParB.IsEnabled = false;
            cajaParC.IsEnabled = false;
            cajaExponente.IsEnabled = true;
        }

        private void Formula4_Selected(object sender, RoutedEventArgs e)
        {
            cajaParB.IsEnabled = true;
            cajaParC.IsEnabled = false;
            cajaExponente.IsEnabled = false;
        }

        private void Formula5_Selected(object sender, RoutedEventArgs e)
        {
            cajaParB.IsEnabled = true;
            cajaParC.IsEnabled = true;
            cajaExponente.IsEnabled = false;
        }

        private void Formula6_Selected(object sender, RoutedEventArgs e)
        {
            cajaParB.IsEnabled = true;
            cajaParC.IsEnabled = false;
            cajaExponente.IsEnabled = false;
        }

        private void Graficas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bean func;
            if(graficas.SelectedItem != null)
            {
                func = (Bean)graficas.SelectedItem;

                cajaExponente.Text = func.Exp.ToString();
                cajaNombre.Text = func.Nombre;
                cajaParA.Text = func.ParA.ToString();
                cajaParB.Text = func.ParB.ToString();
                cajaParC.Text = func.ParC.ToString();
                funciones.SelectedIndex = func.Tipo;
            }

            
        }
    }
}
