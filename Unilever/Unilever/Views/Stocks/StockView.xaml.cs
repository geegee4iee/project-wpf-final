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
using Unilever.DTO.Entity;

namespace Unilever.Views.Stocks
{
    /// <summary>
    /// Interaction logic for StockView.xaml
    /// </summary>
    public partial class StockView : Window
    {
        public StockView()
        {
            InitializeComponent();
        }

        private void btnStockAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStockRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStockUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnViewStockByDist_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lgInfoStock_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void grdStock_Loaded(object sender, RoutedEventArgs e)
        {
            //grdStock.ItemsSource = new StockDAO().GetAll();
        }

       
    }
}
