using LGHSMSBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LGCIM.Forms
{
    public partial class CassetteMAPForm : Form
    {
        public ClientCIM cim { get; set; }
        public List<PortItem> PortItems { get; set; }
        public PortItem SelectedPort { get; set; }

        public CassetteMAPForm()
        {
            InitializeComponent();
        }

        private void CassetteMAPForm_Load(object sender, EventArgs e)
        {
            cim.ShowCassetteMAP += Cim_ShowCassetteMAP;
        }

        private void Cim_ShowCassetteMAP(object sender, CassetteArgs e)
        {
            if (InvokeRequired)
                BeginInvoke((Action)delegate { Cim_ShowCassetteMAP(sender, e); });

            else
            {
                PortItems = new List<PortItem>();
                foreach (var cm in e.CassetteMap)
                {
                    var portItem = new PortItem
                    {
                        LOTID = cm.LOTID,
                        CSTID = cm.CSTID,
                        PORTID = cm.PORTID,
                        MAP = cm.MAP,
                        CSTREADY = cm.CSTREADY
                    };
                    var slot = ReadMAP(portItem);
                    portItem.SlotItems.AddRange(slot);

                    PortItems.Add(portItem);
                }

                portItemBindingSource.DataSource = PortItems;
                portItemBindingSource.ResetBindings(false);
            }
        }

        private List<SlotItem> ReadMAP(PortItem port)
        {
            var result = new List<SlotItem>();

            for(int i = 0; i < port.MAP.Length; i++)
            {
                if (port.MAP[i] == '0')
                    continue;

                result.Add(new SlotItem
                {
                    LOTID = port.LOTID,
                    CSTID = port.CSTID,
                    PORTID = port.PORTID,
                    SLOTID = $"{i + 1}",
                });
            }

            return result;
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            cim.SendS6F69();
        }

        private void portDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || PortItems == null)
                return;

            var slotItem = PortItems[e.RowIndex].SlotItems;
            SelectedPort = new PortItem();
            SelectedPort = PortItems[e.RowIndex];

            slotItemBindingSource.DataSource = slotItem;
            slotItemBindingSource.ResetBindings(false);
        }

        private void reqButton_Click(object sender, EventArgs e)
        {
            if(SelectedPort == null)
            {
                MessageBox.Show("선택된 Slot이 없습니다.");
                return;
            }

            List<SLOTITEM_SAMPLE> reportSlot = new List<SLOTITEM_SAMPLE>();
            foreach(var s in SelectedPort.SlotItems)
            {
                if (s.Select == true)
                {
                    var slot = new SLOTITEM_SAMPLE
                    {
                        LOTID = s.LOTID,
                        CSTID = s.CSTID,
                        PORTID = s.PORTID,
                        SLOTID = s.SLOTID,
                    };
                    reportSlot.Add(slot);
                }
            }

            if(reportSlot.Count == 0)
            {
                MessageBox.Show("요청할 Sample Slot을 선택해주세요.");
                return;
            }

            if(reportSlot.Count > 1)
            {
                MessageBox.Show("하나의 Slot만 선택해야 합니다.");
                reportSlot.Clear();
                return;
            }

            cim.SendS6F71(reportSlot[0]);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class PortItem
    {
        public PortItem()
        {

        }

        public string LOTID { get; set; } = "";
        public string CSTID { get; set; } = "";
        public byte PORTID { get; set; } = 0;
        public string MAP { get; set; } = "1";
        public string CSTREADY { get; set; } = "1";
        public List<SlotItem> SlotItems { get; set; } = new List<SlotItem>();
    }

    public class SlotItem
    {
        public SlotItem()
        {

        }

        public bool Select { get; set; } = false;
        public string LOTID { get; set; } = "";
        public string CSTID { get; set; } = "";
        public byte PORTID { get; set; } = 0;
        public string SLOTID { get; set; } = "1";
    }
}
