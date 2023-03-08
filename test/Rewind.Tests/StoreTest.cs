namespace Rewind.Tests;

using Rewind;

public class StoreTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CreateAndQueryMember()
    {
        RewindMember member = new RewindMember("Eva", 5);
        RewindStock stock = new RewindStock();
        RewindStore rewind = new RewindStore(stock);
        rewind.AddMember(member);
        string id = member.GetMembershipId();
        Assert.That(rewind.HasMember(id), Is.True);
    }

    [Test]
    public void CreateMultipleCopiesOfMovie()
    {
        RewindStock stock = new RewindStock();
        stock.Add("Hello World", "Horror", 1974);
        stock.Add("Hello World", "Horror", 1974);
        RewindStore rewind = new RewindStore(stock);
        Assert.That(stock.GetTotal(), Is.EqualTo(2));
        Assert.That(rewind.CheckStock("Hello World", "Horror", 1974), Is.EqualTo(2));
    }

    [Test]
    public void RentMovie()
    {
        RewindStock stock = new RewindStock();
        stock.Add("Hello World", "Horror", 1974);
        RewindStore rewind = new RewindStore(stock);
        Assert.That(rewind.CheckStock("Hello World", "Horror", 1974), Is.EqualTo(1));
        rewind.LoanMovie("Hello World", "Horror", 1974);
        Assert.That(rewind.CheckStock("Hello World", "Horror", 1974), Is.EqualTo(0));
    }

    [Test]
    public void RentAndReturnMovie()
    {
        RewindStock stock = new RewindStock();
        stock.Add("Hello World", "Horror", 1974);
        RewindStore rewind = new RewindStore(stock);
        Assert.That(rewind.CheckStock("Hello World", "Horror", 1974), Is.EqualTo(1));
        RewindMovie rented = rewind.LoanMovie("Hello World", "Horror", 1974);
        Assert.That(rewind.CheckStock("Hello World", "Horror", 1974), Is.EqualTo(0));
        rewind.ReturnMovie(rented);
        Assert.That(rewind.CheckStock("Hello World", "Horror", 1974), Is.EqualTo(1));
    }

    [Test]
    public void MultipleCopiesRentedAndReplaced()
    {
        RewindStock stock = new RewindStock();
        stock.Add("Hello World", "Horror", 1974);
        stock.Add("Hello World", "Horror", 1974);
        RewindStore rewind = new RewindStore(stock);
        Assert.That(rewind.CheckStock("Hello World", "Horror", 1974), Is.EqualTo(2));
        RewindMovie rented1 = rewind.LoanMovie("Hello World", "Horror", 1974);
        Assert.That(rewind.CheckStock("Hello World", "Horror", 1974), Is.EqualTo(1));
        RewindMovie rented2 = rewind.LoanMovie("Hello World", "Horror", 1974);
        Assert.That(rewind.CheckStock("Hello World", "Horror", 1974), Is.EqualTo(0));
        rewind.ReturnMovie(rented1);
        Assert.That(rewind.CheckStock("Hello World", "Horror", 1974), Is.EqualTo(1));
    }
}