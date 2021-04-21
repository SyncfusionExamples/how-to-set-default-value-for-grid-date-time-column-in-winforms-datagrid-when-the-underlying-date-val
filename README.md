# How to set default value for GridDateTimeColumn in WinForms DataGrid (SfDataGrid) when the underlying date value is null?

## About the sample

This sample illustrates how to set default value for GridDateTimeColumn in WinForms DataGrid (SfDataGrid) when the underlying date value is null.

[WinForms DataGrid](https://www.syncfusion.com/winforms-ui-controls/datagrid) (SfDataGrid) doesn’t have any direct support to set default value for the [GridDateTimeColumn](https://help.syncfusion.com/cr/windowsforms/Syncfusion.WinForms.DataGrid.GridDateTimeColumn.html#%22%22).  However, it is possible to assign a default value to the DateTime column of the underlying data when the date value is null or empty by creating a custom renderer for DateTime column.

```C#

public Form1()
{
    InitializeComponent();

    this.sfDataGrid1.CellRenderers["DateTime"] = new CustomDateTimeRenderer(this.sfDataGrid1);
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

```

## Requirements to run the demo

Visual Studio 2015 and above versions
