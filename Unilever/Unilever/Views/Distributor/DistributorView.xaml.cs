using DevExpress.Xpf.Core;
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
using Unilever.DTO.Entity;

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
            btnDisUpdate.IsEnabled = false;
        }

        private void grdDistributor_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Unilever.DTO.Entity.Distributor item = null;
            try
            {
                item = grdDistributor.SelectedItem as Unilever.DTO.Entity.Distributor;
            }
            catch (System.Exception ex)
            {
                return;
            }

            if (item == null)
            {
                return;
            }

            btnDisAdd.IsEnabled = false;
            btnDisUpdate.IsEnabled = true;
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
            Unilever.DTO.Entity.Distributor item = null;
            try
            {
                item = grdDistributor.SelectedItem as Unilever.DTO.Entity.Distributor;
            }
            catch (System.Exception ex)
            {
                DXMessageBox.Show("Xoá không thành công!");
            }

            if (new DistributorDAO().Remove(item.Id))
            {
                DXMessageBox.Show("Xoá thành công!");
            }
            else
            {
                DXMessageBox.Show("Xoá không thành công!");
            }

            grdDistributor.ItemsSource = new DistributorDAO().GetAll();
        }

        private void btnDisAdd_Click(object sender, RoutedEventArgs e)
        {
            DTO.Entity.Distributor item = new DTO.Entity.Distributor();

            try
            {
                item.Name = txtDisName.Text;
                item.TimeLimit = int.Parse(txtDisTimeLimit.Text);
                item.Phone = txtDisPhone.Text;
                item.Email = txtDisEmail.Text;
                item.Address = txtDisAddress.Text;
            }
            catch (System.Exception ex)
            {
                DXMessageBox.Show("Thêm không thành công!");
                return;
            }

            if (new DistributorDAO().Add(item))
            {
                DXMessageBox.Show("Thêm thành công!");
            }
            else
            {
                DXMessageBox.Show("Thêm không thành công!");
            }

            grdDistributor.ItemsSource = new DistributorDAO().GetAll();
        }

        private void btnDisRefresh_Click(object sender, RoutedEventArgs e)
        {
            DTO.Entity.Distributor item = new DTO.Entity.Distributor();
            ChangeInput(item);
            btnDisAdd.IsEnabled = true;
            btnDisUpdate.IsEnabled = false;
        }

        private void btnDisUpdate_Click(object sender, RoutedEventArgs e)
        {
            DTO.Entity.Distributor item = new DTO.Entity.Distributor();

            try
            {
                item.Id = int.Parse(txtDisId.Text);
                item.Name = txtDisName.Text;
                item.TimeLimit = int.Parse(txtDisTimeLimit.Text);
                item.Phone = txtDisPhone.Text;
                item.Email = txtDisEmail.Text;
                item.Address = txtDisAddress.Text;
            }
            catch (System.Exception ex)
            {
                DXMessageBox.Show("Cập nhật không thành công!");
                return;
            }

            if (new DistributorDAO().UpdateInfo(item))
            {
                DXMessageBox.Show("Cập nhật thành công!");
            }
            else
            {
                DXMessageBox.Show("Cập nhật không thành công!");
            }

            grdDistributor.ItemsSource = new DistributorDAO().GetAll();
        }

        private void removeDistributor_Loaded(object sender, RoutedEventArgs e)
        {
            DTO.Entity.Distributor cat = null;
            try
            {
                cat = grdDistributor.SelectedItem as DTO.Entity.Distributor;
            }
            catch (System.Exception ex)
            {
                return;
            }

            if (cat == null)
            {
                removeDistributor.IsEnabled = false;
            }
            else
            {
                removeDistributor.IsEnabled = true;
            }
        }
    }
}
