using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Exacta.DatabaseTableClasses;
using System.IO;

namespace Exacta
{
    public partial class Settings : Form
    {
        private int _selCount = 0;
        private DataTable _tbl_lines = new DataTable();
        private bool _suggest = false;
        private bool _autoSync = false;
        private bool _backupData = false;
        private static double CBstartp = 6;
        private static double CBendp = 19;
        public static int dgvRC = 0;

        public Settings()
        {
            InitializeComponent();
            dgvLines.EditingControlShowing += DataGridViewUpperCaseValues;
        }

        private void rbConfezione_CheckedChanged(object sender, EventArgs e)
        {
            grpConfezione.Enabled = true;
            grpStiro.Enabled = false;

            rbTurno1.Checked = false;
            rbTurno2.Checked = false;

            _selCount = 0;

            Store.Default.sector = ((RadioButton)sender).Text;

            Store.Default.id_sector = 1;
        }
        private void rbStiro_CheckedChanged(object sender, EventArgs e)
        {
            grpConfezione.Enabled = false;
            grpStiro.Enabled = true;

            rbConfezioneA.Checked = false;
            rbConfezioneB.Checked = false;

            _selCount = 0;

            Store.Default.sector = ((RadioButton)sender).Text;

            Store.Default.id_sector = 2;
        }
        private void Settings_Load(object sender, EventArgs e)
        {
            try
            {
                if (Store.Default.sector == rbConfezione.Text) rbConfezione.Checked = true;
                if (Store.Default.sector == rbStiro.Text) rbStiro.Checked = true;

                if (Store.Default.sub_sector == rbConfezioneA.Text) rbConfezioneA.Checked = true;
                if (Store.Default.sub_sector == rbConfezioneB.Text) rbConfezioneB.Checked = true;
                if (Store.Default.sub_sector == rbTurno1.Text) rbTurno1.Checked = true;
                if (Store.Default.sub_sector == rbTurno2.Text) rbTurno2.Checked = true;

                _suggest = Store.Default.suggestData;
                _autoSync = Store.Default.autoSync;
                _backupData = Store.Default.backupData;

                Additional.DesignMyGrid(dgvLines);
                Additional.DesignMyGrid(dgvMachines);
                Additional.DesignMyGrid(dgvCollection);
                Additional.DesignMyGrid(dgvPause);
                LoadLines();
                LoadMachines();
                dgvMachines.Columns["NrMatricola"].HeaderText = "Reg. Number";
                LoadCollections();
                txtLine.CharacterCasing = CharacterCasing.Upper;
                LoadPause();
                //Clients
                Additional.DesignMyGrid(dgvClients);
                LoadClients();
                dgvClients.Columns["Id"].Visible = false;
                dgvClients.Columns[6].HeaderText = "E-mail";
                dgvClients.Columns[2].HeaderText = "VAT Number";

                Additional.FillTheFilter(dgvClients, cbClient, 1);
                Additional.FillTheFilter(dgvCollection, cbCollection, 1);
                


            }

            catch (Exception err)
            {
                MessageBox.Show("There was an error! " + err);
            }
            //Exacta.Menu.Expiration();
            WorkingTS();

        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (_selCount == 0)
            {
                MessageBox.Show("No selection.");
                return;
            }

            //Store.Default.suggestData = cbSuggest.Checked;
            //Store.Default.autoSync = cbAutoSync.Checked;
            //Store.Default.backupData = cbBackupData.Checked;
            //Store.Default.daysToFinish = Convert.ToInt32(npdComplet.Value);

            Store.Default.Save();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (Store.Default.sector == string.Empty)
            {
                MessageBox.Show("Must define sector and department");
                return;
            }
            else
            {
                Close();
            }
        }
        private void dtp_DateArrival_ValueChanged(object sender, EventArgs e)
        {

        }
        private void cmb_lines_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region Sectors

        private void rbConfezioneA_CheckedChanged(object sender, EventArgs e)
        {
            Store.Default.sector = rbConfezione.Text;
            Store.Default.sub_sector = ((RadioButton)sender).Text;
            _selCount = 1;
        }
        private void rbConfezioneB_CheckedChanged(object sender, EventArgs e)
        {
            Store.Default.sector = rbConfezione.Text;
            Store.Default.sub_sector = ((RadioButton)sender).Text;
            _selCount = 1;
        }
        private void rbTurno1_CheckedChanged(object sender, EventArgs e)
        {
            Store.Default.sector = rbStiro.Text;
            Store.Default.sub_sector = ((RadioButton)sender).Text;
            _selCount = 1;
        }
        private void rbTurno2_CheckedChanged(object sender, EventArgs e)
        {
            Store.Default.sector = rbStiro.Text;
            Store.Default.sub_sector = ((RadioButton)sender).Text;

            _selCount = 1;
        }

        #endregion

        #region Lines

        private string _selectedLine;
        private bool _isNew = false;

        private bool FindLineIndex(int rowIdx)
        {
            var buf_list = new List<string>();

            for (var i = 0; i <= dgvLines.Rows.Count - 2; i++)
            {
                var row = dgvLines.Rows[i];

                if (i == rowIdx) continue;

                buf_list.Add(row.Cells[1].Value.ToString());
            }

            if (buf_list.Contains(dgvLines.Rows[rowIdx].Cells[0].Value.ToString()))
            {
                return true;
            }
            else
                return false;
        }
        private void LoadLines()
        {
            var linesQuery = from lin in Tables.TblLines
                             select lin;

            dgvLines.DataSource = linesQuery;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (FindLineIndex(e.RowIndex))
            {
                MessageBox.Show("Line exist");
                dgvLines.Rows[e.RowIndex].Cells[0].Value = "";
            }
        }
        private static void DataGridViewUpperCaseValues(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox box) box.CharacterCasing = CharacterCasing.Upper;
        }
        private void dgvLines_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLines.SelectedRows.Count <= 0) return;

            _selectedLine = dgvLines.SelectedRows[0].Cells[0].Value.ToString();
            txtLine.Text = dgvLines.SelectedRows[0].Cells[1].Value.ToString();
            txtLineManager.Text = dgvLines.SelectedRows[0].Cells[2].Value.ToString();
            //txtMembers.Text = dgvLines.SelectedRows[0].Cells[1].Value.ToString();
            //txtAbatimentoEff.Text = dgvLines.SelectedRows[0].Cells[2].Value.ToString();            
        }
        private void pbAdd_Click(object sender, EventArgs e)
        {
            dgvLines.Enabled = false;
            _isNew = true;

            txtLineManager.Text = String.Empty;
            txtLine.Text = "";
            txtLine.BackColor = Color.LightYellow;
            txtLine.Focus();
            txtLine.Text = "LINEA ";
            txtLine.SelectionStart = txtLine.Text.Length;
            txtLine.SelectionLength = 0;
        }

        private void pbSave_Click(object sender, EventArgs e)
        {
            if (_isNew)
            //insert new record
            {
                var sb = new StringBuilder();
                string lineFormat = "LINEA ";
                foreach (var ch in txtLine.Text.ToCharArray())
                {
                    sb.Append(ch);
                }
                if (sb.Length < 7 || sb.ToString().Substring(0, 6) != lineFormat || sb.ToString().Substring(6).ToCharArray().All(char.IsLetter))

                {
                    MessageBox.Show("Line format is not correct!\nPlease insert like <LINEA xx>", "Incorrect line format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    var lQuery = (from linex in Tables.TblLines
                                  where linex.LineName == txtLine.Text
                                  select linex).ToList();

                    if (lQuery.Count > 0)
                    {
                        MessageBox.Show("Line already exists");
                        return;
                    }

                    Lines lines = new Lines();
                    lines.LineName = txtLine.Text;
                    lines.LineManager = txtLineManager.Text;

                    Tables.TblLines.InsertOnSubmit(lines);

                    Exacta.Menu.db.SubmitChanges();

                    dgvLines.Enabled = true;
                    txtLine.BackColor = Color.White;

                    _isNew = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            //update record
            {
                if (dgvLines.Rows.Count == 0)
                    return;

                var dr = MessageBox.Show("Do you want to update " + _selectedLine + "?", "Line update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.No) return;

                var sb = new StringBuilder();
                string lineFormat = "LINEA ";
                foreach (var ch in txtLine.Text.ToCharArray())
                {
                    sb.Append(ch);
                }
                if (sb.Length < 7 || sb.ToString().Substring(0, 6) != lineFormat || sb.ToString().Substring(6).ToCharArray().All(char.IsLetter))
                {
                    MessageBox.Show("Line format is not correct!\nPlease insert like <LINEA xx>", "Incorrect line format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    var query = (from line in Tables.TblLines
                                 where line.LineId.ToString() == _selectedLine
                                 select line).Single();

                    Tables.TblLines.DeleteOnSubmit(query);

                    Lines lines = new Lines();
                    lines.LineName = txtLine.Text;
                    lines.LineManager = txtLineManager.Text;

                    Tables.TblLines.InsertOnSubmit(lines);
                    Exacta.Menu.db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            txtLine.Text = "";
            LoadLines();
            LoadMachines();
        }
        private void pbDiscard_Click(object sender, EventArgs e)
        {
            if (dgvLines.Rows.Count == 0)
                return;

            var dr = MessageBox.Show("Are you sure you want to delete " + txtLine.Text + "?", "Lines", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) return;
            var query = from line in Tables.TblLines
                        where line.LineId.ToString() == _selectedLine
                        select line;

            //remove selected line from machines
            foreach (Machines machine in Tables.TblMachines.Where(m => m.Line == txtLine.Text))
            {
                machine.Line = String.Empty;
            }

            var linex = query.ToList();
            foreach (var line in linex)
            {
                Tables.TblLines.DeleteOnSubmit(line);
            }

            Exacta.Menu.db.SubmitChanges();
            LoadLines();
            LoadMachines();

            if (dgvLines.Rows.Count == 0)
            {
                txtLine.Text = String.Empty;
                txtLineManager.Text = String.Empty;
            }
        }

        #endregion

        #region Machines

        private string _selectedMachine;

        private void LoadMachines()
        {
            var machinesQuery = from mac in Tables.TblMachines
                                orderby Convert.ToInt32(mac.Idm)
                                select mac;

            dgvMachines.DataSource = machinesQuery;
            this.dgvMachines.Columns["Id"].Visible = false;

            var query = from line in Tables.TblLines
                        select line;

            var lines = query.ToList();
            cmb_lines.Items.Clear();
            var i = 0;
            foreach (var l in lines)
            {
                cmb_lines.Items.Insert(i++, l.LineName);
            }

            if (cmb_lines.Items.Count != 0)
                cmb_lines.SelectedIndex = 0;
        }

        private void pb_MachineAdd_Click(object sender, EventArgs e)
        {
            dgvMachines.Enabled = false;
            _isNew = true;

            txt_Machine.Text = "";
            txt_nrMatricola.Text = "";
            txt_description_machine.Text = "";
            dtp_DateArrival.Value = DateTime.Now;
            //cmb_lines.SelectedIndex = 0;
            txt_Machine.BackColor = Color.LightYellow;
            txt_nrMatricola.BackColor = Color.LightYellow;
            txt_description_machine.BackColor = Color.LightYellow;
            dtp_ProductionDate.Value = DateTime.Now;
            txt_Machine.Focus();
        }
        private void pb_MachineSave_Click(object sender, EventArgs e)
        {
            if (_isNew)
            //insert new record
            {
                var sb = new StringBuilder();
                foreach (var ch in txt_Machine.Text.ToCharArray())
                {
                    sb.Append(ch);
                }
                if (sb.ToString().ToCharArray().All(char.IsLetter))

                {
                    MessageBox.Show("Machine Number format is not correct!\nPlease insert a number", "Incorrect format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    var lQuery = (from mac in Tables.TblMachines
                                  where mac.Idm == txt_Machine.Text
                                  select mac).ToList();

                    if (lQuery.Count > 0)
                    {
                        MessageBox.Show("Machine already exists");
                        return;
                    }

                    Machines machina = new Machines();
                    machina.Idm = txt_Machine.Text;
                    machina.Description = txt_description_machine.Text;
                    machina.NrMatricola = txt_nrMatricola.Text;
                    machina.DateArrival = dtp_DateArrival.Value;
                    machina.Line = cmb_lines.Text;
                    //machina.Operator = txtOperator.Text; here
                    machina.ProductionDate = dtp_ProductionDate.Value;

                    Tables.TblMachines.InsertOnSubmit(machina);

                    Exacta.Menu.db.SubmitChanges();

                    dgvMachines.Enabled = true;
                    txt_Machine.BackColor = Color.White;
                    txt_nrMatricola.BackColor = Color.White;
                    txt_description_machine.BackColor = Color.White;

                    _isNew = false;
                }
                catch (Exception ex)
                {
                    _isNew = false;
                    MessageBox.Show(ex.Message);
                }
            }
            else
            //update record
            {
                if (dgvMachines.Rows.Count == 0)
                    return;

                var dr = MessageBox.Show("Do you want to update " + _selectedMachine + "?", "Machine update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.No) return;
                try
                {
                    var lQuery = (from mac in Tables.TblMachines
                                  where mac.Idm == txt_Machine.Text
                                  select mac).SingleOrDefault();

                    if (lQuery != null)
                        Tables.TblMachines.DeleteOnSubmit(lQuery);

                    Machines machina = new Machines();
                    machina.Idm = txt_Machine.Text;
                    machina.Description = txt_description_machine.Text;
                    machina.NrMatricola = txt_nrMatricola.Text;
                    machina.DateArrival = dtp_DateArrival.Value;
                    machina.Line = cmb_lines.Text;
                    //machina.Operator = txtOperator.Text; here
                    machina.ProductionDate = dtp_ProductionDate.Value;

                    Tables.TblMachines.InsertOnSubmit(machina);
                    Exacta.Menu.db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            txt_Machine.Text = "";
            txt_nrMatricola.Text = "";
            txt_description_machine.Text = "";

            LoadMachines();
        }
        private void pb_MachineDelete_Click(object sender, EventArgs e)
        {
            if (dgvMachines.Rows.Count == 0)
                return;

            if (_isNew)
            {
                txt_Machine.Text = "";
                txt_nrMatricola.Text = "";
                txt_description_machine.Text = "";
                cmb_lines.SelectedIndex = 0;
                dtp_DateArrival.Value = DateTime.Now;
                txt_Machine.BackColor = Color.White;
                txt_nrMatricola.BackColor = Color.White;
                txt_description_machine.BackColor = Color.White;

                dgvMachines.Enabled = true;

                LoadMachines();
                _isNew = false;
                return;
            }

            var dr = MessageBox.Show("Are you sure you want to delete " + txt_Machine.Text + "?", "Machines", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) return;
            var query = from machine in Tables.TblMachines
                        where machine.Id.ToString() == _selectedMachine
                        select machine;

            var machines = query.ToList();
            foreach (var machine in machines)
            {
                Tables.TblMachines.DeleteOnSubmit(machine);
            }

            Exacta.Menu.db.SubmitChanges();

            LoadMachines();
            if (dgvMachines.Rows.Count == 0)
            {
                txt_Machine.Text = String.Empty;
                dtp_ProductionDate.Value = DateTime.Now;
                txt_description_machine.Text = String.Empty;
                txt_nrMatricola.Text = String.Empty;
                dtp_DateArrival.Value = DateTime.Now;
            }
        }
        private void dgvMachines_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMachines.SelectedRows.Count <= 0) return;

            _selectedMachine = dgvMachines.SelectedRows[0].Cells[0].Value.ToString();
            txt_Machine.Text = dgvMachines.SelectedRows[0].Cells[1].Value.ToString();
            txt_description_machine.Text = dgvMachines.SelectedRows[0].Cells[2].Value.ToString();
            txt_nrMatricola.Text = dgvMachines.SelectedRows[0].Cells[3].Value.ToString();
            dtp_DateArrival.Value = Convert.ToDateTime(dgvMachines.SelectedRows[0].Cells[4].Value);
            cmb_lines.SelectedIndex = cmb_lines.FindStringExact(dgvMachines.SelectedRows[0].Cells[5].Value.ToString());
        }
        private void tpSectors_Click(object sender, EventArgs e)
        {

        }
        private void pbNotepadSave_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Notepad.exe");
        }

        #endregion

        #region Collections

        private string selectedCollection;
        private bool newCollection = false;

        private void LoadCollections()

        {
            var collectionQuery = from collection in Tables.TblCollections
                                  select collection;
            dgvCollection.DataSource = collectionQuery;
        }
        public bool digit = true;
        private void GetDataFromControls(Collections collection)
        { digit = true;
            try
            {
                collection.Id = Convert.ToInt32(txtCollection.Text);
                collection.Code = txtCode.Text;
            }
            catch (Exception)
            {
                digit = false;
                MessageBox.Show("Collection ID has to be Number.", "Atention!", MessageBoxButtons.OK);
            }
        }
        private void pbAddCollection_Click(object sender, EventArgs e)
        {
            newCollection = true;
            txtCode.Text = String.Empty;
            txtCollection.Text = String.Empty;
            txtCollection.Focus();
        }
        private void dgvCollection_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCollection.SelectedRows.Count <= 0)
                return;
            selectedCollection = dgvCollection.SelectedRows[0].Cells[0].Value.ToString();
            txtCollection.Text = dgvCollection.SelectedRows[0].Cells[0].Value.ToString();
            txtCode.Text = dgvCollection.SelectedRows[0].Cells[1].Value.ToString();
        }
        private void pbSaveCollection_Click(object sender, EventArgs e)
        {
            if (newCollection)
            {
                if (String.IsNullOrEmpty(txtCollection.Text) || String.IsNullOrEmpty(txtCode.Text))
                {
                    newCollection = false;
                    return;
                }
                Collections collection = new Collections();
                GetDataFromControls(collection);
                if (digit)
                {
                    try
                    {
                        Tables.TblCollections.InsertOnSubmit(collection);
                        Exacta.Menu.db.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("The Collection ID aready exists\n Try with another ID.", "Atention!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    newCollection = false;
                }
                else return;
            }
            else
            {
                if (dgvCollection.Rows.Count == 0)
                    return;

                var dialog = MessageBox.Show("Do you want to update Collection?", "Collection update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.No)
                    return;

                Collections collection = Exacta.Menu.db.GetTable<Collections>().Single(cl => cl.Id.ToString() == selectedCollection);
                try
                {
                    Articole art = Exacta.Menu.db.GetTable<Articole>().SingleOrDefault(ar => ar.Collection == collection.Code);
                    if (art != null)
                        art.Collection = txtCode.Text;

                    GetDataFromControls(collection);
                    Exacta.Menu.db.SubmitChanges();
                }
                catch (Exception err)
                {
                    MessageBox.Show("There was an error. " + err);
                }
            }
            LoadCollections();
            dgvCollection.Enabled = true;
            Additional.FillTheFilter(dgvCollection, cbCollection, 1);
        }
        private void pbDeleteCollection_Click(object sender, EventArgs e)
        {
            if (dgvCollection.Rows.Count == 0)
                return;

            var dialog = MessageBox.Show("Are you sure you want to delete collection?", "Collection", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.No)
            {
                return;
            }
            var query = from collection in Tables.TblCollections
                        where collection.Id.ToString() == selectedCollection
                        select collection;

            foreach (var line in query)
            {
                Tables.TblCollections.DeleteOnSubmit(line);
            }
            try
            {
                Exacta.Menu.db.SubmitChanges();
            }
            catch (Exception err)
            {
                Console.WriteLine("There was an error. " + err);
            }

            LoadCollections();
            Additional.FillTheFilter(dgvCollection, cbCollection, 1);

            if (dgvCollection.Rows.Count == 0)
            {
                txtCollection.Text = String.Empty;
                txtCode.Text = String.Empty;
            }
        }
        private void cbCollection_SelectedValueChanged(object sender, EventArgs e)
        {
            Exacta.Menu.db = new DataContext(Exacta.Menu.connectionString);
            List<Collections> collection = null;
            if (cbCollection.Text == String.Empty)
                collection = Exacta.Menu.db.GetTable<Collections>().ToList();
            else
                collection = Exacta.Menu.db.GetTable<Collections>().Where(operation => operation.Code.Contains(cbCollection.Text)).ToList();
            dgvCollection.DataSource = collection;
        }

        #endregion

        #region Clients

        private string selectedClient = String.Empty;
        private bool newClient = false;

        private void LoadClients()
        {
            var query = from client in Tables.TblClients
                        select client;
            dgvClients.DataSource = query;
        }
        private void GetDataFromControls(Clientss c)
        {
            c.Name = txtName.Text;
            c.Address = txtAddress.Text;
            c.Country = txtCountry.Text;
            c.Mail = txtMail.Text;
            c.Telephone = txtTelephone.Text;
            c.Bank = txtBank.Text;
            c.VATNumber = txtVat.Text;
        }
        private void ClearClientsTxtBoxes()
        {
            txtName.Text = String.Empty;
            txtVat.Text = String.Empty;
            txtAddress.Text = String.Empty;
            txtCountry.Text = String.Empty;
            txtMail.Text = String.Empty;
            txtTelephone.Text = String.Empty;
            txtBank.Text = String.Empty;
        }

        private void dgvClients_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count <= 0)
                return;

            selectedClient = dgvClients.SelectedRows[0].Cells[0].Value.ToString();
            txtName.Text = dgvClients.SelectedRows[0].Cells[1].Value.ToString();
            txtVat.Text = dgvClients.SelectedRows[0].Cells[2].Value.ToString();
            txtCountry.Text = dgvClients.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = dgvClients.SelectedRows[0].Cells[4].Value.ToString();
            txtTelephone.Text = dgvClients.SelectedRows[0].Cells[5].Value.ToString();
            txtMail.Text = dgvClients.SelectedRows[0].Cells[6].Value.ToString();
            txtBank.Text = dgvClients.SelectedRows[0].Cells[7].Value.ToString();
        }
        private void cbClient_SelectedValueChanged(object sender, EventArgs e)
        {
            Exacta.Menu.db = new DataContext(Exacta.Menu.connectionString);
            List<Clientss> client = null;
            if (cbClient.Text == String.Empty)
                client = Exacta.Menu.db.GetTable<Clientss>().ToList();
            else
                client = Exacta.Menu.db.GetTable<Clientss>().Where(operation => operation.Name.Contains(cbClient.Text)).ToList();
            dgvClients.DataSource = client;
        }
        private void pbDeleteClient_Click(object sender, EventArgs e)
        {
            if (dgvClients.Rows.Count == 0)
                return;

            var dialog = MessageBox.Show("Are you sure you want to delete " + txtName.Text + "?", "Client", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.No)
                return;

            var query = from client in Tables.TblClients
                        where client.Id.ToString() == selectedClient
                        select client;

            foreach (var line in query)
            {
                Tables.TblClients.DeleteOnSubmit(line);
            }
            try
            {
                Exacta.Menu.db.SubmitChanges();
            }
            catch (Exception err)
            {
                Console.WriteLine("There was an error. " + err);
            }
            LoadClients();
            Additional.FillTheFilter(dgvClients, cbClient, 1);

            if (dgvClients.Rows.Count == 0)
                ClearClientsTxtBoxes();
        }
        private void pbAddClient_Click(object sender, EventArgs e)
        {
            newClient = true;

            ClearClientsTxtBoxes();
            txtName.Focus();

            dgvClients.Enabled = false;
        }
        private void pbSaveClient_Click(object sender, EventArgs e)
        {
            if (newClient)
            {

                Clientss client = new Clientss();
                GetDataFromControls(client);
                if (txtName.Text == "")
                {
                    MessageBox.Show("Complete at least Client Name!", "Atention!", MessageBoxButtons.OK);
                }
                else
                {

                    try
                    {
                        Tables.TblClients.InsertOnSubmit(client);
                        Exacta.Menu.db.SubmitChanges();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("There was an error. " + err);
                    }
                    newClient = false;
                }
            }
            else
            {
                if (dgvClients.Rows.Count == 0)
                    return;

                var dialog = MessageBox.Show("Do you want to update Client?", "Client update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.No)
                    return;

                Clientss client = Exacta.Menu.db.GetTable<Clientss>().SingleOrDefault(cl => cl.Id.ToString() == selectedClient);
                try
                {
                    Articole art = Exacta.Menu.db.GetTable<Articole>().SingleOrDefault(ar => ar.Client == client.Name);
                    if (art != null)
                        art.Client = txtName.Text;

                    GetDataFromControls(client);
                    Exacta.Menu.db.SubmitChanges();
                }
                catch (Exception err)
                {
                    MessageBox.Show("There was an error. " + err);
                }
            }

            LoadClients();
            dgvClients.Enabled = true;
            Additional.FillTheFilter(dgvClients, cbClient, 1);
        }

        #endregion

        private void dgvMachines_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
        }
        #region Pause

        public void TakeWorkingT()
        {
                CBstartp = Convert.ToDouble(CbSP.SelectedItem.ToString());
                CBendp = Convert.ToDouble(CbEP.SelectedItem.ToString());
        }
        public static void WorkingTS()
        { 
            Settings f1 = new Settings();
            f1.PutWorkingT();
        }
        public void PutWorkingT()
        {
            CbSP.SelectedItem = CBstartp.ToString();
            CbEP.SelectedItem = CBendp.ToString();
        }

        private string SelectedPause;
        
        private void LoadPause()
        {
            readf();
            var pauseQuery = from pause in Tables.TblPauses
                             select pause;
            dgvPause.DataSource = pauseQuery;
            dgvRC = dgvPause.Rows.Count;
        }

        private void GetPauseFDP(Pause pause)
        {
            try
            {
                pause.StartP = dpStartP.Value.ToShortTimeString();
                pause.EndP = dpEndP.Value.ToShortTimeString();
                var timeOnPause = dpEndP.Value.Date - dpStartP.Value.Date;
                pause.Descriere = richTextBox1.Text.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Something is wrong!", "Error!", MessageBoxButtons.OK);
            }
        }
        private void dgvPause_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPause.SelectedRows.Count <= 0)
                return;
            SelectedPause = dgvPause.SelectedRows[0].Cells[0].Value.ToString();
            dpStartP.Value = Convert.ToDateTime(dgvPause.SelectedRows[0].Cells[1].Value.ToString());
            dpEndP.Value = Convert.ToDateTime(dgvPause.SelectedRows[0].Cells[2].Value.ToString());
            richTextBox1.Text = dgvPause.SelectedRows[0].Cells[3].Value.ToString();

        }
        private bool NewPause = false;

        private void AddPause_Click(object sender, EventArgs e)
        {
            NewPause = true;
            dpStartP.Focus();
            richTextBox1.Text = string.Empty;
        }

        private void SavePause_Click(object sender, EventArgs e)
        {
            Exacta.Menu.db = new DataContext(Exacta.Menu.connectionString);
            if (NewPause)
            {
                Pause pause = new Pause();
                GetPauseFDP(pause);
                try
                {
                    Tables.TblPauses.InsertOnSubmit(pause);
                    Exacta.Menu.db.SubmitChanges();
                    MessageBox.Show("New Pause has been added!");
                    LoadPause();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                NewPause = false;
            }
            else
            {
                if (dgvPause.Rows.Count == 0)
                    return;
                var dialog = MessageBox.Show("Do you want to update Pause?", "Pause Update!", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.No)
                    return;
                Pause pause = Exacta.Menu.db.GetTable<Pause>().Single(cl => cl.Id.ToString() == SelectedPause);
                try
                {
                    GetPauseFDP(pause);
                    Exacta.Menu.db.SubmitChanges();
                    MessageBox.Show("Pause " + richTextBox1.Text.ToString() + " has been updated!");
                    LoadPause();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            LoadPause();
        }
        private void btnDeletePause_Click(object sender, EventArgs e)
        {
            if (dgvPause.Rows.Count == 0)
                return;

            var dialog = MessageBox.Show("Are you sure you want to delete Pause?", "Atention!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.No)
            {
                return;
            }
            var query = from pause in Tables.TblPauses
                        where pause.Id.ToString() == SelectedPause
                        select pause;

            foreach (var line in query)
            {
                Tables.TblPauses.DeleteOnSubmit(line);
            }
            try
            {
                Exacta.Menu.db.SubmitChanges();
                MessageBox.Show("Pause " + richTextBox1.Text.ToString() + " has beed deleted");
                LoadPause();
            }
            catch (Exception err)
            {
                Console.WriteLine("There was an error. " + err);
            }
             LoadPause();
            if (dgvPause.Rows.Count == 0)
                richTextBox1.Text = string.Empty;
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {   
            readf();
            CBstartp = Convert.ToDouble(CbSP.SelectedItem);
            CBendp = Convert.ToDouble(CbEP.SelectedItem);
            writef();
            MessageBox.Show("Programul de lucru a fost salvat !");
        }
        public void writef()
        {
            try
            {
                var _dir = AppDomain.CurrentDomain.BaseDirectory;
                //var _dir=
                string path = (_dir + "\\file.txt");
;                //StreamWriter sw = new StreamWriter(_dir + "\\file.txt");
                //fileStream file = new FileStream(_dir + "\\file.txt", FileMode.Create); 
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(CBstartp.ToString());
                    sw.WriteLine(CBendp.ToString());
                    sw.Close();
                }
            }
            catch (Exception e)
            { 
                MessageBox.Show(e.ToString());
            }
        }
            //byte[] start = new UTF8Encoding(true).GetBytes(txtCBstartp);
            //byte[] end = new UTF8Encoding(true).GetBytes(txtCBendp);
            // Add some information to the file.
            // file.Write(start, 0, start.Length);
            // file.Write(end, 0, end.Length);
            public void readf()
            {
                string start = "";
                string end = "";
                try
                {
                    var _dir = AppDomain.CurrentDomain.BaseDirectory;
                    //FileStream file = new FileStream(_dir + "\\file.txt", FileMode.OpenOrCreate);
                    StreamReader sr = new StreamReader(_dir + "\\file.txt");
                    start = sr.ReadLine();
                    end = sr.ReadLine();
                    sr.Close();
                CBstartp = Convert.ToDouble(start.ToString());
                CBendp = Convert.ToDouble(end.ToString());
            }
                catch (Exception e)
                {
                CBstartp = 6;
                CBendp = 15;
                    //MessageBox.Show(e.ToString());
                }
            }
        #endregion

        private void tcCollection_SelectedIndexChanged(object sender, EventArgs e)
        {
            readf();
            if(tcCollection.SelectedIndex==5)
            {
                CbSP.SelectedItem = CBstartp.ToString();
                CbEP.SelectedItem = CBendp.ToString();
            }
        }
    }
    }

    

