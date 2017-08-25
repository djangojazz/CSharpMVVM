using FakeRepo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Main.ViewModel
{
  public sealed class CollectionChangedBubbleUpViewModel : BaseViewModel
  {
    public CollectionChangedBubbleUpViewModel()
    {
      //Trans = new ObservableCollection<DummyTransaction>(FakeRepo.FakeRepo.Instance.Trans);
      //Trans.CollectionChanged += ModifyCollectionsBindings;
    }

    public ObservableCollection<DummyTransaction> Trans { get; set; }

    private void ModifyCollectionsBindings(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      if (e?.OldItems != null)
      {
        foreach (var arg in e?.OldItems)
        {
          ((INotifyPropertyChanged)arg).PropertyChanged -= CascadeEvent;
          base.OnPropertyChanged(arg.ToString());
        }
      }

      if (e?.NewItems != null)
      {
        foreach (var arg in e?.NewItems)
        {
          ((INotifyPropertyChanged)arg).PropertyChanged += CascadeEvent;
          base.OnPropertyChanged(arg.ToString());
        }
      }
    }

    private void CascadeEvent(object sender, PropertyChangedEventArgs e)
    {
      OnPropertyChanged(e.PropertyName);
    }
  }
}
