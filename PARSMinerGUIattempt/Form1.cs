using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace parsMG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 1200;

            startBtn.Enabled = false;
            startBtn.Text = "Initializing pools...";

            addressTb.Text = parsMG.Properties.Settings.Default.savedAddress;
            cpuMiningCheck.Checked = parsMG.Properties.Settings.Default.cpuCb;
            gpuMiningCheck.Checked = parsMG.Properties.Settings.Default.gpuCb;
            hardwareCb.SelectedIndex = parsMG.Properties.Settings.Default.hwType;
            refreshTimeNum.Value = parsMG.Properties.Settings.Default.statsSecs;
            writeLogCheck.Checked = parsMG.Properties.Settings.Default.logCb;
            showCLICheck.Checked = parsMG.Properties.Settings.Default.cliCb;
            minerSelectionCb.SelectedIndex = parsMG.Properties.Settings.Default.minerPreference;

            initPools();
        }

        string currentPoolList;
        Queue<Process> activeMiners = new Queue<Process>();

        private void initPools()
        {
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(writePoolsToList);
            wc.DownloadStringAsync(new Uri("https://raw.githubusercontent.com/ParsiCoin/parsminer-gui/master/parsicoin-pools.json"));
        }

        private async void writePoolsToList(object sender, DownloadStringCompletedEventArgs e)
        {
            currentPoolList = e.Result;
            List<string[]> poolInfoList;
            Task<List<string[]>> poolInfoTask = new Task<List<string[]>>(GetPoolInfo);
            poolInfoTask.Start();
            poolInfoList = await poolInfoTask;

            foreach (string[] poolInfo in poolInfoList)
            {
                if (poolInfo[0] != "" && poolInfo[1] != "")
                {
                    PoolEntry entryElement = new PoolEntry(poolInfo[0], poolInfo[1], poolInfo[2]);
                    entryElement.setAutoUpdate(poolStatsUpdateCb.Checked);
                    entryElement.selectedCb.Enabled = selectionModeCb.Text == "manual selection" ? true : false;

                    entryElement.selectedCb.Checked = parsMG.Properties.Settings.Default.savedPools.Contains(poolInfo[2]);

                    entryElement.Dock = DockStyle.Top;
                    poolListPanel.Controls.Add(entryElement);
                }
            }

            startBtn.Enabled = true;
            startBtn.Text = "Start mining!";
        }

        private List<string[]> GetPoolInfo()
        {
            List<string[]> res = new List<string[]>();

            string[] poolListElements = currentPoolList.Split('{');
            foreach(string poolInfoEntry in poolListElements)
            {
                // can only support forknote pools this way
                if (poolInfoEntry.Contains("url") && poolInfoEntry.Contains("api") && poolInfoEntry.Contains("forknote"))
                { 
                    string[] pool = new string[3];

                    pool[0] = poolInfoEntry.Split('"')[7];
                    pool[1] = poolInfoEntry.Split('"')[11];
                    pool[2] = poolInfoEntry.Split('"')[19];

                    res.Add(pool);
                }
            }

            return res;
        }

        private List<string> GetSelectedPools()
        {
            List<string> selectedPools = new List<string>();

            foreach (PoolEntry pe in poolListPanel.Controls)
            {
                if (pe.selectedCb.Checked)
                {
                    selectedPools.Add(pe.getMiningAddress());
                }
            }

            return selectedPools;
        }

        private void AnalyzePoolsByCondition(object sender, EventArgs e)
        {
            switch (selectionModeCb.Text)
            {
                case "lower ping":
                    //int pingAvg = 0;

                    //foreach (PoolEntry pe in poolListPanel.Controls)
                    //{
                    //    pe.selectedCb.Enabled = false;
                    //    if (pe.getPing() != -1) pingAvg = pingAvg == 0 ? pe.getPing() : (pingAvg + pe.getPing()) / 2;
                    //}

                    foreach (PoolEntry pe in poolListPanel.Controls)
                    {
                        pe.selectedCb.Enabled = false;
                        if (pe.getPing() != -1)
                        {
                            if (pe.getPing() < 500)
                            {
                                pe.selectedCb.Checked = true;
                            }
                            else pe.selectedCb.Checked = false;
                        }
                        else pe.selectedCb.Checked = false;
                    }

                    break;
                case "smaller payouts":
                    //double payAvg = 0;

                    //foreach (PoolEntry pe in poolListPanel.Controls)
                    //{
                    //    pe.selectedCb.Enabled = false;
                    //    if (pe.getMinPayout() != -1) payAvg = payAvg == 0 ? pe.getMinPayout() : (payAvg + pe.getMinPayout()) / 2;
                    //}

                    foreach (PoolEntry pe in poolListPanel.Controls)
                    {
                        pe.selectedCb.Enabled = false;
                        if (pe.getMinPayout() != -1) { 
                            if (pe.getMinPayout() < 500) { 
                                pe.selectedCb.Checked = true;
                            }
                            else pe.selectedCb.Checked = false;
                        }
                        else pe.selectedCb.Checked = false;
                    }

                    break;
                case "larger payouts":
                    //payAvg = 0;

                    //foreach (PoolEntry pe in poolListPanel.Controls)
                    //{
                    //    pe.selectedCb.Enabled = false;
                    //    if (pe.getMinPayout() != -1) payAvg = payAvg == 0 ? pe.getMinPayout() : (payAvg + pe.getMinPayout()) / 2;
                    //}

                    foreach (PoolEntry pe in poolListPanel.Controls)
                    {
                        pe.selectedCb.Enabled = false;
                        if (pe.getMinPayout() != -1) { 
                            if (pe.getMinPayout() >= 500) { 
                                pe.selectedCb.Checked = true;
                            }
                            else pe.selectedCb.Checked = false;
                        }
                        else pe.selectedCb.Checked = false;
                    }
                    break;
                case "lower hashrate":
                    int hashAvg = 0;

                    foreach (PoolEntry pe in poolListPanel.Controls)
                    {
                        pe.selectedCb.Enabled = false;
                        if (pe.getHashrate() != -1) hashAvg = hashAvg == 0 ? pe.getHashrate() : (hashAvg + pe.getHashrate()) / 2;
                    }

                    foreach (PoolEntry pe in poolListPanel.Controls)
                    {
                        if (pe.getHashrate() != -1) { 
                            if (pe.getHashrate() < hashAvg) { 
                                pe.selectedCb.Checked = true;
                            }
                            else pe.selectedCb.Checked = false;
                        }
                        else pe.selectedCb.Checked = false;
                    }

                    break;
                case "higher hashrate":
                    hashAvg = 0;

                    foreach (PoolEntry pe in poolListPanel.Controls)
                    {
                        pe.selectedCb.Enabled = false;
                        if (pe.getHashrate() != -1) hashAvg = hashAvg == 0 ? pe.getHashrate() : (hashAvg + pe.getHashrate()) / 2;
                    }

                    foreach (PoolEntry pe in poolListPanel.Controls)
                    {
                        if (pe.getHashrate() != -1) { 
                            if (pe.getHashrate() > hashAvg) { 
                                pe.selectedCb.Checked = true;
                            }
                            else pe.selectedCb.Checked = false;
                        }
                        else pe.selectedCb.Checked = false;
                    }

                    break;
                case "lower fee":
                    //double feeAvg = 0;

                    //foreach (PoolEntry pe in poolListPanel.Controls)
                    //{
                    //    pe.selectedCb.Enabled = false;
                    //    if (pe.getFee() != -1) feeAvg = feeAvg == 0 ? pe.getFee() : (feeAvg + pe.getFee()) / 2;
                    //}

                    foreach (PoolEntry pe in poolListPanel.Controls)
                    {
                        if (pe.getFee() != -1) {
                            pe.selectedCb.Enabled = false;
                            if (pe.getFee() <= 0.9) { 
                                pe.selectedCb.Checked = true;
                            }
                            else pe.selectedCb.Checked = false;
                        }
                        else pe.selectedCb.Checked = false;
                    }

                    break;
                case "higher fee":
                    //feeAvg = 0;

                    //foreach (PoolEntry pe in poolListPanel.Controls)
                    //{
                    //    pe.selectedCb.Enabled = false;
                    //    if (pe.getFee() != -1) feeAvg = feeAvg == 0 ? pe.getFee() : (feeAvg + pe.getFee()) / 2;
                    //}

                    foreach (PoolEntry pe in poolListPanel.Controls)
                    {
                        pe.selectedCb.Enabled = false;
                        if (pe.getFee() != -1) { 
                            if (pe.getFee() > 0.9) { 
                                pe.selectedCb.Checked = true;
                            }
                            else pe.selectedCb.Checked = false;
                        }
                        else pe.selectedCb.Checked = false;
                    }

                    break;
                case "manual selection":
                    foreach (PoolEntry pe in poolListPanel.Controls)
                    {
                        pe.selectedCb.Enabled = true;
                    }
                    break;
                default:
                    break;
            }

            parsMG.Properties.Settings.Default.poolPreference = selectionModeCb.SelectedIndex;
        }
        
        private string MinerExecutableNotFound(string filename)
        {
            if(MessageBox.Show("The miner's executable could not be found at " + filename + ". Please check the folder structure or if an Anti-Virus program removed the file at that place. If you want to locate " + filename + " click OK, otherwise click Cancel.", "Miner not found", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                OpenFileDialog openMiner = new OpenFileDialog();
                openMiner.Filter = "Executable|*.exe";
                openMiner.Multiselect = false;
                openMiner.Title = "Locate miner executable";
                if (openMiner.ShowDialog() == DialogResult.OK)
                {
                    return openMiner.FileName;
                }
            }

            return "";
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (startBtn.Text == "Start mining!")
            {
                if (addressRb.Checked && !addressTb.Text.StartsWith("hy") && addressTb.Text.Length != 99)
                {
                    MessageBox.Show("Please check your ParsiCoin receiving address.");
                    return;
                }

                if (GetSelectedPools().Count == 0)
                {
                    AnalyzePoolsByCondition(sender, e);
                }

                if (GetSelectedPools().Count == 0)
                {
                    MessageBox.Show("No pools selected to mine on! Please check your pool selection.");
                    return;
                }

                string chosenPort = "";

                switch (hardwareCb.Text)
                {
                    case "Low end":
                        chosenPort = ":3333";
                        break;
                    case "Mid range":
                        chosenPort = ":5555";
                        break;
                    case "High end":
                        chosenPort = ":7777";
                        break;
                    default:
                        chosenPort = ":3333";
                        break;
                }
                 if (minerSelectionCb.Text == "XMR-stak")
                {
                    Process miner = new Process();
                    ProcessStartInfo minerInfo = new ProcessStartInfo();
                    string arguments = "--noUAC";

                    if (!cpuMiningCheck.Checked)
                    {
                        arguments += " --noCPU";
                    }
                    if (!gpuMiningCheck.Checked)
                    {
                        arguments += " --noAMD --noNVIDIA";
                    }

                    arguments += " --currency cryptonight_v7";
                    arguments += " -i 6777 -r PARSMinerGUI";

                    foreach (string pool in GetSelectedPools())
                    {
                        arguments += " -o " + pool + chosenPort + " -u " + addressTb.Text + " -p x";
                    }

                    if (showCLICheck.Checked)
                    {
                        minerInfo.WindowStyle = ProcessWindowStyle.Normal;
                    }
                    else
                    {
                        minerInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    }

                    minerInfo.WorkingDirectory = Directory.GetCurrentDirectory();

                    minerInfo.FileName = parsMG.Properties.Settings.Default.xmrstakpath; //@"miners\xmr-stak\xmr-stak.exe";
                    minerInfo.Arguments = arguments;

                    miner.StartInfo = minerInfo;

                    try
                    {
                        miner.Start();
                    }
                    catch (Exception excep)
                    {
                        string location = MinerExecutableNotFound(miner.StartInfo.FileName);
                        if (location != "")
                        {
                            miner.StartInfo.FileName = location;
                            parsMG.Properties.Settings.Default.xmrstakpath = location;
                            miner.Start();
                        }
                        else
                        {
                            MessageBox.Show("Couldn't start xmrstak miner. Please check your configuration and folder structure.");
                            return;
                        }
                    }

                    activeMiners.Enqueue(miner);
                }
                
                if(activeMiners.Count == 0)
                {
                    MessageBox.Show("No miners to start! Please check your hardware selection.");
                    return;
                }

                parsMG.Properties.Settings.Default.Save();

                timer1.Start();

                startBtn.Text = "Stop mining!";
            } else
            {
                try
                {
                    while(activeMiners.Count > 0)
                    {
                        activeMiners.Dequeue().Kill();
                    }
                    timer1.Stop();

                    hashrateLbl.Text = "Stats - Hashrate: STOPPED";
                    startBtn.Text = "Start mining!";
                }
                catch
                {
                    MessageBox.Show("No miner was running anymore. It was either manually closed by the user or did not start due to a bug.");
                    timer1.Stop();

                    startBtn.Text = "Start mining!";
                }
            }
        }
        
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Always mine at your own risk for your hardware!\nYour computer might become unresponsive while mining.\nThe program automatically selects the best pool according to its ping time or other selectable parameters.\nThe integrated miners (xmr-stak by fireice-uk and xmrig by psychocrypt) both licensed GPL contain a\ndev fee. I decided to keep them as they're nice pieces of software that deserve help.", "Read this first!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("With the CPU/GPU checkboxes you can (de)select which hardware xmr-stak is supposed to mine on.\nPlease also select your hardware range to mine on certain ports with suitable difficulties.\nMine to address tells the pool to payout the mined PARSs to the given address in the textbox.\nAdvanced options are the possibility to enable logs by the miners and to view their CLI. More are currently hidden which might change with the next releases. If you want to change specific tuning settings, please use the config files created by the miners in their folders.", "Settings explained", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("PARSMinerGUI for ParsiCoin is Based on One Click Miner for TurtleCoin programmed by Encrypted Unicorn it's a graphical interface for mining PARS on pools in the network using xmr-stak by fireice-uk. You can find the PMG's source code on ParsiCoin's GitHub and source code for the bundled miner on it's corresponding GitHub page. Not related to xmr-stak.\nVersion 1.0 beta", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                 if (minerSelectionCb.Text == "XMR-stak")
                {
                    WebClient w = new WebClient();

                    string hashrate = w.DownloadString("http://localhost:6777/h");
                    hashrate = "Stats - Hashrate: " + (hashrate.Substring(hashrate.IndexOf("Totals:</th><td>")).Split('>')[2]).Split('<')[0] + " H/s";
                    hashrateLbl.Text = hashrate;
                    
                    string resultPage = w.DownloadString("http://localhost:6777/r");
                    
                    string shareCount = "Shares: " + (resultPage.Substring(resultPage.IndexOf("Good results</th><td>")).Split('>')[2]).Split('<')[0];
                    string bestShare = "Best: " + (resultPage.Substring(resultPage.IndexOf("1</th><td>")).Split('>')[2]).Split('<')[0];
                    string diff = "Difficulty: " + (resultPage.Substring(resultPage.IndexOf("Difficulty</th><td>")).Split('>')[2]).Split('<')[0];
                    shareCountLbl.Text = shareCount;
                    bestShareLbl.Text = bestShare;
                    difficultyLbl.Text = diff;

                    string connectionPage = w.DownloadString("http://localhost:6777/c");
                    connectionPage = "Connected to: " + (connectionPage.Substring(connectionPage.IndexOf("Pool address</th><td>")).Split('>')[2]).Split('<')[0];
                    connectedServerLbl.Text = connectionPage;
                }
            } catch (Exception excp)
            {
                MessageBox.Show("An error occured while fetching miner's api data: " + excp.ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            addressRb.Checked = true;

            parsMG.Properties.Settings.Default.savedAddress = addressTb.Text;
        }

        private void advancedCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (advancedCheck.Checked)
            {
                this.Width = 1200;
            } else
            {
                this.Width = 560;
            }
        }
        
        private void autoConfigCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (autoConfigCheck.Checked)
            {
                cpuThreadNum.Enabled = false;
                cpuLowPowerCheck.Enabled = false;
                aesCb.Enabled = false;
            }
            else
            {
                cpuThreadNum.Enabled = true;
                cpuLowPowerCheck.Enabled = true;
                aesCb.Enabled = true;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)refreshTimeNum.Value * 1000;

            parsMG.Properties.Settings.Default.statsSecs = (int)refreshTimeNum.Value;
        }

        private void poolStatsUpdateCb_CheckedChanged(object sender, EventArgs e)
        {
            foreach(PoolEntry pe in poolListPanel.Controls)
            {
                pe.setAutoUpdate(poolStatsUpdateCb.Checked);
            }
        }

        private void removePoolBtn_Click(object sender, EventArgs e)
        {
            int index = poolListPanel.Controls.Count - 1;
            poolListPanel.Controls.RemoveAt(index);
        }
        
        private void minerSelectionCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(minerSelectionCb.Text == "Xmr-stak")
            {

                gpuMiningCheck.Enabled = true;
            }

            parsMG.Properties.Settings.Default.minerPreference = minerSelectionCb.SelectedIndex;
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (PoolEntry pe in poolListPanel.Controls)
            {
                pe.UpdateStats();
            }
        }

        private void cpuMiningCheck_CheckedChanged(object sender, EventArgs e)
        {
            parsMG.Properties.Settings.Default.cpuCb = cpuMiningCheck.Checked;
        }

        private void gpuMiningCheck_CheckedChanged(object sender, EventArgs e)
        {
            parsMG.Properties.Settings.Default.gpuCb = gpuMiningCheck.Checked;
        }

        private void hardwareCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            parsMG.Properties.Settings.Default.hwType = hardwareCb.SelectedIndex;
        }

        private void writeLogCheck_CheckedChanged(object sender, EventArgs e)
        {
            parsMG.Properties.Settings.Default.logCb = writeLogCheck.Checked;
        }

        private void showCLICheck_CheckedChanged(object sender, EventArgs e)
        {
            parsMG.Properties.Settings.Default.cliCb = showCLICheck.Checked;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            parsMG.Properties.Settings.Default.Save();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(MessageBox.Show("You are about to reset all settings and saved properties, are sure to continue?", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            parsMG.Properties.Settings.Default.savedPools = "";
            parsMG.Properties.Settings.Default.savedAddress = "";
            parsMG.Properties.Settings.Default.cpuCb = true;
            parsMG.Properties.Settings.Default.nvCb = false;
            parsMG.Properties.Settings.Default.amdCb = false;
            parsMG.Properties.Settings.Default.gpuCb = true;
            parsMG.Properties.Settings.Default.cpuPerc = 100;
            parsMG.Properties.Settings.Default.hwType = 0;
            parsMG.Properties.Settings.Default.statsSecs = 10;
            parsMG.Properties.Settings.Default.logCb = false;
            parsMG.Properties.Settings.Default.cliCb = false;
            parsMG.Properties.Settings.Default.minerPreference = 0;
            parsMG.Properties.Settings.Default.poolPreference = 0;
            parsMG.Properties.Settings.Default.xmrstakpath = @"miners\xmr-stak\xmr-stak.exe";


            addressTb.Text = parsMG.Properties.Settings.Default.savedAddress;
            cpuMiningCheck.Checked = parsMG.Properties.Settings.Default.cpuCb;
            gpuMiningCheck.Checked = parsMG.Properties.Settings.Default.gpuCb;
            hardwareCb.SelectedIndex = parsMG.Properties.Settings.Default.hwType;
            refreshTimeNum.Value = parsMG.Properties.Settings.Default.statsSecs;
            writeLogCheck.Checked = parsMG.Properties.Settings.Default.logCb;
            showCLICheck.Checked = parsMG.Properties.Settings.Default.cliCb;
            minerSelectionCb.SelectedIndex = parsMG.Properties.Settings.Default.minerPreference;
            selectionModeCb.SelectedIndex = parsMG.Properties.Settings.Default.poolPreference;

            poolListPanel.Controls.Clear();
            initPools();

            parsMG.Properties.Settings.Default.Save();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("In this section you can see the statistics of the miners.\nThe 'Hashrate' is the number of hashes the miner computes in one second, which depends on the performance of your PC.\n'Shares' count the number of (valid/total) Proof-of-Work solutions the miners found and submitted to the pool.\n'Connected to' displays the currently active connection to a mining pool.\n'Best' views the difficulty of the best found share (PoW solution).\n'Difficulty' displays the current difficulty the pool assigned to your miner(s) to find a valid share.\nIf more than one mining process has been launched, the stats of all running miners are added (shares, difficulty) / redundancies discarded (connection, best share) after comparison.");
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Here you have to select what kind of hardware you have and which you want to mine with. Please check whether you want to use the miner for CPU and/or GPU (xmrstak) or which GPU brand you have (xmrig).\nPlease also select the hardware type of your PC as pools offer different difficulties for low or high end computers.\nEnter the ParsiCoin address you want to mine to in the text box, it will be saved unless you change it again.\nYou can also set the frequency of updates to the mining statistics above.");
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("In this section you can see information about available mining pools and select under which consideration pools will be chosen automatically by the program or select the pool(s) you want to mine on manually.\nUsually, pools with a low ping time are to be preferred. If you are a slow miner, you can select a pool with a lower payment threshold or the other way around for faster computers. Pools that have a high hashrate usually find blocks faster a produce payments sooner. To keep the network decentralized, it's better to not over-power leading pools though. Selecting multiple pools only gives you the possibility to have a backup selection in case of one of your selected pools going down.\nPlease wait for all stats and pings to load.\nPools that can't be pinged or don't respond to the API request are automatically discarded by the program right now, sorry for that!");
        }

        private void newPoolTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void addPoolAddress_Click(object sender, EventArgs e)
        {

        }
    }
}
