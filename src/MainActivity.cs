using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Selection;
using AndroidX.RecyclerView.Widget;

namespace AndroidXRecyclerViewSelection
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private readonly ZipAdapter adapter = new ZipAdapter();
        private SelectionTracker selectionTracker;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            var recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView1);
            if (savedInstanceState == null)
            {
                recyclerView.SetLayoutManager(new LinearLayoutManager(this));
                recyclerView.SetAdapter(adapter);
            }

            selectionTracker = new SelectionTracker.Builder("selectionId",
                    recyclerView,
                    new ZipKeyProvider(recyclerView),
                    new ZipItemDetailsLookup(recyclerView),
                    StorageStrategy.CreateLongStorage())
                .WithSelectionPredicate(SelectionPredicates.CreateSelectAnything())
                .Build();
            adapter.SetTracker(selectionTracker);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            if (outState != null)
                selectionTracker?.OnSaveInstanceState(outState);
        }
    }
}