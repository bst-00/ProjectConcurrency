using System;
using Packaging;

/// THIS IS STUDENT SOLUTION
/// 
/// Concurrent version of the Packaging
namespace ConcPackaging;

public class ConcWorker : Worker
{
    // todo: add required properties for a thread-safe concurrent worker

    public ConcWorker(string n, ConcStorage s) : base(n,s)
    {
        // todo: implement the body
    }

    //todo: add required methods to implement a thread-safe concurrent worker
}

public class ConcStorage : Storage
{
    // todo: add required properties for a thread-safe concurrent storage

    public override void StartPackaging()
    {
        // todo: replace the exception with your implementation of the body
        throw new NotImplementedException();
    }

    // todo: add required properties for a thread-safe concurrent storage
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