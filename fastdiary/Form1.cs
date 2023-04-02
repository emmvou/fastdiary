
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace fastdiary
{
    public partial class Form1 : Form
    {
        private static readonly string _storageFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "fastdiary");

        private void openFile(string newfile)
        {
            editor.Text = File.ReadAllText(newfile);
        }

        private void refreshListOfFiles()
        {
            // purge listview
            entries.Items.Clear();
            // get list of files
            string[] files = Directory.GetFiles(_storageFolder);
            // add files to listview
            foreach (string file in files)
            {
                var fileName = Path.GetFileName(file);
                entries.Items.Add(new ListViewItem(fileName) { Name = fileName });
            }

        }

        public Form1()
        {
            InitializeComponent();
            // create storage folder if it doesn't exist
            Directory.CreateDirectory(_storageFolder);
            refreshListOfFiles();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void newbtn_Click(object sender, EventArgs e)
        {
            savebtn_Click(sender, e);
            var filename =
                DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss", System.Globalization.CultureInfo.InvariantCulture) +
                ".txt";
            // create file
            File.Create(Path.Combine(_storageFolder, filename)).Close();
            // add to list of files
            refreshListOfFiles();
            // select filename in listview
            entries.SelectedItems.Clear();
            entries.Items[filename].Selected = true;
        }

        private void entries_SelectedIndexChanged(object sender, EventArgs e)
        {
            // open selected file in editor
            if (entries.SelectedItems.Count == 0)
            {
                return;
            }
            
            var path = Path.Combine(_storageFolder, entries.SelectedItems[0].Name);
            openFile(path);

        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            if (entries.SelectedItems.Count == 0)
            {
                return;
            }
            var path = Path.Combine(_storageFolder, entries.SelectedItems[0].Name);
            File.WriteAllText(path, editor.Text);
        }

        private void editor_Leave(object sender, EventArgs e)
        {
savebtn_Click(sender, e);
        }
    }
}