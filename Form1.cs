using SfDataGrid_Demo;
using Syncfusion.WinForms.Controls;
using Syncfusion.WinForms.DataGrid;
using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid.Enums;
using System.Drawing;
using Syncfusion.WinForms.DataGrid.Renderers;
using Syncfusion.WinForms.GridCommon.ScrollAxis;
using Syncfusion.Data.Extensions;
using System.Data;
using Syncfusion.WinForms.DataGrid.Styles;
using Syncfusion.Data;
using Syncfusion.WinForms.Input;

namespace SfDataGrid_Demo
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public partial class Form1 : Form
    {
        #region Constructor

        /// <summary>
        /// Initializes the new instance for the Form.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            this.sfDataGrid1.CellRenderers["DateTime"] = new CustomDateTimeRenderer(this.sfDataGrid1);

            this.sfDataGrid1.DataSource = GetDataTable();

        }

        #endregion

        private DataTable GetDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("OrderID", typeof(int));
            dataTable.Columns.Add("CustomerName", typeof(string));
            dataTable.Columns.Add("CustomerID", typeof(string));
            dataTable.Columns.Add("Country", typeof(string));
            dataTable.Columns.Add("OrderDate", typeof(DateTime));
            dataTable.Rows.Add(1001, "Maria Anders", "ALFKI", "Germany", null);
            dataTable.Rows.Add(1002, "Ana Trujilo", "ANATR", "Mexico", null);
            dataTable.Rows.Add(1003, "Antonio Moreno", "ENDGY", "Mexico", null);
            dataTable.Rows.Add(1004, "Thomas Hardy", "ANTON", "UK", null);
            dataTable.Rows.Add(1005, "Christina Berglund", "BERGS", "Sweden", null);
            dataTable.Rows.Add(1006, "Hanna Moos", "BLAUS", "Germany", null);
            dataTable.Rows.Add(1007, "Frederique Citeaux", "BLONP", "France", null);
            dataTable.Rows.Add(1008, "Martin Sommer", "BOLID", "Spain", null);
            dataTable.Rows.Add(1009, "Laurence Lebihan", "BONAP", "France", null);
            dataTable.Rows.Add(1010, "Kathryn", "BOTTM", "Canada", null);
            dataTable.Rows.Add(1011, "Tamer", "XDKLF", "UK", null);
            dataTable.Rows.Add(1012, "Martin", "QEUDJ", "US", null);
            dataTable.Rows.Add(1013, "Nancy", "ALOPS", "France", null);
            dataTable.Rows.Add(1014, "Janet", "KSDIO", "Canada", null);
            dataTable.Rows.Add(1015, "Dodsworth", "AWSDE", "Canada", null);
            dataTable.Rows.Add(1016, "Buchanan", "CDFKL", "Germany", null);
            dataTable.Rows.Add(1017, "Therasa", "WSCJD", "Canada", null);
            dataTable.Rows.Add(1018, "Margaret", "PLSKD", "UK", null);
            dataTable.Rows.Add(1019, "Anto", "CCDSE", "Sweden", null);
            dataTable.Rows.Add(1020, "Edward", "EWUJG", "Germany", null);
            return dataTable;
        }
    }

    public class CustomDateTimeRenderer : GridDateTimeCellRenderer
    {
        SfDataGrid DataGrid { get; set; }
        public CustomDateTimeRenderer(SfDataGrid dataGrid)
        {
            this.DataGrid = dataGrid;
        }
        protected override void OnRender(Graphics paint, Rectangle cellRect, string cellValue, CellStyleInfo style, DataColumnBase column, RowColumnIndex rowColumnIndex)
        {
            if(string.IsNullOrEmpty(cellValue))
            {
                var recordIndex = this.TableControl.ResolveToRecordIndex(rowColumnIndex.RowIndex);
                object data = null;
                if (recordIndex < 0)
                    return;
                if (this.DataGrid.View.TopLevelGroup != null)
                {
                    var record = this.DataGrid.View.TopLevelGroup.DisplayElements[recordIndex];
                    if (!record.IsRecords)
                        return;
                    data = (record as RecordEntry).Data;
                }
                else
                {
                    data = this.DataGrid.View.Records.GetItemAt(recordIndex);
                }

                if (data != null)
                {
                    this.DataGrid.View.GetPropertyAccessProvider().SetValue(data, column.GridColumn.MappingName, DateTime.Now);
                }
            }

            base.OnRender(paint, cellRect, cellValue, style, column, rowColumnIndex);
        }
    }
}
