using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exacta
{
    class MyTableView : DataGridView
    {
        public MyTableView()
        {
           // this.DoubleBuffered(true);
            //dissalow user access to data architecture
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToOrderColumns = false;
            AllowUserToResizeRows = false;
            AllowUserToResizeColumns = false;
            ReadOnly = true; //disallow user to change data

            BackgroundColor = Color.FromArgb(235, 235, 235); ;

            MultiSelect = false;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            RowsDefaultCellStyle.SelectionForeColor = Color.White;
            RowHeadersDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            EnableHeadersVisualStyles = true;
            BorderStyle = BorderStyle.None;

            DataBindingComplete += delegate
            {
                //disallow manual sorting to follow production life-cycle

                foreach (DataGridViewColumn c in Columns)
                {
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                ColumnHeadersHeight = 50;

                //sets columns and rows appereance
                GridColor = Color.Silver;
                RowTemplate.Height = 25;
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                CellBorderStyle = DataGridViewCellBorderStyle.Single;
                //EnableHeadersVisualStyles = false;
                ColumnHeadersDefaultCellStyle.BackColor = Color.Gold;
                DefaultCellStyle.BackColor = BackgroundColor;
                RowHeadersVisible = false;

                for (var i = 0; i <= Columns.Count - 1; i++)
                {
                    var c = Columns[i];
                    c.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8);
                    c.HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 7, FontStyle.Regular);
                    c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft;

                    // enable this if you want to auto-resize all cells inside table
                    AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                }
            };

            //CellFormatting += Dgv_CellFormation;
        }
    }
}
