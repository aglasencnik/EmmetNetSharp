using System.Collections.Generic;

namespace EmmetNetSharp.Helpers
{
    /// <summary>
    /// Provides helper methods for working with partial objects.
    /// </summary>
    public static class PartialHelper
    {
        /// <summary>
        /// Creates a dictionary containing only the non-null properties of a given object.
        /// </summary>
        /// <param name="model">The object from which to extract the properties. If null, the method returns null.</param>
        /// <returns>
        /// A dictionary with key-value pairs representing the non-null properties of the object.
        /// Returns null if the input object is null.
        /// </returns>
        public static IDictionary<string, object> GetPartialObject(object model)
        {
            if (model == null)
                return null;

            var nonNullProperties = new Dictionary<string, object>();
            foreach (var property in model.GetType().GetProperties())
            {
                var value = property.GetValue(model);
                if (value != null)
                    nonNullProperties.Add(property.Name, value);
            }

            return nonNullProperties;
        }
    }
}
