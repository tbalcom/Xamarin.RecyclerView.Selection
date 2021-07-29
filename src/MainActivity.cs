namespace AndroidXRecyclerViewSelection
{
    using Android.App;
    using Android.OS;
    using Android.Views;
    using AndroidX.AppCompat.App;
    using AndroidX.RecyclerView.Selection;
    using AndroidX.RecyclerView.Widget;
    using Java.Util;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, ActionMode.ICallback
    {
        private const string TAG = "AndroidXRecyclerViewSelection";
        private ActionMode? actionMode;
        private List<Car> cars;
        private CarAdapter adapter;
        private SelectionTracker tracker;
        private CarSelectionObserver trackerObserver;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView1);

            if (savedInstanceState == null)
            {
                cars = GenerateCarList();
                adapter = new CarAdapter(cars);
                adapter.NotifyDataSetChanged();
                recyclerView.SetLayoutManager(new LinearLayoutManager(this));
                recyclerView.HasFixedSize = true;
                recyclerView.AddItemDecoration(new DividerItemDecoration(recyclerView.Context, DividerItemDecoration.Vertical));
                recyclerView.SetAdapter(adapter);
                tracker = new SelectionTracker.Builder("car-selection", 
                    recyclerView,
                    new StableIdKeyProvider(recyclerView), 
                    new CarAdapter.CarItemDetailsLookup(recyclerView), 
                    StorageStrategy.CreateLongStorage())
                    .WithSelectionPredicate(SelectionPredicates.CreateSelectAnything())
                    .Build();
                adapter.SelectionTracker = tracker;

                trackerObserver = new CarSelectionObserver(this);
                tracker.AddObserver(trackerObserver);
            }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            if (outState != null)
                tracker?.OnSaveInstanceState(outState);
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            if (savedInstanceState != null)
                tracker?.OnRestoreInstanceState(savedInstanceState);
        }

        private List<Car> GenerateCarList()
        {
            return new List<Car>(new[]
            {
                new Car
                {
                    Make = "Ford",
                    Model = "Mustang LX",
                    Year = 1980,
                    Vin = "1"
                },
                new Car
                {
                    Make = "Ford",
                    Model = "Mustang GT",
                    Year = 2000,
                    Vin = "2"
                },
                new Car
                {
                    Make = "Honda",
                    Model = "Civic EX",
                    Year = 2006,
                    Vin = "3"
                },
                new Car
                {
                    Make = "Dodge",
                    Model = "Caravan",
                    Year = 1987,
                    Vin = "4"
                },
                new Car
                {
                    Make = "DeLorean",
                    Model = "DMC-12",
                    Year = 1983,
                    Vin = "5"
                },
                new Car
                {
                    Make = "Ford",
                    Model = "F-150",
                    Year = 2020,
                    Vin = "6"
                },
                new Car
                {
                    Make = "Chevrolet",
                    Model = "Camaro",
                    Year = 2021,
                    Vin = "7"
                },
                new Car
                {
                    Make = "Chevrolet",
                    Model = "Corvette",
                    Year = 1969,
                    Vin = "8"
                },
                new Car
                {
                    Make = "Lamborghini",
                    Model = "Aventador SVJ",
                    Year = 2022,
                    Vin = "9"
                },
                new Car
                {
                    Make = "Lamborghini",
                    Model = "Aventador LP 780-4 ULTIMAE",
                    Year = 2022,
                    Vin = "10"
                },
                new Car
                {
                    Make = "Ford",
                    Model = "Pinto",
                    Year = 1971,
                    Vin = "11"
                },
                new Car
                {
                    Make = "Dodge",
                    Model = "Charger",
                    Year = 1980,
                    Vin = "12"
                },
                new Car
                {
                    Make = "Cadillac",
                    Model = "XT4",
                    Year = 2021,
                    Vin = "13"
                },
                new Car
                {
                    Make = "Cadillac",
                    Model = "Escalade ESV",
                    Year = 2021,
                    Vin = "14"
                }
            });
        }

        private List<Car> GetSelection()
        {
            var selection = new List<Car>();
            MutableSelection mutableSelection = new MutableSelection();
            tracker.CopySelection(mutableSelection);
            IIterator iterator = mutableSelection.Iterator();
            while (iterator.HasNext)
            {
                var obj = iterator.Next();
                selection.Add(cars[Convert.ToInt32(obj)]);
            }
            return selection;
        }

        private int GetSelectionCount()
        {
            int counter = 0;
            MutableSelection mutableSelection = new MutableSelection();
            tracker.CopySelection(mutableSelection);
            IIterator iterator = mutableSelection.Iterator();
            while (iterator.HasNext)
            {
                iterator.Next();
                counter++;
            }
            return counter;
        }

        bool ActionMode.ICallback.OnActionItemClicked(ActionMode mode, IMenuItem item)
        {
            Android.Util.Log.Verbose(TAG, "ICallback.OnActionItemClicked()");
            switch (item.ItemId)
            {
                case Resource.Id.action_delete:
                    var selection = GetSelection();
                    foreach (var selected in selection)
                    {
                        Android.Util.Log.Debug(TAG, "Delete {0}", selected);
                    }
                    mode.Finish();
                    return true;
            }
            return false;
        }

        bool ActionMode.ICallback.OnCreateActionMode(ActionMode mode, IMenu menu)
        {
            Android.Util.Log.Verbose(TAG, "ICallback.OnCreateActionMode()");
            mode.MenuInflater.Inflate(Resource.Menu.menu_car, menu);
            return true;
        }

        void ActionMode.ICallback.OnDestroyActionMode(ActionMode mode)
        {
            Android.Util.Log.Verbose(TAG, "ICallback.OnDestroyActionMode()");
            tracker?.ClearSelection();
            actionMode = null;
        }

        bool ActionMode.ICallback.OnPrepareActionMode(ActionMode mode, IMenu menu)
        {
            Android.Util.Log.Verbose(TAG, "ICallback.OnPrepareActionMode()");
            return false;
        }

        internal class CarSelectionObserver : SelectionTracker.SelectionObserver
        {
            private readonly WeakReference<MainActivity> mainActivity;

            public CarSelectionObserver(MainActivity mainActivity)
            {
                this.mainActivity = new WeakReference<MainActivity>(mainActivity);
            }

            public override void OnItemStateChanged(Java.Lang.Object key, bool selected)
            {
                base.OnItemStateChanged(key, selected);
                Android.Util.Log.Verbose(TAG, "SelectionObserver.OnItemStateChanged()");
            }

            public override void OnSelectionChanged()
            {
                base.OnSelectionChanged();
                Android.Util.Log.Verbose(TAG, "SelectionObserver.OnSelectionChanged()");
                if (mainActivity.TryGetTarget(out var activity))
                {
                    if (activity.actionMode == null)
                    {
                        activity.actionMode = activity.StartActionMode(activity);
                    }
                }
            }

            protected override void OnSelectionCleared()
            {
                base.OnSelectionCleared();
                Android.Util.Log.Verbose(TAG, "SelectionObserver.OnSelectionCleared()");
            }

            public override void OnSelectionRefresh()
            {
                base.OnSelectionRefresh();
                Android.Util.Log.Verbose(TAG, "SelectionObserver.OnSelectionRefresh()");
            }

            public override void OnSelectionRestored()
            {
                base.OnSelectionRestored();
                Android.Util.Log.Verbose(TAG, "SelectionObserver.OnSelectionRestored()");
            }
        }
    }
}