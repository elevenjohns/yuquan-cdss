using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WuKeSong.MVC
{
    /// <summary>
    /// ModelBinder for editing collection navigation property
    /// http://www.codetuning.net/blog/post/Binding-Model-Graphs-with-ASPNETMVC.aspx
    /// </summary>
    public class DeepModelBinder : DefaultModelBinder
    { 
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // Bind collections 'our way':
            if ((bindingContext.Model.IsCollection())
                && (bindingContext.Model.CollectionGetCount() > 0))
                return this.BindCollection(controllerContext, bindingContext);
            else
                return base.BindModel(controllerContext, bindingContext);
        }

        private object BindCollection(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object collection = bindingContext.Model;
            Type collectionMemberType = typeof(Object);
            if (collection.GetType().IsGenericType)
                collectionMemberType =
                    collection.GetType().GetGenericArguments()[0];
            int count = collection.CollectionGetCount();
            for (int index = 0; index < count; index++)
            {
                // Create a BindingContext for the collection member:
                ModelBindingContext innerContext = new ModelBindingContext();
                object member = collection.CollectionGetItem(index);
                Type memberType =
                    (member == null) ? collectionMemberType : member.GetType();
                innerContext.ModelMetadata =
                    ModelMetadataProviders.Current.GetMetadataForType(
                        delegate() { return member; },
                        memberType);
                innerContext.ModelName =
                    String.Format("{0}[{1}]", bindingContext.ModelName, index);
                innerContext.ModelState = bindingContext.ModelState;
                innerContext.PropertyFilter = bindingContext.PropertyFilter;
                innerContext.ValueProvider = bindingContext.ValueProvider;

                // Bind the collection member:
                IModelBinder binder = Binders.GetBinder(memberType);
                object boundMember =
                    binder.BindModel(controllerContext, innerContext) ?? member;
                collection.CollectionSetItem(index, boundMember);
            }

            // Return the collection:
            return collection;
        }
    }

    /// <summary>
    /// Extension methods
    /// 
    /// Differences between partial class and extension method
    /// 
    /// Partial Class
    /// •Only works against classes in the same project/assembly 
    /// •Target class has to be marked as partial 
    /// •Has access to the Target class' fields and protected members 
    /// •Target must be a class implementation
    /// 
    /// Extension Method
    /// •Can be applied against classes in other assembles
    /// •Must be static, has access to only the Target classes public members
    /// •Target of extension can be a concrete type, or an abstract type or interface
    /// 
    /// Partial classes should be used in code generation scenarios.
    /// Since the generated file might get overwritten at any time, one uses partial classes to write into the non-generated file.Additionally, partials will only work if they are part of the same assembly - they cannot cross assembly boundaries.If these are not your constraints, you can and should use extension methods - of course, after considering other extension methods such as inheritance and composition for suitablitiy.
    ///
    /// </summary>
    internal static class DeepModelBinderCollectionExtensions
    {
        public static bool IsCollection(this object obj)
        {
            return (obj != null)
                && (obj.GetType() != typeof(String))
                && (typeof(System.Collections.IEnumerable).IsInstanceOfType(obj));
        }

        public static int CollectionGetCount(this object collection)
        {
            if (collection.GetType().IsArray)
                return ((Array)collection).GetLength(0);
            else
                return (int)collection.GetType().GetProperty("Count")
                    .GetValue(collection, null);
        }

        public static object CollectionGetItem(this object collection, int index)
        {
            if (collection.GetType().IsArray)
                return ((Array)collection).GetValue(index);
            else
                return collection.GetType().GetProperty("Item")
                    .GetValue(collection, new object[] { index });
        }

        public static void CollectionSetItem(this object collection, int index, object value)
        {
            if (collection.GetType().IsArray)
                ((Array)collection).SetValue(value, index);
            else
                collection.GetType().GetProperty("Item")
                    .SetValue(collection, value, new object[] { index });
        }
    }
}