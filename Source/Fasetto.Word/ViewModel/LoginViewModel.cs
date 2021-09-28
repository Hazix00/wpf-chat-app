using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Fasetto.Word
{
    /// <summary>
    /// The View Model for a login screen
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// A flag indicating if the login command is running
        /// </summary>
        public bool LoginIsRunning { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to login
        /// </summary>
        public ICommand LoginCommand { get; set; }
        /// <summary>
        /// The command to login
        /// </summary>
        public ICommand RegisterCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginViewModel()
        {
            // Create commands
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await LoginAsync(parameter));
            RegisterCommand = new RelayCommand(async () => await RegisterAsync());
        }


        #endregion
        /// <summary>
        /// Go to the Register page
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task RegisterAsync()
        {
            ((Application.Current.MainWindow as MainWindow).DataContext as WindowViewModel).CurrentPage = ApplicationPage.Register;
            await Task.Delay(0);
        }
        /// <summary>
        /// Try to log in the user
        /// </summary>
        /// <returns></returns>
        public async Task LoginAsync(object parameter)
        {
            await RunCommand(() => LoginIsRunning, async () =>
            {
                await Task.Delay(5000);

                string email = Email;

                // !: Never store unsecure password in variable like this
                string pass = (parameter as IHavePassword).SecurePassword.Unsecure();
            });
        }
    }
}
