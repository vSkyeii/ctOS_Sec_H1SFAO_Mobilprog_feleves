namespace ctOS_Sec_H1SFAO_Mobilprog_feleves;

public partial class ClientPage : ContentPage
{
	public ClientPage(ClientViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
    
}