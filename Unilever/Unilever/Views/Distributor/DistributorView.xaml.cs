using System;
using System.Collections.Generic;
using System.Data;
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
using Unilever.DAO;

namespace Unilever.Views.Distributor
{
    /// <summary>
    /// Interaction logic for DistributorView.xaml
    /// </summary>
    public partial class DistributorView : Window
    {
        public DistributorView()
        {
            InitializeComponent();
        }

        private void grdDistributor_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = grdDistributor.SelectedItem as Unilever.DTO.Entity.Distributor;
            MessageBox.Show(item.Id.ToString());
        }

        private void grdDistributor_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            contextDistributor.ShowEditor();
        }

        private void removeDistributor_Click(object sender, RoutedEventArgs e)
        {
            var item = grdDistributor.SelectedItem as Unilever.DTO.Entity.Distributor;
            new DistributorDAO().Remove(item.Id);
            grdDistributor.ItemsSource = new DistributorDAO().GetAll();
        }
    }
}
