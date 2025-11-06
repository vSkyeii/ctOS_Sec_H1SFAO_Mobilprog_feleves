namespace ctOS_Sec_H1SFAO_Mobilprog_feleves
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }
        private async void login_try(object? sender, EventArgs args)
        {
            string username = username_entry.Text;
            string password = password_entry.Text;
            if (username == "admin" && password == "admin")
            {
                await DisplayAlert("Sikeres bejelentkezés", "Üdvözlünk, admin!", "OK");
                await Shell.Current.GoToAsync("Admin");
                
            }
            else if(username == "boss" && password == "boss")
            {
                await DisplayAlert("Sikertelen bejelentkezés", "huh", "ok bro");
            }
            else
            {
                int userID;
                if (int.TryParse(username, out userID))
                {
                    var db = new ctOsDatabase();
                    var vm = new ClientViewModel(db);
                    await vm.SetUserIdAsync(userID);
                    var clientPage = new ClientPage { BindingContext = vm };
                    await Navigation.PushAsync(clientPage);
                }
                await DisplayAlert("Sikertelen bejelentkezés", "Hibás felhasználónév vagy jelszó!", "OK");
            }
        }
    }
}
