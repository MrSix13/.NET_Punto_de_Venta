using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaPresentacion.Utilidades;

using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void iconButton3_Click(object sender, EventArgs e)
        {

        }

        //Mostrar un combobox con informacion desde una base de datos
        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            cboestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo"});
            cboestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cboestado.DisplayMember = "Texto";
            cboestado.ValueMember = "Valor";
            cboestado.SelectedIndex = 0;

            List<Rol> listaRol = new CN_Rol().Listar();

            //Crear filas en el datagridveiw 
            foreach (Rol item in listaRol)
            {
                cborol.Items.Add(new OpcionCombo() {Valor=item.IdRol, Texto= item.Descripcion });
            }
            cborol.DisplayMember = "Texto";
            cborol.ValueMember = "Valor";
            cborol.SelectedIndex = 0;

            //Crear columnas en el datagridview
            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                if(columna.Visible == true && columna.Name !="btnseleccionar")
                {
                    cbobusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";
            cbobusqueda.SelectedIndex = 0;

            //Mostrar todos los usuarios
            List<Usuario> listaUsuario = new CN_Usuario().Listar();

            foreach(Usuario item in listaUsuario)
            {
                dgvdata.Rows.Add(new object[] {"",item.IdUsaurio,item.Documento,item.NombreCompleto,item.Correo,item.Clave,
                    item.oRol.IdRol,
                    item.oRol.Descripcion,
                    item.Estado == true ? 1:0,
                    item.Estado == true ? "Activo":"No Activo"
                });
            }
        }

        private void txtid_Click(object sender, EventArgs e)
        {

        }

        //Evento Guardar
        private void btnguardar_Click(object sender, EventArgs e)
        {
            dgvdata.Rows.Add(new object[] { "", txtid.Text, txtdocumento.Text, txtnombrecompleto.Text, txtcorreo.Text, txtclave.Text,
                ((OpcionCombo)cborol.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cborol.SelectedItem).Texto.ToString(),
                ((OpcionCombo)cboestado.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cboestado.SelectedItem).Texto.ToString(),
            });
            Limpiar();

        }

        private void Limpiar()
        {
            txtid.Text = "0";
            txtdocumento.Text = "";
            txtnombrecompleto.Text = "";
            txtcorreo.Text = "";
            txtclave.Text = "";
            txtconfirmarclave.Text = "";
            cborol.SelectedIndex = 0;
            cboestado.SelectedIndex = 0;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

        }


        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
         
        }

        private void dgvdata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvdata.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {
                int indice = e.RowIndex;
                if(indice >= 0)
                {
                    txtid.Text = dgvdata.Rows[indice].Cells["Id"].Value.ToString();
                    txtdocumento.Text = dgvdata.Rows[indice].Cells["Documento"].Value.ToString();
                    txtnombrecompleto.Text = dgvdata.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                    txtcorreo.Text = dgvdata.Rows[indice].Cells["Correo"].Value.ToString();
                    txtclave.Text = dgvdata.Rows[indice].Cells["Clave"].Value.ToString();
                    txtconfirmarclave.Text = dgvdata.Rows[indice].Cells["Clave"].Value.ToString();

                    foreach(OpcionCombo oc in cborol.Items)
                    {
                        if(Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["IdRol"].Value))
                        {
                            int indice_combo = cborol.Items.IndexOf(oc);
                            cborol.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                }
            }
        }
    }
}
