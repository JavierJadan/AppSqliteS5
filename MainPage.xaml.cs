using System.Collections.ObjectModel;
using AppSquiliteS5.Models;
using AppSquiliteS5.Repositories;

namespace AppSquiliteS5;

public partial class MainPage : ContentPage
{
    private readonly PersonRepository _repository;
    private Persona? _selectedPerson;
    public ObservableCollection<Persona> Personas { get; } = [];

    public MainPage(PersonRepository repository)
    {
        InitializeComponent();
        _repository = repository;
        PeopleList.ItemsSource = Personas;
    }

    private async void OnAddClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text)) return;

        try
        {
            await _repository.AddAsync(NameEntry.Text ?? string.Empty);
            NameEntry.Text = string.Empty;
        }
        catch (Exception) { }
    }

    private async void OnShowClicked(object? sender, EventArgs e)
    {
        await LoadPeopleAsync();
    }

    private async void OnDeleteClicked(object? sender, EventArgs e)
    {
        if (_selectedPerson is null) return;

        await _repository.DeleteAsync(_selectedPerson);
        _selectedPerson = null;
        PeopleList.SelectedItem = null;
        await LoadPeopleAsync();
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        _selectedPerson = e.CurrentSelection.FirstOrDefault() as Persona;
    }

    private async Task LoadPeopleAsync()
    {
        try
        {
            var people = await _repository.GetAllAsync();
            Personas.Clear();
            foreach (var person in people) Personas.Add(person);
        }
        catch (Exception) { }
    }
}
