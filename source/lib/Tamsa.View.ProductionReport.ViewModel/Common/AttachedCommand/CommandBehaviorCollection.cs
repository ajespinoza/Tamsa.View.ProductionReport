// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandBehaviorCollection.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the CommandBehaviorCollection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tamsa.View.ProductionReport.ViewModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Markup;
    
    /// <summary>
    /// The CommandBehaviorCollection class.
    /// </summary>
    public class CommandBehaviorCollection
    {
        #region Behaviors

        /// <summary>
        /// Behaviors property
        /// </summary>
        public static readonly DependencyProperty BehaviorsProperty = BehaviorsPropertyKey.DependencyProperty;

        /// <summary>
        /// Behaviors Read-Only Dependency Property
        /// As you can see the Attached readonly property has a name registered different (BehaviorsInternal) than the property name, this is a tricks os that we can construct the collection as we want
        /// Read more about this here http://wekempf.spaces.live.com/blog/cns!D18C3EC06EA971CF!468.entry
        /// </summary>
        private static readonly DependencyPropertyKey BehaviorsPropertyKey = DependencyProperty.RegisterAttachedReadOnly("BehaviorsInternal", typeof(BehaviorBindingCollection), typeof(CommandBehaviorCollection), new FrameworkPropertyMetadata((BehaviorBindingCollection)null));

        /// <summary>
        /// Gets the Behaviors property.  
        /// Here we initialze the collection and set the Owner property
        /// </summary>
        /// <param name="d">
        /// Dependency object
        /// </param>
        /// <returns>
        /// Behavior binding collection.
        /// </returns>
        public static BehaviorBindingCollection GetBehaviors(DependencyObject d)
        {
            if (d == null)
            {
                throw new InvalidOperationException("The dependency object trying to attach to is set to null");
            }

            BehaviorBindingCollection collection = d.GetValue(CommandBehaviorCollection.BehaviorsProperty) as BehaviorBindingCollection;
            if (collection == null)
            {
                collection = new BehaviorBindingCollection();
                collection.Owner = d;
                SetBehaviors(d, collection);
            }

            return collection;
        }

        /// <summary>
        /// The collection changed,
        /// </summary>
        /// <param name="sender">
        /// The sender object.
        /// </param>
        /// <param name="e">
        /// The notify event.
        /// </param>
        public static void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            BehaviorBindingCollection sourceCollection = (BehaviorBindingCollection)sender;
            switch (e.Action)
            {
                // when an item(s) is added we need to set the Owner property implicitly
                case NotifyCollectionChangedAction.Add:
                    {
                        if (e.NewItems != null)
                        {
                            foreach (BehaviorBinding item in e.NewItems)
                            {
                                item.Owner = sourceCollection.Owner;
                            }
                        }

                        break;
                    }

                // when an item(s) is removed we should Dispose the BehaviorBinding
                case NotifyCollectionChangedAction.Remove:
                    {
                        if (e.OldItems != null)
                        {
                            foreach (BehaviorBinding item in e.OldItems)
                            {
                                item.Behavior.Dispose();
                            }
                        }

                        break;
                    }

                // here we have to set the owner property to the new item and unregister the old item
                case NotifyCollectionChangedAction.Replace:
                    {
                        if (e.NewItems != null)
                        {
                            foreach (BehaviorBinding item in e.NewItems)
                            {
                                item.Owner = sourceCollection.Owner;
                            }
                        }

                        if (e.OldItems != null)
                        {
                            foreach (BehaviorBinding item in e.OldItems)
                            {
                                item.Behavior.Dispose();
                            }
                        }

                        break;
                    }

                // when an item(s) is removed we should Dispose the BehaviorBinding
                case NotifyCollectionChangedAction.Reset:
                    {
                        if (e.OldItems != null)
                        {
                            foreach (BehaviorBinding item in e.OldItems)
                            {
                                item.Behavior.Dispose();
                            }
                        }

                        break;
                    }

                case NotifyCollectionChangedAction.Move:

                default:
                    break;
            }
        }

        /// <summary>
        /// Provides a secure method for setting the Behaviors property.  
        /// This dependency property indicates ....
        /// </summary>
        /// <param name="d">
        /// Dependency object.
        /// </param>
        /// <param name="value">
        /// Bihavior collection.
        /// </param>
        private static void SetBehaviors(DependencyObject d, BehaviorBindingCollection value)
        {
            d.SetValue(BehaviorsPropertyKey, value);
            INotifyCollectionChanged collection = (INotifyCollectionChanged)value;
            collection.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
        }

        #endregion
    }

    /// <summary>
    /// Collection to store the list of behaviors. This is done so that you can intiniate it from XAML
    /// This inherits from freezable so that it gets inheritance context for DataBinding to work
    /// </summary>
    public class BehaviorBindingCollection : FreezableCollection<BehaviorBinding>
    {
        /// <summary>
        /// Gets or sets the Owner of the binding
        /// </summary>
        public DependencyObject Owner { get; set; }
    }
}
