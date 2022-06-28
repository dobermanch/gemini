using System.Runtime.CompilerServices;

namespace Gemini.Portal.Client.Components;

public class ViewModelBase: IEditable
{
    private readonly IDictionary<string, IEditable> _properties = new Dictionary<string, IEditable>();

    public virtual bool IsEdited => _properties.Values.Any(it => it.IsEdited);

    public event EventHandler<EditableEventArgs> Changed;

    public virtual void Commit()
    {
        foreach (var property in _properties.Values)
        {
            property.Commit();
        }
    }

    public virtual void Reset()
    {
        foreach (var property in _properties.Values)
        {
            property.Reset();
        }
    }

    protected void RegisterProperty<T>(string propertyName, T origialValue, Action<T> updateSource)
    {
        if (!_properties.ContainsKey(propertyName))
        {
            var property = new EditableProperty<T>(origialValue, updateSource);
            property.Changed += (obj, args) => Changed?.Invoke(this, args);
            _properties.Add(propertyName, property);
        }
    }

    protected EditableProperty<T> GetProperty<T>([CallerMemberName] string propertyName = "") 
        => (EditableProperty<T>)_properties[propertyName];
}
