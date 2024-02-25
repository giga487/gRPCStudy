using ClientCore;
using ClientCore.Movable;
using CommunicationLayer.Login;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientApplicationTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            IMobile player = new Player();
        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTB.Text;

            ClientLogin clientLogin = new ClientLogin(new UriBuilder("http", "127.0.0.1", 5297).Uri);
            var response = await clientLogin.Login(username, "stocazzo");

            if (response != null)
            {
                AddLog($"{response.Response} - {response.Hash}\n");
            }
        }

        private List<string> _messageList = new List<string>();
        public void AddLog(string text)
        {
            _messageList.Add(text);

            if (_messageList.Count > 10)
            {
                _messageList.RemoveAt(0);
            }

            logtxtbox.Clear();
            
            foreach(string message in _messageList)
            {
                logtxtbox.Text += message;
            }
        }

        public void AddLogCrossThread(string text)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                AddLog(text);

            }));
        }

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}