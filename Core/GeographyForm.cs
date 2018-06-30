using Google.Maps;
using Google.Maps.StaticMaps;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core
{
    public partial class GeographyForm : Form
    {
        bool waterMarkActive;
        int currentZoom = 11;

        public GeographyForm()
        {
            InitializeComponent();
            InitializeComponentCustom();
        }

        // Initializers
        void InitializeComponentCustom()
        {
            FixApplicationSize();
            InitializeImage();
            InitializeSearchTextBox();
            SetWatermark();
        }

        void FixApplicationSize()
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
        }

        void InitializeImage()
        {
            pictureBox.Click += PictureBox_Click;
        }

        void InitializeSearchTextBox()
        {
            searchTextBox.KeyPress += GeographyForm_EnterKeyPress;
        }

        void SetWatermark()
        {
            SetWatermarkActive(searchTextBox);

            searchTextBox.GotFocus += SearchTextBox_GotFocus;
            searchTextBox.LostFocus += SearchTextBox_LostFocus;
        }

        // Event handlers
        void SearchTextBox_GotFocus(object sender, EventArgs e)
        {
            if (waterMarkActive)
            {
                waterMarkActive = false;
                searchTextBox.Text = string.Empty;
                searchTextBox.ForeColor = Color.Black;
            }
        }

        void SearchTextBox_LostFocus(object sender, EventArgs e)
        {
            if (!waterMarkActive && string.IsNullOrEmpty(searchTextBox.Text))
            {
                SetWatermarkActive(searchTextBox);
            }
        }

        void PictureBox_Click(object sender, EventArgs e)
        {
            webBrowser.Focus();
        }

        async void GeographyForm_EnterKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Return)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                return;
            }

            await SearchAddressAsync(searchTextBox.Text);
        }

        async void PlusButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchTextBox.Text) || waterMarkActive)
            {
                return;
            }
            currentZoom = Math.Min(currentZoom + 1, 18);
            await SearchAddressAsync(searchTextBox.Text);
        }

        async void MinusButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchTextBox.Text) || waterMarkActive)
            {
                return;
            }
            currentZoom = Math.Max(currentZoom - 1, 4);
            await SearchAddressAsync(searchTextBox.Text);
        }

        // Helper methods
        void SetWatermarkActive(TextBox textBox)
        {
            var watermarkText = "Search the address";
            var watermarkColor = Color.Gray;

            waterMarkActive = true;
            textBox.ForeColor = watermarkColor;
            textBox.Text = watermarkText;
        }

        async Task SearchAddressAsync(string text)
        {
            var staticMapsRequest = new StaticMapRequest();
            staticMapsRequest.Center = new Location(text);
            staticMapsRequest.Size = new MapSize(600, 600);
            staticMapsRequest.Zoom = currentZoom;

            var staticMapsService = new StaticMapService();

            var image = await staticMapsService.GetStreamAsync(staticMapsRequest);
            pictureBox.Image = ByteToImage(image);
        }

        public static Bitmap ByteToImage(Stream stream)
        {
            Bitmap bm = new Bitmap(stream);
            stream.Dispose();
            return bm;
        }
    }
}
