using Exacta.DatabaseTableClasses;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Exacta
    {
    public partial class Articles : Form
        {
        public List<FaseForArticle> lstFases;
        private string _selectedArticle = String.Empty;
        private bool _isNewArticleFase, _isNewArticle;
        private string selectedFaseArticle;
        private int _canSaveArt = 0;
        double _totalPc = 0.0;
        int _totalComponents = 1;
        private int _canSaveFase = 0;

        public Articles()
        {
            InitializeComponent();
        }

        private void Articles_Load(object sender, EventArgs e)
        {
            Exacta.Menu.db = new DataContext(Exacta.Menu.connectionString);
           
            Additional.DesignMyGrid(dgvArticles);
            Additional.DesignMyGrid(dgv_FaseArticles);
            fillFaseCombo();
            LoadArticles();
            dgvArticles.Columns[1].HeaderText = "Article";
            dgvArticles.Columns[2].HeaderText = "Description";
            _canSaveArt = 0;
            _canSaveFase = 0;

            EnableArticleFields(false);
            EnableArticlePhaseFields(false);

            Additional.FillTheFilter(dgvArticles, cbArticle, 1);

            txt_Article.Enabled = false;
            txt_description_article.Enabled = false;
            cbClient.Enabled = false;
            cbCollection.Enabled = false;
            btn_save_article.Enabled = false;
            btn_cancel_article.Enabled = false;
            btn_saveFase.Enabled = false;
            btn_cancelFase.Enabled = false;
            cbFinezza.Enabled = true;
            EnableStoptronicFields(false);
           //
            
         //  Exacta.Menu.Expiration();
        }

        private void PrintArticlePDF()
        {
            //custom font
            string slnPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            slnPath = slnPath + "\\Fonts\\arial.ttf";
            BaseFont arial = BaseFont.CreateFont(slnPath, BaseFont.CP1250, BaseFont.EMBEDDED);

            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            iTextSharp.text.Font text = new iTextSharp.text.Font(arial, 10, iTextSharp.text.Font.NORMAL);

            //article table
            PdfPTable articlePdfTable = new PdfPTable(6);
            articlePdfTable.DefaultCell.Padding = 3;
            articlePdfTable.WidthPercentage = 100;
            articlePdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            articlePdfTable.DefaultCell.BorderWidth = 1;
            articlePdfTable.SpacingAfter = 30;

            foreach (DataGridViewColumn column in dgvArticles.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(Color.Coral);
                articlePdfTable.AddCell(cell);
            }

            Articole art = Exacta.Menu.db.GetTable<Articole>().SingleOrDefault(a => a.Id.ToString() == _selectedArticle);
            articlePdfTable.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(Color.White);
            articlePdfTable.AddCell(new Phrase(art.Id.ToString(), text));
            articlePdfTable.AddCell(new Phrase(art.Articol, text));
            articlePdfTable.AddCell(new Phrase(art.Descriere, text));
            articlePdfTable.AddCell(new Phrase(art.Collection, text));
            articlePdfTable.AddCell(new Phrase(art.Client, text));

            //phase table
            PdfPTable phasePdfTable = new PdfPTable(3);
            phasePdfTable.DefaultCell.Padding = 3;
            phasePdfTable.WidthPercentage = 100;
            phasePdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            phasePdfTable.DefaultCell.BorderWidth = 1;

            foreach (DataGridViewColumn column in dgv_FaseArticles.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(Color.Coral);
                phasePdfTable.AddCell(cell);
            }
            
            foreach (DataGridViewRow row in dgv_FaseArticles.Rows)
            {
                foreach(DataGridViewCell cell in row.Cells)
                {
                    phasePdfTable.AddCell(new Phrase(cell.Value.ToString(), text));
                }
            }
            
            //Main title
            iTextSharp.text.Font titleFont = new iTextSharp.text.Font(arial, 36, iTextSharp.text.Font.NORMAL);
            Paragraph title = new Paragraph("Article Report", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            title.SpacingAfter = 20;

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
            Paragraph signature = new Paragraph("E X A C T A",footFont);
            signature.Alignment = Element.ALIGN_LEFT;
            signature.SpacingAfter = 30;

            //title2
            iTextSharp.text.Font titFont = new iTextSharp.text.Font(arial, 26, iTextSharp.text.Font.NORMAL);
            Paragraph tit = new Paragraph("Phases For Current Article", titFont);
            tit.Alignment = Element.ALIGN_CENTER;
            tit.SpacingAfter = 40;

            var sfd = new SaveFileDialog();
            sfd.FileName = "Article" + art.Id.ToString();
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
                    doc.Add(articlePdfTable);
                    doc.Add(tit);
                    doc.Add(phasePdfTable);
                    doc.Close();
                    fs.Close();
                }
                sfd.Dispose();
            }
        }
        private void LoadArticles()
        {
            var artQuery = from art in Tables.TblArticole
                           select art;
            dgvArticles.DataSource = artQuery;

            //fill collection cmbox
            var query = from collection in Tables.TblCollections
                        select collection;
            var collections = query.ToList();
            cbCollection.Items.Clear();
            var i = 0;
            foreach (var c in collections)
            {
                cbCollection.Items.Insert(i++, c.Code);
            }
            if (cbCollection.Items.Count != 0)
            {
                cbCollection.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Please define Collections first \n(Go to Settings, Collections tab)", "Atention! No Collection found!");
                this.Close();
            }

            //fill client cmbox
            var clientQuery = from client in Tables.TblClients
                              select client;
            var clients = clientQuery.ToList();
            cbClient.Items.Clear();
            var j = 0;
            foreach (var c in clients)
            {
                cbClient.Items.Insert(j++, c.Name);
            }
            if (cbClient.Items.Count != 0)
            {
                cbClient.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Please define Clients first \n(Go to Settings, Clients tab)", "Atention! No Clients found!");
                this.Close();
            }
        }
        private void fillFaseCombo()
        {
            var query = from op in Tables.TblOperatii
                        select op;            

            var oper = query.ToList();
            cmb_Fase.Items.Clear();
            var i = 0;
            foreach (var l in oper)
                {
                cmb_Fase.Items.Insert(i++, l.Operatie);
                }
            if (cmb_Fase.Items.Count != 0)
            {
                cmb_Fase.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Please define Phases first \n(Go to Phase)", "Atention! No Phases found!");
                this.Close();
            }
        }
        private void loadArticleFase()
        {
            List<OperationParameters> art = null;
            
            art = Exacta.Menu.db.GetTable<OperationParameters>().Where(operation => operation.IdArticol.ToString() == _selectedArticle).ToList();
            
            lstFases = new List<FaseForArticle>();

            for (var i = 0; i < art.Count; i++)
            {
                Operatii opers = Exacta.Menu.db.GetTable<Operatii>().Where(operation => operation.Id == art[i].IdOperatie).SingleOrDefault();
                FaseForArticle f = new FaseForArticle(opers.Operatie, art[i].BucatiOra, art[i].Components);
                lstFases.Add(f);
            }
            var dt = new DataTable();
            dt.Columns.Add("Phase");
            dt.Columns.Add("Pieces/Hour");
            dt.Columns.Add("Components");
            _totalPc = 0.0;
            _totalComponents = 0;
            foreach (var item in lstFases)
            {
                var newRow = dt.NewRow();
                newRow[0] = item.Fase;
                newRow[1] = item.BucatiOra;
                newRow[2] = item.Components;
                dt.Rows.Add(newRow);
                _totalPc = _totalPc + (item.BucatiOra * item.Components);
                _totalComponents += item.Components;
            }
            var totalRow = dt.NewRow();
            totalRow[0] = "TOTAL";
            totalRow[1] = _totalPc;
            totalRow[2] = _totalComponents;
            dt.Rows.Add(totalRow);
            dgv_FaseArticles.DataSource = dt;

            dgv_FaseArticles.Columns[1].Width = 90;
            dgv_FaseArticles.Columns[2].Width = 90;
        }
        private void EnableArticleFields(bool enable)
        {
            txt_Article.Enabled = enable;
            txt_description_article.Enabled = enable;
            cbClient.Enabled = enable;
            cbCollection.Enabled = enable;
        }
        private void EnableArticlePhaseFields(bool enable)
        {
            txt_capiOra.Enabled = enable;
            cmb_Fase.Enabled = enable;
            txtComponents.Enabled = enable;
        }

        private void btn_add_article_fase_Click(object sender, EventArgs e)
        {
            EnableArticlePhaseFields(true);
            EnableStoptronicFields(true);
            ResetStoptronicFields();
            btn_saveFase.Enabled = true;
            btn_cancelFase.Enabled = true;
            _canSaveFase = 1;
            btn_editFase.Enabled = false;
            btn_deleteFase.Enabled = false;
            btn_add_article_fase.Enabled = false;
            
            dgv_FaseArticles.Enabled = false;
            _isNewArticleFase = true;

            txtComponents.Text = string.Empty;
            cmb_Fase.SelectedIndex = 0;
            txt_capiOra.Text = "";
            txt_capiOra.BackColor = Color.LightYellow;
            txtComponents.Text = "1";
            cmb_Fase.Focus();
        }                
        private void btn_saveFase_Click(object sender, EventArgs e)
            {
            if (_canSaveFase == 0) return;

            if (_isNewArticleFase)
            //insert new record
                {
                try
                    {
                    var s = lstFases.Find(x => x.Fase == cmb_Fase.Text);

                    if (s != null)
                        {
                        MessageBox.Show("Fase already exists");
                        return;
                        }

                    OperationParameters oper = new OperationParameters();
                    oper.IdArticol = Int32.Parse(dgvArticles.SelectedRows[0].Cells[0].Value.ToString());
                    var fase = (from line in Tables.TblOperatii
                                where line.Operatie == cmb_Fase.Text
                                select line).SingleOrDefault();

                    oper.IdOperatie = fase.Id;

                    try
                        {
                        if (decimal.Parse(txtComponents.Text.ToString()) <= 0 || txtComponents.Text.ToString().Contains('.') || txtComponents.Text.ToString().Contains(','))
                        {
                            MessageBox.Show("Components must be an integer number,\n bigger then 0.", "Atention!!!");
                            txtComponents.Text = "1";
                            return;
                        }
                        oper.BucatiOra = Double.Parse(txt_capiOra.Text);
                        oper.Components = int.Parse(txtComponents.Text);
                        
                        oper.Tens1 = int.Parse(nudTens1.Value.ToString());
                        oper.Tens2 = int.Parse(nudTens2.Value.ToString());
                        oper.Tens3 = int.Parse(nudTens3.Value.ToString());
                        oper.P1 = int.Parse(nudP1.Value.ToString());
                        oper.P2 = int.Parse(nudP2.Value.ToString());
                        oper.Velocita = int.Parse(cbVelocita.Text);
                        oper.GapIniziale = int.Parse(nudGapIniziale.Value.ToString());
                        oper.TensIniziali = int.Parse(nudTensIniziali.Value.ToString());
                        oper.Finezza = int.Parse(cbFinezza.Text);
                        oper.GapFinale = int.Parse(nudGapFinale.Value.ToString());
                        oper.TensFinale = int.Parse(nudTensFinale.Value.ToString());
                        oper.PtFinali = int.Parse(nudPtFinali.Value.ToString());
                        oper.AffIniziale = int.Parse(nudAffIniziale.Value.ToString());
                        oper.TensAff = int.Parse(nudTensAff.Value.ToString());
                        oper.AffFinale = int.Parse(nudAffFinale.Value.ToString());

                    }
                    catch (Exception ex)
                        {
                        MessageBox.Show(ex.ToString());
                        }

                    Tables.TblOperatiiArticole.InsertOnSubmit(oper);

                    Exacta.Menu.db.SubmitChanges();

                    txt_capiOra.BackColor = Color.White;
                   
                    _canSaveFase = 0;
                    _isNewArticleFase = false;
                    }
                catch (Exception ex)
                    {
                    _isNewArticleFase = false;
                    MessageBox.Show(ex.Message);
                    _canSaveFase = 0;
                    }
                }
            else
            //update record
                {
                var dr = MessageBox.Show("Do you want to update fase " + cmb_Fase.Text + "?", "Fase update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.No) return;
                try
                    {
                    var fase = (from line in Tables.TblOperatii
                                where line.Operatie == cmb_Fase.Text
                                select line).SingleOrDefault();

                    OperationParameters oper = Exacta.Menu.db.GetTable<OperationParameters>().SingleOrDefault(articol => articol.IdArticol.ToString() == _selectedArticle && articol.IdOperatie == fase.Id);
                    //Field which will be update
                    try
                        {
                        oper.BucatiOra = Double.Parse(txt_capiOra.Text);
                        oper.Components = int.Parse(txtComponents.Text);
                        oper.Tens1 = int.Parse(nudTens1.Value.ToString());
                        oper.Tens2 = int.Parse(nudTens2.Value.ToString());
                        oper.Tens3 = int.Parse(nudTens3.Value.ToString());
                        oper.P1 = int.Parse(nudP1.Value.ToString());
                        oper.P2 = int.Parse(nudP2.Value.ToString());
                        oper.Velocita = int.Parse(cbVelocita.Text);
                        oper.GapIniziale = int.Parse(nudGapIniziale.Value.ToString());
                        oper.TensIniziali = int.Parse(nudTensIniziali.Value.ToString());
                        oper.Finezza = int.Parse(cbFinezza.Text);
                        oper.GapFinale = int.Parse(nudGapFinale.Value.ToString());
                        oper.TensFinale = int.Parse(nudTensFinale.Value.ToString());
                        oper.PtFinali = int.Parse(nudPtFinali.Value.ToString());
                        oper.AffIniziale = int.Parse(nudAffIniziale.Value.ToString());
                        oper.TensAff = int.Parse(nudTensAff.Value.ToString());
                        oper.AffFinale = int.Parse(nudAffFinale.Value.ToString());

                    }
                    catch (Exception ex)
                        {
                        MessageBox.Show(ex.ToString());
                        }

                    Exacta.Menu.db.SubmitChanges();

                    txt_capiOra.BackColor = Color.White;

                    _isNewArticleFase = false;
                    // executes the appropriate commands to implement the changes to the database

                    _canSaveFase = 0;
                    }
                catch (Exception ex)
                    {
                    _canSaveFase = 0;
                    MessageBox.Show(ex.ToString());
                    }
                }

            txt_capiOra.Text = "";

            loadArticleFase();
            cmb_Fase.Enabled = true;
            btn_add_article_fase.Enabled = true;
            btn_editFase.Enabled = true;
            btn_deleteFase.Enabled = true;
            dgv_FaseArticles.Enabled = true;
            fillFaseCombo();            

            btn_saveFase.Enabled = false;
            btn_cancelFase.Enabled = false;
            EnableArticlePhaseFields(false);
            EnableStoptronicFields(false);
        }
        private void EnableStoptronicFields(bool enabled)
        {
            nudTens1.Enabled = enabled;
            nudTens2.Enabled = enabled;
            nudTens3.Enabled = enabled;
            nudP1.Enabled = enabled;
            nudP2.Enabled = enabled;
            cbVelocita.Enabled = enabled;
            nudGapIniziale.Enabled = enabled;
            nudTensIniziali.Enabled = enabled;
            cbFinezza.Enabled = enabled;
            nudGapFinale.Enabled = enabled;
            nudTensFinale.Enabled = enabled;
            nudPtFinali.Enabled = enabled;
            nudAffIniziale.Enabled = enabled;
            nudTensAff.Enabled = enabled;
            nudAffFinale.Enabled = enabled;
        }
        private void btn_cancelFase_Click(object sender, EventArgs e)
        {
            EnableArticlePhaseFields(false);
            EnableStoptronicFields(false);
            btn_saveFase.Enabled = false;
            btn_cancelFase.Enabled = false;
            btn_editFase.Enabled = true;
            btn_deleteFase.Enabled = true;
            btn_add_article_fase.Enabled = true;
            btn_cancelFase.Click += new EventHandler(dgv_FaseArticles_SelectionChanged);

            dgv_FaseArticles.Enabled = true;
        }                
        private void btn_Delete_Article_Click(object sender, EventArgs e)
            {
            if (dgvArticles.Rows.Count == 0)
                return;

            if (_isNewArticle)
                {
                txt_Article.Text = "";
                txt_description_article.Text = "";
                txt_Article.BackColor = Color.White;
                txt_description_article.BackColor = Color.White;
                txt_capiOra.Text = "";

                dgvArticles.Enabled = true;

                LoadArticles();
                _isNewArticle = false;
                return;
                }

            var dr = MessageBox.Show("Are you sure you want to delete article '" + txt_Article.Text + "'?", "Article", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) return;
            var query = from art in Tables.TblArticole
                        where art.Id.ToString() == _selectedArticle
                        select art;

            var queryA = from art in Tables.TblOperatiiArticole
                         where art.IdArticol.ToString() == _selectedArticle
                         select art;

            var articol = queryA.ToList();
            foreach (var line in articol)
                {
                Tables.TblOperatiiArticole.DeleteOnSubmit(line);
                }

            var linex = query.ToList();
            foreach (var line in linex)
                {
                Tables.TblArticole.DeleteOnSubmit(line);
                }

            Exacta.Menu.db.SubmitChanges();

            LoadArticles();
            loadArticleFase();
            Additional.FillTheFilter(dgvArticles, cbArticle, 1);
        }               
        private void btn_editFase_Click(object sender, EventArgs e)
            {
            //if gridview has data
            if (dgv_FaseArticles.DataSource == null)                
                return;                

            if (dgv_FaseArticles.Rows.Count == 0)                
                return;

            EnableArticlePhaseFields(true);
            EnableStoptronicFields(true);
            dgv_FaseArticles.Enabled = false;
            _isNewArticleFase = false;
            btn_add_article_fase.Enabled = false;
            btn_editFase.Enabled = false;
            btn_deleteFase.Enabled = false;
            cmb_Fase.SelectedIndex = cmb_Fase.FindStringExact(dgv_FaseArticles.SelectedRows[0].Cells[0].Value.ToString());
            //cmb_Fase.Enabled = false;
            txt_capiOra.Text = dgv_FaseArticles.SelectedRows[0].Cells[1].Value.ToString();
            //cmb_Fase.Focus();
            txt_capiOra.Focus();
            _canSaveFase = 2;
            btn_saveFase.Enabled = true;
            btn_cancelFase.Enabled = true;
        }
        private void btn_deleteFase_Click(object sender, EventArgs e)
            {
            //if gridview has data
            if (dgv_FaseArticles.DataSource == null)                
                return;                

            if (dgv_FaseArticles.Rows.Count == 0)                
                return;                

            var dr = MessageBox.Show("Are you sure you want to delete article phase '" + cmb_Fase.Text + "'?", "ArticlePhase", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) return;

            var fase = (from line in Tables.TblOperatii
                        where line.Operatie == dgv_FaseArticles.SelectedRows[0].Cells[0].Value.ToString()
                        select line).Single();

            OperationParameters oper = Exacta.Menu.db.GetTable<OperationParameters>().Single(articol => articol.IdArticol.ToString() == _selectedArticle && articol.IdOperatie == fase.Id);

            Tables.TblOperatiiArticole.DeleteOnSubmit(oper);

            Exacta.Menu.db.SubmitChanges();

            //List<OperatiiArticole> art = null;

            //art = Exacta.Menu.db.GetTable<OperatiiArticole>().Where(operation => operation.IdArticol.ToString() == _selectedArticle).ToList();

            //lstFases = new List<FaseForArticle>();

            //for (var i = 0; i < art.Count; i++)
            //    {
            //    Operatii opers = Exacta.Menu.db.GetTable<Operatii>().Where(operation => operation.Id == art[i].IdOperatie).Single();
            //    FaseForArticle f = new FaseForArticle(opers.Operatie, art[i].BucatiOra, art[i].Centes);
            //    lstFases.Add(f);
            //    }

            //dgv_FaseArticles.DataSource = lstFases;
            loadArticleFase();
            }
        private void btn_save_article_Click(object sender, EventArgs e)
            {
            if (_canSaveArt == 0)
                return;

            if (_isNewArticle)
            //insert new record
                {
                try
                    {
                    var lQuery = (from art in Tables.TblArticole
                                  where art.Articol == txt_Article.Text
                                  select art).ToList();

                    if (lQuery.Count > 0)
                        {
                        MessageBox.Show("Article already exists");
                        return;
                        }

                    Articole articol = new Articole();
                    articol.Articol = txt_Article.Text;
                    articol.Descriere = txt_description_article.Text;
                    articol.Collection = cbCollection.Text;
                    articol.Client = cbClient.Text;
                    Tables.TblArticole.InsertOnSubmit(articol);

                    Exacta.Menu.db.SubmitChanges();

                    txt_Article.BackColor = Color.White;
                    txt_description_article.BackColor = Color.White;

                    _isNewArticle = false;
                    btn_add_article_fase.Enabled = true;
                    _canSaveArt = 0;
                    }
                catch (Exception ex)
                    {
                    _canSaveArt = 0;
                    _isNewArticle = false;
                    MessageBox.Show(ex.Message);
                    }
                }
            else
            //update record
                {
                var dr = MessageBox.Show("Do you want to update " + _selectedArticle + "?", "Article update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                    return;

                try
                    {
                    //var lQuery = (from art in Tables.TblArticole
                    //              where art.Articol == txt_Article.Text
                    //              select art).ToList();

                    //if (lQuery.Count > 0)
                    //    {
                    //    MessageBox.Show("Article already exists");
                    //    return;
                    //    }

                    var lsQuery = (from art in Tables.TblArticole
                                   where art.Id.ToString() == _selectedArticle
                                   select art).SingleOrDefault();

                    Articole articola = Exacta.Menu.db.GetTable<Articole>().SingleOrDefault(arti => arti.Id.ToString() == _selectedArticle);

                    articola.Articol = txt_Article.Text;
                    articola.Descriere = txt_description_article.Text;
                    articola.Collection = cbCollection.Text;
                    articola.Client = cbClient.Text;

                    Exacta.Menu.db.SubmitChanges();
                    _canSaveArt = 0;
                    }
                catch (Exception ex)
                    {
                    _canSaveArt = 0;
                    MessageBox.Show(ex.ToString());
                    }

                }

            LoadArticles();

            var Query = (from art in Tables.TblArticole
                         select art).ToList();

            dgvArticles.Rows[Query.Count - 1].Selected = true;

            btn_save_article.Enabled = false;
            btn_cancel_article.Enabled = false;
            btn_Add_Article.Enabled = true;
            btn_Edit_Article.Enabled = true;
            btn_Delete_Article.Enabled = true;
            dgvArticles.Enabled = true;
            Additional.FillTheFilter(dgvArticles, cbArticle, 1);
            dgv_FaseArticles.Enabled = true;
            EnableArticleFields(false);
        }        
        private void btn_Add_Article_Click(object sender, EventArgs e)
            {
            EnableArticleFields(true);
            btn_save_article.Enabled = true;
            btn_cancel_article.Enabled = true;
            btn_editFase.Enabled = false;
            btn_deleteFase.Enabled = false;
            

            //pnl_Edit_Articles.Visible = true;
            btn_Add_Article.Enabled = false;
            btn_Edit_Article.Enabled = false;
            btn_Delete_Article.Enabled = false;
            btn_add_article_fase.Enabled = false;

            dgv_FaseArticles.DataSource = null;

            dgvArticles.Enabled = false;
            _isNewArticle = true;
            dgv_FaseArticles.Enabled = false;
            
            txt_Article.Text = "";
            txt_description_article.Text = "";
            txt_Article.BackColor = Color.LightYellow;
            txt_description_article.BackColor = Color.LightYellow;
            txt_Article.Focus();
            _canSaveArt = 1;
            }
        private void btn_Edit_Article_Click(object sender, EventArgs e)
            {
            if (dgvArticles.Rows.Count == 0)
                return;

            EnableArticleFields(true);
            btn_save_article.Enabled = true;
            btn_cancel_article.Enabled = true;

            pnl_Edit_Articles.Visible = true;
            //pnl_table_Articles.Left = pnl_Edit_Articles.Width + 10;
            btn_Add_Article.Enabled = false;
            btn_Edit_Article.Enabled = false;
            btn_Delete_Article.Enabled = false;
            btn_add_article_fase.Enabled = true;

            dgvArticles.Enabled = false;
            _isNewArticle = false;

            //_selectedArticle = dgvArticles.SelectedRows[0].Cells[0].Value.ToString();
            //txt_Article.Text = dgvArticles.SelectedRows[0].Cells[1].Value.ToString();
            //txt_description_article.Text = dgvArticles.SelectedRows[0].Cells[2].Value.ToString();
            txt_Article.Focus();
            //loadArticleFase();
            //Exacta.Menu.db = new DataContext(Exacta.Menu.connectionString);
            //List<OperatiiArticole> art = null;

            //art = Exacta.Menu.db.GetTable<OperatiiArticole>().Where(operation => operation.IdArticol.ToString() == _selectedArticle).ToList();

            //lstFases = new List<FaseForArticle>();

            //for (var i = 0; i < art.Count; i++)
            //    {
            //    Operatii oper = Exacta.Menu.db.GetTable<Operatii>().Where(operation => operation.Id == art[i].IdOperatie).Single();
            //    FaseForArticle f = new FaseForArticle(oper.Operatie, art[i].BucatiOra, art[i].Centes);
            //    lstFases.Add(f);
            //    }

            //dgv_FaseArticles.DataSource = lstFases;

            _canSaveArt = 2;
            }
        private void dgvArticles_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticles.SelectedRows.Count <= 0) return;

            _selectedArticle = dgvArticles.SelectedRows[0].Cells[0].Value.ToString();
            txt_Article.Text = dgvArticles.SelectedRows[0].Cells[1].Value.ToString();
            txt_description_article.Text = dgvArticles.SelectedRows[0].Cells[2].Value.ToString();
            cbCollection.Text = dgvArticles.SelectedRows[0].Cells[3].Value.ToString();
            cbClient.Text = dgvArticles.SelectedRows[0].Cells[4].Value.ToString();
            

            loadArticleFase();
        }
        private void dgvArticles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void cmb_Fase_SelectedIndexChanged(object sender, EventArgs e)
        {

        }        
        private void pbPDF_Click(object sender, EventArgs e)
        {
            if (dgvArticles.Rows.Count == 0)
            {
                MessageBox.Show("No Article records.", "Export to PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else PrintArticlePDF();
        }        
        private void ResetStoptronicFields()
        {
            nudTens1.Value = 0;
            nudTens2.Value = 0;
            nudTens3.Value = 0;
            nudP1.Value = 0;
            nudP2.Value = 0;
            cbVelocita.Text = "75";
            nudGapIniziale.Value = 1;
            nudTensIniziali.Value = 0;
            cbFinezza.Text = "20";
            nudGapFinale.Value = 1;
            nudTensFinale.Value = 0;
            nudPtFinali.Value = 0;
            nudAffIniziale.Value = 0;
            nudTensAff.Value = 0;
            nudAffFinale.Value = 0;
        }
        private void dgv_FaseArticles_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_FaseArticles.SelectedRows.Count <= 0) return;
            selectedFaseArticle = dgv_FaseArticles.SelectedRows[0].Cells[0].Value.ToString();
            if (selectedFaseArticle == "TOTAL")
            {
                txt_capiOra.Text = string.Empty;
                cmb_Fase.Text = string.Empty;
                txtComponents.Text = string.Empty;
                ResetStoptronicFields();
                return;
            }
            else
            {
                cmb_Fase.Text = selectedFaseArticle;
                txt_capiOra.Text = dgv_FaseArticles.SelectedRows[0].Cells[1].Value.ToString();
                txtComponents.Text = dgv_FaseArticles.SelectedRows[0].Cells[2].Value.ToString();

                var opers = (from operation in Tables.TblOperatii
                             where operation.Operatie == selectedFaseArticle
                             select operation).SingleOrDefault();
                if (opers == null)
                    return;

                var art = (from oper in Tables.TblOperatiiArticole
                           where oper.IdArticol.ToString() == _selectedArticle &&
                            oper.IdOperatie.ToString() == opers.Id.ToString()
                           select oper).SingleOrDefault();


                nudTens1.Value = art.Tens1;
                nudTens2.Value = art.Tens2;
                nudTens3.Value = art.Tens3;
                nudP1.Value = art.P1;
                nudP2.Value = art.P2;
                cbVelocita.Text = art.Velocita.ToString();
                nudGapIniziale.Value = art.GapIniziale;
                nudTensIniziali.Value = art.TensIniziali;
                cbFinezza.Text = art.Finezza.ToString();
                nudGapFinale.Value = art.GapFinale;
                nudTensFinale.Value = art.TensFinale;
                nudPtFinali.Value = art.PtFinali;
                nudAffIniziale.Value = art.AffIniziale;
                nudTensAff.Value = art.TensAff;
                nudAffFinale.Value = art.AffFinale;
            }
        }
        private void cbArticle_SelectionChangeCommitted(object sender, EventArgs e)
        {           
        }
        private void cbArticle_SelectedValueChanged(object sender, EventArgs e)
        {
            Exacta.Menu.db = new DataContext(Exacta.Menu.connectionString);
            List<Articole> art = null;
            if (cbArticle.Text == String.Empty)
                art = Exacta.Menu.db.GetTable<Articole>().ToList();
            else
                art = Exacta.Menu.db.GetTable<Articole>().Where(op => op.Articol.Contains(cbArticle.Text)).ToList();
            dgvArticles.DataSource = art;
        }
        private void cbClient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pbPDF_MouseEnter(object sender, EventArgs e)
        {
            ControlPaint.DrawBorder(pbPDF.CreateGraphics(), pbPDF.ClientRectangle, Color.Orange, ButtonBorderStyle.Inset);
        }

        private void pbPDF_MouseLeave(object sender, EventArgs e)
        {
            pbPDF.Invalidate();
        }

        private void btn_cancel_article_Click(object sender, EventArgs e)
        {
            EnableArticleFields(false);
            btn_save_article.Enabled = false;
            btn_cancel_article.Enabled = false;
            btn_add_article_fase.Enabled = true;
            btn_editFase.Enabled = true;
            btn_deleteFase.Enabled = true;

            //pnl_Edit_Articles.Visible = false;
            //pnl_table_Articles.Left -= (pnl_Edit_Articles.Width + 10);
            btn_Add_Article.Enabled = true;
            btn_Edit_Article.Enabled = true;
            btn_Delete_Article.Enabled = true;

            dgvArticles.Enabled = true;
            dgv_FaseArticles.Visible = true;
            dgv_FaseArticles.Enabled = true;
            loadArticleFase();
        }
      }
}
