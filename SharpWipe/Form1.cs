using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SharpWipe
{
    public partial class Form1 : Form
    {
        private readonly Wipe wipe = new Wipe();
        private string filename = string.Empty;

        private int currentPass = 0;
        private int totalPasses = 0;
        private int currentSector = 0;
        private int totalSectors = 0;

        public Form1()
        {
            InitializeComponent();
            lblInfo.Text = string.Empty;

            wipe.PassInfoEvent += new PassInfoEventHandler(wipe_PassInfoEvent);
            wipe.SectorInfoEvent += new SectorInfoEventHandler(wipe_SectorInfoEvent);
            wipe.WipeDoneEvent += new WipeDoneEventHandler(wipe_WipeDoneEvent);
            wipe.WipeErrorEvent += new WipeErrorEventHandler(wipe_WipeErrorEvent);
        }

        private delegate void FormOnTopDelegate(bool b);
        private void FormOnTop(bool b)
        {
            TopMost = b;
        }

        void wipe_WipeErrorEvent(WipeErrorEventArgs e)
        {
            // Set TopMost to false to make sure that the messagebox is shown on top
            Invoke(new FormOnTopDelegate(FormOnTop), false);
            MessageBox.Show(e.WipeError.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Invoke(new FormOnTopDelegate(FormOnTop), true);
        }

        private void wipe_WipeDoneEvent(WipeDoneEventArgs e)
        {
            WipeDone();
        }

        private void wipe_PassInfoEvent(PassInfoEventArgs e)
        {
            currentPass = e.CurrentPass;
            totalPasses = e.TotalPasses;
            UpdateInfoLabel();
        }

        private void wipe_SectorInfoEvent(SectorInfoEventArgs e)
        {
            currentSector = e.CurrentSector;
            totalSectors = e.TotalSectors;
            UpdateInfoLabel();
        }

        private delegate void UpdateLabelTextDelegate(string text);
        private void UpdateLabellblInfoText(string text)
        {
            lblInfo.Text = text;
        }
        private void UpdateLabellablel2Text(string text)
        {
            label2.Text = text;
        }

        private void UpdateInfoLabel()
        {
            string infoText = string.Format("Running Pass {0} of {1} Sector {2} of {3}", currentPass, totalPasses,
                              currentSector, totalSectors);
            lblInfo.Invoke(new UpdateLabelTextDelegate(UpdateLabellblInfoText), infoText);
        }
        private void UpdateInfoLabel(int c,int t,int cu,int to)
        {
            string infoText = string.Format("Running Pass {0} of {1} Sector {2} of {3}", c, t, cu, to);
            lblInfo.Invoke(new UpdateLabelTextDelegate(UpdateLabellblInfoText), infoText);
        }
        private void WipeDone()
        {
            lblInfo.Invoke(new UpdateLabelTextDelegate(UpdateLabellblInfoText), "The file is now wiped!");
        }

        private void buttWipe_Click(object sender, EventArgs e)
        {
            Thread wipeThread = new Thread(StartWipeFile);
            wipeThread.IsBackground = true;
            wipeThread.Start();
        }

        private void StartWipeFile()
        {
            int number = Convert.ToInt32(textBox1.Text);
            int buffersize = Convert.ToInt32(textBox2.Text);
            if (File.Exists(filename))
            {
                wipe.WipeFile(filename, number);
                label2.Invoke(new UpdateLabelTextDelegate(UpdateLabellablel2Text), "Files: 1 of 1");
                WipeDone();
            }

            else if (Directory.GetDirectoryRoot(filename) == filename)
            {

                DialogResult dr = MessageBox.Show("Fill free space press Yes\nDelete entire drive pree No", "", MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Yes)
                {

                }
                else if (dr == DialogResult.No)
                {

                }

                deletesubfolders(filename);
                WipeDone();
            }
            else if (Directory.Exists(filename))
            {
                string[] arrfile = Directory.GetFiles(filename, "*.*", SearchOption.AllDirectories);
                for (int i = 0; i < arrfile.Length; i++)
                {
                    label2.Invoke(new UpdateLabelTextDelegate(UpdateLabellablel2Text), "Files: " + (1 + i) + " of " + arrfile.Length);
                    WipeFile(arrfile[i], number, buffersize);
                }
                deletesubfolders(filename);
                Directory.Delete(filename);
                WipeDone();
            }
        }
        void deletesubfolders(string dir)
        {
            string[] arrdir = Directory.GetDirectories(dir);
            for (int i = 0; i < arrdir.Length; i++)
            {
                deletesubfolders(arrdir[i]);
                Directory.Delete(arrdir[i]);
            }
        }
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.All;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);
            filename = files[0];

            txtFileName.Text = filename;
        }

        private void buttOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(DialogResult.OK == ofd.ShowDialog())
            {
                filename = ofd.FileName;
                txtFileName.Text = filename;
            }
        }
        /// <summary>
        /// Deletes a file in a secure way by overwriting it with
        /// random garbage data n times.
        /// </summary>
        /// <param name="filename">Full path of the file to be deleted</param>
        /// <param name="timesToWrite">Specifies the number of times the file should be overwritten</param>
        public void WipeFile(string filename, int timesToWrite,int buffersize)
        {
            try
            {
                File.SetAttributes(filename, FileAttributes.Normal);
                double sectors = Math.Ceiling(new FileInfo(filename).Length / 512.0);
                byte[] dummyBuffer = new byte[buffersize];
                Random r = new Random();
                FileStream inputStream = new FileStream(filename, FileMode.Open);
                for (int currentPass = 0; currentPass < timesToWrite; currentPass++)
                {
                    inputStream.Position = 0;
                    for (int sectorsWritten = 0; sectorsWritten < sectors; sectorsWritten++)
                    {

                        UpdateInfoLabel(currentPass, timesToWrite, sectorsWritten,(int) sectors);
                        r.NextBytes(dummyBuffer);
                        inputStream.Write(dummyBuffer, 0, dummyBuffer.Length);
                    }
                }
                inputStream.SetLength(0);
                inputStream.Close();
                DateTime dt = new DateTime(2037, 1, 1, 0, 0, 0);
                File.SetCreationTime(filename, dt);
                File.SetLastAccessTime(filename, dt);
                File.SetLastWriteTime(filename, dt);
                File.SetCreationTimeUtc(filename, dt);
                File.SetLastAccessTimeUtc(filename, dt);
                File.SetLastWriteTimeUtc(filename, dt);
                File.Delete(filename);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void lblInfo_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}