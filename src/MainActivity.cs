namespace AndroidXRecyclerViewSelection
{
    using Android.App;
    using Android.OS;
    using Android.Widget;
    using AndroidX.AppCompat.App;
    using AndroidX.RecyclerView.Selection;
    using AndroidX.RecyclerView.Widget;
    using System.Collections.Generic;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private CarAdapter adapter;
        private SelectionTracker tracker;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView1);

            if (savedInstanceState == null)
            {
                adapter = new CarAdapter(GenerateCarList());
                adapter.NotifyDataSetChanged();
                recyclerView.SetLayoutManager(new LinearLayoutManager(this));
                recyclerView.HasFixedSize = true;
                recyclerView.AddItemDecoration(new DividerItemDecoration(recyclerView.Context, DividerItemDecoration.Vertical));
                recyclerView.SetAdapter(adapter);
                tracker = new SelectionTracker.Builder("mySelection", 
                    recyclerView,
                    new StableIdKeyProvider(recyclerView), 
                    new CarItemDetailsLookup(recyclerView), 
                    StorageStrategy.CreateLongStorage())
                    .WithSelectionPredicate(SelectionPredicates.CreateSelectAnything())
                    .Build();
                adapter.SelectionTracker = tracker;
            }
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            if (outState != null)
                tracker?.OnSaveInstanceState(outState);
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
    }
}