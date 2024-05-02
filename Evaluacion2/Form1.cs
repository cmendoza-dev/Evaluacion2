using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Evaluacion2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ordenesEmpleados();
            llenarComboEmpleados();
        }

        void ordenesEmpleados()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.cnx))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("OrdenesEmpleadoTodos", cn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(dr.GetValue(0),
                        dr.GetValue(1),
                        dr.GetValue(2),
                        dr.GetValue(3),
                        dr.GetValue(4),
                        dr.GetValue(5),
                        dr.GetValue(6),
                        dr.GetValue(7)
                        );
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        void llenarComboEmpleados()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.cnx))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT IdEmpleado, NomEmpleado FROM EMPLEADO", cn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        // Agregar cada empleado al ComboBox
                        cbxEmpleados.Items.Add(new KeyValuePair<int, string>(dr.GetInt32(0), dr.GetString(1)));
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void ordenesEmpleados(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.cnx))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("ObtenerPedidosConFiltros", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                    cmd.Parameters.AddWithValue("@FechaDesde", fechaDesde);
                    cmd.Parameters.AddWithValue("@FechaHasta", fechaHasta);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(dr.GetValue(0),
                        dr.GetValue(1),
                        dr.GetValue(2),
                        dr.GetValue(3),
                        dr.GetValue(4),
                        dr.GetValue(5),
                        dr.GetValue(6),
                        dr.GetValue(7)
                        );
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            ordenesEmpleados();
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            int idEmpleado = ((KeyValuePair<int, string>)cbxEmpleados.SelectedItem).Key;
            DateTime fechaDesde = dtpDesde.Value;
            DateTime fechaHasta = dtpHasta.Value;

            ordenesEmpleados(idEmpleado, fechaDesde, fechaHasta);

        }

        private void cbxEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
