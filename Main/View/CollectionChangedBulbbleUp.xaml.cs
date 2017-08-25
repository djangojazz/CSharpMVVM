using Main.ViewModel;
using System.Windows.Controls;

namespace Main
{
  /// <summary>
  /// Interaction logic for CollectionChangedBulbbleUp.xaml
  /// </summary>
  public partial class CollectionChangedBulbbleUp : UserControl
  {
    public CollectionChangedBulbbleUp()
    {
      InitializeComponent();
      DataContext = new CollectionChangedBubbleUpViewModel();
    }
  }
}
