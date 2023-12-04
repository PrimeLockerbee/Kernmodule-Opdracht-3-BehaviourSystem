using System;
using System.Collections.Generic;


public class Blackboard
{
    private Dictionary<string, object> storedData = new Dictionary<string, object>();

    public void AddOrUpdate(string _id, object _data)
    {
        if (storedData.ContainsKey(_id))
        {
            storedData[_id] = _data;
        }
        else
        {
            storedData.Add(_id, _data);
        }
    }

    public void RemoveEntry(string _id)
    {
        if (storedData.ContainsKey(_id))
        {
            storedData.Remove(_id);
        }
    }

    public bool Contains(string _id)
    {
        return storedData.ContainsKey(_id);
    }

    public T Get<T>(string _id)
    {
        object data;
        storedData.TryGetValue(_id, out data);

        try
        {
            return (T)data;
        }
        catch (Exception e)
        {
            return default(T);
        }
    }
}
