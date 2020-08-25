using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using D2P_Sign.Properties;

namespace D2P_Sign.Windows
{
    public partial class ProgressBar : Form
    {
        public ProgressBar(Func<bool> uploadWork)
        {
            InitializeComponent();
            this.UploadWork = uploadWork;
        }

        private Func<bool> UploadWork = null;

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (UploadWork != null)
            {
                this.backgroundWorker1.CancelAsync();
            }

            this.Close();
        }

        private void backgroundWorker_Upload(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = Upload(worker, e);
        }

        private bool Upload(BackgroundWorker worker, DoWorkEventArgs e)
        {
            bool isUpload = false;
            string processFinishNot = string.Empty;

            if (UploadWork == null)
            {
                throw new ArgumentNullException("UploadWork can't be null!");                
            }

            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                worker.ReportProgress(25);
                isUpload = UploadWork();
                worker.ReportProgress(100);
            }

            return isUpload;
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBarCtrl.Value = e.ProgressPercentage;
        }

        private void backgroundWorker_UploadCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (e.Error is System.Net.WebException)
                {
                    MessageBox.Show(string.Format(Resources.WebError, e.Error.Message));
                    Utils.Log4NetHelper.Error(e.Error);
                }
                else
                {
                    Utils.Log4NetHelper.Error(e.Error.Message);
                    MessageBox.Show(e.Error.Message);
                }
            }
            else if (e.Cancelled)
            {
                Utils.MessageUtil.ShowTips(Properties.Resources.CancelUpload);
            }
            else
            {
                bool isUpload = (bool)e.Result;
                if (isUpload)
                {
                    Utils.MessageUtil.ShowTips("签名文件上传成功!");
                }
                else
                {
                    Utils.MessageUtil.ShowError("签名文件上传失败!");
                }
            }
            this.Close();
        }

        private void ProgressBar_Load(object sender, EventArgs e)
        {
            this.backgroundWorker1.DoWork += 
                                    new DoWorkEventHandler(backgroundWorker_Upload);
            this.backgroundWorker1.RunWorkerCompleted +=
                                    new RunWorkerCompletedEventHandler(backgroundWorker_UploadCompleted);
            this.backgroundWorker1.ProgressChanged +=
                                    new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
            this.backgroundWorker1.RunWorkerAsync();
        }
    }
}
