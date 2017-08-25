using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeRepo
{
  public class FakeRepo
  {
    private static readonly FakeRepo _instance = new FakeRepo();
    static FakeRepo() { }
    private FakeRepo()
    {
      Trans = new List<DummyTransaction>
      {
        new DummyTransaction(1, "A"),
        new DummyTransaction(2, "B"),
        new DummyTransaction(3, "C"),
      };
    }
    public static FakeRepo Instance { get => _instance; }

    public IList<DummyTransaction> Trans { get; set; } 

    public void AddToTrans(int id, string desc)
    {
      Trans.Add(new DummyTransaction(id, desc));
    }
  }
}