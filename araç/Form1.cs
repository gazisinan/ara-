using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace araç
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ShowList();
        }

        public List<Arac> aracs = new List<Arac>()
        {
            new Arac()
            {
                Plaka = "06 AA 4444" ,
                Marka = "BMW",
                Model = "X5",
                Yakıt = "Benzin",
                Renk = "Siyah",
                Vites = "Otomatik",
                KasaTipi = "SUV" ,
            },
            new Arac()
            {
                Plaka = "34 ABC 123" ,
                Marka = "Volkswagen",
                Model = "Tiguan",
                Yakıt = "Benzin",
                Renk = "Beyaz",
                Vites = "Otomatik",
                KasaTipi = "SUV" ,
            }
        };

        public void ShowList()
        {
            listView1.Items.Clear();
            foreach (Arac arac in aracs)
            {
                AddAracToListView(arac);
            }
        }

        public void AddAracToListView(Arac arac)
        {
            ListViewItem item = new ListViewItem(new string[]
                    {
                arac.Plaka,
                       arac.Marka,
                       arac.Model,
                       arac.Yakıt,
                       arac.Renk,
                       arac.Vites,
                       arac.KasaTipi,
                    });

            item.Tag = arac;
            listView1.Items.Add(item);
                 
        }

        void EditAracOnListView(ListViewItem aItem, Arac arac)
        {
            aItem.SubItems[0].Text = arac.Marka;
            aItem.SubItems[1].Text = arac.Model;
            aItem.SubItems[2].Text = arac.Yakıt;
            aItem.SubItems[3].Text = arac.Renk;
            aItem.SubItems[4].Text = arac.Vites;
            aItem.SubItems[5].Text = arac.KasaTipi;

            aItem.Tag = arac;
        }
        private void AddCommand(object sender, EventArgs e)
        {
            FrmArac frm = new FrmArac() 
            { 
                Text = "Araç Ekle",
                StartPosition = FormStartPosition.CenterParent,
                arac = new Arac()
            };

            if (frm.ShowDialog()== DialogResult.OK)
            {
                aracs.Add(frm.arac);
                AddAracToListView(frm.arac);
            }
        }

        private void EditCommand(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            ListViewItem aItem = listView1.SelectedItems[0];
            Arac secili=aItem.Tag as Arac;
            FrmArac frm = new FrmArac()
            {
                Text = "Araç Düzenle",
                StartPosition = FormStartPosition.CenterParent,
                arac = Clone (secili),
            };

            if (frm.ShowDialog() == DialogResult.OK)
            {
                secili = frm.arac;
                EditAracOnListView(aItem, secili);
            }
        }

        Arac Clone(Arac arac)
        {
            return new Arac()
            {
                id = arac.ID,
                Marka = arac.Marka,
                Model = arac.Model,
                Yakıt = arac.Yakıt,
                Renk = arac.Renk,
                Vites = arac.Vites,
                KasaTipi = arac.KasaTipi,
                
            };
        }

        private void DeleteCommand(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            ListViewItem aItem = listView1.SelectedItems[0];
            Arac secili = aItem.Tag as Arac;

            MessageBox.Show("Silmeyi Onayla");
    
        }

        private void SaveCommand(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog()
            {
                Filter = "json Formatı |*.json|Xml Formatı|*.xml",
            };

            if(sf.ShowDialog() == DialogResult.OK)
            {
                if (sf.FileName.EndsWith("json"))
                {
                    string data = jsonSerializer.Serialize(aracs);
                    File.WriteAllText(sf.FileName, data);
                }

                else if (sf.FileName.EndsWith("xml"))
                {
                    StreamWriter sw = new StreamWriter(sf.FileName);
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Arac>));
                    serializer.Serialize(sw, aracs);
                    sw.Close();

                }
                ShowList();
            }
              
        }

        private void LoadCommand(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog()
            {
                Filter = "Json, Xml Formatları|*.json;*xml",
            };

            if (of.ShowDialog() == DialogResult.OK)
            {
                if(of.FileName.ToLower().EndsWith("json"))
                {
                    string jsondata = File.ReadAllText(of.FileName);
                    aracs = JsonSerializer.Deserialize<List<Arac>>(jsondata);
                }

                else if (of.FileName.ToLower().EndsWith("xml"))
                {
                    StreamReader sr = new StreamReader(of.FileName);
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Arac>));
                    aracs = (List<Arac>)serializer.Deserialize(sr);
                    sr.Close();
                }
            }
            
        }
    }



    [Serializable]
     public class Arac
    {
        public string id;

        public string ID
        {
            get
            {
                if (id == null)
                    id = Guid.NewGuid().ToString();
                return id;
            }
            set { id = value; }
        }
        public string Plaka { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public string Yakıt { get; set; }
        public string Renk { get; set; }
        public string Vites { get; set; }
        public string KasaTipi { get; set; }
    }

}







