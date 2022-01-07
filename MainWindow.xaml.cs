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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Tamagochi
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string nombreTamagochi;
        DispatcherTimer t1;
        Double decremento = 5.0;
        Double puntuacionfinal = 0.0;
        Storyboard cansado;
        Storyboard hambriento; 
        Storyboard aburrido;
        bool bCansado=true;
        bool bHambriento=true;
        bool bAburrido=true;
        public MainWindow()
        {
            InitializeComponent();
            VentanaBienvenida pantallaInicio = new VentanaBienvenida(this);
            pantallaInicio.ShowDialog();
            t1 = new DispatcherTimer();
            t1.Interval = TimeSpan.FromMilliseconds(1000.0);
            t1.Tick += new EventHandler(reloj);
            t1.Start();
        }

        private void reloj(object sender, EventArgs e)
        {
            this.pgComer.Value -= decremento/2;
            this.pgDiversion.Value -= decremento/3;
            this.pgEnergia.Value -= decremento/4;
            this.puntuacionfinal++;
            cansado = (Storyboard)this.Resources["Cansado"];
            hambriento = (Storyboard)this.Resources["Hambriento"];
            aburrido = (Storyboard)this.Resources["Aburrido"];

            if (pgComer.Value <= 20 || pgDiversion.Value <= 20 || pgEnergia.Value <= 20)
            {
                this.pgComer.Foreground = new SolidColorBrush(Colors.Red);
                this.pgDiversion.Foreground = new SolidColorBrush(Colors.Red);
                this.pgEnergia.Foreground = new SolidColorBrush(Colors.Red);
                if (pgEnergia.Value <= 20 && bCansado)
                {
                    this.btnComer.IsEnabled = false;
                    this.btnDescansar.IsEnabled = false;
                    this.btnJugar.IsEnabled = false;
                    cansado.Completed += new EventHandler(finCritico);
                    cansado.Begin();
                    bCansado = false;
                }
                if (pgComer.Value <= 20 && bHambriento)
                {
                    this.btnComer.IsEnabled = false;
                    this.btnDescansar.IsEnabled = false;
                    this.btnJugar.IsEnabled = false;
                    hambriento.Completed += new EventHandler(finCritico);
                    hambriento.Begin();
                    bHambriento = false;
                }
                if (pgDiversion.Value <= 20 && bAburrido)
                {
                    this.btnComer.IsEnabled = false;
                    this.btnDescansar.IsEnabled = false;
                    this.btnJugar.IsEnabled = false;
                    aburrido.Completed += new EventHandler(finCritico);
                    aburrido.Begin();
                    bAburrido = false;
                }
            }
            else
            {
                this.pgComer.Foreground = new SolidColorBrush(Colors.Green);
                this.pgDiversion.Foreground = new SolidColorBrush(Colors.Green);
                this.pgEnergia.Foreground = new SolidColorBrush(Colors.Green);
            }
            if (this.puntuacionfinal > 10)
            {
                this.lblPuntuacion.Content = "¡Bronce desbloqueado!";
                this.lblPuntuacion.Visibility = Visibility.Visible;
                bronce.Visibility = Visibility.Visible;
            }
            if (this.puntuacionfinal > 20)
            {
                this.lblPuntuacion.Content = "¡Plata desbloqueada!";
                this.lblPuntuacion.Visibility = Visibility.Visible;
                plata.Visibility = Visibility.Visible;
            }
            if (this.puntuacionfinal > 36)
            {
                this.lblPuntuacion.Content = "¡Oro desbloqueado!";
                this.lblPuntuacion.Visibility = Visibility.Visible;
                oro.Visibility = Visibility.Visible;
            }
            if (this.puntuacionfinal > 40)
            {
                this.lblPuntuacion.Content = "¡Bien jugado!";
                this.lblMini.Content = "¡Lleva a Minnie al Canvas!";
                this.imMini.Visibility = Visibility.Visible;
            }

            if (pgComer.Value <= 0 || pgDiversion.Value <= 0 || pgEnergia.Value <= 0)
            {
                t1.Stop();
                this.lblGameOver.Visibility = Visibility.Visible;
                this.btnComer.IsEnabled = false;
                this.btnDescansar.IsEnabled = false;
                this.btnJugar.IsEnabled = false;
                this.lblPuntuacion.Content = "La puntuacion final es " + puntuacionfinal;
                this.lblPuntuacion.Visibility = Visibility.Visible;
                this.lblGameOver.Content = "Game over, "+nombreTamagochi;
                this.lblPuntuacion.Visibility = Visibility.Visible;
                Storyboard fsbaux = (Storyboard)this.Resources["Crítico"];
                fsbaux.Begin();
                if(this.puntuacionfinal < 39)
                {
                    this.lblChoco.Content = "4) "+this.puntuacionfinal+": "+nombreTamagochi;
                }
                else if (this.puntuacionfinal >= 39 && this.puntuacionfinal < 43)
                {
                    this.lblTercero.Content = "3) " + this.puntuacionfinal + ": " + nombreTamagochi;
                    this.lblChoco.Content = "4) 39: LUIS";
                }
                else if (this.puntuacionfinal >= 43 && this.puntuacionfinal < 46)
                {
                    this.lblSegundo.Content = "2) " + this.puntuacionfinal + ": " + nombreTamagochi;
                    this.lblTercero.Content = "3) 43: ALBA";
                    this.lblChoco.Content = "4) 39: LUIS";
                }
                else
                {
                    this.lblPrimero.Content = "1) " + this.puntuacionfinal + ": " + nombreTamagochi;
                    this.lblSegundo.Content = "2) 49: PACO";
                    this.lblTercero.Content = "3) 43: ALBA";
                    this.lblChoco.Content = "4) 39: LUIS";
                }
            }
        }

        private void btnComer_Click(object sender, RoutedEventArgs e)
        {
            bHambriento = true;
            this.btnComer.IsEnabled = false;
            this.pgComer.Value += 25;
            this.decremento += 1;
            Storyboard sbaux = (Storyboard)this.Resources["animationComer"];
            sbaux.Completed += new EventHandler(finComer);
            sbaux.Begin();

        }

        private void finComer(object sender, EventArgs e)
        {
            if (pgComer.Value > 0 && pgDiversion.Value > 0 && pgEnergia.Value > 0)
            {
                this.btnComer.IsEnabled = true;
            }
        }

        private void finCritico(object sender, EventArgs e)
        {
            this.btnComer.IsEnabled = true;
            this.btnDescansar.IsEnabled = true;
            this.btnJugar.IsEnabled = true;
        }

        private void btnDescansar_Click(object sender, RoutedEventArgs e)
        {
            bCansado = true;
            this.pgEnergia.Value += 20;
            this.decremento += 1;
            cansado.AutoReverse = true;
            this.btnDescansar.IsEnabled = false;

            DoubleAnimation cerrarOjoDer = new DoubleAnimation();
            cerrarOjoDer.To = elOjoDer.Height / 2;
            cerrarOjoDer.Duration = new Duration(TimeSpan.FromSeconds(1));
            cerrarOjoDer.AutoReverse = true;
            cerrarOjoDer.Completed += new EventHandler(finCerrarOjoDer);

            DoubleAnimation cerrarOjoIzq = new DoubleAnimation();
            cerrarOjoIzq.To = elOjoIzq.Height / 2;
            cerrarOjoIzq.Duration = new Duration(TimeSpan.FromSeconds(1));
            cerrarOjoIzq.AutoReverse = true;

            elOjoDer.BeginAnimation(Ellipse.HeightProperty, cerrarOjoDer);
            elOjoIzq.BeginAnimation(Ellipse.HeightProperty, cerrarOjoDer);
        }

        private void finCerrarOjoDer(object sender, EventArgs e)
        {
            if (pgComer.Value > 0 && pgDiversion.Value > 0 && pgEnergia.Value > 0)
            {
                this.btnDescansar.IsEnabled = true;
            }
        }

        private void btnJugar_Click(object sender, RoutedEventArgs e)
        {
            bAburrido = true;
            this.btnJugar.IsEnabled = false;
            this.pgDiversion.Value += 20;
            this.decremento += 1;
            Storyboard jgaux = (Storyboard)this.Resources["animationJugar"];
            jgaux.Completed += new EventHandler(finJugar);
            jgaux.Begin();
        }

        private void finJugar(object sender, EventArgs e)
        {
            if (pgComer.Value > 0 && pgDiversion.Value > 0 && pgEnergia.Value > 0)
            {
                this.btnJugar.IsEnabled = true;
            }
        }

        private void cambiarFondo(object sender, MouseButtonEventArgs e)
        {
            this.imFondo.Source = ((Image)sender).Source;
        }

        private void acercaDe(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("Programa realizado por \n \nAdrián Sánchez", "Acerca de", MessageBoxButton.YesNo);
            switch (resultado) { 
                case MessageBoxResult.Yes:
                    this.Close(); 
                    break; 
                case MessageBoxResult.No: 
                    break; 
            }
        }

        public void setNombre(string nombre)
        {
            this.nombreTamagochi = nombre;
            this.lblGameOver.Content = "¡Hola, " + nombreTamagochi+"!";
            this.lblGameOver.Visibility = Visibility.Visible;
        }

        private void inicioArrastrar(object sender, MouseButtonEventArgs e)
        {
            DataObject dataO = new DataObject(((Image)sender)); 
            DragDrop.DoDragDrop((Image)sender, dataO, DragDropEffects.Move);
        }

        private void aniadirObjeto(object sender, DragEventArgs e)
        {
            Image aux = (Image)e.Data.GetData(typeof(Image));
            switch (aux.Name)
            {
                case "imGorro":
                    imGorroCV.Visibility = Visibility.Visible;
                    break;
                case "imMini":
                    miniDress.Visibility = Visibility.Visible;
                    miniGorro.Visibility = Visibility.Visible;
                    imGorroCV.Visibility = Visibility.Hidden;
                    break;
            }
        }
    }
}
