using System;
using System.Windows;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Collections.Specialized;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Configuration;

namespace Seller
{
    /// <summary>
    /// Interaction logic for ChooseDatabase.xaml
    /// </summary>
    public partial class ChooseDatabase : Window
    {
        private static string databaseDataFile;
        private static string databaseLogFile;
        public ChooseDatabase()
        {
            InitializeComponent();
        }

        private void btnAccess_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    System.Configuration.Configuration config =
                                    ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    // Because it's an EF connection string it's not a normal connection string
                    // so we pull it into the EntityConnectionStringBuilder instead
                    EntityConnectionStringBuilder efb =
                        new EntityConnectionStringBuilder(
                            config.ConnectionStrings.ConnectionStrings["SellerEntities"]
                                .ConnectionString);

                    // Then we extract the actual underlying provider connection string
                    SqlConnectionStringBuilder sqb =
                        new SqlConnectionStringBuilder(efb.ProviderConnectionString);

                    // Now we can set the datasource
                    sqb.DataSource = txtServerName.Text;
                    sqb.InitialCatalog = txtDatabaseName.Text;
                    // Pop it back into the EntityConnectionStringBuilder 
                    efb.ProviderConnectionString = sqb.ConnectionString;

                    // And update...
                    config.ConnectionStrings.ConnectionStrings["SellerEntities"]
                        .ConnectionString = efb.ConnectionString;

                    config.Save(ConfigurationSaveMode.Modified, true);
                    ConfigurationManager.RefreshSection("connectionStrings");
                }
                catch
                {
                    MessageBox.Show("Chỉnh sửa không thành công", "Seller Manager");
                    return;

                }
                //check database exists
                String DatabaseName = txtDatabaseName.Text;

                Server SqlServer = new Server(txtServerName.Text);
                ServerConnection SqlServerConnection = SqlServer.ConnectionContext;

                //SqlServerConnection.LoginSecure = true;
                //SqlServerConnection.DatabaseName = "master";

                if (SqlServer.Databases[DatabaseName] == null)
                {
                    Database NewDatabase = new Database(SqlServer, DatabaseName);

                    FileGroup DatabaseFileGroup = new FileGroup(NewDatabase, "PRIMARY");
                    NewDatabase.FileGroups.Add(DatabaseFileGroup);

                    DataFile DatabaseDataFile = new DataFile(DatabaseFileGroup, DatabaseName);
                    DatabaseFileGroup.Files.Add(DatabaseDataFile);

                    DatabaseDataFile.FileName = databaseDataFile;
                    //DatabaseDataFile.FileName = System.AppDomain.CurrentDomain.BaseDirectory + DatabaseName + ".mdf";

                    LogFile DatabaseLogFile = new LogFile(NewDatabase, DatabaseName + "_log");
                    NewDatabase.LogFiles.Add(DatabaseLogFile);

                    DatabaseLogFile.FileName = databaseLogFile;
                    //DatabaseLogFile.FileName = System.AppDomain.CurrentDomain.BaseDirectory + DatabaseName + "_log.ldf";

                    StringCollection DatabaseFilesCollection = new StringCollection();

                    DatabaseFilesCollection.Add(DatabaseDataFile.FileName);
                    DatabaseFilesCollection.Add(DatabaseLogFile.FileName);

                    SqlServer.AttachDatabase(DatabaseName, DatabaseFilesCollection);
                }
            }
            catch
            {
                MessageBox.Show("Co loi khi thiet lap co so du lieu", "Seller Manager");
            }
        }

        private void btnDataFileName_DefaultButtonClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            // Prompt the user to enter a path/filename to save an example Excel file to
            openFileDialog1.Filter = "Database files (*.mdf)|*.mdf|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            //  If the user hit Cancel, then abort!
            if (openFileDialog1.ShowDialog() != true)
                return;

            btnDataFileName.Text = openFileDialog1.FileName;
            databaseDataFile = openFileDialog1.FileName;
        }

        private void btnDataLogFileName_DefaultButtonClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            // Prompt the user to enter a path/filename to save an example Excel file to
            openFileDialog1.Filter = "Database log files (*.ldf)|*.ldf|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            //  If the user hit Cancel, then abort!
            if (openFileDialog1.ShowDialog() != true)
                return;

            btnDataLogFileName.Text = openFileDialog1.FileName;
            databaseLogFile = openFileDialog1.FileName;
        }

    }
}
