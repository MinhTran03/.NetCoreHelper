using System;
using System.Reflection;

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
   }
}
