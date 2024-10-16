using Module07DataAccess.Services;
using MySql.Data.MySqlClient;

namespace Module07DataAccess
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        private readonly DatabaseConnectionService _dbConnectionService;

        public MainPage()
        {
            InitializeComponent();

            //Initialize database connection
            _dbConnectionService = new DatabaseConnectionService();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void OnTestConnectionClicked(object sender, EventArgs e)
        {
            var connectionsString = _dbConnectionService.GetConnectionString();
            try
            {
                using (var connetion = new MySqlConnection(connectionsString)) 
                { 
                    await connetion.OpenAsync();
                    ConnectionStatusLabel.Text = "Connection Successfull";
                    ConnectionStatusLabel.TextColor = Colors.Green;
                }
            }
            catch (Exception ex) 
            {
                ConnectionStatusLabel.Text = $"Connection Failed: {ex.Message}";
                ConnectionStatusLabel.TextColor = Colors.Red;
            }
        }

        private async void OpenViewpersonal(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ViewPersonal");
        }
    }

}
