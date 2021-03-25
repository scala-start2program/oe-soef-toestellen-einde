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
using Scala.Toestellen.Core;

namespace Scala.Toestellen.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ToestelService toestelService;
        bool isNew;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            toestelService = new ToestelService();
            VulCmbFilter();
            VulCmbSoort();
            VulLstToestellen();
            ClearControls();
            LinksInschakelen();
            DoeStatistieken();
        }
        private void LinksInschakelen()
        {
            grpToestellen.IsEnabled = true;
            grpDetails.IsEnabled = false;
            btnBewaren.Visibility = Visibility.Hidden;
            btnAnnuleren.Visibility = Visibility.Hidden;
        }
        private void RechtsInschakelen()
        {
            grpToestellen.IsEnabled = false;
            grpDetails.IsEnabled = true;
            btnBewaren.Visibility = Visibility.Visible;
            btnAnnuleren.Visibility = Visibility.Visible;
        }
        private void VulCmbFilter()
        {
            cmbFilter.Items.Clear();
            cmbFilter.Items.Add("<alle toestellen>");
            //cmbFilter.Items.Add(ToestelSoort.Diepvries);
            foreach(ToestelSoort toestelsoort in Enum.GetValues(typeof(ToestelSoort)))
            {
                cmbFilter.Items.Add(toestelsoort);
            }


            cmbFilter.SelectedIndex = 0;
        }
        private void VulLstToestellen()
        {
            lstToestellen.ItemsSource = null;
            if (cmbFilter.SelectedIndex == 0)
            {
                lstToestellen.ItemsSource = toestelService.Toestellen;
            }
            else
            {
                ToestelSoort toestelSoort = (ToestelSoort)cmbFilter.SelectedItem;
                lstToestellen.ItemsSource = toestelService.FilterPerSoort(toestelSoort);
            }
        }
        private void VulCmbSoort()
        {
            cmbSoort.Items.Clear();
            foreach (ToestelSoort toestelsoort in Enum.GetValues(typeof(ToestelSoort)))
            {
                cmbSoort.Items.Add(toestelsoort);
            }
        }
        private void ClearControls()
        {
            txtMerk.Text = "";
            txtSerie.Text = "";
            cmbSoort.SelectedIndex = -1;
            txtVerkoopprijs.Text = "";
            txtVoorraad.Text = "";
            txtVermogen.Text = "";
            txtSpanning.Text = "";
            lblStroomsterkte.Content = "";

        }
        private void VulControls(Toestel toestel)
        {
            txtMerk.Text = toestel.Merk;
            txtSerie.Text = toestel.Serie;
            cmbSoort.SelectedItem = toestel.Soort;
            txtVerkoopprijs.Text = toestel.Verkoopprijs.ToString("#,##0.00");
            txtVoorraad.Text = toestel.Voorraad.ToString();
            txtVermogen.Text = toestel.Watt.ToString();
            txtSpanning.Text = toestel.Voltage.ToString();
            lblStroomsterkte.Content = toestel.Ampere.ToString("0.00");
        }
        private void DoeStatistieken()
        {
            lblTotaalAantal.Content = toestelService.TotaalAantalToestellen;
            lblTotaleWaardeVoorraad.Content = toestelService.TotaleStockWaarde.ToString("#,##0.00");
        }

        private void lstToestellen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
            if(lstToestellen.SelectedItem!=null)
            {
                Toestel toestel = (Toestel)lstToestellen.SelectedItem;
                VulControls(toestel);
            }

        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VulLstToestellen();
        }

        private void btnNieuw_Click(object sender, RoutedEventArgs e)
        {
            isNew = true;
            ClearControls();
            RechtsInschakelen();
            cmbSoort.SelectedIndex = 0;
            txtMerk.Focus();
        }

        private void btnWijzig_Click(object sender, RoutedEventArgs e)
        {
            if(lstToestellen.SelectedItem != null)
            {
                isNew = false;
                lblStroomsterkte.Content = "";
                RechtsInschakelen();
                txtMerk.Focus();
            }
            else
            {
                MessageBox.Show("Selecteer eerst een toestel!", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnVerwijder_Click(object sender, RoutedEventArgs e)
        {
            if (lstToestellen.SelectedItem != null)
            {
                if(MessageBox.Show("Ben je zeker?","Wissen",MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Toestel toestel = (Toestel)lstToestellen.SelectedItem;
                    toestelService.ToestelVerwijderen(toestel);
                    ClearControls();
                    VulLstToestellen();
                    DoeStatistieken();
                }
            }
            else
            {
                MessageBox.Show("Selecteer eerst een toestel!", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            LinksInschakelen();
            lstToestellen_SelectionChanged(null, null);
        }

        private void btnBewaren_Click(object sender, RoutedEventArgs e)
        {
            string merk = txtMerk.Text.Trim();
            string serie = txtSerie.Text.Trim();
            ToestelSoort soort = (ToestelSoort)cmbSoort.SelectedItem;
            decimal verkoopprijs;
            decimal.TryParse(txtVerkoopprijs.Text, out verkoopprijs);
            int voorraad;
            int.TryParse(txtVoorraad.Text, out voorraad);
            int vermogen;
            int.TryParse(txtVermogen.Text, out vermogen);
            int spanning;
            int.TryParse(txtSpanning.Text, out spanning);

            Toestel toestel;
            if(isNew)
            {
                toestel = new Toestel(merk, serie, soort, verkoopprijs, voorraad, vermogen, spanning);
                toestelService.ToestelToevoegen(toestel);
            }
            else
            {
                toestel = (Toestel)lstToestellen.SelectedItem;
                toestel.Merk = merk;
                toestel.Serie = serie;
                toestel.Soort = soort;
                toestel.Verkoopprijs = verkoopprijs;
                toestel.Voorraad = voorraad;
                toestel.Watt = vermogen;
                toestel.Voltage = spanning;
            }
            VulLstToestellen();
            lstToestellen.SelectedItem = toestel;
            lstToestellen_SelectionChanged(null, null);
            LinksInschakelen();
            DoeStatistieken();
        }
    }
}
