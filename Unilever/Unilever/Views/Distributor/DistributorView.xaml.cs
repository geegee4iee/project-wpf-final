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
            btnDisAdd.Visibility = Visibility.Hidden;
            btnDisUpdate.Visibility = Visibility.Visible;
            ChangeInput(item);
        }

        private void ChangeInput(DTO.Entity.Distributor item)
        {
            txtDisId.Text = item.Id.ToString();
            txtDisName.Text = item.Name;
            txtDisTimeLimit.Text = item.TimeLimit.ToString();
            txtDisPhone.Text = item.Phone;
            txtDisEmail.Text = item.Email;
            txtDisAddress.Text = item.Address;
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

        private void btnDisAdd_Click(object sender, RoutedEventArgs e)
        {
            DTO.Entity.Distributor item = new DTO.Entity.Distributor();
            item.Name = txtDisName.Text;
            item.TimeLimit = int.Parse(txtDisTimeLimit.Text);
            item.Phone = txtDisPhone.Text;
            item.Email = txtDisEmail.Text;
            item.Address = txtDisAddress.Text;

            new DistributorDAO().Add(item);
            grdDistributor.ItemsSource = new DistributorDAO().GetAll();
        }

        private void btnDisRefresh_Click(object sender, RoutedEventArgs e)
        {
            DTO.Entity.Distributor item = new DTO.Entity.Distributor();
            ChangeInput(item);
            btnDisAdd.Visibility = Visibility.Visible;
            btnDisUpdate.Visibility = Visibility.Hidden;
        }

        private void btnDisUpdate_Click(object sender, RoutedEventArgs e)
        {
            DTO.Entity.Distributor item = new DTO.Entity.Distributor();
            item.Id = int.Parse(txtDisId.Text);
            item.Name = txtDisName.Text;
            item.TimeLimit = int.Parse(txtDisTimeLimit.Text);
            item.Phone = txtDisPhone.Text;
            item.Email = txtDisEmail.Text;
            item.Address = txtDisAddress.Text;

            new DistributorDAO().UpdateInfo(item);
            grdDistributor.ItemsSource = new DistributorDAO().GetAll();
        }
    }
}
