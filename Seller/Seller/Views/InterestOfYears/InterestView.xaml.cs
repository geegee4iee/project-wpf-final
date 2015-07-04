using DevExpress.Xpf.Core;
using Seller.DTO.Entity;
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
using Unilever.DAO;

namespace Seller.Views.InterestOfYears
{
    /// <summary>
    /// Interaction logic for InterestView.xaml
    /// </summary>
    public partial class InterestView : Window
    {
        public InterestView()
        {
            InitializeComponent();

            btnUpdate.IsEnabled = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DTO.Entity.InterestOfYear item = new DTO.Entity.InterestOfYear();
            try
            {
                item.Id = int.Parse(daeYear.SelectedText);

                //Cho phép lãi suất bằng 0
                if (string.IsNullOrEmpty(txtInterest.Text))
                {
                    item.Interest = 0;
                }
                else
                {
                    item.Interest = decimal.Parse(txtInterest.Text);

                    if (item.Interest < 0)
                    {
                        DXMessageBox.Show("Vui lòng nhập lãi suất dương!");
                        return;
                    }
                    else
                    {

                    }
                }

                if (new InterestOfYearDAO().Add(item))
                {
                    DXMessageBox.Show("Thêm lãi suất thành công!");
                }
                else
                {
                    DXMessageBox.Show("Thêm lãi suất không thành công!");
                }
                grdIoy.ItemsSource = new InterestOfYearDAO().GetAll();
            }
            catch (System.Exception ex)
            {
                DXMessageBox.Show("Vui lòng nhập dữ liệu!");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            DTO.Entity.InterestOfYear item = new DTO.Entity.InterestOfYear();
            try
            {
                item.Id = int.Parse(daeYear.SelectedText);

                //Cho phép lãi suất bằng 0
                if(string.IsNullOrEmpty(txtInterest.Text))
                {
                    item.Interest = 0;
                }
                else
                {
                    item.Interest = decimal.Parse(txtInterest.Text);

                    if (item.Interest < 0)
                    {
                        DXMessageBox.Show("Vui lòng nhập lãi suất dương!");
                        return;
                    }
                    else
                    {

                    }
                }

                if (new InterestOfYearDAO().Update(item))
                {
                    DXMessageBox.Show("Cập nhật lãi suất thành công!");
                }
                else
                {
                    DXMessageBox.Show("Cập nhật lãi suất không thành công!");
                }
                grdIoy.ItemsSource = new InterestOfYearDAO().GetAll();
            }
            catch (System.Exception ex)
            {
                DXMessageBox.Show("Vui lòng nhập dữ liệu!");
            }
        }

        private void ChangeInput(DTO.Entity.InterestOfYear item)
        {
            //txtYear.Text = item.Id.ToString();
            //cbxYear.SelectedItem = item.Id;
            daeYear.SelectedText = item.Id.ToString();
            txtInterest.Text = item.Interest.ToString();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            DTO.Entity.InterestOfYear item = new DTO.Entity.InterestOfYear();
            ChangeInput(item);

            daeYear.IsEnabled = true;
            daeYear.SelectedText = "2015";

            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
        }

        private void grdIoy_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = grdIoy.SelectedItem as DTO.Entity.InterestOfYear;

            btnAdd.IsEnabled = false;
            btnUpdate.IsEnabled = true;

            //txtYear.IsReadOnly = true;
            daeYear.IsEnabled = false;

            ChangeInput(item);
        }

        private void grdIoy_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            contextInterestOfYear.ShowEditor();
        }

        private void removeInterestOfYear_Click(object sender, RoutedEventArgs e)
        {
            var item = grdIoy.SelectedItem as DTO.Entity.InterestOfYear;

            if (new InterestOfYearDAO().Remove(item.Id))
            {
                DXMessageBox.Show("Xoá lãi suất thành công!");
            }
            else
            {
                DXMessageBox.Show("Xoá lãi suất không thành công!");
            }
            grdIoy.ItemsSource = new InterestOfYearDAO().GetAll();
        }

        private void removeInterestOfYear_Loaded(object sender, RoutedEventArgs e)
        {
            InterestOfYear interest = null;
            try
            {
                interest = grdIoy.SelectedItem as InterestOfYear;
            }
            catch (System.Exception ex)
            {
                return;
            }

            if (interest == null)
            {
                removeInterestOfYear.IsEnabled = false;
            }
            else
            {
                removeInterestOfYear.IsEnabled = true;
            }
        }
    }
}
