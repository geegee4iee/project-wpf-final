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
           this.conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=d:\\temp.xlsx;Extended Properties=\"Excel 12.0;IMEX=1;HDR=YES;TypeGuessRows=0;ImportMixedTypes=Text\"";
            
        }

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            con = new OleDbConnection(conStr);
            cmd = new OleDbCommand(query, con);
            adap = new OleDbDataAdapter(cmd);
            ds = new DataSet();
            adap.Fill(ds);
            this.grdList.ItemsSource = ds.Tables[0];
        }

        private void grdList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var temp = grdList.SelectedItem as DataRowView;
            var rrr = temp.Row.ItemArray;
        }


    }
}
