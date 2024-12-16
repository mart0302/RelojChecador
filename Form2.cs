using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using RelojChecadorF;

namespace RelojChecadorF1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        // Método para cargar los datos del DataGridView1 (relojchecardor y usuario)
        private void CargarDatos()
        {
            try
            {
                // Establecer la conexión a la base de datos
                MySqlConnection conn = new ConexionBD().GetConnection();
                conn.Open();

                // Consulta SQL para obtener los datos de relojchecardor y usuario
                string query = @"
                    SELECT 
                        usuario.nombre AS nombre, 
                        usuario.apellido AS apellido, 
                        relojchecardor.horaEntrada, 
                        relojchecardor.horaSalida, 
                        relojchecardor.status
                    FROM relojchecardor
                    JOIN usuario ON relojchecardor.id_usuario = usuario.id_usuario
                    ORDER BY relojchecardor.horaEntrada DESC";

                // Crear un comando para ejecutar la consulta
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // Adaptador para llenar el DataTable
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Asignar el DataTable al DataGridView1
                dataGridView1.DataSource = dataTable;

                // Configurar las columnas (opcional)
                dataGridView1.AutoResizeColumns();
                dataGridView1.Columns["nombre"].HeaderText = "Nombre";
                dataGridView1.Columns["apellido"].HeaderText = "Apellido";
                dataGridView1.Columns["horaEntrada"].HeaderText = "Hora Entrada";
                dataGridView1.Columns["horaSalida"].HeaderText = "Hora Salida";
                dataGridView1.Columns["status"].HeaderText = "Estado";

                // Cerrar la conexión
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        // Método para cargar los datos de solo los usuarios en el DataGridView2
        private void CargarUsuarios()
        {
            try
            {
                // Establecer la conexión a la base de datos
                MySqlConnection conn = new ConexionBD().GetConnection();
                conn.Open();

                // Consulta SQL para obtener los datos de los usuarios, incluyendo correo y rol
                string query = @"
                    SELECT 
                        usuario.nombre, 
                        usuario.apellido, 
                        usuario.correo, 
                        usuario.rol 
                    FROM usuario 
                    ORDER BY usuario.nombre ASC";

                // Crear un comando para ejecutar la consulta
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // Adaptador para llenar el DataTable
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Asignar el DataTable al DataGridView2
                dataGridView2.DataSource = dataTable;

                // Configurar las columnas del DataGridView2
                dataGridView2.AutoResizeColumns();
                dataGridView2.Columns["nombre"].HeaderText = "Nombre";
                dataGridView2.Columns["apellido"].HeaderText = "Apellido";
                dataGridView2.Columns["correo"].HeaderText = "Correo";
                dataGridView2.Columns["rol"].HeaderText = "Rol";

                // Cerrar la conexión
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los usuarios: " + ex.Message);
            }
        }

        // Evento que se dispara cuando el formulario se carga
        private void Form2_Load(object sender, EventArgs e)
        {
            CargarDatos();   // Cargar los datos en el DataGridView1 (relojchecardor y usuario)
            CargarUsuarios(); // Cargar solo los usuarios en el DataGridView2
        }

        // Evento de clic en las celdas del DataGridView1 (si lo necesitas)
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Aquí puedes manejar lo que suceda cuando se hace clic en una celda en el DataGridView1
        }

        // Evento de clic en las celdas del DataGridView2 (si lo necesitas)
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Aquí puedes manejar lo que suceda cuando se hace clic en una celda en el DataGridView2
            if (e.RowIndex >= 0)
            {
                string nombre = dataGridView2.Rows[e.RowIndex].Cells["nombre"].Value.ToString();
                string apellido = dataGridView2.Rows[e.RowIndex].Cells["apellido"].Value.ToString();
                string correo = dataGridView2.Rows[e.RowIndex].Cells["correo"].Value.ToString();
                string rol = dataGridView2.Rows[e.RowIndex].Cells["rol"].Value.ToString();
                MessageBox.Show($"Usuario: {nombre} {apellido}\nCorreo: {correo}\nRol: {rol}");
            }
        }

   
        private void button1_Click(object sender, EventArgs e)
        {

            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 form4 = new Form4();
            form4.ShowDialog();
            this.Close();
        }
    }
}
