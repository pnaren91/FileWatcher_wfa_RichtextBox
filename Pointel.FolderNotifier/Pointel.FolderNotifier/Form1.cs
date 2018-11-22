using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Configuration;



namespace Pointel.FolderNotifier
{
    public partial class Form1 : Form
    {
        public string FilePath = ConfigurationManager.AppSettings["Directory"].ToString();
        
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = ConfigurationManager.AppSettings["Directory"];
            textBox1.ReadOnly = true;
        }
        
        public static FileSystemWatcher Watcher;


        public void WatcherEvent()
        {

            Form1.Watcher = new FileSystemWatcher(FilePath);
            Form1.Watcher.Changed += new FileSystemEventHandler(WatcherChanged);
            Form1.Watcher.EnableRaisingEvents = true;
            Form1.Watcher.IncludeSubdirectories = true;
            
        }

        private void WatcherChanged(object sender, FileSystemEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                StringBuilder Builder = new StringBuilder();
                Builder.Append(e.Name+"\t\t\t"+DateTime.Now.ToString()+"\t\t");
                Builder.AppendLine(System.IO.Path.GetExtension(e.Name));
                richTextBox1.Text += Builder.ToString();
            });
        }
        
            private void Form1_Load(object sender, EventArgs e)
            {
                WatcherEvent();
            }

        
    }



    
}
