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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Drawing.Text;

namespace Exacta
    {
    public partial class Phase : Form
    {
        private string _selectedFase = String.Empty;
        private bool _isNewFase = false;

        public Phase()
        {
            InitializeComponent();
        }

        private void Phase_Load(object sender, EventArgs e)
        {
            //pnl_table_Fase.Width = this.Width - pnl_Edit_Fase.Width;
            //dgvFase.Width = pnl_table_Fase.Width - 10;
            Exacta.Menu.db = new DataContext(Exacta.Menu.connectionString);

            Additional.DesignMyGrid(dgvFase);

            LoadFase();
            dgvFase.Columns[1].HeaderText = "Operation Code";
            dgvFase.Columns[2].HeaderText = "Operation";
            dgvFase.Columns[0].Visible = false;

            Additional.FillTheFilter(dgvFase, cbPhase, 1);
            txt_Fase.Enabled = false;
            txt_FaseID.Enabled = false;
           // Exacta.Menu.Expiration();
        }       

        protected void LoadFase()
        {
            var faseQuery = from fase in Tables.TblOperatii
                            select fase;

            dgvFase.DataSource = faseQuery;
            btn_Save_Fase.Enabled = false;
            btn_Cancel_Fase.Enabled = false;
        }
        private void PrintFasePDF()
        {
            //article table
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            PdfPTable pdfTable = new PdfPTable(3);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;
            pdfTable.SpacingAfter = 20;

            //custom font            
            string slnPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            slnPath = slnPath + "\\Fonts\\arial.ttf";
            BaseFont arial = BaseFont.CreateFont(slnPath, BaseFont.CP1250, BaseFont.EMBEDDED);      
            
            iTextSharp.text.Font text = new iTextSharp.text.Font(arial, 10, iTextSharp.text.Font.NORMAL);

            foreach (DataGridViewColumn column in dgvFase.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(Color.Coral);
                pdfTable.AddCell(cell);
            }

            Operatii ph = Exacta.Menu.db.GetTable<Operatii>().SingleOrDefault(p => p.Id.ToString() == _selectedFase);
            pdfTable.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(Color.White);
            pdfTable.AddCell(new Phrase(ph.Id.ToString(), text));
            pdfTable.AddCell(new Phrase(ph.CodOperatie, text));
            pdfTable.AddCell(new Phrase(ph.Operatie, text));           

            //title
            iTextSharp.text.Font titleFont = new iTextSharp.text.Font(arial, 36, iTextSharp.text.Font.NORMAL);
            Paragraph title = new Paragraph("Phase Report", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            title.SpacingAfter = 20;
            //titleFont.SetColor(169, 169, 169);

            //date
            DateTime dateTime = DateTime.Now;
            iTextSharp.text.Font dateTimeFont = new iTextSharp.text.Font(arial, 16, iTextSharp.text.Font.NORMAL);
            Paragraph date = new Paragraph("Date: " + dateTime.ToString("dd-MM-yyyy"), dateTimeFont);
            date.Alignment = Element.ALIGN_RIGHT;
            date.SpacingAfter = 5;
            //dateFont.SetColor(169, 169, 169);

            //time            
            Paragraph time = new Paragraph("Time: " + dateTime.ToString("HH:mm:ss"), dateTimeFont);
            time.Alignment = Element.ALIGN_RIGHT;
            time.SpacingAfter = 30;

            //line separators
            Paragraph sepLine = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            sepLine.SpacingAfter = -10;
            Paragraph sepLine2 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            sepLine2.SpacingAfter = 40;

            //signature
            iTextSharp.text.Font footFont = new iTextSharp.text.Font(arial, 16, iTextSharp.text.Font.NORMAL);
            Paragraph signature = new Paragraph("E X A C T A", footFont);
            signature.Alignment = Element.ALIGN_LEFT;
            signature.SpacingAfter = 30;

            //printing into pdf
            var sfd = new SaveFileDialog();
            sfd.FileName = "Phase" + ph.Id.ToString();
            sfd.DefaultExt = ".pdf";
            sfd.Filter = "PDF(*.pdf)|*.pdf";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                {
                    Document doc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(doc, fs);
                    doc.Open();
                    doc.Add(signature);
                    doc.Add(title);
                    doc.Add(date);
                    doc.Add(time);
                    doc.Add(sepLine);
                    doc.Add(sepLine2);
                    doc.Add(pdfTable);                   
                    
                    doc.Close();
                    fs.Close();
                }
                sfd.Dispose();
            }
        }               
        
        private void btn_Add_Fase_Click(object sender, EventArgs e)
        {
            btn_Save_Fase.Enabled = true;
            btn_Cancel_Fase.Enabled = true;
            txt_Fase.Enabled = true;
            txt_FaseID.Enabled = true;

            pnl_Edit_Fase.Visible = true;
            //pnl_table_Fase.Left = pnl_Edit_Fase.Width + 10;
            btn_Add_Fase.Enabled = false;
            btn_Modify_Fase.Enabled = false;
            btn_Delete_Fase.Enabled = false;

            dgvFase.Enabled = false;
            _isNewFase = true;

            txt_FaseID.Text = "";
            txt_Fase.Text = "";
            txt_FaseID.BackColor = Color.LightYellow;
            txt_Fase.BackColor = Color.LightYellow;
            txt_FaseID.Focus();
        }

        private void btn_Modify_Fase_Click(object sender, EventArgs e)
        {
            if (dgvFase.Rows.Count == 0)
                return;

            txt_Fase.Enabled = true;
            txt_FaseID.Enabled = true;
            btn_Save_Fase.Enabled = true;
            btn_Cancel_Fase.Enabled = true;

            //pnl_Edit_Fase.Visible = true;
           // pnl_table_Fase.Left = pnl_Edit_Fase.Width + 10;
            btn_Add_Fase.Enabled = false;
            //btn_Modify_Fase.Enabled = false;
            btn_Delete_Fase.Enabled = false;

            dgvFase.Enabled = false;
            _isNewFase = false;
            txt_FaseID.Focus();                       
        }

        private void btn_Cancel_Fase_Click(object sender, EventArgs e)
        {
            btn_Save_Fase.Enabled = false;
            btn_Cancel_Fase.Enabled = false;
            txt_Fase.Enabled = false;
            txt_FaseID.Enabled = false;

            pnl_Edit_Fase.Visible = true;
            btn_Add_Fase.Enabled = true;
            btn_Modify_Fase.Enabled = true;
            btn_Delete_Fase.Enabled = true;
            dgvFase.Enabled = true;
        }
        
        private void dgvFase_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFase.SelectedRows.Count <= 0) return;

            _selectedFase = dgvFase.SelectedRows[0].Cells[0].Value.ToString();
            txt_FaseID.Text = dgvFase.SelectedRows[0].Cells[1].Value.ToString();
            txt_Fase.Text = dgvFase.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void btn_Delete_Fase_Click(object sender, EventArgs e)
        {
            if (dgvFase.Rows.Count == 0)
                return;

            if (_isNewFase)
            {
                txt_FaseID.Text = "";
                txt_Fase.Text = "";
                txt_FaseID.BackColor = Color.White;
                txt_Fase.BackColor = Color.White;

                dgvFase.Enabled = true;

                LoadFase();
                _isNewFase = false;
                return;
            }

            var dr = MessageBox.Show("Are you sure you want to delete " + txt_FaseID.Text + "?", "Phase", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) return;
            var query = from fase in Tables.TblOperatii
                        where fase.Id.ToString() == _selectedFase
                        select fase;

            var linex = query.ToList();
            foreach (var line in linex)
            {
                Tables.TblOperatii.DeleteOnSubmit(line);
            }

            Exacta.Menu.db.SubmitChanges();

            LoadFase();
            Additional.FillTheFilter(dgvFase, cbPhase, 1);

            if(dgvFase.Rows.Count == 0)
            {
                txt_FaseID.Text = String.Empty;
                txt_Fase.Text = String.Empty;
            }
        }

        private void btn_Save_Fase_Click(object sender, EventArgs e)
        {
            if (_isNewFase)
            //insert new record
            {
                try
                {
                    var lQuery = (from fas in Tables.TblOperatii
                                    where fas.CodOperatie == txt_FaseID.Text
                                    select fas).ToList();

                    if (lQuery.Count > 0)
                    {
                        MessageBox.Show("Fase already exists");
                        return;
                    }

                    Operatii fase = new Operatii();
                    fase.CodOperatie = txt_FaseID.Text;
                    fase.Operatie = txt_Fase.Text;

                    Tables.TblOperatii.InsertOnSubmit(fase);

                    Exacta.Menu.db.SubmitChanges();

                    dgvFase.Enabled = true;
                    txt_FaseID.BackColor = Color.White;
                    txt_Fase.BackColor = Color.White;

                    _isNewFase = false;
                }
                catch (Exception ex)
                {
                    _isNewFase = false;
                    MessageBox.Show(ex.Message);
                }
            }
            else
            //update record
            {
                var dr = MessageBox.Show("Do you want to update " + _selectedFase + "?", "Fase update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.No)
                    return;
                try
                {
                    //var lQuery = (from fas in Tables.TblOperatii
                    //                where fas.CodOperatie == txt_FaseID.Text
                    //                select fas).ToList();

                    //if (lQuery.Count > 0)
                    //{
                    //    MessageBox.Show("Fase already exists");
                    //    return;
                    //}

                    var lsQuery = (from fas in Tables.TblOperatii
                                    where fas.Id.ToString() == _selectedFase
                                    select fas).SingleOrDefault();                    

                    lsQuery.CodOperatie = txt_FaseID.Text;
                    lsQuery.Operatie = txt_Fase.Text;

                    Exacta.Menu.db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            txt_FaseID.Text = "";
            txt_Fase.Text = "";

            LoadFase();
            pnl_Edit_Fase.Visible = true;
            //pnl_table_Fase.Left -= (pnl_Edit_Fase.Width + 10);
            btn_Add_Fase.Enabled = true;
            btn_Modify_Fase.Enabled = true;
            btn_Delete_Fase.Enabled = true;
            dgvFase.Enabled = true;

            btn_Save_Fase.Enabled = false;
            btn_Cancel_Fase.Enabled = false;
            txt_Fase.Enabled = false;
            txt_FaseID.Enabled = false;
            Additional.FillTheFilter(dgvFase, cbPhase, 1);
        }      

        private void pnl_Edit_Fase_Paint(object sender, PaintEventArgs e)
            {

            }

        private void btnPDF_Click(object sender, EventArgs e)
        {
                    
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void pbPDF_Click(object sender, EventArgs e)
        {             
            if (dgvFase.Rows.Count == 0)
            {
                MessageBox.Show("No Phase records.", "Export to PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else PrintFasePDF();
        }

        private void cbPhase_SelectedValueChanged(object sender, EventArgs e)
        {
            Exacta.Menu.db = new DataContext(Exacta.Menu.connectionString);
            List<Operatii> phase = null;

            if (cbPhase.Text == String.Empty)
                phase = Exacta.Menu.db.GetTable<Operatii>().ToList();
            else
                phase = Exacta.Menu.db.GetTable<Operatii>().Where(operation => operation.CodOperatie.Contains(cbPhase.Text)).ToList();

            dgvFase.DataSource = phase;
        }

        private void pbPDF_MouseEnter(object sender, EventArgs e)
        {
            ControlPaint.DrawBorder(pbPDF.CreateGraphics(), pbPDF.ClientRectangle, Color.Orange, ButtonBorderStyle.Inset);
        }

        private void pbPDF_MouseLeave(object sender, EventArgs e)
        {
            pbPDF.Invalidate();
        }
    }
}
