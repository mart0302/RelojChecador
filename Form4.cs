using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using RelojChecadorF;

namespace RelojChecadorF1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        // Evento al cargar el formulario
        private void Form4_Load(object sender, EventArgs e)
        {

        }

        // Evento para el botón "Registrar"
        private void button2_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;
            string apellido = textBox2.Text;
            string correo = textBox3.Text;
            string rol = textBox4.Text;
            string nombreUsuario = textBox5.Text;

            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) ||
                string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(rol) ||
                string.IsNullOrWhiteSpace(nombreUsuario))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            try
            {
                // Obtener la conexión desde el método GetConnection
                MySqlConnection conn = new ConexionBD().GetConnection();
                conn.Open();  // Abrir la conexión

                // Consulta SQL para insertar los datos del nuevo usuario
                string query = "INSERT INTO usuario (nombre, apellido, correo, rol, nombreUsuario) " +
                               "VALUES (@nombre, @apellido, @correo, @rol, @nombreUsuario)";

                // Crear el comando y asignar los parámetros
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@apellido", apellido);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@rol", rol);
                    cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);

                    // Ejecutar la consulta
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Usuario registrado correctamente.");
                        this.Hide();
                        Form2 form2 = new Form2();
                        form2.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al registrar el usuario.");
                    }
                }

                conn.Close();  // Cerrar la conexión
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión a la base de datos: " + ex.Message);
            }
        }

        // Evento para el botón "Cancelar" (lleva al Form3)
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.ShowDialog();
            this.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
