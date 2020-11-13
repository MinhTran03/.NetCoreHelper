using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace ExtensionMethod
{
    public static class ExtensionMethodClasses
    {
        public static void SetPropertiesData<TModel>(this TModel dataDest, TModel dataSource) where TModel : class, new()
        {
            if (dataSource is null || dataDest is null)
            {
                return;
            }

            foreach (var property in dataDest.GetType().GetProperties())
            {
                if (property.CanWrite && property.CanRead)
                {
                    if (property.PropertyType.IsClass && !property.PropertyType.Namespace.StartsWith("System"))
                    {
                        var destChildValue = property.GetValue(dataDest);
                        var sourceChildValue = property.GetValue(dataSource);

                        if (sourceChildValue is null)
                        {
                            property.SetValue(dataDest, null);
                        }
                        else
                        {
                            if (destChildValue is null)
                            {
                                string typeName = property.PropertyType.FullName;

                                Assembly execAsm = Assembly.GetExecutingAssembly();
                                var newDestChildValue = AppDomain
                                   .CurrentDomain
                                   .CreateInstanceAndUnwrap(execAsm.FullName, typeName);

                                property.SetValue(dataDest, newDestChildValue);
                                destChildValue = property.GetValue(dataDest);
                            }
                            destChildValue.SetPropertiesData(sourceChildValue);
                        }
                    }
                    else
                    {
                        property.SetValue(dataDest, property.GetValue(dataSource));
                    }
                }
            }
        }

        /// <summary>
        /// Set data custom field key value from database to specific model
        /// </summary>
        /// <typeparam name="TModel">Type of model</typeparam>
        /// <param name="model">Model to pass data in</param>
        /// <param name="keyValueItems">List of ([Key: Property name], [Value: String value])</param>
        /// <param name="predicate">Filter condition for pass in properties</param>
        /// <param name="dateTimeFormat">DateTime format to parse from string</param>
        public static void SetCustomValue<TModel>(this TModel model
           , IDictionary<string, string> keyValueItems
           , Predicate<PropertyInfo> predicate = null
           , string dateTimeFormat = "yyyyMMdd") where TModel : class
        {
            string outValueString = string.Empty;
            DateTime outValueDateTime = new DateTime();
            var modelPropList = model
                .GetType()
                .GetProperties()
                .Where(d => d.CanWrite)
                .Where(d => predicate == null ? predicate(d) : true)
                .ToList();

            foreach (var modelProp in modelPropList)
            {
                if (keyValueItems.TryGetValue(modelProp.Name, out outValueString))
                {
                    dynamic setValue = null;
                    if (modelProp.PropertyType == typeof(string))
                    {
                        setValue = outValueString;
                    }
                    else if (modelProp.PropertyType == typeof(DateTime))
                    {
                        if (DateTime.TryParseExact(outValueString, dateTimeFormat, null, DateTimeStyles.None, out outValueDateTime))
                        {
                            setValue = outValueDateTime;
                        }
                    }
                    else if (modelProp.PropertyType.IsEnum)
                    {
                        setValue = Enum.Parse(modelProp.PropertyType, outValueString);
                    }
                    else if (modelProp.PropertyType.IsValueType)
                    {
                        setValue = Convert.ChangeType(outValueString, modelProp.PropertyType);
                    }
                    else
                    {
                        setValue = Activator.CreateInstance(modelProp.PropertyType);
                    }

                    modelProp.SetValue(model, setValue);
                }
            }
        }
    }
}
