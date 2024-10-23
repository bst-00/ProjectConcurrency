using System;
using Packaging;

/// THIS IS STUDENT SOLUTION
/// 
/// Concurrent version of the Packaging
namespace ConcPackaging;

public class ConcWorker : Worker
{
    private object _itemLock;

    public ConcWorker(string n, ConcStorage s, object itemLock) : base(n, s)
    {
        this._itemLock = itemLock;
    }

    public override void PackItems()
    {
        while (true)
        {
            Item cur_item;

            lock (_itemLock)
            {
                if (storage.Items.Count == 0)
                {
                    break;
                }
                cur_item = storage.Items.First.Value;
                storage.Items.RemoveFirst();
                storage.num_pickedItems++;
            }

            Move();

            foreach (Box b in storage.Boxes)
            {
                if (cur_item.BoxName == b.Name)
                {
                    b.AddItem(cur_item);
                    storage.num_packedItems++;
                    this.Log(cur_item.Name + " moved " + b.Name);
                    break;
                }
            }
        }
    }
}

public class ConcStorage : Storage
{
    public ConcStorage() : base() {}

    public override void StartPackaging()
    {
        object itemLock = new object();

        List<Thread> workerThreads = new List<Thread>();

        for (int i = 0; i < FixedParams.maxNumOfWorkers; i++)
        {
            Worker worker = new ConcWorker("WOR_" + (i + 1).ToString(), this, itemLock);
            Thread workerThread = new Thread(() => worker.PackItems());
            workerThreads.Add(workerThread);
            workerThread.Start();
        }

        foreach (Thread workerThread in workerThreads)
        {
            workerThread.Join();
        }
    }
}
/// <summary>
/// A class to run the packaging concurrently.
/// </summary>
public class PackagingConcurrent
{
    private ConcStorage _conc_storage;

    public PackagingConcurrent()
    {
        this._conc_storage = new();
    }
    public void RunPackaging()
    {
        _conc_storage.Initialize();
        _conc_storage.Assign();
        _conc_storage.StartPackaging();
    }
    public Statistics FinalResult()
    {
        return _conc_storage.GetStatistics();
    }
}