namespace IosDeploy;

public partial class List : ContentView
{
    public List()
    {
        InitializeComponent();

        List<Item> items = App.ItemHandler.GetAllItems();

        itemList.ItemsSource = items;

    }

    void Refresh_Button_Clicked(System.Object sender, System.EventArgs e)
    {
        itemList.ItemsSource = App.ItemHandler.GetAllItems();
    }

    private async void itemList_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    {

        if (itemList.SelectedItem != null)
        {
            Item item = (Item)itemList.SelectedItem;
            await Navigation.PushAsync(new Edit(item));
        }

        itemList.SelectedItem = null;
    }
}
