using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RelojChecadorF1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss");
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }

        // Botón 1: Registrar Entrada
        private void button1_Click(object sender, EventArgs e)
        {
            string nombreUsuario = textBox1.Text;
            string horaEntrada = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Fecha y hora completa
            string status = "entrada"; // Establecer el status como 'entrada' cuando se presiona el botón de entrada

            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("Por favor ingrese el nombre de usuario.");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection("server=localhost;database=sistema_relojes;user=root;password=;"))
                {
                    conn.Open();

                    // Verificar si el usuario existe
                    string query = "SELECT id_usuario FROM usuario WHERE nombreUsuario = @nombreUsuario";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                    var usuarioId = cmd.ExecuteScalar();

                    if (usuarioId != null)
                    {
                        // Registrar la hora de entrada y el status
                        string insertQuery = "INSERT INTO relojchecardor (id_usuario, horaEntrada, status) VALUES (@usuarioId, @horaEntrada, @status)";
                        MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                        insertCmd.Parameters.AddWithValue("@usuarioId", usuarioId);
                        insertCmd.Parameters.AddWithValue("@horaEntrada", horaEntrada);
                        insertCmd.Parameters.AddWithValue("@status", status);

                        if (insertCmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Hora de entrada registrada exitosamente.");
                        }
                        else
                        {
                            MessageBox.Show("Error al registrar la entrada. Inténtalo de nuevo.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Usuario no encontrado.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nombreUsuario = textBox1.Text;
            string horaSalida = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Fecha y hora completa
            string status = "salida"; // Establecer el status como 'salida' cuando se presiona el botón de salida

            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("Por favor ingrese el nombre de usuario.");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection("server=localhost;database=sistema_relojes;user=root;password=;"))
                {
                    conn.Open();

                    // Verificar si el usuario existe
                    string query = "SELECT id_usuario FROM usuario WHERE nombreUsuario = @nombreUsuario";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                    var usuarioId = cmd.ExecuteScalar();

                    if (usuarioId != null)
                    {
                        // Verificar si el usuario tiene una entrada sin salida registrada
                        string checkQuery = "SELECT id_relojchecar FROM relojchecardor WHERE id_usuario = @usuarioId AND horaSalida IS NULL";
                        MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                        checkCmd.Parameters.AddWithValue("@usuarioId", usuarioId);
                        var result = checkCmd.ExecuteScalar();

                        if (result != null)
                        {
                            // Registrar la hora de salida y actualizar el status
                            string updateQuery = "UPDATE relojchecardor SET horaSalida = @horaSalida, status = @status WHERE id_usuario = @usuarioId AND horaSalida IS NULL";
                            MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                            updateCmd.Parameters.AddWithValue("@usuarioId", usuarioId);
                            updateCmd.Parameters.AddWithValue("@horaSalida", horaSalida);
                            updateCmd.Parameters.AddWithValue("@status", status);

                            if (updateCmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Hora de salida registrada exitosamente.");
                            }
                            else
                            {
                                MessageBox.Show("Error al registrar la salida. Inténtalo de nuevo.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se puede registrar la salida sin una entrada previa.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Usuario no encontrado.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();  
            Form3 form3 = new Form3();
            form3.ShowDialog(); 
            this.Close(); 
        }

    }
}