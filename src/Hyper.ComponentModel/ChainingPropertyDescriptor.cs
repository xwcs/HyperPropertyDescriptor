namespace Hyper.ComponentModel
{
    using System;
    using System.ComponentModel;


	public static class TypeExtensionMethods
	{
		public static object GetPropValueByPath(this object obj, string name)
		{
			foreach (string part in name.Split('.'))
			{
				if (obj == null) { return null; }

				Type type = obj.GetType();
				System.Reflection.PropertyInfo info = type.GetProperty(part);
				if (info == null) { return null; }

				obj = info.GetValue(obj, null);
			}
			return obj;
		}
		
		/*	
		public static T GetPropValueByPath<T>(this object obj, string name)
		{
			object retval = GetPropValueByPath(obj, name);
			if (retval == null) { return default(T); }

			// throws InvalidCastException if types are incompatible
			return (T)retval;
		}
		*/
	}

	/*
	public class PropertyTypeMorphedEventData: EventArgs
	{
		public PropertyDescriptor Property { get; set; }
	}
	*/

	public abstract class ChainingPropertyDescriptor : PropertyDescriptor
    {
        private readonly PropertyDescriptor _root;

		//make possibility to force type externally
		private Type _forcedType = null;


		//public EventHandler<PropertyTypeMorphedEventData> PropertyTypeMorphed;

		protected PropertyDescriptor Root
        {
            get { return _root; }
        }

        protected ChainingPropertyDescriptor(PropertyDescriptor root)
            : base(root)
        {
            _root = root;
        }

        public override void AddValueChanged(object component, EventHandler handler)
        {
            Root.AddValueChanged(component, handler);
        }

        public override AttributeCollection Attributes
        {
            get { return Root.Attributes; }
        }

        public override bool CanResetValue(object component)
        {
            return Root.CanResetValue(component);
        }

        public override string Category
        {
            get { return Root.Category; }
        }

        public override Type ComponentType
        {
            get { return Root.ComponentType; }
        }

        public override TypeConverter Converter
        {
            get { return Root.Converter; }
        }

        public override string Description
        {
            get { return Root.Description; }
        }

        public override bool DesignTimeOnly
        {
            get { return Root.DesignTimeOnly; }
        }

        public override string DisplayName
        {
            get { return Root.DisplayName; }
        }

        public override bool Equals(object obj)
        {
            return Root.Equals(obj);
        }

        public override PropertyDescriptorCollection GetChildProperties(object instance, Attribute[] filter)
        {
            return Root.GetChildProperties(instance, filter);
        }

        public override object GetEditor(Type editorBaseType)
        {
            return Root.GetEditor(editorBaseType);
        }

        public override int GetHashCode()
        {
            return Root.GetHashCode();
        }

        public override object GetValue(object component)
        {
            return Root.GetValue(component);
        }

        public override bool IsBrowsable
        {
            get { return Root.IsBrowsable; }
        }

        public override bool IsLocalizable
        {
            get { return Root.IsLocalizable; }
        }

        public override bool IsReadOnly
        {
            get { return Root.IsReadOnly; }
        }

        public override string Name
        {
            get { return Root.Name; }
        }

		public override Type PropertyType
		{
			get	{
				//Console.WriteLine("Type:" + (_forcedType == null ? Root.PropertyType : _forcedType).ToString());
				return _forcedType == null ? Root.PropertyType : _forcedType;
			}
		}

		public Type ForcedPropertyType
		{
			get { return _forcedType == null ? Root.PropertyType : _forcedType; }
			set {
				if(_forcedType != value) {
					_forcedType = value;
					/*
					if (PropertyTypeMorphed != null)
					{
						PropertyTypeMorphed(this, new PropertyTypeMorphedEventData { Property = this });
					}
					*/
				}			
			}
		}

		public override void RemoveValueChanged(object component, EventHandler handler)
        {
            Root.RemoveValueChanged(component, handler);
        }

        public override void ResetValue(object component)
        {
            Root.ResetValue(component);
        }

        public override void SetValue(object component, object value)
        {
            Root.SetValue(component, value);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return Root.ShouldSerializeValue(component);
        }

        public override bool SupportsChangeEvents
        {
            get { return Root.SupportsChangeEvents; }
        }

        public override string ToString()
        {
            return Root.ToString();
        }
    }
}