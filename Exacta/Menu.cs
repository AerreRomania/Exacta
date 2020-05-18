using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WindowsFormsApp1;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace Exacta
{
    public partial class Menu : Form
    {
        private Form[] history = new Form[] { };
        private int numOfOpenedForms = 0;
        public static Control _pbReload;
        private bool appOpened = false;
        public List<Control> listButtons;
        private string myAppPath, myStore;
        private List<FileModel> ListOfFM { get; set; }
        private System.Timers.Timer _tmPlay = new System.Timers.Timer();

        //Connection with sql server
        //public static string connectionString = @"data source=KNPC7\SQLEXPRESS;initial catalog=Exacta; integrated security=SSPI";
        //public static string connectionString = "data source=DESKTOP-Q24FF5A;initial catalog=Exacta; integrated security=SSPI";
        public static string connectionString = "data source=192.168.96.17;initial catalog = Exacta; User ID = sa; password=onlyouolimpias";
        //public static string connectionString = "data source=SERGIU;initial catalog=Exacta; integrated security=SSPI";//sergiu
        //public static string connectionString = ConfigurationManager.ConnectionStrings["Exacta.Properties.Settings.sqlConnections"].ConnectionString; //sergiu
        //public static string connectionString = "data source=SERVEREXACTA;initial catalog = Exacta; User ID = sa; password=ExactaServer1122";//clients string



        public static DataContext db;
        public List<FaseForArticle>lstFases;
        public static DateTime dateFrom;
        public static DateTime dateTo;

        private const string _PREVIEW_ = "preview";
        private const string _ALL_RECS_ = "allrecs";
        private const string _OF_ARTICLE_ = "ofarticle";

        #region WorkWithFiles

        private void CreateMainDir()
        {
            if (!Directory.Exists(myStore))
            {
                Directory.CreateDirectory(myStore);
            }
        }

        private void CreateArticleDir(string fileObj)
        // function that will colaborates with readed filename
        // and create dir by plus phase into that
        {
            if (!Directory.Exists(myStore))
            {
                Directory.CreateDirectory(myStore + "\\" + fileObj);
            }

            //return myStore + "\\" + fileObj;
        }

        private void CreatePhaseDir(string filePrim, string secondaryObj)
        {
            //that will create article directory
            CreateArticleDir(filePrim);

            if (!Directory.Exists(myStore + "\\" + filePrim + "\\" + secondaryObj + "\\"))
            {
                Directory.CreateDirectory(myStore + "\\" + filePrim + "\\" + secondaryObj + "\\");
            }
        }

        private void MoveStpFile(string oldFilePath, string newFilePath)
        {
            if (!File.Exists(newFilePath))
            {
                File.Copy(oldFilePath, newFilePath);
            }
        }

        private void PlayThroughDomain()
        {
            const string FILE_EXT = "stp";

            var lstOfFilesToDelete = new List<string>();

            foreach (var file in Directory.GetFiles(myAppPath))
            {
                // search only for .stp files
                var fileExt = file.ToString().Split('.').Last();
                if (fileExt != FILE_EXT) continue;
                var fileName = Path.GetFileName(file);
                lstOfFilesToDelete.Add(file);
                var realString = new char[] { };
                var rs = "";
                var a = "";
                var p = "";

                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        var line = sr.ReadLine().Split(' ').ToArray();

                        foreach (var l in line)
                        {
                            realString = l.Where(ch => !char.IsControl(ch)).ToArray();
                            rs = new string(realString);
                        }
                        a = rs.Substring(0, 9);
                        p = rs.Trim().Substring(rs.Length - 4);
                        CreatePhaseDir(a, p);
                        MoveStpFile(file, myStore + "\\" + a + "\\" + p + "\\" + fileName);

                        sr.Close();
                    }

                    fs.Close();
                }
            }

            foreach (var file in lstOfFilesToDelete)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
        }

        #endregion WorkWithFiles
        
        public Menu()
        {
            InitializeComponent();
            
            listButtons = new List<Control>();

            hideElements(listButtons, false);
            EnablePbReload();

            history = new Form[100];
        }

        public void EnablePbReload()
        {
            _pbReload = this.pbReload;
        }
        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KE7";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";
        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string keyName = @"HKEY_CURRENT_USER\Exacta";
        public static string valueName = "1";
        public static string valueName1 = "2";
        public static string l_key = "";
        public static void Expiration()
        {
                if (Registry.GetValue(keyName, valueName, null) == null||Registry.GetValue(keyName, valueName1, null)==null)
                {
                    KeyInput form = new KeyInput();
                    form.ShowDialog();
                    form.Dispose();
                if (KeyInput.stop == true)
                {
                   
                    Application.ExitThread(); 
                }
                else
                {
                    Microsoft.Win32.RegistryKey key;
                    Microsoft.Win32.RegistryKey Date;
                    key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Exacta");
                    key.SetValue("1", Encrypt(KeyInput.d1));
                    key.Close();
                    Date = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Exacta");
                    Date.SetValue("2", Encrypt(KeyInput.d2));
                    Date.Close();
                    var con = new SqlConnection(KeyInput.conn);
                    var cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "Insert into KeyEnc (L_Key,Key_enc,PC_name) VALUES (@Key, @Enc, @PC)";
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Key", KeyInput.d1);
                    cmd.Parameters.AddWithValue("@Enc", Encrypt(KeyInput.d1));
                    cmd.Parameters.AddWithValue("@PC", Environment.MachineName);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                }
                else
                {
                    Microsoft.Win32.RegistryKey o; 

                    o = Registry.CurrentUser.OpenSubKey("Exacta");


                    if (o != null)
                    {
                        Object o1 = o.GetValue("1");
                        Object d2 = o.GetValue("2");
                    
                    if (o1 != null || d2 != null)
                    { 
                        string value = o1.ToString();
                        string vd = d2.ToString();
                        string vvd = Properties.Settings.Default.d2.ToString();
                        KeyInput.nr_luni = Properties.Settings.Default.o2;
                        DateTime vvvvd = Convert.ToDateTime(vvd);
                        int x = Convert.ToInt32((vvvvd.AddMonths(KeyInput.nr_luni) - DateTime.Now).TotalDays);
                        if (vd != (Encrypt(vvd)) || x<1)
                        {
                            Microsoft.Win32.Registry.CurrentUser.DeleteSubKey("Exacta");
                            Messagebox form = new Messagebox();
                            form.ShowDialog();
                            form.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Licence data miss!\n Contact provider");
                        Application.ExitThread(); 
                    }
                    }
                }
        }
        private void Menu_Load(object sender, EventArgs e)
        {
           // Expiration();

            this.SetBevel(false);
            try
            {
                btnBack.Enabled = false;
                btnForward.Enabled = false;

                pbSettings.Click += delegate
                    {
                        Settings set = new Settings();
                        set.ShowDialog();
                    };

                dateFrom = new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day);
                dateTo = new DateTime(dtpTo.Value.Year, dtpTo.Value.Month, dtpTo.Value.Day);
                pbReload.Enabled = true;
                //pnl_Edit_Articles.Visible = false;
                //pnl_Edit_Fase.Visible = false;

                ListOfFM = new List<FileModel>();

                                //tabControl_Database.Visible = false;
                //pnl_Production.Visible = false; 
                //styleMyDatagridView(dgv_Cuts);
                //styleMyDatagridView(dgv_Time);

                myAppPath = Application.StartupPath;
                myStore = myAppPath + "\\Resources";

                //var connectionStringPath = myAppPath + "\\connectionString.txt";

                //try
                //{
                //    
                //connectionString = System.IO.File.ReadAllText(connectionStringPath);
                //    databaseConnectionString = databaseConnectionString.Replace("\r\n", string.Empty);
                //    var sr = databaseConnectionString.Split('\\');
                //    if (sr.Length > 1)
                //    {
                //        databaseConnectionString = sr[0] + '\\' + sr[2];
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.ToString());
                //}
                //CreateMainDir();
                //db = new DataContext(databaseConnectionString);
                //PlayThroughDomain();

                //_tmPlay = new System.Timers.Timer(120000); //2min
                //_tmPlay.Elapsed += _tmPlay_Elapse;
                //_tmPlay.Enabled = true;
                //_tmPlay.Start();

                //dgv_Time.Height = this.Height - pnNavi.Height;
                //dgv_Time.Width = this.Width - pnDockBar.Width;
                //pnl_Production.Height = this.Height - pnNavi.Height;
                //pnl_Production.Width = this.Width - pnDockBar.Width;

                //pnl_table_Fase.Left -= (pnl_Edit_Fase.Width + 10);
                //pnl_table_Articles.Left -= (pnl_Edit_Articles.Width + 10);

                db = new DataContext(connectionString);
                enableButtons(false);

                //GetCurrentDataProcedure();
                this.FormClosing += (s, ev) =>
                //take the latest inputs by the user
                {
                    PlayThroughDomain();
                };

                foreach (Form frm in MdiChildren)
                {
                    frm.Hide();
                }

                var p = new Production()
                {
                    MdiParent = this
                };

                p.WindowState = FormWindowState.Maximized;

                history[numOfOpenedForms++] = p;

                p.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void openStoptronic()
        {
            if (!appOpened)
            {
                Thread thread = new Thread(new ThreadStart(applicationRunningThread));
                thread.Start();
                appOpened = true;

            }
            else
            {
                MessageBox.Show("Application is already running");
            }
        }

        private void applicationRunningThread()
        {
            try
            {
                Process process = new Process();
                StringBuilder builder = new StringBuilder();
                builder.Append(Application.StartupPath + "\\" + "Stoptronic.exe");
                process.StartInfo.FileName = builder.ToString();
                process.Start();
                process.WaitForExit();

                appOpened = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void hideElements(List<Control> listControl, bool visible)
        {
            foreach (Control control in listControl)
            {
                control.Visible = visible;
            }
        }
        private void txt_search_Click(object sender, EventArgs e)
        {

        }
        private void txt_search_Leave(object sender, EventArgs e)
        {

        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            enableButtons(true);
        }

        private void enableButtons(bool enable)
        {
        }
        private void btn_Sinottico_Click(object sender, EventArgs e)
        {
            var f1 = typeof(Control).GetField("EventClick", BindingFlags.Static | BindingFlags.NonPublic);

            var obj = f1.GetValue(pbReload);

            var pi = pbReload.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);

            var list = (EventHandlerList)pi.GetValue(pbReload, null);
            list.RemoveHandler(obj, list[obj]);

            SuspendLayout();
            foreach (Form frm in MdiChildren)
            {
                frm.Hide();
            }
            ResumeLayout(true);
            Sinottico f = new Sinottico()
            {
                MdiParent = this
            };
            f.WindowState = FormWindowState.Maximized;
            f.Show();

            //history[numOfOpenedForms++] = f;
        }

        private void btn_Sinotico_Click(object sender, EventArgs e)
        {
        }

        private void pbMenu_Click(object sender, EventArgs e)
        {
            if (pnDockBar.Width > 0) pnDockBar.Width = 0;
            else pnDockBar.Width = 188;
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            dateFrom = new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day);
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            dateTo = new DateTime(dtpTo.Value.Year, dtpTo.Value.Month, dtpTo.Value.Day);
        }

        private void btnArticles_Click(object sender, EventArgs e)
        {
            Articles frmArticles = new Articles();
            frmArticles.ShowDialog();
            frmArticles.Dispose();
        }

        private void btnFase_Click(object sender, EventArgs e)
        {
            Phase frmPhase = new Phase();
            frmPhase.ShowDialog();
            frmPhase.Dispose();
        }

        private void btnEfficiency_Click(object sender, EventArgs e)
        {
            LoadingInfo.InfoText = "Loading production...";
            LoadingInfo.ShowLoading();
            btnBack.Enabled = true;

            //var f1 = typeof(Control).GetField("EventClick", BindingFlags.Static | BindingFlags.NonPublic);

            //var obj = f1.GetValue(pbReload);

            //var pi = pbReload.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);

            //var list = (EventHandlerList)pi.GetValue(pbReload, null);
            //list.RemoveHandler(obj, list[obj]);

            SuspendLayout();

            foreach (Form frm in MdiChildren)
            {
                frm.Hide();
            }

            ResumeLayout(true);
            Production f = new Production()
            {
                MdiParent = this
            };
            f.WindowState = FormWindowState.Maximized;
            f.Show();

            if (!firstTimeClicked && history[currentPosition + 1] != null)
            {
                btnForward.Enabled = false;
                numOfOpenedForms = currentPosition + 1;
                history[numOfOpenedForms++] = f;
                for (var i = numOfOpenedForms; i < history.Count() - 1; i++)
                    history[i] = null;
            }
            else
                history[numOfOpenedForms++] = f;
            currentPosition = numOfOpenedForms - 1;

            //pbReload.Click += (s, ev) =>
            //{
            //    f.GetDataPublic();
            //};

            LoadingInfo.CloseLoading();
        }

        private void pbReload_Click(object sender, EventArgs e)
        {
            switch (history[currentPosition].Name)
            {
                case "Reports":
                    {
                        
                        LoadingInfo.InfoText = "Loading reports...";
                        LoadingInfo.ShowLoading();

                        SuspendLayout();

                        foreach (Form frm in MdiChildren)
                            frm.Hide();

                        ResumeLayout(true);

                        var frmReports = new Reports()
                        {
                            MdiParent = this
                        };

                        history[currentPosition] = null;
                        history[currentPosition] = frmReports;
                        frmReports.WindowState = FormWindowState.Maximized;
                        history[currentPosition].Show();

                        LoadingInfo.CloseLoading();

                        break;
                    }
                case "Production":
                    {
                        LoadingInfo.InfoText = "Loading production...";
                        LoadingInfo.ShowLoading();

                        SuspendLayout();

                        foreach (Form frm in MdiChildren)
                            frm.Hide();

                        ResumeLayout(true);

                        var frmProduction = new Production()
                        {
                            MdiParent = this
                        };

                        history[currentPosition] = null;
                        history[currentPosition] = frmProduction;
                        frmProduction.WindowState = FormWindowState.Maximized;
                        history[currentPosition].Show();

                        LoadingInfo.CloseLoading();

                        break;
                    }
                case "Intervals":
                    {
                        LoadingInfo.InfoText = "Loading intervals...";
                        LoadingInfo.ShowLoading();

                        SuspendLayout();

                        foreach (Form frm in MdiChildren)
                            frm.Hide();

                        ResumeLayout(true);

                        var frmIntervals = new Intervals()
                        {
                            MdiParent = this
                        };

                        history[currentPosition] = null;
                        history[currentPosition] = frmIntervals;
                        frmIntervals.WindowState = FormWindowState.Maximized;
                        history[currentPosition].Show();

                        LoadingInfo.CloseLoading();
                        break;
                    }
                case "Efficiency":
                    {
                        LoadingInfo.InfoText = "Loading efficiency...";
                        LoadingInfo.ShowLoading();

                        SuspendLayout();

                        foreach (Form frm in MdiChildren)
                            frm.Hide();

                        ResumeLayout(true);

                        var frmEfficiency = new Efficiency()
                        {
                            MdiParent = this
                        };

                        history[currentPosition] = null;
                        history[currentPosition] = frmEfficiency;
                        frmEfficiency.WindowState = FormWindowState.Maximized;
                        history[currentPosition].Show();

                        LoadingInfo.CloseLoading();
                        break;
                    }
                case "Sinottico":
                    {
                        if (Sinottico.Mode == "Sinottico_Production")
                        {
                            
                            LoadingInfo.InfoText = "Loading exacta production...";
                            LoadingInfo.ShowLoading();

                            SuspendLayout();

                            foreach (Form frm in MdiChildren)
                                frm.Hide();

                            ResumeLayout(true);

                            var frmSinottico = new Sinottico()
                            {
                                MdiParent = this
                            };

                            history[currentPosition] = null;
                            history[currentPosition] = frmSinottico;
                            frmSinottico.WindowState = FormWindowState.Maximized;
                            history[currentPosition].Show();

                            LoadingInfo.CloseLoading();
                        }
                        else if (Sinottico.Mode == "Sinottico_Efficiency")
                        {
                            LoadingInfo.InfoText = "Loading exacta efficiency...";
                            LoadingInfo.ShowLoading();

                            SuspendLayout();

                            foreach (Form frm in MdiChildren)
                                frm.Hide();

                            ResumeLayout(true);

                            var frmSinottico = new Sinottico()
                            {
                                MdiParent = this
                            };

                            history[currentPosition] = null;
                            history[currentPosition] = frmSinottico;
                            frmSinottico.WindowState = FormWindowState.Maximized;
                            history[currentPosition].Show();

                            LoadingInfo.CloseLoading();
                        }
                       
                        break;
                    }
            }
        }

        private void btnSinottico_Click(object sender, EventArgs e)
            {
            LoadingInfo.InfoText = "Loading exacta production...";
            LoadingInfo.ShowLoading();

            btnBack.Enabled = true;
            //var f1 = typeof(Control).GetField("EventClick", BindingFlags.Static | BindingFlags.NonPublic);

            //var obj = f1.GetValue(pbReload);

            //var pi = pbReload.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);

            //var list = (EventHandlerList)pi.GetValue(pbReload, null);
            //list.RemoveHandler(obj, list[obj]);

            SuspendLayout();
            foreach (Form frm in MdiChildren)
                {
                frm.Hide();
                }

            ResumeLayout(true);
            Sinottico f = new Sinottico()
                {
                MdiParent = this
                };
            Sinottico.Mode = "Sinottico_Production";
            f.WindowState = FormWindowState.Maximized;

            f.Show();

            if (!firstTimeClicked && history[currentPosition + 1] != null)
            {
                btnForward.Enabled = false;
                numOfOpenedForms = currentPosition + 1;
                history[numOfOpenedForms++] = f;
                for (var i = numOfOpenedForms; i < history.Count() - 1; i++)
                    history[i] = null;
            }
            else
                history[numOfOpenedForms++] = f;
            currentPosition = numOfOpenedForms - 1;

            LoadingInfo.CloseLoading();
        }
        

        private void btn_Exacta_Click(object sender, EventArgs e)
        {
            LoadingInfo.InfoText = "Loading exacta efficiency...";
            LoadingInfo.ShowLoading();

            btnBack.Enabled = true;
            SuspendLayout();
            foreach (Form frm in MdiChildren)
                frm.Hide();

            ResumeLayout(true);
            Sinottico f = new Sinottico()
            {
                MdiParent = this
            };
            Sinottico.Mode = "Sinottico_Efficiency";
            //f.GetColorByEfficiency();

            f.WindowState = FormWindowState.Maximized;
            f.Show();            

            if (!firstTimeClicked && history[currentPosition + 1] != null)
            {
                btnForward.Enabled = false;
                numOfOpenedForms = currentPosition + 1;
                history[numOfOpenedForms++] = f;
                for (var i = numOfOpenedForms; i < history.Count() - 1; i++)
                    history[i] = null;
            }
            else
                history[numOfOpenedForms++] = f;
            currentPosition = numOfOpenedForms - 1;

            LoadingInfo.CloseLoading();
        }

        private void pb_refresh_Production_Click(object sender, EventArgs e)
        {
            foreach (Form frm in MdiChildren)
            {
                frm.Close();
            }

            var p = new Production()
            {
                MdiParent = this
            };

            p.WindowState = FormWindowState.Maximized;
            p.RefreshDgv();
            p.Show();
        }

        private void btnIntervals_Click(object sender, EventArgs e)
        {
            LoadingInfo.InfoText = "Loading intervals...";
            LoadingInfo.ShowLoading();
            btnBack.Enabled = true;
            SuspendLayout();
            foreach (Form frm in MdiChildren)
            {
                frm.Hide();
            }

            ResumeLayout(true);
            var i = new Intervals()
            {
                MdiParent = this
            };            

            i.WindowState = FormWindowState.Maximized;
            //p.RefreshDgv();
            i.Show();

            if (!firstTimeClicked && history[currentPosition + 1] != null)
            {
                btnForward.Enabled = false;
                numOfOpenedForms = currentPosition + 1;
                history[numOfOpenedForms++] = i;
                for (var j = numOfOpenedForms; j < history.Count() - 1; j++)
                    history[j] = null;
            }
            else
                history[numOfOpenedForms++] = i;
            currentPosition = numOfOpenedForms - 1;

            LoadingInfo.CloseLoading();
        }
       
        private void btnCurrentDay_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Now.Date;
            dtpTo.Value = DateTime.Now.Date;
            dateFrom = new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day);
            dateTo = new DateTime(dtpTo.Value.Year, dtpTo.Value.Month, dtpTo.Value.Day);

            pbReload_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadingInfo.InfoText = "Loading efficiency...";
            LoadingInfo.ShowLoading();

            btnBack.Enabled = true;
            SuspendLayout();
            foreach (Form frm in MdiChildren)
            {
                frm.Hide();
            }
            ResumeLayout(true);
            Efficiency frmEfficiency = new Efficiency()
            {
                MdiParent = this
            };
            frmEfficiency.WindowState = FormWindowState.Maximized;
            frmEfficiency.Show();

            if (!firstTimeClicked && history[currentPosition + 1] != null)
            {
                btnForward.Enabled = false;
                numOfOpenedForms = currentPosition + 1;
                history[numOfOpenedForms++] = frmEfficiency;
                for (var i = numOfOpenedForms; i < history.Count() - 1; i++)
                    history[i] = null;
            }
            else
                history[numOfOpenedForms++] = frmEfficiency;
            currentPosition = numOfOpenedForms - 1;

            LoadingInfo.CloseLoading();
        }

        private int currentPosition = 0;
        private bool firstTimeClicked = true;
        private void btnBack_Click(object sender, EventArgs e)
        {
            btnForward.Enabled = true;

            if (firstTimeClicked)
            {
                currentPosition = numOfOpenedForms - 1;
                firstTimeClicked = false;
            }

            if (currentPosition <= 0)
            {
                currentPosition = 0;
                btnBack.Enabled = false;
                return;
            }

            SuspendLayout();
            foreach (Form frm in MdiChildren)
            {
                frm.Hide();
            }

            if (history[currentPosition - 1] != null)
            {
                ResumeLayout(true);
                history[currentPosition - 1].Show();
                history[currentPosition - 1].WindowState = FormWindowState.Maximized;
                currentPosition--;
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            btnBack.Enabled = true;

            if (currentPosition.Equals(numOfOpenedForms - 1))
            {
                btnForward.Enabled = false;
                return;
            }

            SuspendLayout();
            foreach (Form frm in MdiChildren)
            {
                frm.Hide();
            }

            if (history[currentPosition + 1] != null)
            {
                ResumeLayout(true);
                history[currentPosition + 1].Show();
                history[currentPosition + 1].WindowState = FormWindowState.Maximized;
                currentPosition++;
            }
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            var frmOrders = new Orders();
            frmOrders.ShowDialog();
            frmOrders.Dispose();
        }

        private void btnOperators_Click(object sender, EventArgs e)
        {
            var frmOperators = new Operators();
            frmOperators.ShowDialog();
            frmOperators.Dispose();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            LoadingInfo.InfoText = "Loading reports...";
            LoadingInfo.ShowLoading();
            
            btnBack.Enabled = true;
            //SuspendLayout();
            foreach (Form frm in MdiChildren)
            {
                frm.Hide();
            }
            //ResumeLayout(true);
            Reports frmReports = new Reports()
            {
                MdiParent = this
            };
            frmReports.WindowState = FormWindowState.Maximized;
            frmReports.Show();

            if (!firstTimeClicked && history[currentPosition + 1] != null)
            {
                btnForward.Enabled = false;
                numOfOpenedForms = currentPosition + 1;
                history[numOfOpenedForms++] = frmReports;
                for (var i = numOfOpenedForms; i < history.Count() - 1; i++)
                    history[i] = null;
            }
            else
                history[numOfOpenedForms++] = frmReports;
            currentPosition = numOfOpenedForms - 1;

            LoadingInfo.CloseLoading();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("    You are about to exit the program !\n " +
                "   Are you sure? ", "ATENTION !!!", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                   foreach (var f in MdiChildren)
                    {
                      f.Close();
                    f.Dispose();
                   }
                   Close();
            }

            else this.Show();
        }

        private void btnStoptronic_Click(object sender, EventArgs e)
        {
            openStoptronic();
        }      

        }

    public class FileModel
        {
        public FileModel(string art, string ph)
            {
            Article = art;
            Phase = ph;
            }

        public string Article { get; set; }

        public string Phase { get; set; }
        }

    public static class Additional
    {   
        public static void FillTheFilter(DataGridView dgv,ComboBox cb, int cellNumber)
        {
            cb.Items.Clear();
            int i = 0;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                DataGridViewCell cell = row.Cells[cellNumber];
                if (cb.Items.Contains(cell.Value.ToString()) || cell.Value.ToString() == String.Empty)
                    continue;
                cb.Items.Insert(i, cell.Value.ToString());
            }
            cb.Items.Insert(i, String.Empty);
            if (cb.Items.Count != 0)
                cb.SelectedIndex = 0;
        }
        public static void DesignMyGrid(DataGridView myDataGridView)
        {
            myDataGridView.AllowUserToAddRows = false;
            myDataGridView.AllowUserToDeleteRows = false;
            myDataGridView.AllowUserToOrderColumns = false;
            myDataGridView.AllowUserToResizeRows = false;
            myDataGridView.AllowUserToResizeColumns = false;

            myDataGridView.ReadOnly = true; //disallow user to change data

            myDataGridView.BackgroundColor = Color.FromArgb(235, 235, 235);

            myDataGridView.MultiSelect = false;
            myDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            myDataGridView.RowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            myDataGridView.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            myDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            myDataGridView.EnableHeadersVisualStyles = true;
            myDataGridView.BorderStyle = BorderStyle.None;
            myDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            myDataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            myDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            myDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            myDataGridView.DataBindingComplete += delegate
            {
                //disallow manual sorting to follow production life-cycle
                foreach (DataGridViewColumn c in myDataGridView.Columns)
                {
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                myDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                myDataGridView.ColumnHeadersHeight = 50;

                //sets columns and rows appereance
                myDataGridView.GridColor = Color.Silver;
                myDataGridView.RowTemplate.Height = 25;
                myDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised; //Sunken
                myDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Raised; //Sunken
                myDataGridView.EnableHeadersVisualStyles = false;
                myDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Gold;
                myDataGridView.DefaultCellStyle.BackColor = myDataGridView.BackgroundColor;
                myDataGridView.RowHeadersVisible = false;
                myDataGridView.ColumnHeadersHeight = 30;

                for (var i = 0; i <= myDataGridView.Columns.Count - 1; i++)
                {
                    var c = myDataGridView.Columns[i];
                    c.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8);
                    c.HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 7, FontStyle.Regular);
                    c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    // enable this if you want to auto-resize all cells inside table
                    //AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                }
            };
        }

        internal static void FillTheFilter(object myDataGridView, ComboBox headerButton, int colIndex)
        {
            throw new NotImplementedException();
        }
    }

    }
