using NHotkey;
using NHotkey.WindowsForms;
namespace Screenshot2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            HotkeyManager.Current.AddOrReplace("Decrement", Keys.Control | Keys.Shift | Keys.S, OnDecrement);
        }
        private void OnDecrement(object sender, HotkeyEventArgs e)
        {
            Point mousePosition = Control.MousePosition; //Capture
            int mouseMonitor = Screen.AllScreens.ToList().FindIndex(s => s.Bounds.Contains(mousePosition));
            Screen screen = Screen.AllScreens[mouseMonitor];
            Rectangle bounds = screen.Bounds;
            using Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
            using Graphics g = Graphics.FromImage(bitmap);
            g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
            string picturesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures); //Save
            string fileName = $"Screenshot_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
            string filePath = Path.Combine(picturesFolder, fileName);
            bitmap.Save(filePath);
            Clipboard.SetImage(bitmap);
            e.Handled = true;
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) { notifyIcon1.Icon = null; this.Close(); }
        private void minimizeToolStripMenuItem_Click(object sender, EventArgs e) { this.Hide(); this.Visible = false; }
    } }