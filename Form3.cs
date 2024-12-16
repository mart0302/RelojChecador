using MySql.Data.MySqlClient;
using RelojChecadorF;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RelojChecadorF1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        // Evento cuando se hace clic en el botón de inicio de sesión
        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;  // Obtener el correo del TextBox
            string pass = textBox2.Text;   // Obtener la contraseña del TextBox

            // Llamar al método para verificar las credenciales
            VerificarCredenciales(email, pass);
        }

        // Método para verificar las credenciales del usuario
        private void VerificarCredenciales(string email, string pass)
        {
            try
            {
                // Usar la clase ConexionBD para obtener la conexión
                MySqlConnection conn = new ConexionBD().GetConnection();
                conn.Open();

                // Consulta SQL para obtener los datos del usuario
                string query = "SELECT * FROM admin WHERE correo = @correo LIMIT 1";

                // Crear el comando SQL con parámetros para evitar SQL injection
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@correo", email);

                // Ejecutar la consulta y obtener el resultado
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    string storedPass = reader["contraseña"].ToString();

                    if (pass == storedPass)
                    {
                        MessageBox.Show("Inicio de sesión exitoso");

                        // Abrir Form2 y ocultar el formulario actual (sin cerrarlo)
                        
                            this.Hide();
                            Form2 form2 = new Form2();
                            form2.ShowDialog();
                            this.Close();
                            // Ocultar el formulario de inicio de sesión
                    }
                    else
                    {
                        MessageBox.Show("Contraseña incorrecta");
                    }
                }
                else
                {
                    MessageBox.Show("Correo no registrado");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar las credenciales: " + ex.Message);
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

            private void button3_Click(object sender, EventArgs e)
            {
                this.Hide();
                Form1 form1 = new Form1();
                form1.ShowDialog();
                this.Close();
            }
        }


    }


