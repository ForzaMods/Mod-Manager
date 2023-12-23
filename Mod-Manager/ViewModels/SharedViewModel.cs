using System.ComponentModel;
using Mod_Manager.Models;

namespace Mod_Manager.ViewModels;

public sealed class SharedViewModel : INotifyPropertyChanged
{
    private DataMod? _mod;
    
    public DataMod? Mod
    {
        get => _mod;
        set
        {
            if (_mod == value) return;
            _mod = value;
            OnPropertyChanged(nameof(Mod));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}