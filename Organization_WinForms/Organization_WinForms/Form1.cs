using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Organization_WinForms
{
    public partial class Form1 : Form
    {
        DataSet dataSet;

        public Form1()
        {
            InitializeComponent();
            cmbRoles.DataSource = Enum.GetValues(typeof(Roles));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
 
            try
            {

                using (XmlWriter xmlFileToWrite = XmlWriter.Create(ServiceProvider.xmlPath, new XmlWriterSettings()))
                {
                    dataSet.WriteXml(xmlFileToWrite);
                    xmlFileToWrite.Close();
                }
                ServiceProvider.Instance.SendData();
             
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void Form_Load(object sender, EventArgs e)
        {

            BindDataGrid();

        }

        private void BindDataGrid()
        {
            dataSet = new DataSet();
            dataSet.ReadXml(ServiceProvider.xmlPath);

            
            dataGridPersons.DataSource = dataSet.Tables[0];

            var m = dataGridPersons.GetType().GetMethod("OnDataSourceChanged", BindingFlags.NonPublic | BindingFlags.Instance);
            m.Invoke(dataGridPersons, new object[] { EventArgs.Empty });

            dataGridPersons.Refresh();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ServiceProvider.Instance.ClientConnected.OpenSession();

        }

        public void UpdatePersonDetails(string id,string name,Roles role)
        {
            if (InvokeRequired)
            {

                this.Invoke(new MethodInvoker(delegate {
                    txtID.Text = id;
                    txtName.Text = name;
                    cmbRoles.SelectedItem = role;
                }));
                return;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Person updatedPerson = new Person(Int32.Parse(txtID.Text), txtName.Text, (Roles)cmbRoles.SelectedItem);
                ServiceProvider.Instance.UpdatePerson(updatedPerson);

                //refresh datagrid
                BindDataGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void dataGridPersons_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected) return;
            DataGridViewRow per = dataGridPersons.Rows[e.Row.Index];

            txtID.Text = e.Row.Cells[0].Value.ToString();
            txtName.Text = e.Row.Cells[1].Value.ToString();
            cmbRoles.SelectedItem = (Roles)Enum.Parse(typeof(Roles), e.Row.Cells[2].Value.ToString()); 
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Person addPerson = new Person(Int32.Parse(txtID.Text), txtName.Text, (Roles)cmbRoles.SelectedItem);
                ServiceProvider.Instance.AddRecord(addPerson);

                //refresh datagrid
                BindDataGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Person deletePerson = new Person(Int32.Parse(txtID.Text), txtName.Text, (Roles)cmbRoles.SelectedItem);
            ServiceProvider.Instance.DeleteRecord(deletePerson);

            //refresh datagrid
            BindDataGrid();
        }
    }
}
