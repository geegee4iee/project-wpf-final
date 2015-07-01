using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

namespace Unilever.Views.ImportExcel
{
    /// <summary>
    /// Interaction logic for ImportView.xaml
    /// </summary>
    public partial class ImportView : Window
    {
        private OleDbConnection con = null;
        private OleDbCommand cmd = null;
        private OleDbDataReader dr = null;
        private OleDbDataAdapter adap = null;
        private DataTable dt = null;
        private DataSet ds = null;
        private string query;
        private string conStr;
        public ImportView()
        {
            InitializeComponent();
            this.query = "SELECT * FROM [Sheet1$]";


        }

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            // Prompt the user to enter a path/filename to save an example Excel file to
            openFileDialog1.Filter = "Excel 2007 files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            //  If the user hit Cancel, then abort!
            if (openFileDialog1.ShowDialog() != true)
                return;

            string TargetFilename = openFileDialog1.FileName;

            this.conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + TargetFilename + ";Extended Properties=\"Excel 12.0;IMEX=1;HDR=YES;TypeGuessRows=0;ImportMixedTypes=Text\"";
            con = new OleDbConnection(conStr);
            cmd = new OleDbCommand(query, con);
            adap = new OleDbDataAdapter(cmd);
            ds = new DataSet();
            adap.Fill(ds);
            dt = ds.Tables[0];
            this.grdList.ItemsSource = ds.Tables[0];
            var item = cbxDistributor.SelectedItem as DTO.Entity.Distributor;
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    Sale s = new Sale();
                    s.DistributorId = item.Id;
                    s.ProId = Convert.ToInt32(row.ItemArray[0].ToString());
                    s.Year = Convert.ToInt32(row.ItemArray[2].ToString());
                    s.Month = Convert.ToInt32(row.ItemArray[3].ToString());
                    s.Quantity = Convert.ToInt32(row.ItemArray[4].ToString());
                    s.Sales = Convert.ToDecimal(row.ItemArray[5].ToString());
                    using (UnileverEntities entity = new UnileverEntities())
                    {
                        entity.Sales.Add(s);
                        entity.SaveChanges();
                        
                    }
                }
                MessageBox.Show("Thêm dữ liệu vào hệ thống thành công.");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Thêm dữ liệu bị lỗi \n" + ex.Message + "\n Vui lòng kiểm tra lại file!!!");
            }
        }

        private void grdList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var temp = grdList.SelectedItem as DataRowView;
            var rrr = temp.Row.ItemArray;
        }

        private void lcImportView_Loaded(object sender, RoutedEventArgs e)
        {
            cbxDistributor.ItemsSource = new DistributorDAO().GetAll();
            if(new DistributorDAO().GetAll().Count != 0)
                cbxDistributor.SelectedIndex = 0;
            else
                cbxDistributor.SelectedIndex = -1;
        }

        private void btnSelectFileStock_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            // Prompt the user to enter a path/filename to save an example Excel file to
            openFileDialog1.Filter = "Excel 2007 files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            //  If the user hit Cancel, then abort!
            if (openFileDialog1.ShowDialog() != true)
                return;

            string TargetFilename = openFileDialog1.FileName;

            this.conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + TargetFilename + ";Extended Properties=\"Excel 12.0;IMEX=1;HDR=YES;TypeGuessRows=0;ImportMixedTypes=Text\"";
            con = new OleDbConnection(conStr);
            cmd = new OleDbCommand(query, con);
            adap = new OleDbDataAdapter(cmd);
            ds = new DataSet();
            adap.Fill(ds);
            dt = ds.Tables[0];
            this.grdList.ItemsSource = ds.Tables[0];
            var item = cbxDistributor.SelectedItem as DTO.Entity.Distributor;
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    Stock s = new Stock();
                    s.DistributorId = item.Id;
                    s.ProId = Convert.ToInt32(row.ItemArray[0].ToString());
                    s.Year = Convert.ToInt32(row.ItemArray[2].ToString());
                    s.Month = Convert.ToInt32(row.ItemArray[3].ToString());
                    s.Quantity = Convert.ToInt32(row.ItemArray[4].ToString());
                    using (UnileverEntities entity = new UnileverEntities())
                    {
                        entity.Stocks.Add(s);
                        entity.SaveChanges();

                    }
                }
                MessageBox.Show("Thêm dữ liệu vào hệ thống thành công.");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Thêm dữ liệu bị lỗi \n" + ex.Message + "\n Vui lòng kiểm tra lại file!!!");
            }

        }


    }
}
