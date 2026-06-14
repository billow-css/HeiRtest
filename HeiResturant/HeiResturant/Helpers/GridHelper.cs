using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HeiResturant.Helpers
{
    public static class GridHelper
    {
        /// <summary>
        /// 启用表格编辑。readOnlyColumnNames 中的列锁定；
        /// 绑定到只读字段的列会自动保持只读，不会强行改为可编辑。
        /// </summary>
        public static void EnableEdit(DataGridView grid, params string[] readOnlyColumnNames)
        {
            grid.ReadOnly = false;
            grid.AllowUserToAddRows = false;
            grid.EditMode = DataGridViewEditMode.EditOnEnter;

            if (grid.Columns.Count == 0) return;

            var readOnlySet = new HashSet<string>(readOnlyColumnNames ?? Array.Empty<string>());

            foreach (DataGridViewColumn col in grid.Columns)
            {
                if (readOnlySet.Contains(col.Name))
                {
                    col.ReadOnly = true;
                    continue;
                }

                try
                {
                    col.ReadOnly = false;
                }
                catch (InvalidOperationException)
                {
                    // 绑定到只读属性（如计算列 SubTotal）时不能设为可编辑
                    col.ReadOnly = true;
                }
            }
        }
    }
}
