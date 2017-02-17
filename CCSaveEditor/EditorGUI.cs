using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace CCSaveEditor
{
    public partial class EditorGUI : Form
    {
        public delegate void dlgProgress(int value, string status);
        public event dlgProgress Progress;
        public Stats allstats;

        private List<PictureBox> icons = new List<PictureBox>();
        //private PictureBox iconsBase = new PictureBox();
        private bool[] upgBool = new bool[398];
        private bool[] achBool = new bool[266];
        private TextBox upgradeName = new TextBox();       
        private List<TextBox> textboxes;

        private int currentTab = 0;
        private int buildOffset = 0;
        private bool upgMod = false;
        private bool achMod = false;

        public EditorGUI(Stats data)
        {
            InitializeComponent();
            Progress += updateProgress;
            progressBar1.Visible = false;
            progLbl.Text = "";
            allstats = data;
            popMainTab(data.general);
            popBuildTab(data.buildings);
        }

        // ---------- POPULATE TABS ---------- //
        private void popMainTab(List<string> general)
        {
            textboxes = new List<TextBox>();
            verLabel.Text = "v" + general[0];
            string[] labels =
            {
                "Bank", "Baked (session)", "Big Cookie Clicks", "Golden Cookies (all time)", "Handmade Cookies",
                "Golden Cookies missed", "Cookies Forfeited", "Grandmatriarch Stage", "Pledge Timer", "Number of Resets",
                "Golden Cookies (session)",  "Cookies Withered", "Wrinklers Popped", "Santa Stage", "Reindeer Found",
                "Season Timer (frames)", "Seasons Bought", "Season", "Cookies Sucked", "Number of Wrinklers",
                "Prestige Level", "Heavenly Chips", "Permanent Upgrade I", "Permanent Upgrade II", "Permanent Upgrade III",
                "Permanent Upgrade IV", "Permanent Upgrade V", "Dragon Level", "Dragon Aura I", "Dragon Aura II"
            };

            sessLabel.Text = "Session Started: " +
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddMilliseconds(Convert.ToInt64(general[1]))
                .ToLocalTime()
                .ToString();
            sessLabel.Location = new Point(
                verLabel.Location.X + TextRenderer.MeasureText(verLabel.Text, verLabel.Font).Width + 45, 
                sessLabel.Location.Y);

            legacyLabel.Text = "Legacy Started: " +
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddMilliseconds(Convert.ToInt64(general[2]))
                .ToLocalTime()
                .ToString();
            legacyLabel.Location = new Point(
                sessLabel.Location.X + TextRenderer.MeasureText(sessLabel.Text, sessLabel.Font).Width + 45,
                legacyLabel.Location.Y);

            // Misc Stats
            nameLabel.Text = "Bakery Name: " + general[4];
            nameLabel.Location = new Point(
                legacyLabel.Location.X + TextRenderer.MeasureText(legacyLabel.Text, legacyLabel.Font).Width + 45,
                nameLabel.Location.Y);

            // Dynamically create labels and textboxes and divide them into to sides of the tab page
            int half = (labels.Length + 1) / 2;
            for (int i = 0; i < labels.Length; i++)
            {
                Label lbl = new Label();
                lbl.Text = labels[i];
                lbl.Location = new Point(35 + (i / half) * (tabControl1.Width / 2 - 35),                    // first half of the labels are 35px from left edge, second half are at the halfway point
                    47 + (i % half) * (tabControl1.Height - 94) / half);                                    // labels are evenly spaced with the top and bottom each 47px from the edges
                lbl.AutoSize = true;
                lbl.Font = new Font("Georgia", 9.75f, FontStyle.Bold);
                lbl.ForeColor = Color.Azure;
                tabControl1.TabPages[0].Controls.Add(lbl);

                textboxes.Add(new TextBox());
                textboxes[i].BorderStyle = BorderStyle.Fixed3D;
                textboxes[i].Location = new Point(lbl.Location.X + 200, lbl.Location.Y);                    // place textbox 200px from its label (arbitrary) and at the same Y location as the label
                textboxes[i].Text = addcommas(general[i + 5]);
                textboxes[i].Width = TextRenderer.MeasureText(textboxes[i].Text, textboxes[i].Font).Width;  // size the textbox to fit the text
                textboxes[i].Tag = textboxes[i].Text;
                textboxes[i].TextChanged += EditorGUI_TextChanged;
                //textboxes[i].Leave += EditorGUI_Leave;
                tabControl1.TabPages[0].Controls.Add(textboxes[i]);

                if (labels[i] == "Grandmatriarch Stage")
                {
                    //Label lbl2 = new Label();
                    //string gms = textboxes[i].Text;
                    //if (gms == "0") lbl2.Text = "(Appeased)";
                    //else if (gms == "1") lbl2.Text = "(Awoken)";
                    //else if (gms == "2") lbl2.Text = "(Displeased)";
                    //else if (gms == "3") lbl2.Text = "(Angered)";

                    //ComboBox cb = new ComboBox();
                    //cb.Items.Add("Appeased");
                    //cb.Items.Add("Awoken");
                    //cb.Items.Add("Displeased");
                    //cb.Items.Add("Angered");

                    //lbl2.Location = new Point(textboxes[i].Location.X + textboxes[i].Width + 10, lbl.Location.Y);
                    //lbl2.AutoSize = true;
                    //lbl2.Font = new Font("Georgia", 9.75f, FontStyle.Bold);
                    //lbl2.ForeColor = Color.Azure;
                    //tabControl1.TabPages[0].Controls.Add(lbl2);
                }
                else if (labels[i] == "Season Timer (frames)")
                {
                    Label lbl2 = new Label();
                    lbl2.Text = "frames = seconds x 30";
                    lbl2.Location = new Point(textboxes[i].Location.X + textboxes[i].Width + 5, lbl.Location.Y + 2);
                    lbl2.AutoSize = true;
                    lbl2.Font = new Font(FontFamily.GenericSansSerif, 8.5f, FontStyle.Italic);
                    lbl2.ForeColor = Color.Azure;
                    tabControl1.TabPages[0].Controls.Add(lbl2);
                }
                else if (labels[i] == "Season")
                {
                    ComboBox cb = new ComboBox();
                    cb.Items.Add("christmas");
                    cb.Items.Add("halloween");
                    cb.Items.Add("valentine's day");
                    cb.Items.Add("business day");
                    cb.Items.Add("easter");
                    cb.Items.Add("(none)");

                    cb.Location = textboxes[i].Location;
                    string txt = textboxes[i].Text;
                    if (cb.Items.Contains(txt)) cb.SelectedItem = txt;
                    else cb.SelectedItem = "(none)";
                    cb.Width = 100;
                    cb.SelectedValueChanged += Cb_SelectedValueChanged;
                    cb.Tag = i.ToString();
                    textboxes[i].Visible = false;
                    tabControl1.TabPages[0].Controls.Add(cb);
                }
                else if (labels[i] == "Prestige Level")
                {
                    Label lbl2 = new Label();
                    lbl2.Text = "The game recalculates this during import";
                    lbl2.Location = new Point(textboxes[i].Location.X + textboxes[i].Width + 5, lbl.Location.Y + 2);
                    lbl2.AutoSize = true;
                    lbl2.Font = new Font(FontFamily.GenericSansSerif, 8.5f, FontStyle.Italic);
                    lbl2.ForeColor = Color.Azure;
                    tabControl1.TabPages[0].Controls.Add(lbl2);
                }
                else if (labels[i].Contains("Permanent Upgrade"))
                {
                    ComboBox cb = new ComboBox();
                    string[] upg = Properties.Resources.upgrades.Split('\n');
                    for (int j = 0; j < upg.Length; j++)
                    {
                        cb.Items.Add(upg[j]);
                    }
                    cb.Items.Add("(none)");
                    cb.Location = textboxes[i].Location;
                    int id = Convert.ToInt32(textboxes[i].Text);
                    if (id > -1) cb.SelectedItem = upg[id];
                    else cb.SelectedItem = "(none)";
                    cb.Width = 150;
                    cb.SelectedValueChanged += Cb_SelectedValueChanged;
                    cb.Tag = i.ToString();
                    textboxes[i].Visible = false;
                    tabControl1.TabPages[0].Controls.Add(cb);
                }
                // increase buildOffset so we know which textbox starts with building data
                buildOffset++;                                                          
            }
        }

        private void popBuildTab(List<string> buildings)
        {
            string[] labels =
            {
                "Cursor", "Grandma", "Farm", "Mine", "Factory",
                "Bank", "Temple", "Wizard Tower", "Shipment", "Alchemy Lab",
                "Portal", "Time Machine", "Antimatter Condensor", "Prism"
            };

            int offset = textboxes.Count;
            for (int i = 0; i < labels.Length; i++)
            {
                Label lbl = new Label();
                lbl.Text = labels[i];
                lbl.Location = new Point(15, 30 + i * (tabControl1.Height - 60) / 14);  // 15px from the left edge, evenly vertically spaced and 30px away from top and bottom edges
                //lbl.Width = TextRenderer.MeasureText(lbl.Text, lbl.Font).Width;
                lbl.AutoSize = true;
                lbl.Font = new Font("Georgia", 9.75f, FontStyle.Bold);
                lbl.ForeColor = Color.Azure;
                //lbl.Name = "Label_" + i;
                tabControl1.TabPages[3].Controls.Add(lbl);

                // line up textboxes with header labels (with arbitrary buffers)
                textboxes.Add(new TextBox());
                textboxes[i + offset].Location = new Point(label34.Location.X + 12, lbl.Location.Y - 4);
                textboxes.Add(new TextBox());
                textboxes[i + offset + 1].Location = new Point(label35.Location.X + 12, lbl.Location.Y - 4);
                textboxes.Add(new TextBox());
                textboxes[i + offset + 2].Location = new Point(label36.Location.X - 4, lbl.Location.Y - 4);
                offset += 2;
            }

            // Same as popMainTab loop
            for (int i = 0; i < buildings.Count; i++)
            {
                textboxes[i + buildOffset].Font = new Font(FontFamily.GenericSansSerif, 10.0f);
                textboxes[i + buildOffset].Text = addcommas(buildings[i]);
                textboxes[i + buildOffset].Width = TextRenderer.MeasureText(textboxes[i + buildOffset].Text, textboxes[i + buildOffset].Font).Width;
                textboxes[i + buildOffset].Tag = textboxes[i + buildOffset].Text;
                textboxes[i + buildOffset].TextChanged += EditorGUI_TextChanged;
                textboxes[i + buildOffset].Leave += EditorGUI_Leave;
                tabControl1.TabPages[3].Controls.Add(textboxes[i + buildOffset]);
            }
        }
        
        private void popUpgradesTab(List<string> upgrades)
        {
            currentTab = 1;
            upgMod = true;
            progressBar1.Visible = true;

            //string[] names = File.ReadAllLines(@"..\..\upgrades.txt");
            string[] names = Properties.Resources.upgrades.Split('\n');
            tabControl1.TabPages[1].AutoScroll = true;

            if (!Directory.Exists("upgrades"))
            {
                MessageBox.Show("Unable to find folder containing upgrade icons. \nMake sure the 'upgrades' folder is in the same location as CCSaveEditor.exe",
                    "Missing Files",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            // 20x20 grid to display the 398 icons
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    int id = j + i * 20;
                    string path = "upgrades\\" + (id) + ".png";             // grab icon image
                    if (!File.Exists(path)) continue;

                    PictureBox pb = new PictureBox();
                    pb.Name = (id).ToString(); //icons.Count.ToString();
                    pb.Tag = names[(id)].Replace("\r","");
                    pb.Location = new Point(j * 48, i * 48);                // place icon in correct location on grid
                    pb.Size = new Size(48, 48);
                    pb.BackgroundImage = Image.FromFile(path);

                    pb.Click += Pb_Click;
                    pb.MouseHover += Pb_MouseEnter;
                    pb.MouseMove += Pb_MouseMove;
                    pb.MouseLeave += Pb_MouseLeave;

                    icons.Add(pb);
                    tabControl1.TabPages[1].Controls.Add(pb);
                    if (!upgBool[id] && upgrades[id] != "11")               // if upgrade isn't bought yet, fade the icon
                    {
                        upgBool[id] = false;
                        fadeIcon(id);
                    }
                    else
                        upgBool[id] = true;

                    // Increase progress bar while loading images
                    Progress?.Invoke(id * 100 / (upgBool.Length - 1), "Loading image " + (id + 1) + "/" + upgBool.Length);
                }
            }

            progressBar1.Visible = false;
            progLbl.Text = "";
        }

        private void popAchTab(List<string> ach)
        {
            currentTab = 2;
            achMod = true;
            progressBar1.Visible = true;

            //string[] names = File.ReadAllLines(@"..\..\achievements.txt");
            string[] names = Properties.Resources.achievements.Split('\n');
            tabControl1.TabPages[2].AutoScroll = true;

            if (!Directory.Exists("achievements"))
            {
                MessageBox.Show("Unable to find folder containing achievement icons. \nMake sure the 'achievements' folder is in the same location as CCSaveEditor.exe", 
                    "Missing Files",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    int id = j + i * 20;
                    string path = "achievements\\" + (id) + ".png";
                    if (!File.Exists(path)) continue;

                    PictureBox pb = new PictureBox();
                    pb.Name = (id).ToString(); //icons.Count.ToString();
                    pb.Tag = names[(id)].Replace("\r","");
                    pb.Location = new Point(j * 48, i * 48);
                    pb.Size = new Size(48, 48);
                    pb.BackgroundImage = Image.FromFile(path);

                    pb.Click += Pb_Click;
                    pb.MouseHover += Pb_MouseEnter;
                    pb.MouseMove += Pb_MouseMove;
                    pb.MouseLeave += Pb_MouseLeave;

                    icons.Add(pb);
                    tabControl1.TabPages[2].Controls.Add(pb);
                    if (!achBool[id] && ach[id] != "1")
                    {
                        achBool[id] = false;
                        fadeIcon(id);
                    }
                    else
                        achBool[id] = true;

                    Progress?.Invoke(id * 100 / (achBool.Length - 1), "Loading image " + (id + 1) + "/" + achBool.Length);
                }
            }

            progressBar1.Visible = false;
            progLbl.Text = "";
        }





        // ---------- MISC FUNCTIONS ---------- //
        private string shortenNum(string num)
        {
            string[] suffix =
            {
                "billion",
                "trillion",
                "quadrillion",
                "quintillion",
                "sextillion",
                "septillion",
                "octillion",
                "nonillion",
                "decillion",
                "undecillion"
            };
            string shortNum = "";
            if (num.Contains("e"))
            {
                int pow = Convert.ToInt32(num.Substring(num.IndexOf('e') + 2));
                int magn = pow % 3;
                double dNum = Convert.ToDouble(num.Substring(0, 5 + magn));
                shortNum = (dNum * Math.Pow(10, magn)).ToString() + " " + suffix[pow / 3 - 3];
            }
            else
            {
                int pow = num.Length - 1;
                if (pow < 9)
                    shortNum = addcommas(num);
                else
                {
                    int magn = pow % 3;
                    double dNum = Convert.ToDouble(num.Substring(0, 4 + magn));
                    shortNum = (dNum / Math.Pow(10, magn + 1)).ToString() + " " + suffix[pow / 3 - 3];
                }
            }
            return shortNum;
        }

        private string addcommas(string num)
        {
            string[] seasons = { "christmas", "halloween", "valentine's day", "business day", "easter" };
            if (num.Contains("e") || seasons.Contains(num)) return num;
            string res = "";
            for (int i = 0; i < num.Length; i++)
            {
                res = num[num.Length - 1 - i] + res;
                if ((i + 1) % 3 == 0 && i != num.Length - 1) res = "," + res;
            }
            return res;
        }

        private void fadeIcon(int id)
        {
            bool isenabled = false;
            if (currentTab == 1) isenabled = upgBool[id];
            else if (currentTab == 2) isenabled = achBool[id];
            Bitmap image = new Bitmap(icons[id].BackgroundImage);
            BitmapData bmpdata = image.LockBits(new Rectangle(new Point(0, 0), new Size(48, 48)), System.Drawing.Imaging.ImageLockMode.ReadWrite, image.PixelFormat);

            IntPtr ptr = bmpdata.Scan0;
            byte[] px = new byte[48 * 48 * 4];
            Marshal.Copy(ptr, px, 0, px.Length);
            for (int i = 0; i < 2304; i++)
            {
                if (px[i * 4 + 3] == 0) continue;
                if (isenabled)                  // upgrade is bought, increase opacity
                {
                    px[i * 4 + 3] = 255;
                }
                else                            // upgrade is not bought, decrease opacity
                {
                    px[i * 4 + 3] = 80;
                }
            }

            Marshal.Copy(px, 0, ptr, px.Length);
            image.UnlockBits(bmpdata);

            // necessary, otherwise the program poops out from memory
            icons[id].BackgroundImage.Dispose();
            icons[id].BackgroundImage = image;
        }





        // ---------- EVENT HANDLERS ---------- //
        private void EditorGUI_TextChanged(object sender, EventArgs e)
        {
            // If value is changed, make textbox yellow
            ((TextBox)sender).Width = TextRenderer.MeasureText(((TextBox)sender).Text, ((TextBox)sender).Font).Width;
            if (((TextBox)sender).Text != ((TextBox)sender).Tag.ToString())
            {
                ((TextBox)sender).BackColor = Color.LightGoldenrodYellow;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.White;
            }

            // Make sure text is aligned to be visible
            ((TextBox)sender).TextAlign = HorizontalAlignment.Right;
            ((TextBox)sender).TextAlign = HorizontalAlignment.Left;
        }

        private void EditorGUI_Leave(object sender, EventArgs e)
        {
            // if building "owned" is changed, change "bought" by the same amount otherwise it's just silly
            //int tb = textboxes.IndexOf((TextBox)sender);
            //if (tb >= buildOffset && (tb - buildOffset) % 3 == 0)
            //{
            //    int n0;
            //    int n1;
            //    int n2;
            //    if (int.TryParse(textboxes[tb].Text, out n1) && int.TryParse(allstats.buildings[tb - buildOffset + 1], out n2) && int.TryParse(allstats.buildings[tb - buildOffset], out n0))
            //    {
            //        textboxes[tb + 1].Text = (n2 + (n1 - n0)).ToString();
            //    }
            //}

        }

        private void Cb_SelectedValueChanged(object sender, EventArgs e)
        {
            string txt = ((ComboBox)sender).Text;
            List<string> upg = Properties.Resources.upgrades.Split('\n').ToList();
            if (upg.Contains(txt)) txt = upg.IndexOf(txt).ToString();
            else if (txt == "(none)") txt = "";

            textboxes[Convert.ToInt32(((ComboBox)sender).Tag)].Text = txt;
        }

        private void Pb_MouseLeave(object sender, EventArgs e)
        {
            Controls.Remove(upgradeName);
        }

        private void Pb_MouseEnter(object sender, EventArgs e)
        {
            string tag = ((PictureBox)sender).Tag.ToString();

            upgradeName.Size = TextRenderer.MeasureText(tag, upgradeName.Font);
            upgradeName.Location = new Point(
                MousePosition.X - this.DesktopLocation.X - upgradeName.Width / 2,
                MousePosition.Y - this.DesktopLocation.Y);
            upgradeName.Text = tag;
            upgradeName.Enabled = false;
            upgradeName.BackColor = Color.White;
            upgradeName.BringToFront();
            Controls.Add(upgradeName);
        }

        private void Pb_MouseMove(object sender, EventArgs e)
        {
            upgradeName.Location = new Point(
            MousePosition.X - this.DesktopLocation.X - upgradeName.Width / 2,
            MousePosition.Y - this.DesktopLocation.Y - 10);
            upgradeName.BringToFront();
        }

        private void Pb_Click(object sender, EventArgs e)
        {
            //Point mouse = MousePosition;
            //mouse.X -= DesktopLocation.X;
            //mouse.Y -= DesktopLocation.Y + 46;

            //int x = mouse.X / 48;
            //int y = mouse.Y / 48;

            //int icon = x + y * 20;

            //icons[icon] = !icons[icon];
            //fadeIcon(icon, x * 48, y * 48);
            
            int iconNum = Convert.ToInt32(((PictureBox)sender).Name);
            if (currentTab == 1) upgBool[iconNum] = !upgBool[iconNum];
            else if (currentTab == 2) achBool[iconNum] = !achBool[iconNum];
            fadeIcon(iconNum);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Collect all data to generate new save
            for (int i = 0; i < buildOffset; i++)
            {
                allstats.general[i + 5] = textboxes[i].Text.Replace(",", "");
            }
            for (int i = 0; i < allstats.buildings.Count; i++)
            {
                allstats.buildings[i] = textboxes[i + buildOffset].Text.Replace(",", "");
            }

            if (upgMod)
            {
                for (int i = 0; i < upgBool.Length; i++)
                {
                    if (upgBool[i])
                        allstats.upgrades[i] = "11";
                    else
                    {
                        if (allstats.upgrades[i] == "11")
                            allstats.upgrades[i] = "10";
                        //else
                        //    allstats.upgrades[i] = "00";
                    }
                }
            }
            if (achMod)
            {
                for (int i = 0; i < achBool.Length; i++)
                {
                    if (achBool[i])
                        allstats.achievements[i] = "1";
                    else
                        allstats.achievements[i] = "0";
                }
            }

            this.Close();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            // Only populate the upgrades and achievements tabs when they are selected
            // Otherwise clear out the icons
            // This should prevent memory issues
            if (tabControl1.SelectedTab == tabControl1.TabPages[0] || tabControl1.SelectedTab == tabControl1.TabPages[3])
            {
                icons.Clear();
                if (currentTab == 1)
                {
                    for (int i = tabControl1.TabPages[1].Controls.Count - 1; i >= 0; i--)
                    {
                        tabControl1.TabPages[1].Controls.RemoveAt(i);
                    }
                }
                else if (currentTab == 2)
                {
                    for (int i = tabControl1.TabPages[2].Controls.Count - 1; i >= 0; i--)
                    {
                        tabControl1.TabPages[2].Controls.RemoveAt(i);
                    }
                }
                currentTab = 0; // doesn't matter if it's 0 or 3, they act the same
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
                if (currentTab == 2)
                {
                    icons.Clear();
                    for (int i = tabControl1.TabPages[2].Controls.Count - 1; i >= 0; i--)
                    {
                        tabControl1.TabPages[2].Controls.RemoveAt(i);
                    }
                }
                //for (int i = 0; i < tabControl1.TabPages[0].Controls.Count; i++)
                //{               
                //    tabControl1.TabPages[0].Controls[i].Visible = false;                  
                //}
                popUpgradesTab(allstats.upgrades);

            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages[2])
            {
                if (currentTab == 1)
                {
                    icons.Clear();
                    for (int i = tabControl1.TabPages[1].Controls.Count - 1; i >= 0; i--)
                    {
                        tabControl1.TabPages[1].Controls.RemoveAt(i);
                    }
                }
                popAchTab(allstats.achievements);
            }
        }

        private void updateProgress(int value, string status)
        {
            progressBar1.Value = value;
            progLbl.Text = status;
        }

    }
}
