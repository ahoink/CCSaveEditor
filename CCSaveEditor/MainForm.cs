using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CCSaveEditor
{
    public partial class MainForm : Form
    {

        Stats allstats = new Stats();

        public MainForm()
        {
            InitializeComponent();
        }

        private void main()
        {
        }   

        // Convert save string from base64 to readable text
        private string decodeSave(string str64)
        {
            var bytes = Encoding.ASCII.GetBytes(str64);
            str64 = str64.Replace("%21END%21", "").Replace("%3D", "=");
            var bytes64 = Convert.FromBase64String(str64);
            string plainText = Encoding.UTF8.GetString(bytes64);
            return plainText;
        }

        // Encode the data back into a base64 string
        private string encodeSave(string rawdata)
        {
            string[] datasplit = rawdata.Split('|');

            // Basics
            rawdata.Replace(datasplit[0], allstats.general[0]);
            rawdata =  rawdata.Replace(datasplit[2], 
                        allstats.general[1] + ";" + 
                        allstats.general[2] + ";" + 
                        allstats.general[3] + ";" + 
                        allstats.general[4]);

            // Stats
            string data = "";
            string[] statsplit = datasplit[4].Split(';');
            int[] skip = { 6, 7, 10, 12, 13, 27, 28, 29};  // temporary fix
            int genind = 5;
            for (int i = 0; i < statsplit.Length; i++)
            {
                if (skip.Contains(i)) data += statsplit[i] + ";";
                else if (genind < allstats.general.Count)
                {
                    data += allstats.general[genind] + ";";
                    genind++;
                }
                else data += statsplit[i] + ";";
            }           
            rawdata = rawdata.Replace(datasplit[4], data.Remove(data.Length - 2));  // instead of starting from scratch, just replace the old readable data with the new data

            // Buildings
            data = "";
            for (int i = 0; i < allstats.buildings.Count; i++)
            {
                if ((i + 1) % 3 == 0)
                    data += allstats.buildings[i] + ";";
                else data += allstats.buildings[i] + ",";
            }
            rawdata = rawdata.Replace(datasplit[5], data);

            // Upgrades
            byte[] upgbytes = Encoding.Unicode.GetBytes(datasplit[6]);
            int up = 0;
            for (int i = 0; i < upgbytes.Length; i++)
            {
                byte b = upgbytes[i];
                if (b == 0 || b == 225) continue;
                string bin = Convert.ToString(b, 2);
                if (bin.Substring(0, 6) == "111001")
                {
                    upgbytes[i] = Convert.ToByte("111001" + allstats.upgrades[up], 2);  // convert binary bits back into bytes
                    up++;
                }
                else
                {
                    upgbytes[i] = Convert.ToByte("10" + allstats.upgrades[up] + allstats.upgrades[up + 1] + allstats.upgrades[up + 2], 2);
                    up += 3;
                }
            }
            data = Encoding.Unicode.GetString(upgbytes);
            rawdata = rawdata.Replace(datasplit[6], data);

            // Achievements
            byte[] achbytes = Encoding.Unicode.GetBytes(datasplit[7]);
            int ach = 0;
            for (int i = 0; i < achbytes.Length; i++)
            {
                byte b = achbytes[i];
                if (b == 0) continue;
                string bin = Convert.ToString(b, 2);
                if (bin.Substring(0, 6) == "111001")
                {
                    achbytes[i] = Convert.ToByte("111001" + allstats.achievements[ach] + allstats.achievements[ach + 1], 2);
                    ach += 2;
                }
                else
                {
                    //string strbyte = bin.Remove(2);
                    string strbyte = "10";
                    for (int j = 0; j < 6; j++)
                        strbyte += allstats.achievements[ach + j];
                    achbytes[i] = Convert.ToByte(strbyte, 2);
                    ach += 6;
                }
            }
            data = Encoding.Unicode.GetString(achbytes);
            rawdata = rawdata.Replace(datasplit[7], data);

            // Convert back to base64
            var newBytes = Encoding.UTF8.GetBytes(rawdata);
            var str64 = Convert.ToBase64String(newBytes);
            str64 = str64.Replace("=", "%3D") + "%21END%21";

            return str64;
        }

        private int parseData(string data)
        {
            string[] splitPipe = data.Split('|');

            allstats.general = new List<string>();

            // Basics
            allstats.general.Add(splitPipe[0]);         // version
            if (splitPipe[0] != "2.002")
            {
                //MessageBox.Show("Sorry, this editor only supports game saves from v2.002", "Uh-oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return 1;
                MessageBox.Show("This editor has only been tested on game saves from v2.002", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            string[] times = splitPipe[2].Split(';');
            allstats.general.Add(times[0]);             // started session
            allstats.general.Add(times[1]);             // started legacy
            allstats.general.Add(times[2]);             // save created
            allstats.general.Add(times[3]);             // name

            // Stats
            string[] stats = splitPipe[4].Split(';');
            allstats.general.Add(stats[0]);             // current bank
            allstats.general.Add(stats[1]);             // baked this session
            allstats.general.Add(stats[2]);             // big cookie clicks
            allstats.general.Add(stats[3]);             // golden cookie clicks
            allstats.general.Add(stats[4]);             // handmade cookies
            allstats.general.Add(stats[5]);             // golden cookies missed
            allstats.general.Add(stats[8]);             // forfeited for ascension
            allstats.general.Add(stats[9]);             // grandmatriarch status
            allstats.general.Add(stats[11]);            // pledge time left
            allstats.general.Add(stats[14]);            // number of resets
            allstats.general.Add(stats[15]);            // golden clicks this session
            allstats.general.Add(stats[16]);            // cookies withered
            allstats.general.Add(stats[17]);            // wrinklers popped
            allstats.general.Add(stats[18]);            // Santa Stage
            allstats.general.Add(stats[19]);            // Reindeer found
            allstats.general.Add(stats[20]);            // Season timer
            allstats.general.Add(stats[21]);            // Seasons bought
            allstats.general.Add(stats[22]);            // Season
            allstats.general.Add(stats[23]);            // Cookies sucked
            allstats.general.Add(stats[24]);            // Number of wrinklers
            allstats.general.Add(stats[25]);            // Prestige Level
            allstats.general.Add(stats[26]);            // Heavenly Chips
            allstats.general.Add(stats[30]);            // Permanent Upgrade Slot I
            allstats.general.Add(stats[31]);            // PU II
            allstats.general.Add(stats[32]);            // PU III
            allstats.general.Add(stats[33]);            // PU IV
            allstats.general.Add(stats[34]);            // PU V
            allstats.general.Add(stats[35]);            // Krumblor Stage
            allstats.general.Add(stats[36]);            // Krumblor Aura I
            allstats.general.Add(stats[37]);            // Krumblor Aura II     
            //27 = HC spent

            // Buildings
            string[] builds = splitPipe[5].Split(';');
            allstats.buildings = new List<string>();
            for (int i = 0; i < 14; i++)
            {
                string[] buildStats = builds[i].Split(',');
                allstats.buildings.Add(buildStats[0]);  // owned
                allstats.buildings.Add(buildStats[1]);  // bought
                allstats.buildings.Add(buildStats[2]);  // cookies produced
            }

            // Upgrades
            byte[] upBytes = Encoding.Unicode.GetBytes(splitPipe[6]);   // upgrades are binary
            allstats.upgrades = new List<string>();
            foreach (byte b in upBytes)
            {
                if (b == 0 || b == 225) continue;
                string bin = Convert.ToString(b, 2);
                if (bin.Substring(0, 6) == "111001")                    // every third byte begins with these bits, for some reason
                {
                    allstats.upgrades.Add(bin.Substring(6));
                }
                else
                {
                    allstats.upgrades.Add(bin.Substring(2, 2));         // every upgrade has two bits: the first says if it's unlocked and the second says if it's bought
                    allstats.upgrades.Add(bin.Substring(4, 2));
                    allstats.upgrades.Add(bin.Substring(6, 2));
                }
            }

            // Achievements
            byte[] achBytes = Encoding.Unicode.GetBytes(splitPipe[7]);  // achievements are also binary
            allstats.achievements = new List<string>();
            foreach (byte b in achBytes)
            {
                if (b == 0) continue;
                string bin = Convert.ToString(b, 2);
                if (bin.Substring(0, 6) == "111001")
                {
                    allstats.achievements.Add(bin[6].ToString());
                    allstats.achievements.Add(bin[7].ToString());
                }
                else
                {
                    for (int i = 2; i < 8; i++)
                        allstats.achievements.Add(bin[i].ToString());   // achievements only have 1 bit to say whether or not it's been unlocked
                }
            }

            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Decode the save string and open the editor
            string rawdata = decodeSave(textBox1.Text);
            if (parseData(rawdata) != 0) return;
            EditorGUI gui = new EditorGUI(allstats);
            gui.ShowDialog();
            allstats = gui.allstats;
            string newdata = encodeSave(rawdata);
            textBox2.Text = newdata;
        }
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.SelectAll();
        }

        
    }
}
