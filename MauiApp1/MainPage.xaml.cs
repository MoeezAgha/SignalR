namespace MauiApp1
{

    public partial class MainPage : ContentPage
    {
        private readonly SignalRService _signalRService;

        public MainPage()
        {
            InitializeComponent();
            string userId = "unique-user-id"; // Retrieve from a secure source
        }
        private void OnMessageReceived(string message)
        {
            // Call a method when a message is received
            HandleReceivedMessage(message);
        }

        private void HandleReceivedMessage(string message)
        {
            // Implement your logic here
            Console.WriteLine($"Received message: {message}");
            // For example, update the UI or trigger other actions
        }
    }
}
