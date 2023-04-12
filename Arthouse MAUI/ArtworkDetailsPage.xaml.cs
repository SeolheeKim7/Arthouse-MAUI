using Arthouse_MAUI.Data;
using Arthouse_MAUI.Models;
using Arthouse_MAUI.Utilities;
using System.Text;

namespace Arthouse_MAUI;

public partial class ArtworkDetailsPage : ContentPage
{
    private Artwork artwork;
    private App thisApp;
    private List<ArtType> artTypes;
    public ArtworkDetailsPage()
	{
		InitializeComponent();
        thisApp = Application.Current as App;
        artTypes = new List<ArtType>();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        artwork = (Artwork)this.BindingContext;
        if (artwork.ID == 0)//Adding New
        {
            this.Title = "Add New Artwork";
            //Since we do not have an ArtType yet, we want to get one ready
            ArtType noArtType = new ArtType { ID = 0, Type = " Select an Art Type" };
            artTypes.Add(noArtType);
            btnDelete.IsEnabled = false;
        }
        else
        {
            this.Title = "Edit Artwork Details";
            btnDelete.IsEnabled = true;
        }

        FillArtType();
    }

    private void FillArtType()
    {

        foreach (ArtType d in thisApp.AllArtTypes.OrderBy(d => d.Type))
        {
            artTypes.Add(d);
        }
        //Fill the Drop Down Items
        ddlArtTypes.ItemsSource = artTypes;
        //Set value to current or if inserting, set it to the prompt
        if (artwork.ArtTypeID == 0)
        {
            ddlArtTypes.SelectedIndex = 0;
        }
        else if (artwork.ArtTypeID >= 0)
        {
            ddlArtTypes.SelectedItem = thisApp.AllArtTypes.FirstOrDefault(d => d.ID == artwork.ArtTypeID);
        }
    }

    private async void SaveClicked(object sender, EventArgs e)
    {
        try
        {
            //If nothing is selected then we still want 0 for the foreign key
            artwork.ArtTypeID = (((ArtType)ddlArtTypes.SelectedItem)?.ID).GetValueOrDefault();

            ArtworkRepository r = new ArtworkRepository();
            if (artwork.ID == 0)//Inserting a new record
            {
                await r.AddArtwork(artwork);
            }
            else
            {
                await r.UpdateArtwork(artwork);
            }
            await Navigation.PopAsync();

        }
        catch (ApiException apiEx)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Errors:");
            foreach (var error in apiEx.Errors)
            {
                sb.AppendLine("-" + error);
            }
            await DisplayAlert("Problem Saving the Artwork:", sb.ToString(), "Ok");
        }
        catch (Exception ex)
        {
            if (ex.GetBaseException().Message.Contains("connection with the server"))
            {
                await DisplayAlert("Error", "No connection with the server.", "Ok");
            }
            else
            {
                await DisplayAlert("Error", "Could not complete operation.", "Ok");
            }
        }
    }

    private async void DeleteClicked(object sender, EventArgs e)
    {
        var answer = await DisplayAlert("Confirm Delete", "Are you certain that you want to delete " + artwork.Summary + "?", "Yes", "No");
        if (answer == true)
        {
            try
            {
                ArtworkRepository er = new ArtworkRepository();
                await er.DeleteArtwork(artwork);
                await Navigation.PopAsync();
            }
            catch (ApiException apiEx)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Errors:");
                foreach (var error in apiEx.Errors)
                {
                    sb.AppendLine("-" + error);
                }
                await DisplayAlert("Problem Deleting the Artwork:", sb.ToString(), "Ok");
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException().Message.Contains("connection with the server"))
                {
                    await DisplayAlert("Error", "No connection with the server.", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", "Could not complete operation.", "Ok");
                }
            }
        }
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}