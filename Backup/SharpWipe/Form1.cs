using System;
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
        private void UpdateLabelText(string text)
        {
            lblInfo.Text = text;
        }

        private void UpdateInfoLabel()
        {
            string infoText = string.Format("Running Pass {0} of {1} Sector {2} of {3}", currentPass, totalPasses,
                              currentSector, totalSectors);
            lblInfo.Invoke(new UpdateLabelTextDelegate(UpdateLabelText), infoText);
        }

        private void WipeDone()
        {
            lblInfo.Invoke(new UpdateLabelTextDelegate(UpdateLabelText), "The file is now wiped!");
        }

        private void buttWipe_Click(object sender, EventArgs e)
        {
            Thread wipeThread = new Thread(StartWipeFile);
            wipeThread.Start();
        }

        private void StartWipeFile()
        {
            wipe.WipeFile(filename, 5);
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
    }
}