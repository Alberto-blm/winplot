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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Interfaz
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public ViewModel vm;

        List<Polyline> lineas;

        public struct FuncRect
        {
            public double xMin, yMin, xMax, yMax;
        }

        public FuncRect real, screen;

        public MainWindow()
        {
            InitializeComponent();
            vm = new ViewModel();
            vm.addFunction += Dibujar;
            vm.modifyFunction += Modificar;
            vm.deleteFunction += Eliminar;
            lineas = new List<Polyline>();
        }

        private void Eliminar(object sender, ModelEventargs e)
        {
            int pos = (int)e.posicion;
            lienzo.Children.Remove(lineas[pos]);
            lineas.RemoveAt(pos);
        }

        private void Modificar(object sender, ModelEventargs e)
        {
            Bean funcion = (Bean)e.miObjeto;
            int pos = (int)e.posicion;
            lienzo.Children.Remove(lineas[pos]);
            lineas.RemoveAt(pos);
            Polyline poliLinea = DrawGraphics(funcion);
            lineas.Insert(pos, poliLinea);
        }

        private void Dibujar(object sender, ModelEventargs e)
        {
            Bean funcion = (Bean)e.miObjeto;
            Polyline poliLinea = DrawGraphics(funcion);
            lineas.Add(poliLinea);
        }

        private void PreferencesClick(object sender, RoutedEventArgs e)
        {
            Preferences preferences = new Preferences(vm);
            preferences.Owner = this;
            preferences.Show();
        }

        private void declareFuncion()
        {
            screen.xMax = lienzo.ActualWidth;
            screen.yMax = lienzo.ActualHeight;
            screen.xMin = 0;
            screen.yMin = 0;

            real.xMax = vm.Real.xMax;
            real.yMax = vm.Real.yMax;
            real.xMin = vm.Real.xMin;
            real.yMin = vm.Real.yMin;
        }

        private Polyline DrawGraphics(Bean plot)
        {
            PointCollection puntos = new PointCollection();
            Polyline poliLinea = new Polyline();
            double xReal, yReal, xPant, yPant;
            int numPtos;

            declareFuncion();

            numPtos = (int)screen.xMax;
            poliLinea.Stroke = new SolidColorBrush(plot.Color);
            for (int i = 0; i<numPtos; i++)
            {
                xReal = real.xMin + i * (real.xMax - real.xMin) / numPtos;
                yReal = TipoFuncion(xReal, plot);

                xPant = convertXRealToPant(xReal, screen.xMin);
                yPant = convertYRealToPant(yReal, screen.yMin);
                puntos.Add(new Point(xPant, yPant));
            }
            poliLinea.Points = new PointCollection(puntos);
            lienzo.Children.Add(poliLinea);

            pintarEjes();
            return poliLinea;
        }

        private double convertXRealToPant(double xReal, double width)
        {
            return (screen.xMax - width) * ((xReal - real.xMin) / (real.xMax - real.xMin)) + screen.xMin;
        }
        private double convertYRealToPant(double yReal, double height)
        {
            return (height - screen.yMax)*((yReal-real.yMin)/(real.yMax-real.yMin))+screen.yMax;
        }
        private double convertXPantToReal(double xPant, double width)
        {
            return (real.xMax - real.xMin) * ((xPant - screen.xMin) / (screen.xMax - screen.xMin));
        }
        private double convertYPantToReal(double yPant, double height)
        {
            return (real.yMax - real.yMin) * ((yPant - screen.yMax) / (screen.yMin - screen.yMax));
        }

        private void pintarEjes()
        {
            Line ejeX = new Line();
            Line ejeY = new Line();

            double pos = real.xMin;
           

            ejeX.Stroke = Brushes.Black;
            ejeY.Stroke = Brushes.Black;

            ejeX.X1 = 0;
            ejeX.X2 = screen.xMax;
            ejeX.Y1 = convertYRealToPant(0, 0);
            ejeX.Y2 = convertYRealToPant(0, 0);

            ejeY.Y1 = 0;
            ejeY.Y2 = screen.yMax;
            ejeY.X1 = convertXRealToPant(0, 0);
            ejeY.X2 = convertXRealToPant(0, 0);

            lienzo.Children.Add(ejeX);
            lienzo.Children.Add(ejeY);

            
        }

        private double TipoFuncion(double xReal, Bean plot)
        {
            switch (plot.Tipo)
            {
                case 0:
                    return plot.ParA * Math.Sin(plot.ParB * xReal);
                case 1:
                    return plot.ParA * Math.Cos(plot.ParB * xReal);
                case 2:
                    return plot.ParA * Math.Pow(xReal, plot.Exp);
                case 3:
                    return plot.ParA * xReal + plot.ParB;
                case 4:
                    return (plot.ParA * Math.Pow(xReal, 2)) + (plot.ParB * xReal) + plot.ParC;
                case 5:
                    return plot.ParA / (plot.ParB * xReal);
                default:
                    return xReal;

            }
        }

        
    }
}
