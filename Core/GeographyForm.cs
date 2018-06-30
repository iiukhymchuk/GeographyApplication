using Google.Maps;
using Google.Maps.StaticMaps;
using System;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core
{
    public partial class GeographyForm : Form
    {
        bool searchBoxError = false;
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
            roadMap.Checked = true;
            radioButtonsPanel.Click += RadioButtonsPanel_Click;
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
            pictureBox.Image = DataSource.GetInitialImage();
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

            if (searchBoxError)
            {
                SetWatermarkActive(searchTextBox);
            }
        }

        void PictureBox_Click(object sender, EventArgs e)
        {
            webBrowser.Focus();
        }

        void RadioButtonsPanel_Click(object sender, EventArgs e)
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

            try
            {
                await SearchAddressAsync(searchTextBox.Text);
            }
            catch (HttpRequestException ex) when (ex.Message == "Response status code does not indicate success: 403 (Forbidden).")
            {
                MessageBox.Show("Add proper API_KEY to settings file",
                    "Wrong api key", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (HttpRequestException ex) when (ex.Message == "An error occurred while sending the request."
                && ex.InnerException.Message == "The remote name could not be resolved: 'maps.google.com'")
            {
                MessageBox.Show("Problems with connection to the internet",
                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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

        async void RoadMap_CheckedChanged(object sender, EventArgs e)
        {
            await RadioButtonCheckedChanged();
        }

        async void Satellite_CheckedChanged(object sender, EventArgs e)
        {
            await RadioButtonCheckedChanged();
        }

        async void Terrain_CheckedChanged(object sender, EventArgs e)
        {
            await RadioButtonCheckedChanged();
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
            searchBoxError = false;
            searchTextBox.ForeColor = Color.Black;

            var cachedImage = DataSource.GetCachedImage(text, currentZoom, MapType);
            if (cachedImage != null)
            {
                pictureBox.Image = cachedImage;
                return;
            }

            var staticMapsRequest = new StaticMapRequest
            {
                Center = new Location(text),
                Size = new MapSize(600, 600),
                Zoom = currentZoom,
                MapType = MapType
            };
            var staticMapsService = new StaticMapService();

            var byteArray = await staticMapsService.GetImageAsync(staticMapsRequest);
            var errorHash = "6i62JfRBdd+DQcwx5Q0p4MpeYWI=";
            string hash = Utils.CulculateSHA1Hash(byteArray);

            DataSource.CacheImage(text, currentZoom, MapType, byteArray);

            if (hash == errorHash)
            {
                searchBoxError = true;
                searchTextBox.ForeColor = Color.Red;
                return;
            }

            pictureBox.Image = Utils.BytesArrayToBitmap(byteArray);
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            hex.Append("0x");
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        async Task RadioButtonCheckedChanged()
        {
            if (string.IsNullOrWhiteSpace(searchTextBox.Text) || waterMarkActive)
            {
                return;
            }

            await SearchAddressAsync(searchTextBox.Text);
        }

        public MapTypes MapType =>
            roadMap.Checked
                ? MapTypes.Roadmap
                : satellite.Checked
                    ? MapTypes.Hybrid
                    : MapTypes.Terrain;
    }
}
