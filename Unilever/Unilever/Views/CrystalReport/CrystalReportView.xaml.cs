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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Unilever.DTO.Entity;

namespace Unilever.Views.CrystalReport
{
    /// <summary>
    /// Interaction logic for CrystalReportView.xaml
    /// </summary>
    public partial class CrystalReportView : Window
    {
        public CrystalReportView()
        {
            InitializeComponent();
           

           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnHello_Click(object sender, RoutedEventArgs e)
        {
            ReportDocument report = new ReportDocument();
            report.Load(@"D:\MyGitHub\project-wpf-final\Unilever\Unilever\Views\CrystalReport\SaleReport.rpt");

            using (UnileverEntities ent = new UnileverEntities())
            {
                report.SetDataSource(ent.Sales.ToList());
            }

            crystalReportsViewer1.ViewerCore.ReportSource = report;
        }
    }
}
