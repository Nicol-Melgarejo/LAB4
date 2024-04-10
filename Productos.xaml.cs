using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;

namespace Lab04
{
    /// <summary>
    /// Lógica de interacción para Productos.xaml
    /// </summary>
    public partial class Productos : Window
    {
        public Productos()
        {
            InitializeComponent();

            string connectionString = "Data Source=NICOL\\MSSQLSERVER01; Initial Catalog=NeptunoBD; User Id=User01; " +
               "Password=123456";

            List<Producto> productos = new List<Producto>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("USP_ListarProductos", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idproducto = reader.GetInt32(reader.GetOrdinal("idproducto"));
                                string nombreProducto = reader.GetString(reader.GetOrdinal("nombreProducto"));
                                int idProveedor = reader.GetInt32(reader.GetOrdinal("idProveedor"));
                                int idCategoria = reader.GetInt32(reader.GetOrdinal("idCategoria"));

                                productos.Add(new Producto { idproducto = idproducto, nombreProducto = nombreProducto, idProveedor = idProveedor, idCategoria = idCategoria });
                            }
                        }
                    }
                }

                dataGridProductos.ItemsSource = productos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public class Producto
        {
            public int idproducto { get; set; }
            public string nombreProducto { get; set; }
            public int idProveedor { get; set; }
            public int idCategoria { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window ventanaActual = Window.GetWindow(this);
            ventanaActual.Close();
        }
    }
}