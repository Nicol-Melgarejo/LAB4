using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Lab04
{
    /// <summary>
    /// Lógica de interacción para Categorias.xaml
    /// </summary>
    public partial class Categorias : Window
    {
        public Categorias()
        {
            InitializeComponent();

            string connectionString = "Data Source=NICOL\\MSSQLSERVER01; Initial Catalog=NeptunoBD; User Id=User01; " +
                "Password=123456";
            List<Categoria> categorias = new List<Categoria>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("USP_ListarCategorias", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idcategoria = reader.GetInt32(reader.GetOrdinal("idcategoria"));
                                string nombrecategoria = reader.GetString(reader.GetOrdinal("nombrecategoria"));
                                string descripcion = reader.GetString(reader.GetOrdinal("descripcion"));

                                categorias.Add(new Categoria { idcategoria = idcategoria, nombrecategoria = nombrecategoria, descripcion = descripcion });
                            }
                        }
                    }
                }

                dataGridCategorias.ItemsSource = categorias;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public class Categoria
        {
            public int idcategoria { get; set; }
            public string nombrecategoria { get; set; }
            public string descripcion { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window ventanaActual = Window.GetWindow(this);
            ventanaActual.Close();
        }
    }
}